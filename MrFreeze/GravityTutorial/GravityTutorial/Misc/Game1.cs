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
        public static Level Level;
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
        int compteur;

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

            Level Level = new Level(0);
            VidPlayer = new VideoPlayer();
            menu = new Menu(Menu.MenuType.none, 0, Ressource.BackgroundMenuMain);
            inGame = false;
            reload = false;

            cooldown_reseau = false;
            compteur = 0;
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
        {        }


        protected override void Update(GameTime gameTime)
        {
            if (Ressource.parameter[5] && !reseau)
            {
                reseau = true;
                client = new Client();
                client.Connect("localhost", 4242, Ressource.pseudo);
            }
            if (Ressource.parameter[4] && !reseau)
            {
                server = new Server(4242);
                reseau = true;
                server.Run();
            }
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
            
            if (exitgame)
                this.Exit();

            if (reload)
            {
                reload = false;
                if (Level != null)
                {
                    Level = new Level(Level.lvl);
                }
                Hud.youlose = false;
                Hud.youwin = false;
                score = new Hud(new TimeSpan(0, 0, 80), new Vector2(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height));
                score.rectangle_life.Width = 150;
            }

            if (inGame)
            {
                
                if (Ressource.parameter[5])
                {
                        compteur = 0;
                        client.Write();
                        client.Read();
                }
                if (Ressource.parameter[4])
                {
                    server.Chat();
                }
                
                if (!Ressource.parameter[5])
                {
                    Level.Update(gameTime, Ressource.effect2, score);
                }

                if (Ressource.parameter[3])
                {
                    score.Update(Level.Heroes[0], Level.Heroes[1]);
                }
                else
                {
                    score.Update(Level.Heroes[0]);
                }

                if (!Ressource.parameter[5])
                {
                    camera.update(Level.Heroes.ElementAt(0).position, Level.map.Width, Level.map.Height);
                }
                else
                {
                    camera.update(Ressource.position_j2_multi, Level.map.Width, Level.map.Height);
                }
                particule.Update(gameTime);
            }
            else
            {
                menu.Update(Mouse.GetState(), Keyboard.GetState(), ref menu, sizeChanged);
            }

            if (VidPlayer.State == MediaState.Stopped)
            {
                vidHasBeenPlayed = true;
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
                    spriteBatch.Draw(Ressource.background, new Rectangle((int)(Camera.Transform.Translation.X * 0.1f), (int)(Camera.Transform.Translation.Y * 0.1f), /*Level.map.Width*/(int)(Ressource.background.Width * 1.5), /*GraphicsDevice.Viewport.Height*/ (int)(Ressource.background.Height*1.5)), Color.White);

                    spriteBatch.End();

                    spriteBatch.Begin(SpriteSortMode.Deferred,
                            BlendState.AlphaBlend,
                            null, null, null, null,
                            Camera.Transform);
                    if (!Ressource.parameter[5])
                    {
                        Level.Draw(spriteBatch);
                    }
                    else
                    {
                        Level.Draw(spriteBatch, Ressource.level_multi_j1);
                    }
                    spriteBatch.End();

                    particule.Draw();

                    // HUD
                    spriteBatch.Begin();
                    if (!Ressource.parameter[5])
                    {
                        score.Draw(spriteBatch);
                    }
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