using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

using ProjectMercury;
using ProjectMercury.Emitters;
using ProjectMercury.Modifiers;
using ProjectMercury.Renderers; 


namespace GravityTutorial
{
    public static class Ressource
    {
        public static Texture2D Player_animation,
            background,
            Button, BackgroundMenuMain, BackgroundMenuPause, Title, TextBox,
            Gold, Items, Loser, igloo, healthbar, Bullet, moving_plateform;

        public static Texture2D Ennemy3, Ennemy2, Ennemy1;

        public static ParticleEffect
            BasicExplosion, Basicfireball, BasicSmokePlume, BeamMeUp, CampFire, FlowerBloom, MagicTrail, Paparazzi, SimpleRain, StarTrail, WaterJet, snow;

        public static SpriteFont Font;
        public static SpriteFont MenuPolice, ArialDefaultMenu;

        public static SoundEffect effect;
        public static Song song;
        public static SoundEffectInstance effect2;

        public static Video vid;

        public static bool[] parameter = new bool[3];

        public enum inGameAction
        {
            Left,
            Right,
            Jump,
            Pause,
            Shoot,
        };

        public static Dictionary<Ressource.inGameAction, Microsoft.Xna.Framework.Input.Keys> Key =
           new Dictionary<Ressource.inGameAction, Microsoft.Xna.Framework.Input.Keys>();

        public static Dictionary<string, Tuple<string, string>> MenuString = new Dictionary<string, Tuple<string, string>>();

        public static int screenHeight, screenWidth;

        public static string pseudo;


        public static void LoadContent(ContentManager Content)
        {
            //FILES
            string MenuFile = "MenuRessources\\";
            //string TileFile = "TileRessources\\";
            string CharacterFile = "CharacterRessources\\";
            string MusicFile = "MusicRessources\\";
            //string MiscFile = "MiscRessources\\";
            string BonusFile = "BonusRessources\\";
            string InGameFile = "InGameRessources\\";
            //string particules
            string particules = "particules\\";
            string EnnemiesFile = "EnnemiesResources\\";

            //PLAYER
            Player_animation = Content.Load<Texture2D>(CharacterFile + "FIXMegaman");
            healthbar = Content.Load<Texture2D>(CharacterFile + "healthbar");

            //BULLET
            Bullet = Content.Load<Texture2D>("shoot");

            // ENEMIES
            Ennemy3 = Content.Load<Texture2D>(EnnemiesFile + "Ennemy3");
            Ennemy2 = Content.Load<Texture2D>(EnnemiesFile + "Ennemy2-2");
            Ennemy1 = Content.Load<Texture2D>(EnnemiesFile + "Ennemy1");


            //GAME
            background = Content.Load<Texture2D>(InGameFile + "back");
            Loser = Content.Load<Texture2D>(InGameFile + "bleucrash");
            igloo = Content.Load<Texture2D>(InGameFile + "Tile10");
            moving_plateform = Content.Load<Texture2D>("Tile5");

            //TOUCHES
            Key.Add(inGameAction.Shoot, Keys.LeftShift);
            Key.Add(inGameAction.Left, Keys.Left);
            Key.Add(inGameAction.Right, Keys.Right);
            Key.Add(inGameAction.Jump, Keys.Space);
            Key.Add(inGameAction.Pause, Keys.Escape);

            //MENU
            Button = Content.Load<Texture2D>(MenuFile + "boutton");
            BackgroundMenuMain = Content.Load<Texture2D>(MenuFile + "backgroundmenu");
            BackgroundMenuPause = Content.Load<Texture2D>(MenuFile + "backgroundmenugris");
            Title = Content.Load<Texture2D>(MenuFile + "title");
            TextBox = Content.Load<Texture2D>(MenuFile + "champtxt");
            MenuPolice = Content.Load<SpriteFont>(MenuFile + "MenuFont");
            ArialDefaultMenu = Content.Load<SpriteFont>(MenuFile + "ArialDefaultMenu");
            pseudo = "USER";

            //MENUSTRING
            MenuString.Add("Retour", new Tuple<string, string>("Retour", "Back"));
            MenuString.Add("Jouer", new Tuple<string, string>("Jouer", "Play"));
            MenuString.Add("Options", new Tuple<string, string>("Options", "Options"));
            MenuString.Add("Quitter", new Tuple<string, string>("Quitter", "Quit"));
            MenuString.Add("Touches", new Tuple<string, string>("Touches", "Keys"));
            MenuString.Add("Droite", new Tuple<string, string>("Droite", "Right"));
            MenuString.Add("Gauche", new Tuple<string, string>("Gauche", "Left"));
            MenuString.Add("Saut", new Tuple<string, string>("Saut", "Jump"));
            MenuString.Add("Pause", new Tuple<string, string>("Pause", "Pause"));
            MenuString.Add("Niveau", new Tuple<string, string>("Niveau", "Level"));
            MenuString.Add("Reprendre", new Tuple<string, string>("Reprendre", "Back to game"));
            MenuString.Add("Aventure", new Tuple<string, string>("Aventure", "Aventure"));
            MenuString.Add("Jeu libre", new Tuple<string, string>("Jeu libre", "Free level"));
            MenuString.Add("Accueil", new Tuple<string, string>("Accueil", "Home"));
            MenuString.Add("Recommencer", new Tuple<string, string>("Recommencer", "Start again"));
            MenuString.Add("Bruitages", new Tuple<string, string>("Bruitage", "Sounds"));
            MenuString.Add("Musique", new Tuple<string, string>("Musique", "Music"));
            MenuString.Add("Par defaut", new Tuple<string, string>("Par defaut", "Default"));
            MenuString.Add("Anglais", new Tuple<string, string>("Anglais", "English"));
            MenuString.Add("Page 1", new Tuple<string, string>("Page 1", "Page 1"));
            MenuString.Add("Page 2", new Tuple<string, string>("Page 2", "Page 2"));
            //MenuString.Add("Pseudo actuel", new Tuple<string, string>("Pseudo actuel : ", "Current pseudo : "));
            MenuString.Add("Pseudo", new Tuple<string, string>("Pseudo", "Pseudo"));
            MenuString.Add("Valider", new Tuple<string, string>("Valider", "Validate"));


            //PARAMETERS
            parameter[0] = false; //Musique
            parameter[1] = false; //Bruitages
            parameter[2] = false; //English version

            //FONT
            Font = Content.Load<SpriteFont>(InGameFile + "Arial");

            //ITEMS
            Gold = Content.Load<Texture2D>(BonusFile + "Tile4");
            //Items = Content.Load<Texture2D>(BonusFile + "items");

            //SOUND 
            effect = Content.Load<SoundEffect>(MusicFile + "SF-course_sable1");
            effect2 = effect.CreateInstance();
            song = Content.Load<Song>(MusicFile + "DRUM&BASS");

            //INTRO
            vid = Content.Load<Video>("vid");

            //PARTICULES
            BasicExplosion= Content.Load<ParticleEffect>((particules + "BasicExplosion"));
            Basicfireball = Content.Load<ParticleEffect>(particules + "BasicFireball");
            BasicSmokePlume = Content.Load<ParticleEffect>(particules + "BasicSmokePlume");
            BeamMeUp = Content.Load<ParticleEffect>(particules + "BeamMeUp");
            CampFire = Content.Load<ParticleEffect>(particules + "CampFire");
            FlowerBloom = Content.Load<ParticleEffect>(particules + "FlowerBloom");
            MagicTrail = Content.Load<ParticleEffect>(particules + "MagicTrail");
            Paparazzi = Content.Load<ParticleEffect>(particules + "Paparazzi");
            SimpleRain = Content.Load<ParticleEffect>(particules + "SimpleRain");
            StarTrail = Content.Load<ParticleEffect>(particules + "StarTrail");
            WaterJet = Content.Load<ParticleEffect>(particules + "WaterJet");
            snow = Content.Load<ParticleEffect>(particules + "snow_lens");


            particule.particleEffects.Add("Snow", snow);
            particule.particleEffects.Add("BasicSmokePlume", BasicSmokePlume);
            particule.particleEffects.Add("BasicExplosion", BasicExplosion);
            particule.particleEffects.Add("BasicFireball", Basicfireball);
            particule.particleEffects.Add("BeamMeUp", BeamMeUp);
            particule.particleEffects.Add("CampFire", CampFire);
            particule.particleEffects.Add("FlowerBloom", FlowerBloom);
            particule.particleEffects.Add("MagicTrail", MagicTrail);
            particule.particleEffects.Add("Paparazzi", Paparazzi);
            particule.particleEffects.Add("StarTrail", StarTrail);
            particule.particleEffects.Add("Rain", SimpleRain);
        }

    }
}
