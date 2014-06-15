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
        public Rectangle rectangle_life2;
        int damage;
        int damage2;

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
        public int nbBonus;
        Tuple<string, string> tuple;
        public string nomBonus;

        Item.Type typeBonus2;
        public int nbBonus2;
        Tuple<string, string> tuple2;
        public string nomBonus2;

        public Hud(TimeSpan timespan, Vector2 position_data)
        {
            //health
            youwin = false;
            youlose = false;
            damage = 0;
            damage2 = 0;
            texture_life = Ressource.healthbar;
            position_life = new Vector2(12, 70);

            rectangle_life = new Rectangle(0, 0, 150, texture_life.Height);
            rectangle_life2 = new Rectangle(0, 0, 150, texture_life.Height);

            saved = false;
            this.score = 0;
            timer = timespan.TotalSeconds;
            this.position = new Vector2(15, 1);
            this.position_timer = new Vector2(position_data.X - 175, 1);
            loser = new Rectangle(0, 0, (int)position_data.X, (int)position_data.Y);

            typeBonus = Item.Type.None;
            tuple = new Tuple<string, string>("", "");
            nomBonus = "";

            typeBonus2 = Item.Type.None;
            tuple2 = new Tuple<string, string>("", "");
            nomBonus2 = "";
        }



        public void Update(Character player, Character player2 = null)
        {
            //health
            damage = player.life_changment;
            if (damage != 0)
            {
                rectangle_life.Width += damage;
            }

            if (Ressource.parameter[3] && player2 != null)
            {
                damage2 = player2.life_changment;
                if (damage2 != 0)
                {
                    rectangle_life2.Width += damage2;

                }
            }

            if (rectangle_life.Width <= 0 || rectangle_life2.Width <= 0)
                youlose = true;


            if (Ressource.parameter[3] && player2 != null && typeBonus2 != player2.CurrentItem)
            {
                typeBonus2 = player2.CurrentItem;
                switch (typeBonus2)
                {
                    case Item.Type.Invincibility:
                        nbBonus2 = 5;
                        tuple2 = Ressource.MenuString["Invincibilite"];
                        break;
                    case Item.Type.DoubleJump:
                        nbBonus2 = 0;
                        tuple2 = Ressource.MenuString["Double-saut"];
                        break;
                    case Item.Type.MoonJump:
                        nbBonus2 = 1;
                        tuple2 = Ressource.MenuString["Super saut"];
                        break;
                    case Item.Type.MultiShot:
                        nbBonus2 = 2;
                        tuple2 = Ressource.MenuString["Tir rafalle"];
                        break;
                    case Item.Type.SuperSpeed:
                        nbBonus2 = 3;
                        tuple2 = Ressource.MenuString["Super vitesse"];
                        break;
                    case Item.Type.SlowSpeed:
                        nbBonus2 = 4;
                        tuple2 = Ressource.MenuString["Ralentissement"];
                        break;
                    case Item.Type.ReverseDirection:
                        nbBonus2 = 6;
                        tuple2 = Ressource.MenuString["Direction inversee"];
                        break;
                    default:
                        nbBonus2 = -1;
                        break;



                }

                if (!Ressource.parameter[2])
                {
                    nomBonus2 = tuple2.Item1;
                }
                else
                {
                    nomBonus2 = tuple2.Item2;
                }
            }


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
                    default:
                        nbBonus = -1;
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
            if (Ressource.parameter[3])
            {

                foreach (Character c in Game1.level.Heroes)
                {
                    if (c.player == 1)
                    {
                        if (typeBonus == Item.Type.None)
                        {
                            nbBonus = -1;
                        }
                        spriteBatch.DrawString(Ressource.SmallMenuPolice, "Score : " + this.score, new Vector2(Ressource.screenWidth / 2 - Ressource.SmallMenuPolice.MeasureString("Score : " + this.score).Length() / 2, 40), Color.White);
                        spriteBatch.DrawString(Ressource.SmallMenuPolice, "Timer : " + this.timer, new Vector2(Ressource.screenWidth / 2 - Ressource.SmallMenuPolice.MeasureString("Timer : " + this.timer).Length() / 2, 5), Color.White);
                        spriteBatch.Draw(Ressource.fondHealthbar, new Vector2(position_life.X - 2, position_life.Y - 2), Color.White);
                        spriteBatch.Draw(Ressource.FondBonus, new Rectangle(10, 10, 400, 50), Color.White);
                        if (nbBonus != -1)
                        {
                            Item i = new Item(new Vector2(10, 10), Ressource.Items, typeBonus, nbBonus);
                            i.Draw(spriteBatch);
                            spriteBatch.DrawString(Ressource.SmallMenuPolice, nomBonus, new Vector2(70, 15), Color.White);
                        }

                        spriteBatch.Draw(texture_life, position_life, rectangle_life, Color.White);
                        spriteBatch.Draw(Ressource.Cursor1, new Rectangle(12 + 150 + 30, 70 - 5, 25, 43), Color.White);
                    }
                    else
                    {
                        if (typeBonus2 == Item.Type.None)
                        {
                            nbBonus2 = -1;
                        }
                        spriteBatch.Draw(Ressource.fondHealthbar, new Vector2(Ressource.screenWidth - 150 - 10 - 2, 70 - 2), Color.White);
                        spriteBatch.Draw(Ressource.FondBonus, new Rectangle(Ressource.screenWidth - 400 - 10, 10, 400, 50), Color.White);
                        if (nbBonus2 != -1)
                        {

                            Item i = new Item(new Vector2(Ressource.screenWidth - 410, 10), Ressource.Items, typeBonus2, nbBonus2);
                            i.Draw(spriteBatch);
                            spriteBatch.DrawString(Ressource.SmallMenuPolice, nomBonus2, new Vector2(Ressource.screenWidth - 400 - 10 + 65, 15), Color.White);
                        }

                        spriteBatch.Draw(texture_life, new Vector2(Ressource.screenWidth - 150 - 10, 70), rectangle_life2, Color.White);
                        spriteBatch.Draw(Ressource.Cursor2, new Rectangle(Ressource.screenWidth - 150 - 10 - 30 - 25, 70 - 5, 25, 43), Color.White);
                    }
                }
            }
            else
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
        }
    }
}
