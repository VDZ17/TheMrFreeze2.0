using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GravityTutorial
{
    public class MenuTitle
    {
        //FIELDS
        Vector2 pos;
        Rectangle hitbox;

        int SpriteHeight;
        int SpriteWidth;
        int FrameLine;


        //CONSTRUCTOR
        public MenuTitle(Vector2 pos, int FrameLine)
        {
            this.pos = pos;
            this.FrameLine = FrameLine;
            SpriteHeight = 250;
            SpriteWidth = 700;
            this.hitbox = new Rectangle((int)pos.X, (int)pos.Y, this.SpriteWidth, this.SpriteHeight);
        }

        //METHODS

        //UPDATE & DRAW
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Ressource.Title, this.hitbox, new Rectangle(0, this.FrameLine * this.SpriteHeight, this.SpriteWidth, this.SpriteHeight),
                Color.White);
        }

    }
}
