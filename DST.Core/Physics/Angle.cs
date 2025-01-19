namespace DST.Core.Physics
{
    // Represents a mathematical angle.
    public readonly partial struct Angle : IEquatable<Angle>
    {
        // The number of arc-milliseconds per unit degree, arc-minute, arc-second, and arc-millisecond, respectively.
        // The quotient of every two consecutive values results in the magnitude of the first component 
        // to the second component (a constant ratio).
        // E.g., milliseconds-per-minute / milliseconds-per-second = seconds-per-minute
        private static readonly double[] _millisecondsPerUnitComponent
            = { 3.6E+6, 6E+4, 1E+3, 1.0 };

        // The number of fractional digits to round when formatting as a decimal value.
        private static readonly int _displayPrecision = 4;

        // Gets a new Angle with a value of 0°.
        public static Angle Zero => new();

        // Gets the total decimal degrees of this Angle.
        // This is not rounded to the nearest milli-arcsecond.
        public double TotalDegrees { get; }

        // Gets the total decimal radians of this Angle.
        public double TotalRadians => ToRadians();

        // Gets the total decimal hours of this Angle.
        public double TotalHours => TotalDegrees / Constants.RotationPerHour;

        // Gets this Angle's whole degrees value.
        // This is negative if TotalDegrees is negative.
        public int Degrees => GetComponent(0);

        // Gets this Angle's whole arc-minutes value.
        // This is negative if TotalDegrees is negative.
        public int Minutes => GetComponent(1);

        // Gets this Angle's whole arc-seconds value.
        // This is negative if TotalDegrees is negative.
        public int Seconds => GetComponent(2);

        // Gets this Angle's whole milli-arc-seconds value.
        // This is negative if TotalDegrees is negative.
        public int Milliseconds => GetComponent(3);

        // Creates a new Angle given the total decimal degrees, onto the interval (-360°, 360°) if negatives should be allowed,
        // or onto the interval [0°, 360°) if negatives should not be allowed.
        private Angle(double degrees, bool allowNegative)
        {
            TotalDegrees = default;
            TotalDegrees = GetAdjustedTotalDegrees(degrees, allowNegative);
        }

        // Creates a new Angle given the total decimal degrees, onto the interval (-360°, 360°).
        public Angle(double degrees)
            : this(degrees, allowNegative: true)
        { }

        // Creates a new Angle given the whole degrees.
        public Angle(int degrees)
            : this(degrees, 0, 0, 0)
        { }

        // Creates a new Angle given the whole degrees and arc-minutes.
        // If a negative angle is expected, then each component must be specified as negative.
        public Angle(int degrees, int minutes)
            : this(degrees, minutes, 0, 0)
        { }

        // Creates a new Angle given the whole degrees, arc-minutes, and arc-seconds.
        // If a negative angle is expected, then each component must be specified as negative.
        public Angle(int degrees, int minutes, int seconds)
            : this(degrees, minutes, seconds, 0)
        { }

        // Creates a new Angle given the whole degrees, arc-minutes, arc-seconds, and milli-arc-seconds.
        // If a negative angle is expected, then each component must be specified as negative.
        public Angle(int degrees, int minutes, int seconds, int milliseconds)
        {
            // Evaluate the total decimal degrees using the components.
            // Note that the sign of each component may affect the result, and that the components don't need
            // to be bounds-checked.
            double totalDegrees = degrees + (minutes + (seconds + milliseconds / 1000.0) / 60.0) / 60.0;

            TotalDegrees = default;
            TotalDegrees = GetAdjustedTotalDegrees(totalDegrees);
        }

        // Creates a new Angle by converting the total hours of a TimeSpan value into total degrees.
        // If argument 'timeDegrees' equals TimeSpan.MinValue, the resulting angle will be zeroed (0°).
        public Angle(TimeSpan time)
        {
            // Note - We do not need to verify that time.TotalHours <= 24.0, since
            // GetAdjustedTotalDegrees(totalDegrees) will ensure the final angle is on (-360°, 360°).
            double totalDegrees = time.TotalHours * Constants.RotationPerHour;

            TotalDegrees = default;
            TotalDegrees = GetAdjustedTotalDegrees(totalDegrees);
        }

        // Returns a value that indicates whether two specified Angle values are equal.
        public static bool operator ==(Angle left, Angle right)
        {
            return left.Equals(right);
        }

        // Returns a value that indicates whether two specified Angle values are not equal.
        public static bool operator !=(Angle left, Angle right)
        {
            return !(left == right);
        }

        // Returns a value that indicates whether a specified Angle value is less than
        // another specified Angle value.
        public static bool operator <(Angle left, Angle right)
        {
            return left.TotalDegrees < right.TotalDegrees;
        }

        // Returns a value that indicates whether a specified Angle value is greater than
        // another specified Angle value.
        public static bool operator >(Angle left, Angle right)
        {
            return left.TotalDegrees > right.TotalDegrees;
        }

        // Returns a value that indicates whether a specified Angle value is less than
        // or equal to another specified Angle value.
        public static bool operator <=(Angle left, Angle right)
        {
            return left.TotalDegrees <= right.TotalDegrees;
        }

        // Returns a value that indicates whether a specified Angle value is greater than
        // or equal to another specified Angle value.
        public static bool operator >=(Angle left, Angle right)
        {
            return left.TotalDegrees >= right.TotalDegrees;
        }

        // Returns the sum of two Angle values, in degrees.
        public static Angle operator +(Angle left, Angle right)
        {
            return new Angle(left.TotalDegrees + right.TotalDegrees);
        }

        // Returns the difference of two Angle values, in degrees.
        public static Angle operator -(Angle left, Angle right)
        {
            return new Angle(left.TotalDegrees - right.TotalDegrees);
        }

        // Returns the negated value of an Angle, in degrees.
        public static Angle operator -(Angle angle)
        {
            return new Angle(-angle.TotalDegrees);
        }

        // Returns the product of two Angle values, in degrees.
        public static Angle operator *(Angle left, Angle right)
        {
            return new Angle(left.TotalDegrees * right.TotalDegrees);
        }

        // Returns the quotient of two Angle values, in degrees.
        public static Angle operator /(Angle left, Angle right)
        {
            if (right == Zero)
            {
                throw new DivideByZeroException();
            }

            return new Angle(left.TotalDegrees / right.TotalDegrees);
        }

        // Defines an implicit cast operator to return the value of this Angle represented in total decimal degrees.
        public static implicit operator double(Angle angle)
        {
            return angle.TotalDegrees;
        }

        // Defines an explicit cast operator to return a new Angle whose value is equal to the specified floating point value.
        public static explicit operator Angle(double degrees)
        {
            return new Angle(degrees);
        }

        // Returns a new Angle instance given an amount of radians.
        public static Angle FromRadians(double radians)
        {
            double degrees = radians.IsFiniteRealNumber() ? radians * (180.0 / Math.PI) : 0.0;

            return new Angle(degrees);
        }

        // Returns the smallest positive coterminal angle onto the interval [0°, 360°) given an amount of decimal degrees,
        // rotating counterclockwise with respect to the positive x-axis.
        private static double GetCoterminal(double degrees)
        {
            double result = degrees;

            // Only continue if the specified angle is not on the interval [0°, 360°).
            if (degrees < 0.0 || 360.0 <= degrees)
            {
                // Note: Math.Floor() rounds in the negative direction on negative numbers.
                result = degrees.IsFiniteRealNumber() ? degrees - 360.0 * Math.Floor(degrees / 360.0) : 0.0;
            }

            return result;
        }

        // Returns a new Angle instance given an amount of decimal degrees, adjusted onto the interval [0°, 360°).
        public static Angle Coterminal(double degrees)
        {
            return new Angle(degrees, allowNegative: false);
        }

        // Relates the specified floating point value onto the interval (-360°, 360°) if negatives should be allowed,
        // or [0°, 360°) if negatives should not be allowed.
        // Returns 0.0 if argument 'totalDegrees' is not a finite, real number.
        private static double GetAdjustedTotalDegrees(double totalDegrees, bool allowNegative = true)
        {
            // Relate the total degrees onto the interval [0°, 360°).
            double result = GetCoterminal(totalDegrees);

            // If negatives are allowed, relate the total degrees onto the interval (-360°, 0°) if the original angle was negative.
            if (allowNegative && totalDegrees < 0.0)
            {
                result -= 360.0;
            }

            return result;
        }

        // Returns a value that indicates whether this current Angle instance lies on the interval [0°, 360°).
        public bool IsCoterminal()
        {
            // If this Angle is positive, then it is already on the interval [0°, 360°).
            return TotalDegrees >= 0.0;
        }

        // Returns a new Angle instance whose value is equal to the coterminal value, on the interval [0°, 360°),
        // of this current Angle instance.
        public Angle Coterminal()
        {
            return IsCoterminal() ? this : Coterminal(TotalDegrees);
        }

        // Returns a 180° rotation of this current Angle instance onto the interval (-360°, 360°).
        public Angle Flipped()
        {
            return new Angle(this - 180.0);
        }

        // Returns the reference angle of this current Angle instance onto the interval [0°, 90°].
        public Angle Reference()
        {
            // Relate the angle to [0°, 360°) if it is not already a coterminal angle.
            double degrees = IsCoterminal() ? TotalDegrees : Coterminal().TotalDegrees;

            return degrees switch
            {
                > 270.0 => new Angle(360.0 - degrees), // 4th Quadrant
                > 180.0 => new Angle(degrees - 180.0), // 3rd Quadrant
                > 90.0 => new Angle(180.0 - degrees), // 2nd Quadrant
                _ => new Angle(degrees) // 1st Quadrant
            };
        }

        // By default, this yields the whole degrees, arc-minutes, arc-seconds, and milli-arc-seconds components, 
        // respectively, from the TotalDegrees value.
        // Otherwise, this yields up to 'precision' number of components.
        public IEnumerable<int> GetComponents(int precision = 4)
        {
            double remainder = TotalDegrees;

            int previousComponent = 0;

            int stop;

            if (precision < 1)
            {
                stop = 1;
            }
            else if (precision > _millisecondsPerUnitComponent.Length)
            {
                stop = _millisecondsPerUnitComponent.Length;
            }
            else
            {
                stop = precision;
            }

            for (int i = 0; i < stop; i++)
            {
                if (i > 0)
                {
                    // Calculate the magnitude of this component from the remainder of the previous component.
                    remainder = (remainder - previousComponent) * (_millisecondsPerUnitComponent[i - 1] / _millisecondsPerUnitComponent[i]);
                }

                // Round this component to the nearest millisecond.
                remainder = Math.Round(remainder * _millisecondsPerUnitComponent[i]) / _millisecondsPerUnitComponent[i];

                // Yield only the whole portion of this component.
                yield return previousComponent = (int)Math.Truncate(remainder);
            }
        }

        // Returns the whole unitary component of this Angle instance specified by the index, or 0 if the index is out of range.
        private int GetComponent(int index)
        {
            IEnumerable<int> components = GetComponents(index + 1);

            return components?.LastOrDefault() ?? 0;
        }

        // Returns the value of this Angle expressed in radians.
        private double ToRadians()
        {
            return this * (Math.PI / 180.0);
        }

        // Returns the TimeSpan-representation of this current Angle instance.
        public TimeSpan ToTime()
        {
            // This should never throw an exception since the absolute value of TotalDegrees is always less than 360.
            return TimeSpan.FromHours(TotalDegrees / Constants.RotationPerHour);
        }

        // Indicates whether this Angle instance and a specified object are equal.
        public override bool Equals(object? obj)
        {
            return obj is Angle other && Equals(other);
        }

        // Indicates whether this Angle instance and another Angle instance are equal.
        public bool Equals(Angle other)
        {
            return other.TotalDegrees == TotalDegrees;
        }

        // Returns the hash code for this Angle instance.
        public override int GetHashCode()
        {
            return TotalDegrees.GetHashCode();
        }

        // Returns the string-representation of this current Angle instance, formatted in decimal degrees.
        public override string ToString()
        {
            return ToString(FormatType.DecimalDegrees, FormatModifierType.None);
        }

        // Returns the string-representation of this current Angle instance, formatted by the specified FormatType.
        public string ToString(FormatType format)
        {
            return ToString(format, FormatModifierType.None);
        }

        // Returns the string-representation of this current Angle instance, formatted by the specified
        // FormatType and with the FormatModifierType.
        public string ToString(FormatType format, FormatModifierType modifier)
        {
            string result;
            double rounded;

            // Note that these formats passed into string.Format will always round away from zero.

            switch (format)
            {
                case FormatType.DecimalDegrees:
                default:
                    {
                        // Round to the nearest 4 digits, away from zero.
                        rounded = Math.Round(TotalDegrees, _displayPrecision, MidpointRounding.AwayFromZero);

                        // The underlying total degrees will always be on (-360°, 360°),
                        // So theoretically, rounding will always be on [-360°, 360°].
                        // If the rounded angle is on (-∞, -360°]U[360°, ∞) set it to 0°.
                        if (Math.Abs(rounded) >= 360.0)
                        {
                            rounded = 0.0;
                        }

                        result = string.Format(Resources.AngleFormats.DecimalFormatDegrees, Math.Abs(rounded));

                        break;
                    }

                case FormatType.DecimalHours:
                    {
                        // Round to the nearest 4 digits, away from zero.
                        rounded = Math.Round(ToTime().TotalHours, _displayPrecision, MidpointRounding.AwayFromZero);

                        // The underlying total hours will always be on (-24h, 24h),
                        // So theoretically, rounding will always be on [-24h, 24h].
                        // If the rounded angle is on (-∞, -24h]U[24h, ∞) set it to 0h.
                        if (Math.Abs(rounded) >= 24.0)
                        {
                            rounded = 0.0;
                        }

                        result = string.Format(Resources.AngleFormats.DecimalFormatHours, Math.Abs(rounded));

                        break;
                    }

                case FormatType.ComponentDegrees:
                    {
                        IEnumerable<int> components = GetComponents();

                        // Combine the whole seconds with fractional seconds as a single decimal value.
                        // Decimal seconds = Seconds + (Milliseconds / 1000.0)
                        double remainingSeconds = components.ElementAtOrDefault(2) + components.ElementAtOrDefault(3) / 1000.0;

                        // If all components of this angle are zero, the rounded angle should also be zero.
                        // Otherwise, just use the total degrees.
                        rounded = components.All(x => x == 0) ? 0.0 : TotalDegrees;

                        result = string.Format(Resources.AngleFormats.ComponentFormatDegrees,
                            Math.Abs(components.ElementAtOrDefault(0)),
                            Math.Abs(components.ElementAtOrDefault(1)),
                            Math.Abs(remainingSeconds));

                        break;
                    }

                case FormatType.ComponentHours:
                    {
                        TimeSpan time = ToTime();
                        
                        int[] components = { time.Hours, time.Minutes, time.Seconds, time.Milliseconds };

                        // Combine the whole seconds with fractional seconds as a single decimal value.
                        // Decimal seconds = Seconds + (Milliseconds / 1000.0)
                        double remainingSeconds = time.Seconds + time.Milliseconds / 1000.0;

                        // If all components of this angle are zero, the rounded angle should also be zero.
                        // Otherwise, just use the total hours.
                        rounded = components.All(x => x == 0) ? 0.0 : TotalHours;

                        result = string.Format(Resources.AngleFormats.ComponentFormatHours,
                            Math.Abs(time.Hours),
                            Math.Abs(time.Minutes),
                            Math.Abs(remainingSeconds));

                        break;
                    }

                case FormatType.CompactDegrees:
                    {
                        // Round to the nearest integral value, away from zero.
                        rounded = Math.Round(TotalDegrees, MidpointRounding.AwayFromZero);

                        // The underlying total degrees will always be on (-360°, 360°),
                        // So theoretically, rounding will always be on [-360°, 360°].
                        // If the rounded angle is on (-∞, -360°]U[360°, ∞) set it to 0°.
                        if (Math.Abs(rounded) >= 360.0)
                        {
                            rounded = 0.0;
                        }

                        result = string.Format(Resources.AngleFormats.CompactFormatDegrees, Math.Abs(rounded));
                        
                        break;
                    }

                case FormatType.CompactHours:
                    {
                        // Round to the nearest integral value, away from zero.
                        rounded = Math.Round(ToTime().TotalHours, MidpointRounding.AwayFromZero);

                        // The underlying total hours will always be on (-24h, 24h),
                        // So theoretically, rounding will always be on [-24h, 24h].
                        // If the rounded angle is on (-∞, -24h]U[24h, ∞) set it to 0h.
                        if (Math.Abs(rounded) >= 24.0)
                        {
                            rounded = 0.0;
                        }

                        result = string.Format(Resources.AngleFormats.CompactFormatHours, Math.Abs(rounded));
                        
                        break;
                    }
            }

            switch (modifier)
            {
                case FormatModifierType.None:
                default:
                    {
                        // Include only the negative sign if the angle is negative.
                        if (rounded < 0.0)
                        {
                            result = string.Format(Resources.AngleFormats.SignedFormatNegativeValue, result);
                        }

                        break;
                    }

                case FormatModifierType.Signed:
                    {
                        // Include a negative sign if the angle is negative, or a positive sign if the angle is positive or zero.
                        if (rounded < 0.0)
                        {
                            result = string.Format(Resources.AngleFormats.SignedFormatNegativeValue, result);
                        }
                        else
                        {
                            result = string.Format(Resources.AngleFormats.SignedFormatPositiveValue, result);
                        }

                        break;
                    }

                case FormatModifierType.Unsigned:
                    {
                        // The result already used absolute values on the angle.
                        break;
                    }

                case FormatModifierType.Latitude:
                    {
                        // Include a South bearing indicator if the angle is negative, 
                        // or a North bearing indicator if the angle is positive or zero.
                        if (rounded < 0.0)
                        {
                            result = string.Format(Resources.AngleFormats.BearingFormatSouth, result);
                        }
                        else
                        {
                            result = string.Format(Resources.AngleFormats.BearingFormatNorth, result);
                        }

                        break;
                    }

                case FormatModifierType.Longitude:
                    {
                        // Include a West bearing indicator if the angle is negative, 
                        // or an East bearing indicator if the angle is positive or zero.
                        if (rounded < 0.0)
                        {
                            result = string.Format(Resources.AngleFormats.BearingFormatWest, result);
                        }
                        else
                        {
                            result = string.Format(Resources.AngleFormats.BearingFormatEast, result);
                        }

                        break;
                    }
            }

            return result;
        }
    }
}