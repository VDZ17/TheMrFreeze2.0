using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;

namespace GravityTutorial
{
    public class Hud
    {
        // Healthbar
        public Texture2D texture_life;
        public Vector2 position_life;
        public Rectangle rectangle_life;
        int damage;

        //OTHER STUFF
        public Vector2 position;
        public Vector2 position_timer;
        public int score;
        public double timer;
        int cd;
        bool new_cd;
        public static bool youlose;
        public static bool youwin;
        Rectangle loser;
        bool saved;

        public Hud(TimeSpan timespan, Vector2 position_data)
        {
            //health
            youwin = false;
            damage = 0;
            texture_life = Ressource.healthbar;
            position_life = new Vector2(15, 45);
            rectangle_life = new Rectangle(0, 0, texture_life.Width, texture_life.Height);

            saved = false;
            this.score = 0;
            timer = timespan.TotalSeconds;
            this.position = new Vector2(15, 1);
            this.position_timer = new Vector2(position_data.X - 175, 10);
            loser = new Rectangle(0, 0, (int)position_data.X, (int)position_data.Y);
        }

        

        public void Update(Character player)
        {
            //health
            damage = player.life_changment;
            if (damage != 0)
            {
                if (damage < 0)
                {
                    rectangle_life.Width += damage;
                }
                else
                {
                    rectangle_life.Width += damage;
                }
            }


            if (rectangle_life.Width <= 0)
                youlose = true;





            if (youwin)
            { 
                if(!(saved))
                {
                    loadfile.Save((score) + timer * 10, Ressource.pseudo);
                    saved = true;
                }

            }
            if (timer < 0)
            {
                if (!(youlose))
                {
                    loadfile.Save((score) + timer * 10, Ressource.pseudo);
                }
                timer = 0;
                youlose = true;
                Game1.inGame = false;
                Game1.menu = Game1.menu.ChangeMenu(Menu.MenuType.loose);
            }
            else
            {
                if (!(youlose))
                {
                    if (new_cd)
                    {
                        timer--;
                        new_cd = false;
                    }
                    else
                    {
                        cd++;
                    }
                    if (cd >= 60)
                    {
                        cd = 0;
                        new_cd = true;
                    }
                }
            }


        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Ressource.SmallMenuPolice, "Score: " + this.score, position, Color.White);
            spriteBatch.DrawString(Ressource.SmallMenuPolice, "Timer: " + this.timer, position_timer, Color.White);
            spriteBatch.Draw(Ressource.fondHealthbar, new Vector2(position_life.X - 2, position_life.Y - 2), Color.White);
            spriteBatch.Draw(texture_life, position_life, rectangle_life, Color.White);
        }
    }
}
