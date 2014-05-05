using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using GravityTutorial.LevelGeneration;
using System.IO;
using Microsoft.Xna.Framework.Content;
using GravityTutorial.InGame_Jeu;

namespace GravityTutorial
{
    public class Level
    {
        //FIELDS
        public static List<Character> Heroes;
        public List<moving_platform> moving_platform;
        public List<Destroying_platform> destroy_platform;
        public List<Ennemy3> Ennemies3;
        public List<Ennemy2> Ennemies2;
        public List<Ennemy1> Ennemies1;
        public List<Bonus> Bonuses;
        public web Web;
        public int[,] read;
        public loadfile file = new loadfile();
        public Map map;
        public int lvl;
        public static bool updateHero = true;
        int timerspeedAttack3 = 30;
        int timerEnd3 = 0;


        //CONSRTRUCTOR
        public Level(int lvl)
        {
            Web = new web();
            string dir = (Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName).FullName) + "\\GravityTutorialContent\\level\\");
            this.lvl = lvl;
            map = new Map();
            moving_platform = new List<GravityTutorial.moving_platform>();
            Heroes = new List<Character>();
            Bonuses = new List<Bonus>();
            destroy_platform = new List<Destroying_platform>();

            Ennemies1 = Map.Ennemies1;
            Ennemies2 = Map.Ennemies2;
            Ennemies3 = Map.Ennemies3;

            switch (lvl)
            {
                case 0:
                    {
                        break;
                    }
                case 1:
                    {
                        int block_size = 50;
                        for (int i = 0; i < Ennemies1.Count; i++)
                            Ennemies1.RemoveAt(i);
                        for (int i = 0; i < Ennemies2.Count; i++)
                            Ennemies2.RemoveAt(i);
                        for (int i = 0; i < Ennemies3.Count; i++)
                            Ennemies3.RemoveAt(i);
                        map.Generate(file.read(dir + "lvl1.txt"), block_size, this);
                        Heroes.Add(new Character(Ressource.Player_animation, new Vector2(0, 0)));
                        Console.WriteLine("case1");
                        break;
                    }
                case 2:
                    {
                        int block_size = 50;
                        map.Generate(file.read(dir + "lvl2.txt"), block_size, this);
                        Heroes.Add(new Character(Ressource.Player_animation, new Vector2(0, 0)));
                        break;
                    }
                case 3:
                    {
                        int block_size = 50;
                        map.Generate(file.read(dir + "lvl3.txt"), block_size, this);
                        Heroes.Add(new Character(Ressource.Player_animation, new Vector2(0, 15 * block_size)));
                        break;
                    }
                case 4:
                    {
                        int block_size = 50;
                        map.Generate(file.read(dir + "lvl4.txt"), block_size, this);
                        Heroes.Add(new Character(Ressource.Player_animation, new Vector2(0, 0)));
                        break;
                    }
                case 5:
                    {
                        int block_size = 50;
                        map.Generate(file.read(dir + "lvl5.txt"), block_size, this);
                        Heroes.Add(new Character(Ressource.Player_animation, new Vector2(0, 0)));
                        break;
                    }
                case 6:
                    {
                        int block_size = 50;
                        map.Generate(file.read(dir + "lvl6.txt"), block_size, this);
                        Heroes.Add(new Character(Ressource.Player_animation, new Vector2(0, 0)));
                        break;
                    }
                default:
                    break;
            }
            //TODO
        }




        //UPDATE & DRAW
        public void Update(GameTime gameTime, SoundEffectInstance effect, Hud score)
        {
            if (Hud.youlose)
            {
                Game1.inGame = false;
                Game1.menu = Game1.menu.ChangeMenu(Menu.MenuType.loose);
            }

            particule.particleEffects["Snow"].Trigger(Vector2.Zero);
            if (updateHero)
                Heroes.ElementAt(0).Update(gameTime, effect);

            foreach (CollisionTiles tile in map.CollisionTiles)
            {
                Heroes.ElementAt(0).Collision(tile.Rectangle, map.Width, map.Height, Ressource.effect2, tile.Tile_name);
                foreach (Bullet b in Heroes[0].Bullets)
                {
                    b.Update(tile);
                }
                foreach (Ennemy3 e in Ennemies3)
                {
                    e.Patrol(tile.Rectangle, tile.Tile_name);
                    e.Collision(tile.Rectangle, tile.Tile_name);
                    e.hit(Heroes.ElementAt(0));
                }
                foreach (Ennemy2 e in Ennemies2)
                {
                    e.Collision(tile.Rectangle, tile.Tile_name);
                }
                foreach (Ennemy1 e in Ennemies1)
                {
                    e.Collision(tile.Rectangle, tile.Tile_name);
                }
            }


            //Collisions invisibles
            foreach (CollisionTiles tile in map.InvisibleTiles)
            {
                foreach (Ennemy3 e in Ennemies3)
                {
                    e.Patrol(tile.Rectangle, tile.Tile_name);
                    e.Collision(tile.Rectangle, tile.Tile_name);
                }
            }

            for (int i = 0; i < destroy_platform.Count; i++)
            {
                if ((destroy_platform[i].detruit))
                {
                    destroy_platform.RemoveAt(i);
                    i--;
                }
            }
            foreach (Destroying_platform item in destroy_platform)
            {
                Heroes[0].Collision(item.hitbox, map.Width, map.Height, Ressource.effect2, "Tile6");
                foreach (Bullet b in Heroes[0].Bullets)
                {
                    b.Update(item);
                    item.update(b);
                }
                foreach (Ennemy1 e in Ennemies1)
                {
                    e.Collision(item.hitbox, "Tile5");
                }
            }

            foreach (moving_platform item in moving_platform)
            {
                Heroes[0].Collision(item.hitbox, map.Width, map.Height, Ressource.effect2, "Tile5");
                foreach (Bullet b in Heroes[0].Bullets)
                {
                    b.Update(item);
                }
                foreach (Ennemy1 e in Ennemies1)
                {
                    e.Collision(item.hitbox, "Tile5");
                }
                item.update();
            }

            foreach (Bonus gold in Bonuses)
            {
                gold.Update(Heroes.ElementAt(0), Game1.score);
            }

            //Updates Ennemies
            foreach (Ennemy2 e in Ennemies2)
            {
                e.Update(gameTime);
                e.hit(Heroes.ElementAt(0));
                if (e.firstHit)
                {
                    Heroes.ElementAt(0).velocity.X = 0;

                    if (e.direction == Direction2.Right)
                    {
                        Heroes.ElementAt(0).velocity.X = 5f;
                    }
                    else if (e.direction == Direction2.Left)
                    {
                        Heroes.ElementAt(0).velocity.X = -5f;
                    }
                }

                foreach (Bullet b in Heroes[0].Bullets)
                {
                    if (b.hitbox_bullet.Intersects(e.rectangle))
                    {
                        b.Update(e);
                    }
                }

            }
            foreach (Ennemy3 e in Ennemies3)
            {

                if (e.hasHit)
                    updateHero = false;


                e.Update(gameTime);


                foreach (Bullet b in Heroes[0].Bullets)
                {
                    if (b.hitbox_bullet.Intersects(e.rectangle))
                    {
                        b.Update(e);
                    }
                }


                if (!updateHero)
                {
                    if (timerEnd3 == timerspeedAttack3)
                        Heroes[0].life_changment = -1;

                    else
                        timerEnd3++;
                }
            }
            foreach (Ennemy1 e in Ennemies1)
            {
                e.Update(gameTime);
                foreach (Bullet b in Heroes[0].Bullets)
                {
                    b.Update(e);
                }
            }

            for (int i = 0; i < Ennemies1.Count; i++)
            {
                if (Ennemies1[i].life <= 0)
                    Ennemies1.RemoveAt(i);
            }
            for (int i = 0; i < Ennemies2.Count; i++)
            {
                if (Ennemies2[i].life <= 0)
                    Ennemies2.RemoveAt(i);
            }
            for (int i = 0; i < Ennemies3.Count; i++)
            {
                if (Ennemies3[i].life <= 0)
                    Ennemies3.RemoveAt(i);
            }

            if (Hud.youwin)
            {
                try
                {
                    Web.send_request(Ressource.pseudo, score.score, lvl);
                }
                catch (Exception) 
                {

                }
                    
            }

        }


        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Ressource.background, new Rectangle(0, -200, map.Width, Ressource.screenHeight + 500), Color.White);
            map.Draw(spriteBatch);

            foreach (Destroying_platform item in destroy_platform)
            {
                item.Draw(spriteBatch);
            }

            foreach (moving_platform item in moving_platform)
            {
                item.Draw(spriteBatch);
            }

            foreach (Bonus gold in Bonuses)
            {
                gold.Draw(spriteBatch);
            }


            foreach (Character c in Heroes)
            {
                if (updateHero)
                    c.Draw(spriteBatch);
            }

            foreach (Bonus b in Bonuses)
            {
                b.Draw(spriteBatch);
            }
            foreach (Ennemy3 e in Ennemies3)
            {
                e.Draw(spriteBatch);
            }
            foreach (Ennemy2 e in Ennemies2)
            {
                e.Draw(spriteBatch);
            }
            foreach (Ennemy1 e in Ennemies1)
            {
                e.Draw(spriteBatch);
            }

        }


    }

}
