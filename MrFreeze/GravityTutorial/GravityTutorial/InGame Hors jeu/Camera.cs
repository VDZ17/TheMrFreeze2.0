using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GravityTutorial
{
    class Camera
    {
        public static Matrix Transform;
       

        public Vector2 center;
        public Viewport viewport;
        public Camera(Viewport newviewport)
        {
            viewport = newviewport;
        }

        public void update(Vector2 position, int xoffset, int yoffset)
        {
            int width = viewport.Width;
            int height = viewport.Height;
            if (position.X < width / 2)
            {
                center.X = width / 2;
            }
            else if (position.X > xoffset - (width / 2))
            {
                center.X = xoffset - (width / 2);
            }
            else
            {
                center.X = position.X;
            }

            if (position.Y < height / 2)
            {
                //center.Y = viewport.Height / 3; ANCIEN HADRIEN
                center.Y = position.Y + height / 5;
            }
            //else if (position.Y > yoffset - (viewport.Height / 2))
            else if (position.Y > yoffset - (height / 2))
            {
                center.Y = yoffset - (height / 2);
            }
            else if (position.Y - 80 > height)
            {
                //center.Y = yoffset - (viewport.Height / 3);
                center.Y = position.Y - height / 5;
                
            }
            
            /*else
            {
                center.Y = position.Y;
            }*/

             Transform = Matrix.CreateTranslation(new Vector3(-center.X + (viewport.Width / 2),
                                                                 /*- center.Y + (viewport.Height / 2)*/0, 0));
        }
    }
}
