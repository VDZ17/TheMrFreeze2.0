using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GravityTutorial
{

    public class LevelButton : Button
    {
        //FIELDS
        int lvl;

        //CONSTRUCTOR
        public LevelButton(Vector2 pos, int lvl) :
            base(pos, Ressource.MenuString["Niveau"])
        {
            Text = Text + " " + lvl;
            this.lvl = lvl;
        }

        //UPDATE & DRAW
        public void Update()
        {
            Game1.Level = new Level(lvl);
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            spriteBatch.Draw(Ressource.Button, this.hitbox, new Rectangle(0, 0, this.SpriteWidth, this.SpriteHeight), color);
            spriteBatch.DrawString(Ressource.MenuPolice, Text, new Vector2(pos.X + 95, pos.Y), Color.White);
        }

    }
}
