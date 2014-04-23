using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GravityTutorial
{
    public class MenuButton : Button
    {
        //FIELDS
        public Menu.MenuType nextMenu;


        //CONSTRUCTOR
        public MenuButton(Vector2 pos, Tuple<string, string> text, Menu.MenuType nextMenu)
            : base(pos, text)
        {
            this.nextMenu = nextMenu;
        }

        //METHODS

        //UPDATE & DRAW
        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            spriteBatch.Draw(Ressource.Button, this.hitbox, new Rectangle(0, 0, this.SpriteWidth, this.SpriteHeight), color);
            spriteBatch.DrawString(Ressource.MenuPolice, Text, new Vector2(pos.X + 95, pos.Y), Color.White);
        }

    }
}
