﻿using DST.Core.DateAndTime;
using DST.Core.LocalTimeKeeper;
using DST.Core.Observer;
using DST.Core.Physics;

namespace DST.Core.LocalHourAngle
{
    public class LocalStellarHourAngle : BaseLocalHourAngle
    {
        // Creates a new LocalMeanSiderealHourAngle given the specified ILocalTimeKeeper argument.
        public LocalStellarHourAngle(ILocalTimeKeeper localTimeKeeper)
            : base(localTimeKeeper)
        { }

        // Returns the stellar local hour angle (LHA) for the specified ILocalObserver and IAstronomicalDateTime arguments.
        public override Angle Calculate(ILocalObserver localObserver, IAstronomicalDateTime dateTime)
        {
            return base.Calculate(localObserver, dateTime);
        }
    }
}
