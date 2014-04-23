using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GravityTutorial
{
    public class Button
    {
        //FIELDS
        public Vector2 pos;
        protected Rectangle hitbox;

        public int SpriteHeight;
        public int SpriteWidth;
        protected string Text;

        //CONSTRUCTOR
        public Button(Vector2 pos, Tuple<string, string> Text)
        {
            this.pos = pos;
            if (Ressource.parameter[2])
            {
                this.Text = Text.Item2;
            }
            else
            {
                this.Text = Text.Item1;
            }
            SpriteHeight = 75;
            SpriteWidth = 500;
            this.hitbox = new Rectangle((int)pos.X, (int)pos.Y, this.SpriteWidth, this.SpriteHeight);
        }
    }
}
