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

            for (int x = 0; x < map.GetLength(1); x++)
            {
                for (int y = 0; y < map.GetLength(0); y++)
                {
                    int number = map[y, x];


                    if (number == 1 || number == 2)
                    {
                        CollisionTiles.Add(new CollisionTiles(number, new Rectangle(x * size, y * size, size, size)));
                    }
                    if (number == 4)
                    {
                        Level.Bonuses.Add(new gold(new Vector2(x * size, y * size)));
                    }
                    if (number == 5)
                    {
                        Level.moving_platform.Add(new moving_platform(new Vector2(x * size, y * size), Ressource.moving_plateform));
                    }
                    if (number == 10)
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
                    //ENNEMY3
                    if (number == 13)
                    {
                        Ennemies3.Add(new Ennemy3(Ressource.Ennemy3, new Vector2(x * size, y * size - 77)));

                    }
                    if (number == 14)
                    {
                        Ennemies2.Add(new Ennemy2(Ressource.Ennemy2, new Vector2(x * size, y * size - 45)));
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
