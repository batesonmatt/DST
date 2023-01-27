namespace DST.Core.Physics
{
    public static class Extensions
    {
        // Determines whether the specified number is a finite, real number greater than 
        // double.MinValue and less than double.MaxValue.
        public static bool IsFiniteRealNumber(this double d)
        {
            return d switch
            {
                double.MinValue or double.MaxValue or double.NegativeInfinity or double.PositiveInfinity or double.NaN => false,
                _ => true,
            };
        }
    }
}