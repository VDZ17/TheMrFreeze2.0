using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GravityTutorial.InGame_Jeu;

namespace GravityTutorial
{
    public class Map
    {
        public List<CollisionTiles> CollisionTiles = new List<CollisionTiles>();
        public List<CollisionTiles> InvisibleTiles = new List<CollisionTiles>();
        public static List<Ennemy3> Ennemies3 = new List<Ennemy3>();
        public static List<Ennemy2> Ennemies2 = new List<Ennemy2>();
        public static List<Ennemy1> Ennemies1 = new List<Ennemy1>();

        /*public List<CollisionTiles> CollisionTiles
        {
            get { return collisionTiles; }
        }*/

        private int width, height;
        public int Width
        {
            get { return width; }
        }
        public int Height
        {
            get { return height; }
        }

        public Map()
        {

        }


        public void Generate(int[,] map, int size, Level Level)
        {
            for (int i = 0; i < Ennemies1.Count; i++)
            {
                Ennemies1.RemoveAt(i);
            }
            for (int i = 0; i < Ennemies2.Count; i++)
            {
                Ennemies2.RemoveAt(i);
            }
            for (int i = 0; i < Ennemies3.Count; i++)
            {
                Ennemies3.RemoveAt(i);
            }

            for (int x = 0; x < map.GetLength(1); x++)
            {
                for (int y = 0; y < map.GetLength(0); y++)
                {
                    int number = map[y, x];


                    if (number == 1 || number == 2 || number == 16)
                    {
                        CollisionTiles.Add(new CollisionTiles(number, new Rectangle(x * size, y * size, size, size)));
                    }
                    if (number == 6)
                    {
                        Level.destroy_platform.Add(new Destroying_platform(new Vector2(x * size, y * size), Ressource.detruit_c));
                    }
                    if (number == 4) //Gold
                    {
                        Level.Bonuses.Add(new gold(new Vector2(x * size, y * size)));
                    }
                    if (number == 5)
                    {
                        Level.moving_platform.Add(new moving_platform(new Vector2(x * size, y * size), Ressource.moving_plateform));
                    }
                    if (number == 10) //Star
                    {
                        Level.Bonuses.Add(new igloo(new Vector2(x * size, y * size)));
                    }
                    //INSIVIBLE BLOC
                    if (number == 12)
                    {
                        if (x == 0)
                            InvisibleTiles.Add(new CollisionTiles(number, new Rectangle(x * size, y * size, 5, size)));
                        else
                            InvisibleTiles.Add(new CollisionTiles(number, new Rectangle((x + 2) * size, y * size, 5, size)));

                    }
                    //ENNEMY
                    if (number == 13) //Bleu
                    {
                        Ennemies3.Add(new Ennemy3(Ressource.Ennemy3, new Vector2(x * size, y * size - 100)));

                    }
                    if (number == 14) //Jaune
                    {
                        Ennemies2.Add(new Ennemy2(Ressource.Ennemy2, new Vector2(x * size, y * size - 45)));
                    }
                    if (number == 15) // Rouge boss
                    {
                        Ennemies1.Add(new Ennemy1(Ressource.Ennemy1, new Vector2(x * size, y * size - 14)));

                    }

                    //BONUS
                    if (number == 100)
                    {
                        Level.Items.Add(new Item(new Vector2(x * size, y * size), Ressource.Items, Item.Type.DoubleJump, number - 100));
                    }
                    if (number == 101)
                    {
                        Level.Items.Add(new Item(new Vector2(x * size, y * size), Ressource.Items, Item.Type.MoonJump, number - 100));
                    }
                    if (number == 102)
                    {
                        Level.Items.Add(new Item(new Vector2(x * size, y * size), Ressource.Items, Item.Type.MultiShot, number - 100));
                    }
                    if (number == 103)
                    {
                        Level.Items.Add(new Item(new Vector2(x * size, y * size), Ressource.Items, Item.Type.SuperSpeed, number - 100));
                    }
                    if (number == 104)
                    {
                        Level.Items.Add(new Item(new Vector2(x * size, y * size), Ressource.Items, Item.Type.SlowSpeed, number - 100));
                    }
                    if (number == 105)
                    {
                        Level.Items.Add(new Item(new Vector2(x * size, y * size), Ressource.Items, Item.Type.Invincibility, number - 100));
                    }
                    if (number == 106)
                    {
                        Level.Items.Add(new Item(new Vector2(x * size, y * size), Ressource.Items, Item.Type.ReverseDirection, number - 100));
                    }

                    width = (x + 1) * size;
                    height = (y + 1) * size;
                }
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (CollisionTiles tile in CollisionTiles)
            {
                tile.Draw(spriteBatch);
            }
        }
    }
}
