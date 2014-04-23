using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GravityTutorial
{
    public class SwitchButton : Button
    {
        //FIELDS
        public int nbParameter;

        //CONSTRUCTOR
        public SwitchButton(Vector2 pos, Tuple<string, string> text, int nbParameter)
            : base(pos, text)
        {
            this.nbParameter = nbParameter;
        }

        //METHODS

        //UPDATE & DRAW
        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            spriteBatch.Draw(Ressource.Button, this.hitbox, new Rectangle(0, 0, this.SpriteWidth, this.SpriteHeight), color);
            spriteBatch.DrawString(Ressource.MenuPolice, Text, new Vector2(pos.X + 95, pos.Y), Color.White);
            if (Ressource.parameter[nbParameter])
            {
                spriteBatch.DrawString(Ressource.MenuPolice, "ON", new Vector2(pos.X + 380, pos.Y), Color.LimeGreen);
            }
            else
            {
                spriteBatch.DrawString(Ressource.MenuPolice, "OFF", new Vector2(pos.X + 380, pos.Y), Color.Red);
            }
        }

        public void Update()
        {
            Ressource.parameter[this.nbParameter] = !Ressource.parameter[this.nbParameter];
        }
    }
}
