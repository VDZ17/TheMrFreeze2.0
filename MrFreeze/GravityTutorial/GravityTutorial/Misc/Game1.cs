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

namespace GravityTutorial
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        // GENERAL DATA
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public static Renderer particleRenderer;
        public static ParticleEffect particleEffect; 

        public int height_size;
        public int width_size;


        //LEVEL
        public static Level Level;
        public static Hud score;

        //VIEWPORT & Map
        Camera camera;

        //VIDEO
        VideoPlayer VidPlayer;
        Rectangle vidRectangle;
        bool vidHasBeenPlayed = false;

        //MENU
        public static Boolean exitgame = false;
        public static Menu menu;
        public static bool inGame;

        //CONSTRUCTOR
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            //this.graphics.IsFullScreen = true;
            this.Window.AllowUserResizing = true;
            this.Window.Title = "MrFreeze";

            particleRenderer = new SpriteBatchRenderer
            {
                GraphicsDeviceService = graphics
            };

            particleEffect = new ParticleEffect(); 

        }

        protected override void Initialize()
        {
            Level Level = new Level(0);

            VidPlayer = new VideoPlayer();

            

            menu = new Menu(Menu.MenuType.none, 0, Ressource.BackgroundMenuMain);
            inGame = false;

            base.Initialize();
        }


        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Ressource.LoadContent(Content);

            score = new Hud(new TimeSpan(0, 0, 50), new Vector2(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height));

            //particules
            particleRenderer.LoadContent(Content);
            particleEffect = Ressource.BeamMeUp;
            particleEffect.LoadContent(Content);
            particleEffect.Initialise(); 

            // VIDEO DISPLAY

            vidRectangle = new Rectangle(GraphicsDevice.Viewport.X, GraphicsDevice.Viewport.Y,
                GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            if (!vidHasBeenPlayed)
            {
                VidPlayer.Play(Ressource.vid);
                vidHasBeenPlayed = true;
            }


            //MENU GENERATION
            menu = menu.ChangeMenu(Menu.MenuType.welcome);


            // MAP GENERARATION
            Tile.Content = Content;
            camera = new Camera(GraphicsDevice.Viewport);

            if (Ressource.parameter[0])
            {
                MediaPlayer.Play(Ressource.song);
                MediaPlayer.Volume = 0.1f;
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
            bool sizeChanged = false;
            if (Ressource.screenWidth != GraphicsDevice.Viewport.Width || Ressource.screenHeight != GraphicsDevice.Viewport.Height)
            {
                Ressource.screenWidth = GraphicsDevice.Viewport.Width;
                Ressource.screenHeight = GraphicsDevice.Viewport.Height;
                sizeChanged = true;
            }

            //if (Keyboard.GetState().IsKeyDown(Keys.Enter)) Exit();
            if (exitgame)
                this.Exit();

            if (inGame)
            {
                Level.Update(gameTime, Ressource.effect2);
                score.Update(Level.Heroes[0]);
                camera.update(Level.Heroes.ElementAt(0).position, Level.map.Width, Level.map.Height);
                foreach (particule p in particule.particules_list)
                {
                    p.Update(gameTime);
                }
            }
            else
            {
                menu.Update(Mouse.GetState(), Keyboard.GetState(), ref menu, sizeChanged);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //PROCESS
            /* VIDEO A FIX
                    spriteBatch.Begin();
                    spriteBatch.Draw(VidPlayer.GetTexture(), vidRectangle, Color.White);*/
            if (inGame || menu.actualType == Menu.MenuType.pause || menu.actualType == Menu.MenuType.loose || menu.actualType == Menu.MenuType.win)
            {
                
                spriteBatch.Begin(SpriteSortMode.Deferred,
                        BlendState.AlphaBlend,
                        null, null, null, null,
                        camera.Transform);
                Level.Draw(spriteBatch);
                spriteBatch.End();

                foreach (particule item in particule.particules_list)
                {
                    item.Draw(camera.Transform);
                }

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

            base.Draw(gameTime);
        }
    }
}