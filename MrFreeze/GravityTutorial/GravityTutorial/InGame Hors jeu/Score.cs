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

        //Bonus
        Item.Type typeBonus;
        int nbBonus;
        Tuple<string, string> tuple;
        string nomBonus;

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
            this.position_timer = new Vector2(position_data.X - 175, 1);
            loser = new Rectangle(0, 0, (int)position_data.X, (int)position_data.Y);

            typeBonus = Item.Type.None;
            tuple = new Tuple<string, string>("", "");
            nomBonus = "";
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
                if (!(saved))
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

            if (typeBonus != player.CurrentItem)
            {
                typeBonus = player.CurrentItem;
                switch (typeBonus)
                {
                    case Item.Type.Invincibility:
                        nbBonus = 5;
                        tuple = Ressource.MenuString["Invincibilite"];
                        break;
                    case Item.Type.DoubleJump:
                        nbBonus = 0;
                        tuple = Ressource.MenuString["Double-saut"];
                        break;
                    case Item.Type.MoonJump:
                        nbBonus = 1;
                        tuple = Ressource.MenuString["Super saut"];
                        break;
                    case Item.Type.MultiShot:
                        nbBonus = 2;
                        tuple = Ressource.MenuString["Tir rafalle"];
                        break;
                    case Item.Type.SuperSpeed:
                        nbBonus = 3;
                        tuple = Ressource.MenuString["Super vitesse"];
                        break;
                    case Item.Type.SlowSpeed:
                        nbBonus = 4;
                        tuple = Ressource.MenuString["Ralentissement"];
                        break;
                    case Item.Type.ReverseDirection:
                        nbBonus = 6;
                        tuple = Ressource.MenuString["Direction inversee"];
                        break;
                    case Item.Type.None:
                        break;
                    default:
                        break;
                }

                if (!Ressource.parameter[2])
                {
                    nomBonus = tuple.Item1;
                }
                else
                {
                    nomBonus = tuple.Item2;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Character c in Level.Heroes)
            {
                if (c.player == 1)
                {
                    spriteBatch.DrawString(Ressource.SmallMenuPolice, "Score : " + this.score, position, Color.White);
                    spriteBatch.DrawString(Ressource.SmallMenuPolice, "Timer : " + this.timer, position_timer, Color.White);
                    spriteBatch.Draw(Ressource.fondHealthbar, new Vector2(position_life.X - 2, position_life.Y - 2), Color.White);
                    spriteBatch.Draw(Ressource.FondBonus, new Rectangle((Ressource.screenWidth / 2) - 200, 10, 400, 50), Color.White);
                    if (typeBonus != Item.Type.None)
                    {
                        Item i = new Item(new Vector2((Ressource.screenWidth / 2) - 200, 10), Ressource.Items, typeBonus, nbBonus);
                        i.Draw(spriteBatch);
                        spriteBatch.DrawString(Ressource.SmallMenuPolice, nomBonus, new Vector2((Ressource.screenWidth / 2) - 125, 15), Color.White);
                    }

                    spriteBatch.Draw(texture_life, position_life, rectangle_life, Color.White);
                }
                else
                {
                    spriteBatch.Draw(Ressource.fondHealthbar, new Vector2(Ressource.screenWidth - 150 - 15 - 2, 45 - 2), Color.White);
                    spriteBatch.Draw(Ressource.FondBonus, new Rectangle((Ressource.screenWidth / 2) - 200, 10, 400, 50), Color.White);
                    if (typeBonus != Item.Type.None)
                    {
                        Item i = new Item(new Vector2((Ressource.screenWidth / 2) - 200, 10), Ressource.Items, typeBonus, nbBonus);
                        i.Draw(spriteBatch);
                        spriteBatch.DrawString(Ressource.SmallMenuPolice, nomBonus, new Vector2((Ressource.screenWidth / 2) - 125, 15), Color.White);
                    }

                    spriteBatch.Draw(texture_life, new Vector2(Ressource.screenWidth - 150 - 15, 45), rectangle_life, Color.White);
                }
            }
        }
    }
}
