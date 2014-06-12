using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GravityTutorial
{
    static class RectangleHelper
    {
        public static bool Collide_object(this Rectangle r1, Rectangle r2)
        {
            return (isOnTopOf(r1, r2) || isOnBotOf(r1, r2) || isOnLeftOf(r1, r2) || isOnRightOf(r1, r2));
        }
        public static bool isOnTopOf(this Rectangle r1, Rectangle r2)
        {
            return (r1.Bottom >= r2.Top - 5 &&
                r1.Bottom <= r2.Top + (r2.Height / 2) &&
                r1.Right >= r2.Left + r2.Width / 5 &&
                r1.Left <= r2.Right - (r2.Width / 5));
        }
        public static bool isOnBotOf(this Rectangle r1, Rectangle r2)
        {
            return (r1.Top <= r2.Bottom &&
                r1.Bottom > r2.Bottom + 5 &&
                r1.Right > r2.Left + 5 &&
                r1.Left < r2.Right);
        }
        public static bool isOnLeftOf(this Rectangle r1, Rectangle r2)
        {
            return (r1.Right <= r2.Right &&
                r1.Right >= r2.Left &&
                r1.Top < r2.Bottom - (r2.Width / 4) &&
                r1.Bottom > r2.Top + (r2.Height / 4));
        }
        public static bool isOnRightOf(this Rectangle r1, Rectangle r2)
        {
            return (r1.Left >= r2.Left &&
                r1.Left <= r2.Right &&
                r1.Top < r2.Bottom - (r2.Width / 4) &&
                r1.Bottom > r2.Top + (r2.Height / 4));
        }
    }
}
