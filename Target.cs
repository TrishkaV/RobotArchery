using System;
using System.Collections.Generic;
using System.Linq;

namespace RobotArchery
{
    /* Singleton Class, all Archers are shooting the same target
    */
    internal sealed class Target
    {
        internal double[,] tgt { get; init; }

#region Singleton Construction
        private static Target? instance;
        private Target()
        {
            var t = PopulateTgt();
            tgt = t;
        }

        internal static Target GetTarget()
        {
            if (instance == null)
                instance = new Target();
            return instance;
        }
#endregion

        private double[,] PopulateTgt()
        {
            /* 225_030_001 Target hitboxes. 
             * Side length must be an odd number so that there can be only one central hitbox
            */
            ushort side = 15_001;
            var center = new Tuple<ushort, ushort>((ushort)(side / 2), (ushort)(side / 2));
            var t = new double[side, side];

            for (var i = 0; i < t.GetLength(0); i++)
            {
                for (var m = 0; m <= t.GetLength(1) / 2; m++)
                {
                    t[i, m] = Math.Sqrt(Math.Pow(center.Item1 - i, 2) + Math.Pow(center.Item2 - m, 2));
                    t[i, t.GetLength(1) - 1 - m] = t[i, m];
                }
            }

            return t;
        }
    }
}

/* Target design:
*  each target's hitbox's value represents its distance from the center so to optimize computation resources
*
*  8 7 6 5 4 5 6 7 8
*  7 6 5 4 3 4 5 6 7
*  6 3 4 3 2 2 4 3 6
*  5 3 3 2 1 2 3 3 5
*  4 3 2 1 0 1 2 3 4
*  5 3 3 2 1 2 3 3 5
*  6 3 4 3 2 3 4 3 6
*  7 6 5 4 3 4 5 6 7
*  8 7 6 5 4 5 6 7 8
*/