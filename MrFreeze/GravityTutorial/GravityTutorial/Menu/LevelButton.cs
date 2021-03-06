﻿using System;
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
            Game1.level = new Level(lvl);
            Game1.score = new Hud(new TimeSpan(0, 0, 80), new Vector2(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height));
            Ressource.messageJ1toJ2 = "Z/newlvl/" + lvl + "+";
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            spriteBatch.Draw(Ressource.Button, this.hitbox, new Rectangle(0, 0, this.SpriteWidth, this.SpriteHeight), color);
            spriteBatch.DrawString(Ressource.MenuPolice, Text, new Vector2(pos.X + 95, pos.Y), Color.White);
        }

    }
}
