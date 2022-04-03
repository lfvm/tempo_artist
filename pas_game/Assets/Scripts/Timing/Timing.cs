using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Timing
{
    public class ARTiming
    {
        public static float getArLessTimingThanFive(float AR)
        {
            var timing = ((-120 * AR) + 1800);
            return timing;
        }

        public static float getArTimingBiggerThanFive(float AR)
        {
            var timing = -150 * AR + 1950;
            return timing;
        }
    }
    
    public class ODTiming
    {
        public static float GetODTimingForPerfectHit(float OD)
        {
            var timing = (-6 * OD) + 79.5f;
            return timing;
        }

        public static float GetODTimingForGoodHit(float OD)
        {
            var timing = (-8 * OD) + 139.5f;
            return timing;
        }

        public static float GetODTimingForOkHit(float OD)
        {
            var timing = (-10 * OD) + 199.5f;
            return timing;
        }
    }
}
