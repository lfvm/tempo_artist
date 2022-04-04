using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Timing
{
    public class ARTiming
    {
        public static float GetArLessTimingThanFive(float AR)
        {
            var mult = 120 * AR;
            var timing =  mult + 1800;
            return timing;
        }

        public static float GetArTimingBiggerThanFive(float AR)
        {
            var mult = -150 * AR;
            var timing = mult + 1950;
            return timing;
        }
    }
    
    public class ODTiming
    {
        public static float GetODTimingForPerfectHit(float OD)
        {
            var timing = 64 - (OD * 3);
            return timing;
        }

        public static float GetODTimingForGoodHit(float OD)
        {
            var timing = 127 - (OD * 3);
            return timing;
        }

        public static float GetODTimingForOkHit(float OD)
        {
            var timing = 151 - (OD * 3);
            return timing;
        }
    }
}
