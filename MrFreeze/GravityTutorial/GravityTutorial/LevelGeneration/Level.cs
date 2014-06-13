using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using GravityTutorial;
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

        public List<float> Distance;

        public List<Bonus> Bonuses;
        public List<Item> Items;

        public web Web;
        public int[,] read;
        public Map map;
        public int lvl;
        public static bool updateHero;
        public static bool updateHero2;
        int timerspeedAttack3 = 30;
        int timerEnd3 = 0;


        //CONSRTRUCTOR
        public Level(int lvl)
        {
            Web = new web();
            string dir = (Directory.GetCurrentDirectory() +"\\Content\\Levels\\");
            this.lvl = lvl;
            map = new Map();

            updateHero = true;
            updateHero2 = true;
            moving_platform = new List<GravityTutorial.moving_platform>();

            Heroes = new List<Character>();
            Bonuses = new List<Bonus>();
            Items = new List<Item>();
            destroy_platform = new List<Destroying_platform>();

            Ennemies1 = Map.Ennemies1;
            Ennemies2 = Map.Ennemies2;
            Ennemies3 = Map.Ennemies3;

            for (int i = 0; i < Ennemies1.Count; i++)
                Ennemies1.RemoveAt(i);
            for (int i = 0; i < Ennemies2.Count; i++)
                Ennemies2.RemoveAt(i);
            for (int i = 0; i < Ennemies3.Count; i++)
                Ennemies3.RemoveAt(i);
            #region switch lvl
            switch (lvl)
            {
                case 0:
                    {
                        break;
                    }
                case 1:
                    {
                        int block_size = 50;
                        map.Generate(loadfile.read(dir + "lvl1.txt"), block_size, this);

                        Heroes.Add(new Character(Ressource.Player_animation, new Vector2(0, 0),
                            Ressource.KeyJ1[Ressource.inGameAction.Left], Ressource.KeyJ1[Ressource.inGameAction.Right],
                            Ressource.KeyJ1[Ressource.inGameAction.Jump], Ressource.KeyJ1[Ressource.inGameAction.Shoot], 1));

                        if (Ressource.parameter[3])
                            Heroes.Add(new Character(Ressource.Player_animation, new Vector2(130, 0),
                                Ressource.KeyJ2[Ressource.inGameAction.Left], Ressource.KeyJ2[Ressource.inGameAction.Right],
                                Ressource.KeyJ2[Ressource.inGameAction.Jump], Ressource.KeyJ2[Ressource.inGameAction.Shoot], 2));

                        //Console.WriteLine("count" + Heroes.Count);
                        break;
                    }
                case 2:
                    {
                        int block_size = 50;
                        map.Generate(loadfile.read(dir + "lvl2.txt"), block_size, this);
                        Heroes.Add(new Character(Ressource.Player_animation, new Vector2(0, 0),
                            Ressource.KeyJ1[Ressource.inGameAction.Left], Ressource.KeyJ1[Ressource.inGameAction.Right],
                            Ressource.KeyJ1[Ressource.inGameAction.Jump], Ressource.KeyJ1[Ressource.inGameAction.Shoot], 1));

                        if (Ressource.parameter[3])
                            Heroes.Add(new Character(Ressource.Player_animation, new Vector2(130, 0),
                                Ressource.KeyJ2[Ressource.inGameAction.Left], Ressource.KeyJ2[Ressource.inGameAction.Right],
                                Ressource.KeyJ2[Ressource.inGameAction.Jump], Ressource.KeyJ2[Ressource.inGameAction.Shoot], 2));

                        break;
                    }
                case 3:
                    {
                        int block_size = 50;
                        map.Generate(loadfile.read(dir + "lvl3.txt"), block_size, this);
                        Heroes.Add(new Character(Ressource.Player_animation, new Vector2(0, 0),
                            Ressource.KeyJ1[Ressource.inGameAction.Left], Ressource.KeyJ1[Ressource.inGameAction.Right],
                            Ressource.KeyJ1[Ressource.inGameAction.Jump], Ressource.KeyJ1[Ressource.inGameAction.Shoot], 1));


                        if (Ressource.parameter[3])
                            Heroes.Add(new Character(Ressource.Player_animation, new Vector2(130, 0),
                                Ressource.KeyJ2[Ressource.inGameAction.Left], Ressource.KeyJ2[Ressource.inGameAction.Right],
                                Ressource.KeyJ2[Ressource.inGameAction.Jump], Ressource.KeyJ2[Ressource.inGameAction.Shoot], 2));

                        break;
                    }
                case 4:
                    {
                        int block_size = 50;
                        map.Generate(loadfile.read(dir + "lvl4.txt"), block_size, this);
                        Heroes.Add(new Character(Ressource.Player_animation, new Vector2(0, 0),
                            Ressource.KeyJ1[Ressource.inGameAction.Left], Ressource.KeyJ1[Ressource.inGameAction.Right],
                            Ressource.KeyJ1[Ressource.inGameAction.Jump], Ressource.KeyJ1[Ressource.inGameAction.Shoot], 1));

                        if (Ressource.parameter[3])
                            Heroes.Add(new Character(Ressource.Player_animation, new Vector2(130, 0),
                                Ressource.KeyJ2[Ressource.inGameAction.Left], Ressource.KeyJ2[Ressource.inGameAction.Right],
                                Ressource.KeyJ2[Ressource.inGameAction.Jump], Ressource.KeyJ2[Ressource.inGameAction.Shoot], 2));
                        break;
                    }
                case 5:
                    {
                        int block_size = 50;
                        map.Generate(loadfile.read(dir + "lvl5.txt"), block_size, this);
                        Heroes.Add(new Character(Ressource.Player_animation, new Vector2(0, 0),
                            Ressource.KeyJ1[Ressource.inGameAction.Left], Ressource.KeyJ1[Ressource.inGameAction.Right],
                            Ressource.KeyJ1[Ressource.inGameAction.Jump], Ressource.KeyJ1[Ressource.inGameAction.Shoot], 1));

                        if (Ressource.parameter[3])
                            Heroes.Add(new Character(Ressource.Player_animation, new Vector2(130, 0),
                                Ressource.KeyJ2[Ressource.inGameAction.Left], Ressource.KeyJ2[Ressource.inGameAction.Right],
                                Ressource.KeyJ2[Ressource.inGameAction.Jump], Ressource.KeyJ2[Ressource.inGameAction.Shoot], 2));

                        break;
                    }
                case 6:
                    {
                        int block_size = 50;
                        map.Generate(loadfile.read(dir + "lvl6.txt"), block_size, this);
                        Heroes.Add(new Character(Ressource.Player_animation, new Vector2(1500, 0),
                            Ressource.KeyJ1[Ressource.inGameAction.Left], Ressource.KeyJ1[Ressource.inGameAction.Right],
                            Ressource.KeyJ1[Ressource.inGameAction.Jump], Ressource.KeyJ1[Ressource.inGameAction.Shoot], 1));

                        if (Ressource.parameter[3])
                            Heroes.Add(new Character(Ressource.Player_animation, new Vector2(130, 0),
                                Ressource.KeyJ2[Ressource.inGameAction.Left], Ressource.KeyJ2[Ressource.inGameAction.Right],
                                Ressource.KeyJ2[Ressource.inGameAction.Jump], Ressource.KeyJ2[Ressource.inGameAction.Shoot], 2));

                        break;
                    }
                case 7:
                    {
                        int block_size = 50;
                        map.Generate(loadfile.read(dir + "lvl7.txt"), block_size, this);
                        Heroes.Add(new Character(Ressource.Player_animation, new Vector2(0, 0),
                            Ressource.KeyJ1[Ressource.inGameAction.Left], Ressource.KeyJ1[Ressource.inGameAction.Right],
                            Ressource.KeyJ1[Ressource.inGameAction.Jump], Ressource.KeyJ1[Ressource.inGameAction.Shoot], 1));

                        if (Ressource.parameter[3])
                            Heroes.Add(new Character(Ressource.Player_animation, new Vector2(130, 0),
                                Ressource.KeyJ2[Ressource.inGameAction.Left], Ressource.KeyJ2[Ressource.inGameAction.Right],
                                Ressource.KeyJ2[Ressource.inGameAction.Jump], Ressource.KeyJ2[Ressource.inGameAction.Shoot], 2));

                        break;
                    }
                case 8:
                    {
                        int block_size = 50;
                        map.Generate(loadfile.read(dir + "lvl8.txt"), block_size, this);
                        Heroes.Add(new Character(Ressource.Player_animation, new Vector2(0, 0),
                            Ressource.KeyJ1[Ressource.inGameAction.Left], Ressource.KeyJ1[Ressource.inGameAction.Right],
                            Ressource.KeyJ1[Ressource.inGameAction.Jump], Ressource.KeyJ1[Ressource.inGameAction.Shoot], 1));

                        if (Ressource.parameter[3])
                            Heroes.Add(new Character(Ressource.Player_animation, new Vector2(130, 0),
                                Ressource.KeyJ2[Ressource.inGameAction.Left], Ressource.KeyJ2[Ressource.inGameAction.Right],
                                Ressource.KeyJ2[Ressource.inGameAction.Jump], Ressource.KeyJ2[Ressource.inGameAction.Shoot], 2));

                        break;
                    }
                case 9:
                    {
                        int block_size = 50;
                        map.Generate(loadfile.read(dir + "lvl9.txt"), block_size, this);
                        Heroes.Add(new Character(Ressource.Player_animation, new Vector2(0, 0),
                            Ressource.KeyJ1[Ressource.inGameAction.Left], Ressource.KeyJ1[Ressource.inGameAction.Right],
                            Ressource.KeyJ1[Ressource.inGameAction.Jump], Ressource.KeyJ1[Ressource.inGameAction.Shoot], 1));

                        if (Ressource.parameter[3])
                            Heroes.Add(new Character(Ressource.Player_animation, new Vector2(130, 0),
                                Ressource.KeyJ2[Ressource.inGameAction.Left], Ressource.KeyJ2[Ressource.inGameAction.Right],
                                Ressource.KeyJ2[Ressource.inGameAction.Jump], Ressource.KeyJ2[Ressource.inGameAction.Shoot], 2));

                        break;
                    }
                case 10:
                    {
                        int block_size = 50;
                        map.Generate(loadfile.read(dir + "lvl10.txt"), block_size, this);
                        Heroes.Add(new Character(Ressource.Player_animation, new Vector2(0, 0),
                            Ressource.KeyJ1[Ressource.inGameAction.Left], Ressource.KeyJ1[Ressource.inGameAction.Right],
                            Ressource.KeyJ1[Ressource.inGameAction.Jump], Ressource.KeyJ1[Ressource.inGameAction.Shoot], 1));

                        if (Ressource.parameter[3])
                            Heroes.Add(new Character(Ressource.Player_animation, new Vector2(130, 0),
                                Ressource.KeyJ2[Ressource.inGameAction.Left], Ressource.KeyJ2[Ressource.inGameAction.Right],
                                Ressource.KeyJ2[Ressource.inGameAction.Jump], Ressource.KeyJ2[Ressource.inGameAction.Shoot], 2));
                        break;
                    }
                default:
                    break;
            }
            #endregion
            //TODO
        }

        //METHODS
        public string LvlToStr(bool youlose, bool youwin)
        {
            string s = "";
            #region character
            foreach (Character h in Heroes) //H/nbjoueur/x/y/couleur/sens/colonne/ligne/hauteur/largeur/rectangle_width/rectangle_height/nbBonus/bonusvo/bonusvf/stop/attack/spawn/jump+
            {
                
                s += "H/";
                s += h.player + "/";
                s += (int)h.position.X + "/";
                s += (int)h.position.Y + "/";

                Color c = h.color;

                if (c == Color.White)
                {
                    s += "w/";
                }
                else if (c == Color.Red)
                {
                    s += "r/";
                }
                else if (c == Color.Orange)
                {
                    s += "o/";
                }
                else if (c == Color.Yellow)
                {
                    s += "y/";
                }
                else if (c == Color.LimeGreen)
                {
                    s += "g/";
                }
                else if (c == Color.LightBlue)
                {
                    s += "b/";
                }
                else if (c == Color.Pink)
                {
                    s += "p/";
                }
                else
                {
                    s += "w/";
                }


                if (h.Direction == Direction.Left)
                {
                    s += "l/";
                }
                else
                {
                    s += "r/";
                }


                s += h.frameCollumn + "/" + h.frameRow + "/" + h.player_Height + "/" + h.player_Width + "/" + h.rectangle.Width + "/" + h.rectangle.Height + "/";


                int nbBonus = -1;
                Tuple<string, string> tuple = new Tuple<string, string>("", "");
                switch (h.CurrentItem)
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

                s += nbBonus + "/" + tuple.Item1 + "/" + tuple.Item2 + "/" + h.stop + "/" + h.attack + "/" + h.spawn + "/" + h.jump + "+";
              

                #region bullet
                foreach (Bullet b in h.Bullets) //B/x/y/couleur/sens/colonne+
                {
                    s += "B/";

                    s += (int)b.position.X + "/";
                    s += (int)b.position.Y + "/";

                    if (h.bulletColor == Color.White)
                    {
                        s += "w/";
                    }
                    else
                    {
                        s += "g/";
                    }


                    if (b.effect_stable == SpriteEffects.None)
                    {
                        s += "r/";
                    }
                    else
                    {
                        s += "l/";
                    }

                    s += b.frameCollumn + "+";
                }
                #endregion
            }
  #endregion

            #region ennemi1
            foreach (Ennemy1 e in Ennemies1) //E1/x/y/height/width/colonne/ligne/sens
            {
                s += "E1/" + (int)e.position.X + "/" + (int)e.position.Y + "/" + e.height + "/" + e.width + "/" + e.frameCollumn + "/" + e.frameRow + "/";
                if (e.Effect == SpriteEffects.None)
                {
                    s += "r+";
                }
                else
                {
                    s += "l+";
                }
            }
            #endregion
            #region ennemi2
            foreach (Ennemy2 e in Ennemies2) //E2/x/y/height/width/colonne/ligne/sens
            {
                s += "E2/" + (int)e.position.X + "/" + (int)e.position.Y + "/" + e.height + "/" + e.width + "/" + e.frameCollumn + "/" + e.frameRow + "/" + e.fixYwidth + "/";
                if (e.Effect == SpriteEffects.None)
                {
                    s += "r+";
                }
                else
                {
                    s += "l+";
                }
            }
            #endregion
            #region ennemi3
            foreach (Ennemy1 e in Ennemies1) //E3/x/y/sens/colonne/ligne
            {
                s += "E3/" + (int)e.position.X + "/" + (int)e.position.Y + "/" + e.height + "/" + e.width + "/" + e.frameCollumn + "/" + e.frameRow + "/";
                if (e.Effect == SpriteEffects.None)
                {
                    s += "r+";
                }
                else
                {
                    s += "l+";
                }
            }
            #endregion

            #region powerup
            int j = 0;
            foreach (Item i in Items) //I/i/hasbeentaken+
            {
                s += "I/" + j + "/";
                j++;
                if (i.hasBeenTaken)
                {
                    s += "0+";
                }
                else
                {
                    s += "1+";
                }
            }
            #endregion
            #region coins
            foreach (Bonus b in Bonuses) //C/hasbeentaken/x/y+
            {
                s += "C/";
                if (b.hasBeenTaken)
                {
                    s += "0/";
                }
                else
                {
                    s += "1/";
                }
                s += (int)b.position.X + "/" + (int)b.position.Y + "+";
            }
            #endregion

            #region hud 
            //S/score/timer/vieJ1/vieJ2/youloose/youwin/ingame+
            s += "S/" + Game1.score.score + "/" + Game1.score.timer+ "/" + Game1.score.rectangle_life.Width + "/" + Game1.score.rectangle_life2.Width + "/";
            if (youlose)
            {
                s += "1/";
            }
            else
            {
                s += "0/";
            }

            if (youwin)
            {
                s += "1/";
            }
            else
            {
                s += "0/";
            }

            if (Game1.inGame)
            {
                s += "1+";
            }
            else
            {
                s += "0+";
            }
            #endregion

            //TODO plateforme mouvante/destructible
            return s;
        }

        #region StrToLvl Helper
        public List<string> Split(string s)
        {
            List<string> splitted = new List<string>();
            string s2 = "";
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == '+')
                {
                    splitted.Add(s2);
                    s2 = "";
                }
                else
                {
                    s2 += s[i];
                }
            }
            return splitted;
        }

        public string ToNextSlash(string s, ref int i)
        {
            string s2 = "";
            for (int j = i; j < s.Length; j++)
            {
                if (s[j] != '/' && s[j] != '+')
                {
                    s2 += s[j];
                }
                else
                {
                    i = j+1;
                    return s2;
                }
            }
            return s2;
        }
        #endregion

        public string KeyboardToStr()
        {
            KeyboardState k = Keyboard.GetState();
            string s = "";
            //K/left/right/jump/shoot
            s += "K/";

            if (k.IsKeyDown(Ressource.KeyJ1[Ressource.inGameAction.Left]))
            {
                s += "1/";
            }
            else
            {
                s += "0/";
            }

            if (k.IsKeyDown(Ressource.KeyJ1[Ressource.inGameAction.Right]))
            {
                s += "1/";
            }
            else
            {
                s += "0/";
            }

            if (k.IsKeyDown(Ressource.KeyJ1[Ressource.inGameAction.Jump]))
            {
                s += "1/";
            }
            else
            {
                s += "0/";
            }

            if (k.IsKeyDown(Ressource.KeyJ1[Ressource.inGameAction.Shoot]))
            {
                s += "1+";
            }
            else
            {
                s += "0+";
            }
            return s;
        }
        public bool[] StrToKeyboard(string s)
        {
            bool[] k = new bool[5];

            if (s[0] == 'K')
            {
                int i = 2;
                int x = Convert.ToInt32(ToNextSlash(s, ref i));
                int y = Convert.ToInt32(ToNextSlash(s, ref i));
                int z = Convert.ToInt32(ToNextSlash(s, ref i));
                int a = Convert.ToInt32(ToNextSlash(s, ref i));
                k[0] = true;
                k[1] = x == 1;
                k[2] = y == 1;
                k[3] = z == 1;
                k[4] = a == 1;
            }
            else
            {
                k[0] = false;
            }
            return k;
        
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


            foreach (Character c in Heroes)
            {
                if (c.player == 1 && updateHero)
                    c.Update(gameTime, effect);
                else if (c.player == 2 && updateHero2)
                    c.Update(gameTime, effect, Ressource.keybord_multi_j2);


            }

            foreach (CollisionTiles tile in map.CollisionTiles)
            {
                foreach (Character c in Heroes)
                {
                    c.Collision(tile.Rectangle, map.Width, map.Height, Ressource.effect2, tile.Tile_name);
                }

                foreach (Character c in Heroes)
                {
                    foreach (Bullet b in c.Bullets)
                    {
                        b.Update(tile, c);
                    }
                }
                foreach (Ennemy3 e in Ennemies3)
                {
                    e.Patrol(tile.Rectangle, tile.Tile_name);
                    e.Collision(tile.Rectangle, tile.Tile_name);
                    foreach (Character player in Heroes)
                    {
                        e.hit(player);
                    }
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

                foreach (Character c in Heroes)
                {
                    c.Collision(item.hitbox, map.Width, map.Height, Ressource.effect2, "Tile6");
                }

                foreach (Character c in Heroes)
                {
                    foreach (Bullet b in c.Bullets)
                    {
                        b.Update(item, c);
                        item.update(b);
                    }
                }

                foreach (Ennemy1 e in Ennemies1)
                {
                    e.Collision(item.hitbox, "Tile5");
                }
            }

            foreach (moving_platform item in moving_platform)
            {

                foreach (Character c in Heroes)
                {
                    c.Collision(item.hitbox, map.Width, map.Height, Ressource.effect2, "Tile5");
                }

                foreach (Character c in Heroes)
                {
                    foreach (Bullet b in c.Bullets)
                    {
                        b.Update(item, c);
                    }
                }
                foreach (Ennemy1 e in Ennemies1)
                {
                    e.Collision(item.hitbox, "Tile5");
                }
                item.update();
            }

            foreach (Bonus gold in Bonuses)
            {
                foreach (Character c in Heroes)
                {
                    gold.Update(c, Game1.score);
                }

            }

            foreach (Item i in Items)
            {
                foreach (Character c in Heroes)
                {
                    i.Update(c, Game1.score);
                }
            }

            //Updates Ennemies
            foreach (Ennemy2 e in Ennemies2)
            {
                Character Nearest = Interaction.NearCharacter(e, Heroes);

                e.Update(gameTime);

                e.updateDirection(e.direction, Nearest);
                e.hit(Nearest);

                if (e.firstHit)
                {
                    if (e.player1)
                        Heroes.ElementAt(0).velocity.X = 0;
                    else if (e.player2)
                        Heroes.ElementAt(1).velocity.X = 0;

                    e.timerAttack = 0;

                    if (e.direction == Direction2.Right)
                    {
                        if (e.player1)
                            Heroes.ElementAt(0).velocity.X = 5f;
                        else if (e.player2)
                            Heroes.ElementAt(1).velocity.X = 5f;
                    }
                    else if (e.direction == Direction2.Left)
                    {
                        if (e.player1)
                            Heroes.ElementAt(0).velocity.X = -5f;
                        else if (e.player2)
                            Heroes.ElementAt(1).velocity.X = -5f;
                    }
                }

                foreach (Character c in Heroes)
                {
                    foreach (Bullet b in c.Bullets)
                    {
                        if (b.hitbox_bullet.Intersects(e.rectangle))
                        {
                            b.Update(e, c);

                        }
                    }
                }
            }

            foreach (Ennemy3 e in Ennemies3)
            {

                if (e.hasHit)
                    updateHero = false;
                if (e.hasHit2)
                    updateHero2 = false;


                e.Update(gameTime);

                foreach (Character c in Heroes)
                {
                    foreach (Bullet b in c.Bullets)
                    {
                        if (b.hitbox_bullet.Intersects(e.rectangle))
                            b.Update(e, c);
                    }
                }


                if (!updateHero)
                {
                    if (timerEnd3 == timerspeedAttack3)
                        Heroes[0].life_changment = -1;

                    else
                        timerEnd3++;
                }

                if (!updateHero2)
                {
                    if (timerEnd3 == timerspeedAttack3)
                        Heroes[1].life_changment = -1;

                    else
                        timerEnd3++;
                }
            }




            foreach (Ennemy1 e in Ennemies1)
            {
                Character Nearest = Interaction.NearCharacter(e, Heroes);
                e.Update(gameTime, Nearest);
                foreach (Character c in Heroes)
                {
                    foreach (Bullet b in c.Bullets)
                    {
                        b.Update(e, c);
                    }
                }
            }

            for (int i = 0; i < Ennemies1.Count; i++)
            {
                if (Ennemies1[i].life <= 0)
                {
                    Bonuses.Add(new gold(Ennemies1[i].position));
                    Ennemies1.RemoveAt(i);
                }

            }
            for (int i = 0; i < Ennemies2.Count; i++)
            {
                if (Ennemies2[i].life <= 0)
                {
                    Bonuses.Add(new gold(Ennemies2[i].position));
                    Ennemies2.RemoveAt(i);
                }
            }
            for (int i = 0; i < Ennemies3.Count; i++)
            {
                if (Ennemies3[i].life <= 0)
                {
                    Bonuses.Add(new gold(Ennemies3[i].position));
                    Ennemies3.RemoveAt(i);
                }
            }

            /*if (Hud.youwin)
            {
                loadfile.Save((score.score) + score.timer * 10, Ressource.pseudo);
                try
                {
                    Web.send_request(Ressource.pseudo, score.score, lvl);
                }
                catch (Exception)
                {

                }
            }*/

        }


        public void Draw(SpriteBatch spriteBatch)
        {

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

            foreach (Item i in Items)
            {
                i.Draw(spriteBatch);
            }


            foreach (Character c in Heroes)
            {
                if (c.player == 1 && updateHero)
                    c.Draw(spriteBatch);
                else if (c.player == 2 && updateHero2)
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

        public void Draw(SpriteBatch spriteBatch, string s)
        {
            #region base
            map.Draw(spriteBatch);

            foreach (Destroying_platform item in destroy_platform)
            {
                item.Draw(spriteBatch);
            }

            foreach (moving_platform item in moving_platform)
            {
                item.Draw(spriteBatch);
            }

            List<Bonus> ToDel = new List<Bonus>();
            foreach (Bonus c in Bonuses)
            {
                if (c.type == Bonus.Type.Gold)
                {
                    ToDel.Add(c);
                }
            }
            foreach (Bonus c in ToDel)
            {
                Bonuses.Remove(c);
            }
            #endregion

            List<string> splitted = Split(s);

            foreach (string a in splitted)
            {
                #region Character
                if (a[0] == 'H') //H/nbjoueur/x/y/couleur/sens/colonne/ligne/hauteur/largeur/rectangle_width/rectangle_height/nbBonus/bonusvo/bonusvf/stop/attack/spawn/jump+
                {
                    int i = 2;
                    string b = ToNextSlash(a, ref i);
                    int nbPlayer = Convert.ToInt32(b);
                    int x = Convert.ToInt32(ToNextSlash(a, ref i));
                    int y = Convert.ToInt32(ToNextSlash(a, ref i));
                    b = ToNextSlash(a, ref i);

                    Color color = Color.White;
                    switch (b[0])
                    {
                        case 'r':
                            color = Color.Red;
                            break;
                        case 'o':
                            color = Color.Orange;
                            break;
                        case 'y':
                            color = Color.Yellow;
                            break;
                        case 'g':
                            color = Color.LimeGreen;
                            break;
                        case 'b':
                            color = Color.LightBlue;
                            break;
                        case 'p':
                            color = Color.Pink;
                            break;
                        default:
                            break;
                    }
                    b = ToNextSlash(a, ref i);
                    SpriteEffects effect = SpriteEffects.None;
                    if (b[0] == 'l')
                    {
                        effect = SpriteEffects.FlipHorizontally;
                    }

                    int frameCollumn = Convert.ToInt32(ToNextSlash(a, ref i));

                    
                    //Console.WriteLine(frameCollumn);

                    int frameRow = Convert.ToInt32(ToNextSlash(a, ref i));
                    int player_Height = Convert.ToInt32(ToNextSlash(a, ref i));
                    int player_Width = Convert.ToInt32(ToNextSlash(a, ref i));

                    int rectangle_width = Convert.ToInt32(ToNextSlash(a, ref i));
                    int rectangle_height = Convert.ToInt32(ToNextSlash(a, ref i));

                    Vector2 position = new Vector2(x,y);
                    Ressource.position_j2_multi = position;

                    int nbBonus = Convert.ToInt32(ToNextSlash(a, ref i));
                    string BonusNameEn = ToNextSlash(a, ref i);
                    string BonusNameFr = ToNextSlash(a, ref i);

                    bool stop = Convert.ToBoolean(ToNextSlash(a, ref i));
                    bool attack = Convert.ToBoolean(ToNextSlash(a, ref i));
                    bool spawn = Convert.ToBoolean(ToNextSlash(a, ref i));
                    bool jump = Convert.ToBoolean(ToNextSlash(a, ref i));

                    #region draw_multi
                    /*if (spawn)
                    {
                        frameRow = 0;
                        spriteBatch.Draw(Ressource.Player_animation, new Rectangle(x, y, rectangle_width, rectangle_height), new Rectangle((frameCollumn - 1) * player_Width, 0, player_Width, player_Height), color, 0f, new Vector2(0, 0), effect, 0f);
                    }
                    else if (stop)
                    {
                        if (attack)
                        {
                            player_Height = 43;
                            player_Width = 51;
                            frameRow = 64 + 41 + 43;
                            spriteBatch.Draw(Ressource.Player_animation, new Rectangle(x, y, rectangle_width, rectangle_height), new Rectangle(player_Width, 64 + 41 + 43, player_Width, player_Height), color, 0f, new Vector2(0, 0), effect, 0f);
                        }
                        else
                        {
                            player_Height = 41;
                            player_Width = 32;
                            frameRow = 64;
                            spriteBatch.Draw(Ressource.Player_animation, new Rectangle(x, y, rectangle_width, rectangle_height), new Rectangle((frameCollumn - 1) * player_Width, 64, player_Width, player_Height), color, 0f, new Vector2(0, 0), effect, 0f);
                        }
                    }
                    else if (jump)
                    {

                        if (attack)
                        {
                            frameRow = 64 + 41 + 43 + 43 + 55;
                            spriteBatch.Draw(Ressource.Player_animation, new Rectangle(x, y, rectangle_width, rectangle_height), new Rectangle((frameCollumn - 1) * player_Width, 64 + 41 + 43 + 43 + 55, player_Width, player_Height), color, 0f, new Vector2(0, 0), effect, 0f);
                        }
                        else
                        {
                            frameRow = 64 + 41 + 43 + 43 + 1;
                            spriteBatch.Draw(Ressource.Player_animation, new Rectangle(x, y, rectangle_width, rectangle_height), new Rectangle((frameCollumn - 1) * player_Width, 64 + 41 + 43 + 43 + 1, player_Width, player_Height - 1), color, 0f, new Vector2(0, 0), effect, 0f);
                        }
                    }
                    else
                    {
                        if (attack)
                        {
                            frameRow = 64 + 41 + 43;
                            spriteBatch.Draw(Ressource.Player_animation, new Rectangle(x, y, rectangle_width, rectangle_height), new Rectangle((frameCollumn - 1) * player_Width, 64 + 41 + 43, player_Width, player_Height), color, 0f, new Vector2(0, 0), effect, 0f);
                        }
                        else
                        {
                            frameRow = 64 + 41;
                            spriteBatch.Draw(Ressource.Player_animation, new Rectangle(x, y, rectangle_width, rectangle_height), new Rectangle((frameCollumn - 1) * player_Width, 64 + 41, player_Width, player_Height), color, 0f, new Vector2(0, 0), effect, 0f);
                        }
                    }*/
                    #endregion

                    spriteBatch.Draw(Ressource.Player_animation, new Rectangle(x, y, rectangle_width, rectangle_height),
                        new Rectangle((frameCollumn - 1) * player_Width, frameRow, player_Width, player_Height), color, 0f, new Vector2(0, 0), effect, 0f);
                    particule.particleEffects["Snow"].Trigger(new Vector2(position.X + Camera.Transform.Translation.X, 0));

                    if (nbPlayer == 1)
                    {
                        if (nbBonus != -1)
                        {
                            string nomBonus = "";
                            if (Ressource.parameter[2])
                            {
                                nomBonus = BonusNameEn;
                            }
                            else
                            {
                                nomBonus = BonusNameFr;
                            }
                        
                        Game1.score.nbBonus = nbBonus;
                        Game1.score.nomBonus = nomBonus;
                        }
                    }
                    else
                    {
                        if (nbBonus != -1)
                        {
                            string nomBonus = "";
                            if (Ressource.parameter[2])
                            {
                                nomBonus = BonusNameEn;
                            }
                            else
                            {
                                nomBonus = BonusNameFr;
                            }
                            Game1.score.nbBonus2 = nbBonus;
                            Game1.score.nomBonus2 = nomBonus;
                        }
                    }

                }
                #endregion
                #region Bullet
                //B/x/y/couleur/sens/colonne+
                if (a[0] == 'B')
                {
                    int i = 2;
                    int x = Convert.ToInt32(ToNextSlash(a, ref i));
                    int y = Convert.ToInt32(ToNextSlash(a, ref i));
                    Vector2 position = new Vector2(x, y);
                    string b = ToNextSlash(a, ref i);
                    Color color = Color.White;
                    if (b[0] == 'g')
                    {
                        color = Color.Yellow;
                    }
                    b = ToNextSlash(a, ref i);
                    SpriteEffects effect = SpriteEffects.None;
                    if (b[0] == 'l')
                    {
                        effect = SpriteEffects.FlipHorizontally;
                    }
                    int collumn = Convert.ToInt32(ToNextSlash(a, ref i));
                    Texture2D t = Ressource.Bullet;
                    spriteBatch.Draw(t, new Rectangle((int)position.X, (int)position.Y, 55, 62), new Rectangle((collumn - 1) * 55, 0, 55, 62), color, 0f, new Vector2(0, 0), effect, 0f);
                }
                #endregion
                #region Ennemis 
                if (a[0] == 'E')
                {
                    if (a[1] == '1') //E1/x/y/height/width/colonne/ligne/sens
                    {
                        int i = 3;
                        int x = Convert.ToInt32(ToNextSlash(a, ref i));
                        int y = Convert.ToInt32(ToNextSlash(a, ref i));
                        int height = Convert.ToInt32(ToNextSlash(a, ref i));
                        int width = Convert.ToInt32(ToNextSlash(a, ref i));
                        int frameCollumn = Convert.ToInt32(ToNextSlash(a, ref i));
                        int frameRow = Convert.ToInt32(ToNextSlash(a, ref i));
                        string b  = ToNextSlash(a, ref i);
                        SpriteEffects effect = SpriteEffects.None;
                        if (b[0] == 'l')
                        {
                            effect = SpriteEffects.FlipHorizontally;
                        }
                        spriteBatch.Draw(Ressource.Ennemy1, new Rectangle(x,y,width,height), new Rectangle((frameCollumn - 1) * width, frameRow, width, height), Color.White, 0f, new Vector2(0, 0), effect, 0f);
                        
                    }
                    if (a[1] == '2') //E2/x/y/height/width/colonne/ligne/fixwidth/sens
                    {
                        int i = 3;
                        int x = Convert.ToInt32(ToNextSlash(a, ref i));
                        int y = Convert.ToInt32(ToNextSlash(a, ref i));
                        int height = Convert.ToInt32(ToNextSlash(a, ref i));
                        int width = Convert.ToInt32(ToNextSlash(a, ref i));

              

                        int frameCollumn = Convert.ToInt32(ToNextSlash(a, ref i));
                        int frameRow = Convert.ToInt32(ToNextSlash(a, ref i));
                        int fixwidth = Convert.ToInt32(ToNextSlash(a, ref i));
                        string b = ToNextSlash(a, ref i);
                        SpriteEffects effect = SpriteEffects.None;
                        if (b[0] == 'l')
                        {
                            effect = SpriteEffects.FlipHorizontally;
                        }
                        Rectangle r = new Rectangle(x,y+fixwidth,width,height);
                        spriteBatch.Draw(Ressource.Ennemy2, r, new Rectangle((frameCollumn - 1) * width, frameRow, width, height), Color.White, 0f, new Vector2(0, 0),effect, 0f);

                    }
                    if (a[1] == '3') //E3/x/y/height/width/colonne/ligne/sens
                    {
                        int i = 3;
                        int x = Convert.ToInt32(ToNextSlash(a, ref i));
                        int y = Convert.ToInt32(ToNextSlash(a, ref i));
                        int height = Convert.ToInt32(ToNextSlash(a, ref i));
                        int width = Convert.ToInt32(ToNextSlash(a, ref i));
                        int frameCollumn = Convert.ToInt32(ToNextSlash(a, ref i));
                        int frameRow = Convert.ToInt32(ToNextSlash(a, ref i));
                        string b = ToNextSlash(a, ref i);
                        SpriteEffects effect = SpriteEffects.None;
                        if (b[0] == 'l')
                        {
                            effect = SpriteEffects.FlipHorizontally;
                        }
                        spriteBatch.Draw(Ressource.Ennemy3, new Rectangle(x, y+5, width, height), new Rectangle((frameCollumn - 1) * width, frameRow, width, height), Color.White, 0f, new Vector2(0, 0), effect, 0f);

                    }
                }
                #endregion
                #region powerup & coins
                if (a[0] == 'I')
                {
                    int i = 2;
                    int x = Convert.ToInt32(ToNextSlash(a, ref i));
                    int y = Convert.ToInt32(ToNextSlash(a, ref i));
                    Items[x].hasBeenTaken = (y != 1);
                }
                if (a[0] == 'C')
                {
                    int i = 2;
                    int hbt = Convert.ToInt32(ToNextSlash(a, ref i));
                    int x = Convert.ToInt32(ToNextSlash(a, ref i));
                    int y = Convert.ToInt32(ToNextSlash(a, ref i));
                    
                    Bonuses.Add(new Bonus(new Vector2(x,y), Ressource.Gold, Bonus.Type.Gold));
                    Bonuses[Bonuses.Count - 1].hasBeenTaken = (hbt != 1);
                }

                #endregion
                #region hud
                //S/score/timer/vieJ1/vieJ2/youloose/youwin/ingame+
                if (a[0] == 'S')
                {
                    int i = 2;
                    int score = Convert.ToInt32(ToNextSlash(a, ref i));
                    int timer = Convert.ToInt32(ToNextSlash(a, ref i));
                    int vieJ1 = Convert.ToInt32(ToNextSlash(a, ref i));
                    int vieJ2 = Convert.ToInt32(ToNextSlash(a, ref i));

                    string b = ToNextSlash(a, ref i);
                    if (b[0] == '1')
                    {
                        Game1.inGame = false;
                        Game1.menu = Game1.menu.ChangeMenu(Menu.MenuType.loose);
                    }

                    b = ToNextSlash(a, ref i);
                    if (b[0] == '1')
                    {
                        Game1.inGame = false;
                        Game1.menu = Game1.menu.ChangeMenu(Menu.MenuType.win);
                    }

                    b = ToNextSlash(a, ref i);
                    if (b[0] == '0')
                    {
                        Game1.inGame = false;
                    }

                    Vector2 position_life = new Vector2(12, 70);
                    Rectangle rectangle_life = new Rectangle(0, 0, vieJ1, Ressource.healthbar.Height);
                    Rectangle rectangle_life2 = new Rectangle(0, 0, vieJ2, Ressource.healthbar.Height);


                    Game1.score.score = score;
                    Game1.score.timer = timer;
                    Game1.score.rectangle_life = rectangle_life;
                    Game1.score.rectangle_life2 = rectangle_life2;
                    


                }
                #endregion
            }

            #region base2
            foreach (Bonus gold in Bonuses)
            {
                gold.Draw(spriteBatch);
            }

            foreach (Item i in Items)
            {
                i.Draw(spriteBatch);
            }
            #endregion



        }
    }

}