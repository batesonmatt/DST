﻿using DST.Core.Physics;

namespace DST.Core.DateTimeAdder
{
    public interface IDateTimeAdder
    {
        int Min { get; }
        int Max { get; }
        AstronomicalDateTime Add(AstronomicalDateTime start, int value);
    }
}