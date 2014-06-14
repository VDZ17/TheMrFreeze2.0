using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using ProjectMercury.Emitters;
using ProjectMercury.Modifiers;
using ProjectMercury.Renderers;
using ProjectMercury;

using Effects;

namespace GravityTutorial
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        // GENERAL DATA
        public static GraphicsDeviceManager graphics_particle;
        public SpriteBatch spriteBatch;
        public GraphicsDeviceManager graphics;
        public Server server;
        public Client client;

        public int height_size;
        public int width_size;

        //PARTICLE
        particule particule;

        //LEVEL
        public static Level level;
        public static Hud score;

        //VIEWPORT & Map
        Camera camera;
        Matrix data;

        //VIDEO
        VideoPlayer VidPlayer;
        Rectangle vidRectangle;
        bool vidHasBeenPlayed = false;

        //MENU
        public static Boolean exitgame = false;
        public static Menu menu;
        public static bool inGame;
        public static bool reload;
        public static bool reseau;
        public bool cooldown_reseau;

        //CONSTRUCTOR
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics_particle = graphics;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            graphics.ApplyChanges();
            //this.graphics.IsFullScreen = true;
            this.Window.AllowUserResizing = true;
            this.Window.Title = "MrFreeze";
        }

        protected override void Initialize()
        {
            particule = new particule();

            level = new Level(0);
            VidPlayer = new VideoPlayer();
            menu = new Menu(Menu.MenuType.none, 0, Ressource.BackgroundMenuMain);
            inGame = false;
            reload = false;

            cooldown_reseau = false;
            reseau = false;
            base.Initialize();
        }


        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Ressource.LoadContent(Content);


            particule.LoadContent(Content);
            score = new Hud(new TimeSpan(0, 0, 50), new Vector2(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height));


            // VIDEO DISPLAY

            vidRectangle = new Rectangle(GraphicsDevice.Viewport.X, GraphicsDevice.Viewport.Y,
                GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            if (!vidHasBeenPlayed)
            {
                VidPlayer.Play(Ressource.vid);
            }


            //MENU GENERATION
            menu = menu.ChangeMenu(Menu.MenuType.welcome);


            // MAP GENERARATION
            Tile.Content = Content;
            camera = new Camera(GraphicsDevice.Viewport);

            if (Ressource.parameter[0])
            {
                MediaPlayer.Play(Ressource.song);
                MediaPlayer.Volume = 1;
            }
            else
            {
                MediaPlayer.Stop();
            }


        }


        protected override void UnloadContent()
        { }


        protected override void Update(GameTime gameTime)
        {
            #region init reseau

            if (Ressource.parameter[5] && !reseau) // Client
            {
                reseau = true;
                client = new Client();
                client.Connect(Ressource.ipJ2, 4242, Ressource.pseudo);
            }
            if (Ressource.parameter[4] && !reseau) //Host
            {
                reseau = true;
                server = new Server(4242);
                server.Run();
            }
            #endregion

            if (Ressource.parameter[5] && reseau)
            {
                client.Write();
                client.Read();

                #region split msg J1
                if (Ressource.messageFromJ1 != null && Ressource.messageFromJ1 != "" && Ressource.messageFromJ1[0] == 'Z')
                {
                    if (Ressource.messageFromJ1[0] == 'Z')
                    {
                        string part1 = "";
                        string part2 = "";
                        bool isPlusFound = false;
                        for (int i = 0; i < Ressource.messageFromJ1.Length; i++)
                        {
                            if (!isPlusFound)
                            {
                                if (Ressource.messageFromJ1[i] == '+')
                                {
                                    isPlusFound = true;
                                }
                                else
                                {
                                    part1 += Ressource.messageFromJ1[i];
                                }
                            }
                            else
                            {
                                part2 += Ressource.messageFromJ1[i];
                            }
                        }
                        Ressource.messageFromJ1 = part1;
                        Ressource.levelFromJ1Previous = Ressource.levelFromJ1;
                        Ressource.levelFromJ1 = part2;

                    }
                    else
                    {
                        Ressource.levelFromJ1Previous = Ressource.levelFromJ1;
                        Ressource.levelFromJ1 = Ressource.messageFromJ1;
                        Ressource.messageFromJ1 = "";
                    }
                }
                #endregion

                if (Ressource.parameter[5] && reseau && Ressource.messageFromJ1 != null && Ressource.messageFromJ1 != "" && Ressource.messageFromJ1[0] == 'Z')
                {
                    #region
                    int i = 2;
                    string b = Level.ToNextSlash(Ressource.messageFromJ1, ref i);
                    if (b == "newlvl")
                    {
                        int lvl = Convert.ToInt32(Level.ToNextSlash(Ressource.messageFromJ1, ref i));
                        level = new Level(lvl);
                        score = new Hud(new TimeSpan(0, 0, 80), new Vector2(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height));
                        score.rectangle_life.Width = 150;
                        inGame = true;
                    }
                    if (b == "pause")
                    {
                        menu = menu.ChangeMenu(Menu.MenuType.multipause);
                        inGame = false;
                    }
                    if (b == "unpause")
                    {
                        inGame = true;
                    }
                    if (b == "win")
                    {
                        menu = menu.ChangeMenu(Menu.MenuType.multiwin);
                        inGame = false;
                    }
                    if (b == "loose")
                    {
                        menu = menu.ChangeMenu(Menu.MenuType.multiloose);
                        inGame = false;
                    }
                    #endregion
                }
            }

            #region size
            bool sizeChanged = false;
            if (Ressource.screenWidth != GraphicsDevice.Viewport.Width || Ressource.screenHeight != GraphicsDevice.Viewport.Height)
            {
                Ressource.screenWidth = GraphicsDevice.Viewport.Width;
                Ressource.screenHeight = GraphicsDevice.Viewport.Height;
                sizeChanged = true;
            }
            if (sizeChanged)
            {
                camera = new Camera(GraphicsDevice.Viewport);
                vidRectangle = new Rectangle(GraphicsDevice.Viewport.X, GraphicsDevice.Viewport.Y,
                GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            }
            #endregion

            if (exitgame)
                this.Exit();

            #region reload
            if (reload)
            {
                reload = false;
                if (level != null)
                {
                    level = new Level(level.lvl);
                    Ressource.messageJ1toJ2 = "Z/newlvl/" + level.lvl + "+";
                }
                Hud.youlose = false;
                Hud.youwin = false;
                score = new Hud(new TimeSpan(0, 0, 80), new Vector2(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height));
                score.rectangle_life.Width = 150;

            }
            #endregion

            if (inGame)
            {
                #region if ingame

                if (!Ressource.parameter[5])
                {
                    level.Update(gameTime, Ressource.effect2, score);
                }

                if (Ressource.parameter[3])
                {
                    if (!Ressource.parameter[5])
                    {
                        score.Update(level.Heroes[0], level.Heroes[1]);
                    }

                }
                else
                {
                    score.Update(level.Heroes[0]);
                }

                if (!Ressource.parameter[5])
                {
                    camera.update(level.Heroes.ElementAt(0).position, level.map.Width, level.map.Height);
                }
                else
                {
                    camera.update(Ressource.positionFromJ2, level.map.Width, level.map.Height);
                }
                particule.Update(gameTime);
                #endregion
            }
            else
            {
                menu.Update(Mouse.GetState(), Keyboard.GetState(), ref menu, sizeChanged);
            }

            if (VidPlayer.State == MediaState.Stopped)
            {
                vidHasBeenPlayed = true;
            }

            if (Ressource.parameter[4] && reseau)
            {
                server.Chat();
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Blue);
            //PROCESS
            //if (!vidHasBeenPlayed)
            //{
            //    spriteBatch.Begin();
            //    spriteBatch.Draw(VidPlayer.GetTexture(), vidRectangle, Color.White);
            //    spriteBatch.End();
            //}
            //else
            //{
            if (inGame || menu.actualType == Menu.MenuType.pause || menu.actualType == Menu.MenuType.loose || menu.actualType == Menu.MenuType.win)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(Ressource.background, new Rectangle((int)(Camera.Transform.Translation.X * 0.1f), (int)(Camera.Transform.Translation.Y * 0.1f), /*Level.map.Width*/(int)(Ressource.background.Width * 1.5), /*GraphicsDevice.Viewport.Height*/ (int)(Ressource.background.Height * 1.5)), Color.White);

                spriteBatch.End();

                spriteBatch.Begin(SpriteSortMode.Deferred,
                        BlendState.AlphaBlend,
                        null, null, null, null,
                        Camera.Transform);

                if (!Ressource.parameter[5])
                {
                    level.Draw(spriteBatch);
                }
                else
                {
                    if (Ressource.levelFromJ1 == "")
                    {
                        level.Draw(spriteBatch, Ressource.levelFromJ1Previous);
                    }
                    else
                    {
                        level.Draw(spriteBatch, Ressource.levelFromJ1);
                    }

                }

                spriteBatch.End();

                particule.Draw();

                // HUD
                spriteBatch.Begin();
                score.Draw(spriteBatch);
                spriteBatch.End();

            }
            if (!inGame)
            {
                spriteBatch.Begin();
                menu.Draw(spriteBatch, Mouse.GetState());
                spriteBatch.End();
            }
            //}
            base.Draw(gameTime);
        }
    }
}