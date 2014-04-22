using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace GravityTutorial
{
    public class Menu
    {
        //FIELDS
        int nbButton;
        public MenuButton[] Buttons;

        int nbSwitchButton;
        public SwitchButton[] SButtons;

        int nbControleButton;
        public ControleButton[] CButtons;

        public MenuTitle title;
        public Texture2D background;

        public MenuType actualType;

        int nbLevelButton;
        public LevelButton[] LButtons;

        int nbPseudo;
        public Pseudo[] Pseudo;

        public bool cooldown;

        public enum MenuType
        {
            none,
            close,
            welcome,
            play,
            option,
            pause,
            freeplay,
            freeplay2,
            adventure,
            unpause,
            reloadlevel,
            loose,
            win,
            setcontrole,
            defaultcommand,
            setpseudo,
        }

        //CONSTRUCTOR
        public Menu(MenuType type, int nbButton, Texture2D background, int nbSwitchButton = 0, int nbControleButton = 0, int nbLevelButton = 0, int nbPseudo = 0)
        {
            this.nbButton = nbButton;
            Buttons = new MenuButton[nbButton];

            this.actualType = type;

            this.cooldown = true;

            this.background = background;

            this.nbSwitchButton = nbSwitchButton;
            SButtons = new SwitchButton[nbSwitchButton];

            this.nbControleButton = nbControleButton;
            CButtons = new ControleButton[nbControleButton];

            this.nbLevelButton = nbLevelButton;
            LButtons = new LevelButton[nbLevelButton];

            this.nbPseudo = nbPseudo;
            Pseudo = new Pseudo[nbPseudo];
        }
        //METHODS
        public Menu ChangeMenu(MenuType type)
        {
            Menu actualMenu;
            actualMenu = this;

            if (Ressource.screenWidth <= 0)
            {
                Ressource.screenWidth = 800;
            }

            int Xtitle = (Ressource.screenWidth / 2) - 350;
            int Ytitle = 50;

            int Xbutton = (Ressource.screenWidth / 2) - 250;
            int Xbutton0 = (Ressource.screenWidth / 4) - 250;
            int Xbutton1 = (3 * Ressource.screenWidth / 4) - 250; ;
            int Ybutton0 = 300;
            int Ybutton1 = 400;
            int Ybutton2 = 500;
            int Ybutton3 = 600;

            switch (type)
            {
                case MenuType.none:
                    break;
                case MenuType.close:
                    {
                        Game1.exitgame = true;
                        break;
                    }

                case MenuType.welcome:
                    {
                        actualMenu = new Menu(type, 3, Ressource.BackgroundMenuMain);
                        actualMenu.title = new MenuTitle(new Vector2(Xtitle, Ytitle), 0);
                        actualMenu.Buttons[0] = new MenuButton(new Vector2(Xbutton, Ybutton0), Ressource.MenuString["Jouer"], MenuType.play);
                        actualMenu.Buttons[1] = new MenuButton(new Vector2(Xbutton, Ybutton1), Ressource.MenuString["Options"], MenuType.option);
                        actualMenu.Buttons[2] = new MenuButton(new Vector2(Xbutton, Ybutton2), Ressource.MenuString["Quitter"], MenuType.close);
                        break;
                    }
                case MenuType.play:
                    {
                        actualMenu = new Menu(type, 3, Ressource.BackgroundMenuMain);
                        actualMenu.title = new MenuTitle(new Vector2(Xtitle, Ytitle), 1);
                        actualMenu.Buttons[0] = new MenuButton(new Vector2(Xbutton, Ybutton0), Ressource.MenuString["Aventure"], MenuType.adventure);
                        actualMenu.Buttons[1] = new MenuButton(new Vector2(Xbutton, Ybutton1), Ressource.MenuString["Jeu libre"], MenuType.freeplay);
                        actualMenu.Buttons[2] = new MenuButton(new Vector2(Xbutton, Ybutton2), Ressource.MenuString["Retour"], MenuType.welcome);
                        break;
                    }
                case MenuType.option:
                    {
                        actualMenu = new Menu(type, 3, Ressource.BackgroundMenuMain, 3);
                        actualMenu.title = new MenuTitle(new Vector2(Xtitle, Ytitle), 2);
                        actualMenu.SButtons[0] = new SwitchButton(new Vector2(Xbutton0, Ybutton0), Ressource.MenuString["Musique"], 0);
                        actualMenu.SButtons[1] = new SwitchButton(new Vector2(Xbutton0, Ybutton1), Ressource.MenuString["Bruitages"], 1);
                        actualMenu.SButtons[2] = new SwitchButton(new Vector2(Xbutton1, Ybutton0), Ressource.MenuString["Anglais"], 2);
                        actualMenu.Buttons[1] = new MenuButton(new Vector2(Xbutton1, Ybutton1), Ressource.MenuString["Touches"], MenuType.setcontrole);
                        actualMenu.Buttons[2] = new MenuButton(new Vector2(Xbutton, Ybutton2), Ressource.MenuString["Pseudo"], MenuType.setpseudo);
                        actualMenu.Buttons[0] = new MenuButton(new Vector2(Xbutton, Ybutton3), Ressource.MenuString["Retour"], MenuType.welcome);
                        break;
                    }
                case MenuType.setcontrole:
                    {
                        actualMenu = new Menu(type, 2, Ressource.BackgroundMenuMain, 0, 4);
                        actualMenu.title = new MenuTitle(new Vector2(Xtitle, Ytitle), 2);
                        actualMenu.CButtons[0] = new ControleButton(new Vector2(Xbutton0, Ybutton0), Ressource.MenuString["Droite"], Ressource.inGameAction.Right);
                        actualMenu.CButtons[1] = new ControleButton(new Vector2(Xbutton0, Ybutton1), Ressource.MenuString["Gauche"], Ressource.inGameAction.Left);
                        actualMenu.CButtons[2] = new ControleButton(new Vector2(Xbutton1, Ybutton0), Ressource.MenuString["Saut"], Ressource.inGameAction.Jump);
                        actualMenu.CButtons[3] = new ControleButton(new Vector2(Xbutton1, Ybutton1), Ressource.MenuString["Pause"], Ressource.inGameAction.Pause);
                        actualMenu.Buttons[1] = new MenuButton(new Vector2(Xbutton, Ybutton2), Ressource.MenuString["Par defaut"], MenuType.defaultcommand);
                        actualMenu.Buttons[0] = new MenuButton(new Vector2(Xbutton, Ybutton3), Ressource.MenuString["Retour"], MenuType.option);
                        break;
                    }
                case MenuType.pause:
                    {
                        Game1.inGame = true;
                        actualMenu = new Menu(type, 3, Ressource.BackgroundMenuPause);
                        actualMenu.title = new MenuTitle(new Vector2(Xtitle, Ytitle), 3);
                        actualMenu.Buttons[0] = new MenuButton(new Vector2(Xbutton, Ybutton0), Ressource.MenuString["Reprendre"], MenuType.unpause);
                        actualMenu.Buttons[1] = new MenuButton(new Vector2(Xbutton, Ybutton1), Ressource.MenuString["Recommencer"], MenuType.reloadlevel);
                        actualMenu.Buttons[2] = new MenuButton(new Vector2(Xbutton, Ybutton2), Ressource.MenuString["Accueil"], MenuType.welcome);
                        break;
                    }
                case MenuType.freeplay:
                    {
                        actualMenu = new Menu(MenuType.freeplay, 2, Ressource.BackgroundMenuMain, 0, 0, 6);
                        actualMenu.title = new MenuTitle(new Vector2(Xtitle, Ytitle), 2);
                        actualMenu.Buttons[0] = new MenuButton(new Vector2(Xbutton0, Ybutton3 + 30), Ressource.MenuString["Retour"], MenuType.play);
                        actualMenu.Buttons[1] = new MenuButton(new Vector2(Xbutton1, Ybutton3 + 30), Ressource.MenuString["Page 2"], MenuType.freeplay2);
                        actualMenu.LButtons[0] = new LevelButton(new Vector2(Xbutton0, Ybutton0), 1);
                        actualMenu.LButtons[1] = new LevelButton(new Vector2(Xbutton0, Ybutton1), 2);
                        actualMenu.LButtons[2] = new LevelButton(new Vector2(Xbutton0, Ybutton2), 3);
                        actualMenu.LButtons[3] = new LevelButton(new Vector2(Xbutton1, Ybutton0), 4);
                        actualMenu.LButtons[4] = new LevelButton(new Vector2(Xbutton1, Ybutton1), 5);
                        actualMenu.LButtons[5] = new LevelButton(new Vector2(Xbutton1, Ybutton2), 6);
                        break;
                    }
                case MenuType.freeplay2:
                    {
                        actualMenu = new Menu(MenuType.freeplay, 2, Ressource.BackgroundMenuMain, 0, 0, 4);
                        actualMenu.title = new MenuTitle(new Vector2(Xtitle, Ytitle), 2);
                        actualMenu.Buttons[0] = new MenuButton(new Vector2(Xbutton0, Ybutton2 + 30), Ressource.MenuString["Retour"], MenuType.play);
                        actualMenu.Buttons[1] = new MenuButton(new Vector2(Xbutton1, Ybutton2 + 30), Ressource.MenuString["Page 1"], MenuType.freeplay);
                        actualMenu.LButtons[0] = new LevelButton(new Vector2(Xbutton0, Ybutton0), 7);
                        actualMenu.LButtons[1] = new LevelButton(new Vector2(Xbutton0, Ybutton1), 8);
                        actualMenu.LButtons[2] = new LevelButton(new Vector2(Xbutton1, Ybutton0), 9);
                        actualMenu.LButtons[3] = new LevelButton(new Vector2(Xbutton1, Ybutton1), 10);
                        break;
                    }
                case MenuType.adventure:
                    {
                        Game1.inGame = true;
                        Game1.Level = new Level(1);
                        //Précharge la pause
                        actualMenu = new Menu(MenuType.pause, 3, Ressource.BackgroundMenuPause);
                        actualMenu.title = new MenuTitle(new Vector2(Xtitle, Ytitle), 3);
                        actualMenu.Buttons[0] = new MenuButton(new Vector2(Xbutton, Ybutton0), Ressource.MenuString["Reprendre"], MenuType.unpause);
                        actualMenu.Buttons[1] = new MenuButton(new Vector2(Xbutton, Ybutton1), Ressource.MenuString["Recommencer"], MenuType.reloadlevel);
                        actualMenu.Buttons[2] = new MenuButton(new Vector2(Xbutton, Ybutton2), Ressource.MenuString["Accueil"], MenuType.welcome);
                        break;
                    }
                case MenuType.unpause:
                    {
                        Game1.inGame = true;
                        //Précharge la pause
                        actualMenu = new Menu(MenuType.pause, 3, Ressource.BackgroundMenuPause);
                        actualMenu.title = new MenuTitle(new Vector2(Xtitle, Ytitle), 3);
                        actualMenu.Buttons[0] = new MenuButton(new Vector2(Xbutton, Ybutton0), Ressource.MenuString["Reprendre"], MenuType.unpause);
                        actualMenu.Buttons[1] = new MenuButton(new Vector2(Xbutton, Ybutton1), Ressource.MenuString["Recommencer"], MenuType.reloadlevel);
                        actualMenu.Buttons[2] = new MenuButton(new Vector2(Xbutton, Ybutton2), Ressource.MenuString["Accueil"], MenuType.welcome);
                        break;
                    }
                case MenuType.reloadlevel:
                    {
                        Game1.inGame = true;

                        Game1.Level = new Level(Game1.Level.lvl);


                        //Précharge la pause
                        actualMenu = new Menu(MenuType.pause, 3, Ressource.BackgroundMenuPause);
                        actualMenu.title = new MenuTitle(new Vector2(Xtitle, Ytitle), 3);
                        actualMenu.Buttons[0] = new MenuButton(new Vector2(Xbutton, Ybutton0), Ressource.MenuString["Reprendre"], MenuType.unpause);
                        actualMenu.Buttons[1] = new MenuButton(new Vector2(Xbutton, Ybutton1), Ressource.MenuString["Recommencer"], MenuType.reloadlevel);
                        actualMenu.Buttons[2] = new MenuButton(new Vector2(Xbutton, Ybutton2), Ressource.MenuString["Accueil"], MenuType.welcome);
                        break;
                    }
                case MenuType.loose:
                    {
                        actualMenu = new Menu(MenuType.pause, 2, Ressource.BackgroundMenuPause);
                        actualMenu.title = new MenuTitle(new Vector2(Xtitle, Ytitle), 3);
                        actualMenu.Buttons[0] = new MenuButton(new Vector2(Xbutton, Ybutton1), Ressource.MenuString["Recommencer"], MenuType.reloadlevel);
                        actualMenu.Buttons[1] = new MenuButton(new Vector2(Xbutton, Ybutton2), Ressource.MenuString["Accueil"], MenuType.welcome);
                        break;
                    }
                case MenuType.win:
                    {
                        actualMenu = new Menu(MenuType.pause, 2, Ressource.BackgroundMenuPause);
                        actualMenu.title = new MenuTitle(new Vector2(Xtitle, Ytitle), 3);
                        actualMenu.Buttons[0] = new MenuButton(new Vector2(Xbutton, Ybutton1), Ressource.MenuString["Recommencer"], MenuType.reloadlevel);
                        actualMenu.Buttons[1] = new MenuButton(new Vector2(Xbutton, Ybutton2), Ressource.MenuString["Acceuil"], MenuType.welcome);
                        break;
                    }
                case MenuType.defaultcommand:
                    {

                        Ressource.Key[Ressource.inGameAction.Left] = Keys.Left;
                        Ressource.Key[Ressource.inGameAction.Right] = Keys.Right;
                        Ressource.Key[Ressource.inGameAction.Jump] = Keys.Space;
                        Ressource.Key[Ressource.inGameAction.Pause] = Keys.Escape;

                        actualMenu = new Menu(type, 2, Ressource.BackgroundMenuMain, 0, 4);
                        actualMenu.title = new MenuTitle(new Vector2(Xtitle, Ytitle), 2);
                        actualMenu.CButtons[0] = new ControleButton(new Vector2(Xbutton0, Ybutton0), Ressource.MenuString["Droite"], Ressource.inGameAction.Right);
                        actualMenu.CButtons[1] = new ControleButton(new Vector2(Xbutton0, Ybutton1), Ressource.MenuString["Gauche"], Ressource.inGameAction.Left);
                        actualMenu.CButtons[2] = new ControleButton(new Vector2(Xbutton1, Ybutton0), Ressource.MenuString["Saut"], Ressource.inGameAction.Jump);
                        actualMenu.CButtons[3] = new ControleButton(new Vector2(Xbutton1, Ybutton1), Ressource.MenuString["Pause"], Ressource.inGameAction.Pause);
                        actualMenu.Buttons[1] = new MenuButton(new Vector2(Xbutton, Ybutton2), Ressource.MenuString["Par defaut"], MenuType.defaultcommand);
                        actualMenu.Buttons[0] = new MenuButton(new Vector2(Xbutton, Ybutton3), Ressource.MenuString["Retour"], MenuType.option);
                        break;
                    }
                case MenuType.setpseudo:
                    {
                        actualMenu = new Menu(type, 1, Ressource.BackgroundMenuMain, 0, 0, 0, 1);
                        actualMenu.title = new MenuTitle(new Vector2(Xtitle, Ytitle), 2);
                        actualMenu.Buttons[0] = new MenuButton(new Vector2(Xbutton, Ybutton3), Ressource.MenuString["Retour"], MenuType.option);
                        actualMenu.Pseudo[0] = new Pseudo(new Vector2(Xbutton, Ybutton2));
                        break;
                    }
                default:
                    {
                        actualMenu = new Menu(type, 0, Ressource.BackgroundMenuMain);
                        actualMenu.title = new MenuTitle(new Vector2(Xtitle, Ytitle), 0);
                        break;
                    }
            }
            return actualMenu;
        }

        //UPDATE & DRAW
        public void Draw(SpriteBatch spriteBatch, MouseState mouse)
        {
            if (this.actualType == MenuType.pause)
            {
                //A REVOIR
                //Game1.Level.Draw(spriteBatch);
            }
            spriteBatch.Draw(this.background, new Rectangle(0, 0, 1900, 1200), Color.White);

            Color color;

            foreach (MenuButton b in Buttons)
            {
                if (mouse.X >= b.pos.X
                    & mouse.X <= b.pos.X + b.SpriteWidth
                    & mouse.Y >= b.pos.Y
                    & mouse.Y <= b.pos.Y + b.SpriteHeight
                    & b.nextMenu != MenuType.none)
                {
                    color = Color.LightGray;
                }
                else
                {
                    color = Color.White;
                }
                b.Draw(spriteBatch, color);
            }

            foreach (SwitchButton b in SButtons)
            {
                if (mouse.X >= b.pos.X
                    & mouse.X <= b.pos.X + b.SpriteWidth
                    & mouse.Y >= b.pos.Y
                    & mouse.Y <= b.pos.Y + b.SpriteHeight)
                {
                    color = Color.LightGray;
                }
                else
                {
                    color = Color.White;
                }
                b.Draw(spriteBatch, color);
            }

            foreach (LevelButton b in LButtons)
            {
                if (mouse.X >= b.pos.X
                    & mouse.X <= b.pos.X + b.SpriteWidth
                    & mouse.Y >= b.pos.Y
                    & mouse.Y <= b.pos.Y + b.SpriteHeight)
                {
                    color = Color.LightGray;
                }
                else
                {
                    color = Color.White;
                }
                b.Draw(spriteBatch, color);
            }

            foreach (ControleButton b in CButtons)
            {
                Boolean anotherChanging = false;
                foreach (ControleButton b2 in CButtons)
                {
                    if (b2.isChanging && b2 != b)
                    {
                        anotherChanging = true;
                    }
                }

                if (mouse.X >= b.pos.X
                    && mouse.X <= b.pos.X + b.SpriteWidth
                    && mouse.Y >= b.pos.Y
                    && mouse.Y <= b.pos.Y + b.SpriteHeight
                    && !anotherChanging)
                {
                    color = Color.LightGray;
                }
                else
                {
                    color = Color.White;
                }
                b.Draw(spriteBatch, color);
            }

            foreach (Pseudo b in Pseudo)
            {
                if (mouse.X >= b.pos.X
                    & mouse.X <= b.pos.X + b.SpriteWidth
                    & mouse.Y >= b.pos.Y
                    & mouse.Y <= b.pos.Y + b.SpriteHeight)
                {
                    color = Color.LightGray;
                }
                else
                {
                    color = Color.White;
                }
                b.Draw(spriteBatch, color);
            }

            title.Draw(spriteBatch);
        }

        public void Update(MouseState mouse, KeyboardState keyboard, ref Menu menu, bool sizeChanged)
        {

            //Musique
            if (Ressource.parameter[0] && MediaPlayer.State != MediaState.Playing)
            {
                MediaPlayer.Play(Ressource.song);
                MediaPlayer.Volume = 0.1f;
            }

            else if (Ressource.parameter[0] == false)
            {
                MediaPlayer.Stop();
            }

            if (sizeChanged)
            {
                menu = ChangeMenu(actualType);
            }


            if (!menu.cooldown &
                mouse.LeftButton == ButtonState.Released &
                keyboard.IsKeyUp(Keys.Escape))
            {
                menu.cooldown = true;
            }


            foreach (MenuButton b in menu.Buttons)
            {
                if (mouse.LeftButton == ButtonState.Pressed
                    & mouse.X >= b.pos.X
                    & mouse.X <= b.pos.X + b.SpriteWidth
                    & mouse.Y >= b.pos.Y
                    & mouse.Y <= b.pos.Y + b.SpriteHeight
                    & b.nextMenu != MenuType.none
                    & cooldown)
                {
                    menu = ChangeMenu(b.nextMenu);
                    menu.cooldown = false;
                    return;
                }
            }

            foreach (SwitchButton b in SButtons)
            {
                if (mouse.LeftButton == ButtonState.Pressed
                    & mouse.X >= b.pos.X
                    & mouse.X <= b.pos.X + b.SpriteWidth
                    & mouse.Y >= b.pos.Y
                    & mouse.Y <= b.pos.Y + b.SpriteHeight
                    & cooldown)
                {
                    b.Update();
                    if (b.nbParameter == 2)
                    {
                        menu = ChangeMenu(actualType);
                    }
                    menu.cooldown = false;
                    return;
                }
            }

            foreach (LevelButton b in LButtons)
            {
                if (mouse.LeftButton == ButtonState.Pressed
                    & mouse.X >= b.pos.X
                    & mouse.X <= b.pos.X + b.SpriteWidth
                    & mouse.Y >= b.pos.Y
                    & mouse.Y <= b.pos.Y + b.SpriteHeight
                    & cooldown)
                {
                    b.Update();
                    menu = menu.ChangeMenu(Menu.MenuType.pause);
                    menu.cooldown = false;
                    return;
                }
            }

            foreach (ControleButton b in CButtons)
            {
                bool oneIsChanging = false;
                foreach (ControleButton b2 in CButtons)
                {
                    if (b2.isChanging && b2 != b)
                    {
                        oneIsChanging = true;
                    }
                }
                if (!oneIsChanging)
                {
                    b.Update(mouse, ref menu);
                }

            }

            foreach (Pseudo p in Pseudo)
            {
                p.Update(mouse, ref menu);
            }

        }

    }

    public class MenuTitle
    {
        //FIELDS
        Vector2 pos;
        Rectangle hitbox;

        int SpriteHeight;
        int SpriteWidth;
        int FrameLine;


        //CONSTRUCTOR
        public MenuTitle(Vector2 pos, int FrameLine)
        {
            this.pos = pos;
            this.FrameLine = FrameLine;
            SpriteHeight = 250;
            SpriteWidth = 700;
            this.hitbox = new Rectangle((int)pos.X, (int)pos.Y, this.SpriteWidth, this.SpriteHeight);
        }

        //METHODS

        //UPDATE & DRAW
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Ressource.Title, this.hitbox, new Rectangle(0, this.FrameLine * this.SpriteHeight, this.SpriteWidth, this.SpriteHeight),
                Color.White);
        }

    }

    public class MenuButton : Button
    {
        //FIELDS
        public Menu.MenuType nextMenu;


        //CONSTRUCTOR
        public MenuButton(Vector2 pos, Tuple<string, string> text, Menu.MenuType nextMenu)
            : base(pos, text)
        {
            this.nextMenu = nextMenu;
        }

        //METHODS

        //UPDATE & DRAW
        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            spriteBatch.Draw(Ressource.Button, this.hitbox, new Rectangle(0, 0, this.SpriteWidth, this.SpriteHeight), color);
            spriteBatch.DrawString(Ressource.MenuPolice, Text, new Vector2(pos.X + 95, pos.Y), Color.White);
        }

    }

    public class Button
    {
        //FIELDS
        public Vector2 pos;
        protected Rectangle hitbox;

        public int SpriteHeight;
        public int SpriteWidth;
        protected string Text;

        //CONSTRUCTOR
        public Button(Vector2 pos, Tuple<string, string> Text)
        {
            this.pos = pos;
            if (Ressource.parameter[2])
            {
                this.Text = Text.Item2;
            }
            else
            {
                this.Text = Text.Item1;
            }
            SpriteHeight = 75;
            SpriteWidth = 500;
            this.hitbox = new Rectangle((int)pos.X, (int)pos.Y, this.SpriteWidth, this.SpriteHeight);
        }
    }

    public class SwitchButton : Button
    {
        //FIELDS
        public int nbParameter;

        //CONSTRUCTOR
        public SwitchButton(Vector2 pos, Tuple<string, string> text, int nbParameter)
            : base(pos, text)
        {
            this.nbParameter = nbParameter;
        }

        //METHODS

        //UPDATE & DRAW
        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            spriteBatch.Draw(Ressource.Button, this.hitbox, new Rectangle(0, 0, this.SpriteWidth, this.SpriteHeight), color);
            spriteBatch.DrawString(Ressource.MenuPolice, Text, new Vector2(pos.X + 95, pos.Y), Color.White);
            if (Ressource.parameter[nbParameter])
            {
                spriteBatch.DrawString(Ressource.MenuPolice, "ON", new Vector2(pos.X + 380, pos.Y), Color.LimeGreen);
            }
            else
            {
                spriteBatch.DrawString(Ressource.MenuPolice, "OFF", new Vector2(pos.X + 380, pos.Y), Color.Red);
            }
        }

        public void Update()
        {
            Ressource.parameter[this.nbParameter] = !Ressource.parameter[this.nbParameter];
        }
    }

    public class ControleButton : Button
    {
        //FIELDS
        Ressource.inGameAction action;
        string ActualKey;
        public bool isChanging;

        //CONSTRUCTOR
        public ControleButton(Vector2 pos, Tuple<string, string> text, Ressource.inGameAction action)
            : base(pos, text)
        {
            this.action = action;
            Keys k = Ressource.Key[action];

            if (((int)k >= 65 && (int)k <= 90))
            {
                ActualKey = "" + (char)k;
            }
            else if (k == Keys.Space)
            {
                ActualKey = "Spa.";
            }

            else if (k == Keys.Escape)
            {
                ActualKey = "Esc.";
            }

            else if ((int)k >= 48 && (int)k <= 57)
            {
                ActualKey = "" + (char)k;
            }

            else if (k == Keys.Left)
            {
                ActualKey = "<-";
            }

            else if (k == Keys.Right)
            {
                ActualKey = "->";
            }

            else if (k == Keys.Up)
            {
                ActualKey = "Up";
            }

            else if (k == Keys.Down)
            {
                ActualKey = "Down";
            }


            isChanging = false;
        }

        //UPDATE & DRAW
        public void Update(MouseState mouse, ref Menu menu)
        {
            if (mouse.LeftButton == ButtonState.Pressed
                & mouse.X >= pos.X
                & mouse.X <= pos.X + SpriteWidth
                & mouse.Y >= pos.Y
                & mouse.Y <= pos.Y + SpriteHeight
                && menu.cooldown)
            {
                isChanging = !isChanging;
                menu.cooldown = false;
            }
            if (isChanging)
            {
                foreach (Keys k in (Keys[])Enum.GetValues(typeof(Keys)))
                {
                    if (Keyboard.GetState().IsKeyDown(k)
                        && (!(Ressource.Key.ContainsValue(k)) || Ressource.Key[action] == k))
                    {
                        if (((int)k >= 65 && (int)k <= 90))
                        {
                            Ressource.Key[action] = k;
                            ActualKey = "" + (char)k;
                            isChanging = false;
                            return;
                        }
                        else if (k == Keys.Space)
                        {
                            Ressource.Key[action] = k;
                            ActualKey = "Spa.";
                            isChanging = false;
                            return;
                        }

                        else if (k == Keys.Escape)
                        {
                            Ressource.Key[action] = k;
                            ActualKey = "Esc.";
                            isChanging = false;
                            return;
                        }

                        else if ((int)k >= (int)Keys.NumPad0 && (int)k <= (int)Keys.NumPad9)
                        {
                            Ressource.Key[action] = k;
                            ActualKey = "" + (char)((int)k - (int)Keys.NumPad0 + 48);
                            isChanging = false;
                            return;
                        }

                        else if (k == Keys.Left)
                        {
                            Ressource.Key[action] = k;
                            ActualKey = "<-";
                            isChanging = false;
                            return;
                        }

                        else if (k == Keys.Right)
                        {
                            Ressource.Key[action] = k;
                            ActualKey = "->";
                            isChanging = false;
                            return;
                        }

                        else if (k == Keys.Up)
                        {
                            Ressource.Key[action] = k;
                            ActualKey = "Up";
                            isChanging = false;
                            return;
                        }

                        else if (k == Keys.Down)
                        {
                            Ressource.Key[action] = k;
                            ActualKey = "Down";
                            isChanging = false;
                            return;
                        }

                    }
                }

            }
        }


        public void Draw(SpriteBatch spriteBatch, Color color)
        {

            spriteBatch.Draw(Ressource.Button, this.hitbox, new Rectangle(0, 0, this.SpriteWidth, this.SpriteHeight), color);
            spriteBatch.DrawString(Ressource.MenuPolice, Text, new Vector2(pos.X + 95, pos.Y), Color.White);

            if (!isChanging)
            {
                spriteBatch.DrawString(Ressource.MenuPolice, ActualKey, new Vector2(pos.X + 350, pos.Y), Color.Yellow);
            }
            else
            {
                spriteBatch.DrawString(Ressource.MenuPolice, "???", new Vector2(pos.X + 350, pos.Y), Color.Yellow);
            }
        }



    }

    public class LevelButton : Button
    {
        //FIELDS
        int lvl;

        //CONSTRUCTOR
        public LevelButton(Vector2 pos, int lvl) :
            base(pos, Ressource.MenuString["Niveau"])
        {
            Text = Text + " " + lvl;
            this.lvl = lvl;
        }

        //UPDATE & DRAW
        public void Update()
        {
            Game1.Level = new Level(lvl);
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            spriteBatch.Draw(Ressource.Button, this.hitbox, new Rectangle(0, 0, this.SpriteWidth, this.SpriteHeight), color);
            spriteBatch.DrawString(Ressource.MenuPolice, Text, new Vector2(pos.X + 95, pos.Y), Color.White);
        }

    }

    public class Pseudo : Button
    {
        string newPseudo;
        string newPseudoMsg;
        string currentPseudoMsg;

        KeyboardState previousKeyboard;

        //CONSTRUCTOR
        public Pseudo(Vector2 pos) :
            base(pos, Ressource.MenuString["Valider"])
        {
            newPseudo = "";
            previousKeyboard = Keyboard.GetState();

            if (Ressource.parameter[2])
            {
                newPseudoMsg = "New pseudo : ";
                currentPseudoMsg = "Current pseudo : ";
            }
            else
            {
                newPseudoMsg = "Nouveau pseudo : ";
                currentPseudoMsg = "Pseudo actuel : ";
            }
        }

        //DRAW & UPDATE
        public void Update(MouseState mouse, ref Menu menu)
        {
            if (((mouse.LeftButton == ButtonState.Pressed
                && mouse.X >= pos.X
                && mouse.X <= pos.X + SpriteWidth
                && mouse.Y >= pos.Y
                && mouse.Y <= pos.Y + SpriteHeight
                && menu.cooldown) || Keyboard.GetState().IsKeyDown(Keys.Enter))
                && newPseudo.Length > 3)
            {
                Ressource.pseudo = newPseudo;
                newPseudo = "";
                menu.cooldown = false;
            }

            KeyboardState keyboard = Keyboard.GetState();

            if (keyboard == previousKeyboard)
            {
                return;
            }

            foreach (Keys k in (Keys[])Enum.GetValues(typeof(Keys)))
            {
                if (Keyboard.GetState().IsKeyDown(k))
                {
                    if (k >= Keys.A && k <= Keys.Z && newPseudo.Length <= 10)
                    {
                        newPseudo += (char)k;
                    }

                    if (k == Keys.Back && newPseudo.Length > 0)
                    {
                        newPseudo = newPseudo.Remove(newPseudo.Length - 1);
                    }
                }
            }
            previousKeyboard = keyboard;
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            spriteBatch.DrawString(Ressource.MenuPolice, currentPseudoMsg + Ressource.pseudo, new Vector2(Ressource.screenWidth / 2 - Ressource.MenuPolice.MeasureString(currentPseudoMsg + Ressource.pseudo).Length() / 2, pos.Y - 300), Color.Black);

            spriteBatch.DrawString(Ressource.MenuPolice, newPseudoMsg, new Vector2(Ressource.screenWidth / 2 - Ressource.MenuPolice.MeasureString(newPseudoMsg).Length() / 2, pos.Y - 200), Color.Black);

            spriteBatch.Draw(Ressource.Button, new Rectangle((int)pos.X, (int)pos.Y - 100, 500, 75), new Rectangle(0, 0, this.SpriteWidth, this.SpriteHeight), Color.White);
            spriteBatch.DrawString(Ressource.MenuPolice, newPseudo, new Vector2(pos.X + 95, pos.Y - 100), Color.White);

            spriteBatch.Draw(Ressource.Button, this.hitbox, new Rectangle(0, 0, this.SpriteWidth, this.SpriteHeight), color);
            spriteBatch.DrawString(Ressource.MenuPolice, Text, new Vector2(pos.X + 95, pos.Y), Color.White);
        }
    }
}