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
        public List<Ennemy3> Ennemies3;
        public List<Ennemy2> Ennemies2;
        public List<Bonus> Bonuses;
        public int[,] read;
        public loadfile file = new loadfile();
        public Map map;
        public int lvl;
        public static bool updateHero = true;
        int timerEndLevel = 300;
        int timerEnd = 0;

        //CONSRTRUCTOR
        public Level(int lvl)
        {
            string dir = (Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName).FullName) + "\\GravityTutorialContent\\level\\");
            this.lvl = lvl;
            map = new Map();
            Heroes = new List<Character>();
            Bonuses = new List<Bonus>();
            Ennemies3 = Map.Ennemies3;
            Ennemies2 = Map.Ennemies2;

            switch (lvl)
            {
                case 0:
                    {
                        break;
                    }
                case 1:
                    {
                        int block_size = 50;
                        map.Generate(file.read(dir + "lvl1.txt"), block_size, this);
                        Heroes.Add(new Character(Ressource.Player_animation, new Vector2(0, 0)));

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
        public void Update(GameTime gameTime, SoundEffectInstance effect)
        {
            if (updateHero)
                Heroes.ElementAt(0).Update(gameTime, effect);

            foreach (CollisionTiles tile in map.CollisionTiles)
            {
                Heroes.ElementAt(0).Collision(tile.Rectangle, map.Width, map.Height, Ressource.effect2, tile.Tile_name);

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


            foreach (Bonus gold in Bonuses)
            {
                gold.Update(Heroes.ElementAt(0), Game1.score);
            }

            //Updates Ennemies
            bool hasAlreadyHit = false;
            foreach (Ennemy2 e in Ennemies2)
            {
                hasAlreadyHit = false;
                e.Update(gameTime);
                e.hit(Heroes.ElementAt(0));
                if (e.firstHit)
                {
                    if (e.direction == Direction2.Right)
                    {
                        Heroes.ElementAt(0).velocity.X += 5f;
                    }
                    else if (e.direction == Direction2.Left)
                    {
                        Heroes.ElementAt(0).velocity.X += -5f;
                    }
                    

                }


            }
            foreach (Ennemy3 e in Ennemies3)
            {

                if (e.hasHit)
                {
                    updateHero = false;
                    //Hud.youlose = true;
                }
                e.Update(gameTime);

                if (!updateHero)
                {
                    if (timerEnd == timerEndLevel)
                        Hud.youlose = true;
                    else
                        timerEnd++;
                }



            }
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Ressource.background, new Rectangle(0, -200, map.Width, Ressource.screenHeight + 500), Color.White);
            map.Draw(spriteBatch);
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

        }


    }

}
