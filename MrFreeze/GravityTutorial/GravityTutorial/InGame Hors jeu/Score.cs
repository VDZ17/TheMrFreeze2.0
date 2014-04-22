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
        Rectangle loser;
        bool saved;

        public Hud(TimeSpan timespan, Vector2 position_data)
        {
            //health
            damage = 0;
            texture_life = Ressource.healthbar;
            position_life = new Vector2(0, 30);
            rectangle_life = new Rectangle(0, 0, texture_life.Width, texture_life.Height);

            saved = false;
            this.score = 0;
            timer = timespan.TotalSeconds;
            this.position = Vector2.One;
            this.position_timer = new Vector2(position_data.X - 100, 10);
            loser = new Rectangle(0, 0, (int)position_data.X, (int)position_data.Y);

        }

        public void Save(double score, string pseudo)
        {
            System.Collections.Generic.List<Tuple<string, double>> dicotop5 = new List<Tuple<string, double>> { };
            dicotop5.Add(new Tuple<string, double>(pseudo, score));
            StreamReader fluxInfos2;
            string ligne;
            using (fluxInfos2 = new StreamReader(Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName).FullName) + "\\GravityTutorialContent\\hightscore.txt"))
            {
                ligne = fluxInfos2.ReadLine();
                while (ligne != null)
                {
                    string[] done = ligne.Split('/');
                    double point = double.Parse(done[1]);
                    dicotop5.Add(new Tuple<string, double>(done[0], point));
                    ligne = fluxInfos2.ReadLine();
                }
            }
            var DicoTrie = (from entry in dicotop5 orderby entry.Item2 descending select entry);
            StreamWriter fluxInfos3;
            using (fluxInfos3 = new StreamWriter((Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName).FullName) + "\\GravityTutorialContent\\hightscore.txt")))
            {

            }

            int i = 0;
            foreach (System.Tuple<string, double> pair in DicoTrie)
            {

                if (i < 5)
                {
                    i++;
                    StreamWriter fluxInfos;
                    using (fluxInfos = new StreamWriter(Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName).FullName) + "\\GravityTutorialContent\\hightscore.txt", true))
                    {
                        fluxInfos.WriteLine(pair.Item1 + '/' + pair.Item2);
                    }
                }
                else
                {
                    break;
                }
            }
        }

        public void Update(Character player)
        {
            //health
            damage = player.life_changment;
            if (damage != 0)
            {
                if (damage < 0)
                {
                    rectangle_life.Width -= 10;
                }
                else
                {
                    rectangle_life.Width += 10;
                }
            }
            if (rectangle_life.Width <= 0)
                youlose = true;





            if (youlose)
            { 
                if(!(saved))
                {
                    this.Save((score) * 100, "Hadrien");
                    saved = true;
                }

            }
            if (timer < 0)
            {
                if (!(youlose))
                {
                    this.Save((score) * 100, "Hadrien");
                }
                timer = 0;
                youlose = true;
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
            if (youlose)
            {
                spriteBatch.Draw(Ressource.Loser, loser, Color.White);
            }
            spriteBatch.DrawString(Ressource.Font, "Score: " + this.score, position, Color.Red);
            spriteBatch.DrawString(Ressource.Font, "Timer: " + this.timer, position_timer, Color.Red);
            spriteBatch.Draw(texture_life, position_life, rectangle_life, Color.White);
        }
    }
}
