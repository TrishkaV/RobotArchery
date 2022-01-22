using System;
using System.Collections.Generic;
using System.Linq;

namespace RobotArchery
{
    internal sealed class Archer
    {
        internal string name {get;}

        internal Archer(string n) => name = n ?? throw new ArgumentNullException("");

        /* Returns the value of the hitbox hit on the target, representing its distance from the center
        */
        internal double Shoot(Target t)
        {
            var aim = new Random();
            var x = aim.Next(0, t.tgt.GetLength(0));
            var y = aim.Next(0, t.tgt.GetLength(1));

            return t.tgt[x, y];
        }
    }
}