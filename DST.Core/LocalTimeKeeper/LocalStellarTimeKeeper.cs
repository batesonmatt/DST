﻿using DST.Core.Observer;
using DST.Core.TimeKeeper;
using DST.Core.Physics;
using DST.Core.DateAndTime;

namespace DST.Core.LocalTimeKeeper
{
    public class LocalStellarTimeKeeper : BaseLocalTimeKeeper
    {
        // Creates a new LocalStellarTimeKeeper given a specified ITimeKeeper argument.
        public LocalStellarTimeKeeper(ITimeKeeper timeKeeper)
            : base(timeKeeper)
        { }

        // Returns the local Earth rotation angle (LERA) for the specified ILocalObserver and IAstronomicalDateTime arguments.
        public override Angle Calculate(ILocalObserver localObserver, IAstronomicalDateTime dateTime)
        {
            return base.Calculate(localObserver, dateTime);
        }

        // Returns the string-representation of this LocalStellarTimeKeeper instance.
        public override string ToString()
        {
            return Resources.DisplayText.AlgorithmLERAFull;
        }
    }
}
