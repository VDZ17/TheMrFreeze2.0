using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.IO;
using System.Diagnostics;

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

        public SetIp setIp;

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
            setcontroleJ1,
            defaultcommandJ1,
            setcontroleJ2,
            defaultcommandJ2,
            setpseudo,
            sendhighscore,
            multi,
            host,
            ipjoin,
            join,
            coop,
            multipause,
            multiwin,
            multiloose,
            reseaulost,
            uninstall,
        }

        //CONSTRUCTOR
        public Menu(MenuType type, int nbButton, Texture2D background, int nbSwitchButton = 0, int nbControleButton = 0, int nbLevelButton = 0, int nbPseudo = 0, int XposSetIp = 0, int YposSetIP = 0)
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

            if (actualType == MenuType.ipjoin)
	        {
                 this.setIp = new SetIp(new Vector2(XposSetIp, YposSetIP));
	        }
            
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
            //int Ytitle = 50;
            int Ytitle = 10;
            int YtitleOptions = -50;

            int Xbutton = (Ressource.screenWidth / 2) - 250;
            int Xbutton0 = (Ressource.screenWidth / 4) - 250;
            int Xbutton1 = (3 * Ressource.screenWidth / 4) - 250; ;
            /*int Ybutton0 = 350;
            int Ybutton1 = 450;
            int Ybutton2 = 550;
            int Ybutton3 = 650;*/
            int Ybutton0 = 250;
            int Ybutton1 = 350;
            int Ybutton2 = 450;
            int Ybutton3 = 550;

            int nbPlay = 1;
            if (Ressource.parameter[2])
            {
                nbPlay = 6;
            }

            #region Switch Menu
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
                        Game1.reload = true;
                        Ressource.parameter[3] = false;
                        Ressource.parameter[4] = false;
                        Ressource.parameter[5] = false;
                        actualMenu = new Menu(type, 4, Ressource.BackgroundMenuMain);
                        actualMenu.title = new MenuTitle(new Vector2(Xtitle, Ytitle), 0);
                        actualMenu.Buttons[0] = new MenuButton(new Vector2(Xbutton, Ybutton0), Ressource.MenuString["Jouer"], MenuType.freeplay);
                        actualMenu.Buttons[3] = new MenuButton(new Vector2(Xbutton, Ybutton1), Ressource.MenuString["Multijoueur"], MenuType.multi);
                        actualMenu.Buttons[1] = new MenuButton(new Vector2(Xbutton, Ybutton2), Ressource.MenuString["Options"], MenuType.option);
                        actualMenu.Buttons[2] = new MenuButton(new Vector2(Xbutton, Ybutton3), Ressource.MenuString["Quitter"], MenuType.close);
                        break;
                    }
                case MenuType.play: //useless
                    {
                        actualMenu = new Menu(type, 3, Ressource.BackgroundMenuMain);
                        actualMenu.title = new MenuTitle(new Vector2(Xtitle, Ytitle), nbPlay);
                        actualMenu.Buttons[0] = new MenuButton(new Vector2(Xbutton, Ybutton0), Ressource.MenuString["Aventure"], MenuType.adventure);
                        actualMenu.Buttons[1] = new MenuButton(new Vector2(Xbutton, Ybutton1), Ressource.MenuString["Jeu libre"], MenuType.freeplay);
                        actualMenu.Buttons[2] = new MenuButton(new Vector2(Xbutton, Ybutton2), Ressource.MenuString["Retour"], MenuType.welcome);
                        break;
                    }
                case MenuType.option:
                    {
                        actualMenu = new Menu(type, 5, Ressource.BackgroundMenuMain, 3);
                        actualMenu.title = new MenuTitle(new Vector2(Xtitle, Ytitle), 2);
                        actualMenu.SButtons[0] = new SwitchButton(new Vector2(Xbutton0, Ybutton0), Ressource.MenuString["Musique"], 0);
                        actualMenu.SButtons[1] = new SwitchButton(new Vector2(Xbutton0, Ybutton1), Ressource.MenuString["Bruitages"], 1);
                        actualMenu.SButtons[2] = new SwitchButton(new Vector2(Xbutton0, Ybutton2), Ressource.MenuString["Anglais"], 2);
                        actualMenu.Buttons[1] = new MenuButton(new Vector2(Xbutton1, Ybutton1), Ressource.MenuString["Touches J1"], MenuType.setcontroleJ1);
                        
                        actualMenu.Buttons[2] = new MenuButton(new Vector2(Xbutton1, Ybutton2), Ressource.MenuString["Touches J2"], MenuType.setcontroleJ2);
                        actualMenu.Buttons[3] = new MenuButton(new Vector2(Xbutton1, Ybutton0), Ressource.MenuString["Pseudo"], MenuType.setpseudo);
                        actualMenu.Buttons[4] = new MenuButton(new Vector2(Xbutton0, Ybutton3), Ressource.MenuString["Desinstaller"], MenuType.uninstall);
                        actualMenu.Buttons[0] = new MenuButton(new Vector2(Xbutton1, Ybutton3), Ressource.MenuString["Retour"], MenuType.welcome);
                        break;
                    }
                case MenuType.setcontroleJ1:
                    {
                        actualMenu = new Menu(type, 2, Ressource.BackgroundMenuMain, 0, 5);
                        actualMenu.title = new MenuTitle(new Vector2(Xtitle, Ytitle), 2);
                        actualMenu.CButtons[0] = new ControleButton(new Vector2(Xbutton0, Ybutton0), Ressource.MenuString["Droite"], Ressource.inGameAction.Right);
                        actualMenu.CButtons[1] = new ControleButton(new Vector2(Xbutton0, Ybutton1), Ressource.MenuString["Gauche"], Ressource.inGameAction.Left);
                        actualMenu.CButtons[2] = new ControleButton(new Vector2(Xbutton1, Ybutton0), Ressource.MenuString["Saut"], Ressource.inGameAction.Jump);
                        actualMenu.CButtons[3] = new ControleButton(new Vector2(Xbutton1, Ybutton1), Ressource.MenuString["Pause"], Ressource.inGameAction.Pause);
                        actualMenu.CButtons[4] = new ControleButton(new Vector2(Xbutton0, Ybutton2), Ressource.MenuString["Tir"], Ressource.inGameAction.Shoot);
                        actualMenu.Buttons[1] = new MenuButton(new Vector2(Xbutton1, Ybutton2), Ressource.MenuString["Par defaut"], MenuType.defaultcommandJ1);
                        actualMenu.Buttons[0] = new MenuButton(new Vector2(Xbutton, Ybutton3), Ressource.MenuString["Retour"], MenuType.option);
                        break;
                    }
                case MenuType.setcontroleJ2:
                    {
                        actualMenu = new Menu(type, 2, Ressource.BackgroundMenuMain, 0, 5);
                        actualMenu.title = new MenuTitle(new Vector2(Xtitle, Ytitle), 2);
                        actualMenu.CButtons[0] = new ControleButton(new Vector2(Xbutton0, Ybutton0), Ressource.MenuString["Droite"], Ressource.inGameAction.Right, 2);
                        actualMenu.CButtons[1] = new ControleButton(new Vector2(Xbutton0, Ybutton1), Ressource.MenuString["Gauche"], Ressource.inGameAction.Left, 2);
                        actualMenu.CButtons[2] = new ControleButton(new Vector2(Xbutton1, Ybutton0), Ressource.MenuString["Saut"], Ressource.inGameAction.Jump, 2);
                        actualMenu.CButtons[3] = new ControleButton(new Vector2(Xbutton1, Ybutton1), Ressource.MenuString["Pause"], Ressource.inGameAction.Pause, 2);
                        actualMenu.CButtons[4] = new ControleButton(new Vector2(Xbutton0, Ybutton2), Ressource.MenuString["Tir"], Ressource.inGameAction.Shoot, 2);
                        actualMenu.Buttons[1] = new MenuButton(new Vector2(Xbutton1, Ybutton2), Ressource.MenuString["Par defaut"], MenuType.defaultcommandJ2);
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
                        actualMenu.Buttons[2] = new MenuButton(new Vector2(Xbutton, Ybutton2), Ressource.MenuString["Changer niv."], MenuType.freeplay);
                        break;
                    }
                case MenuType.freeplay:
                    {
                        actualMenu = new Menu(MenuType.freeplay, 2, Ressource.BackgroundMenuMain, 0, 0, 6);
                        actualMenu.title = new MenuTitle(new Vector2(Xtitle, Ytitle), nbPlay);
                        actualMenu.Buttons[0] = new MenuButton(new Vector2(Xbutton0, Ybutton3 + 30), Ressource.MenuString["Retour"], MenuType.welcome); //
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
                        actualMenu.title = new MenuTitle(new Vector2(Xtitle, Ytitle), nbPlay);
                        actualMenu.Buttons[0] = new MenuButton(new Vector2(Xbutton0, Ybutton2 + 30), Ressource.MenuString["Retour"], MenuType.welcome); //
                        actualMenu.Buttons[1] = new MenuButton(new Vector2(Xbutton1, Ybutton2 + 30), Ressource.MenuString["Page 1"], MenuType.freeplay);
                        actualMenu.LButtons[0] = new LevelButton(new Vector2(Xbutton0, Ybutton0), 7);
                        actualMenu.LButtons[1] = new LevelButton(new Vector2(Xbutton0, Ybutton1), 8);
                        actualMenu.LButtons[2] = new LevelButton(new Vector2(Xbutton1, Ybutton0), 9);
                        actualMenu.LButtons[3] = new LevelButton(new Vector2(Xbutton1, Ybutton1), 10);
                        break;
                    }
                case MenuType.adventure: //useless
                    {
                        Game1.inGame = true;
                        Game1.level = new Level(1);
                        //Précharge la pause
                        actualMenu = new Menu(MenuType.pause, 3, Ressource.BackgroundMenuPause);
                        actualMenu.title = new MenuTitle(new Vector2(Xtitle, Ytitle), 3);
                        actualMenu.Buttons[0] = new MenuButton(new Vector2(Xbutton, Ybutton0), Ressource.MenuString["Reprendre"], MenuType.unpause);
                        actualMenu.Buttons[1] = new MenuButton(new Vector2(Xbutton, Ybutton1), Ressource.MenuString["Recommencer"], MenuType.reloadlevel);
                        actualMenu.Buttons[2] = new MenuButton(new Vector2(Xbutton, Ybutton2), Ressource.MenuString["Changer niv."], MenuType.freeplay);
                        break;
                    }
                case MenuType.unpause:
                    {
                        Game1.inGame = true;
                        Ressource.messageJ1toJ2 = "Z/unpause+";
                        break;
                    }
                case MenuType.reloadlevel:
                    {

                        Game1.reload = true;
                        Game1.inGame = true;

                        actualMenu = new Menu(MenuType.pause, 3, Ressource.BackgroundMenuPause);
                        actualMenu.title = new MenuTitle(new Vector2(Xtitle, Ytitle), 3);
                        actualMenu.Buttons[0] = new MenuButton(new Vector2(Xbutton, Ybutton0), Ressource.MenuString["Reprendre"], MenuType.unpause);
                        actualMenu.Buttons[1] = new MenuButton(new Vector2(Xbutton, Ybutton1), Ressource.MenuString["Recommencer"], MenuType.reloadlevel);
                        actualMenu.Buttons[2] = new MenuButton(new Vector2(Xbutton, Ybutton2), Ressource.MenuString["Changer niv."], MenuType.freeplay);
                        break;
                    }
                case MenuType.loose:
                    {
                        Ressource.messageJ1toJ2 = "Z/loose+";
                        actualMenu = new Menu(MenuType.loose, 2, Ressource.BackgroundMenuPause);
                        actualMenu.title = new MenuTitle(new Vector2(Xtitle, Ytitle), 5);
                        actualMenu.Buttons[0] = new MenuButton(new Vector2(Xbutton, Ybutton1), Ressource.MenuString["Recommencer"], MenuType.reloadlevel);
                        actualMenu.Buttons[1] = new MenuButton(new Vector2(Xbutton, Ybutton2), Ressource.MenuString["Changer niv."], MenuType.freeplay);
                        break;
                    }
                case MenuType.win:
                    {
                        Ressource.messageJ1toJ2 = "Z/win+";
                        actualMenu = new Menu(MenuType.win, 3, Ressource.BackgroundMenuPause);
                        actualMenu.title = new MenuTitle(new Vector2(Xtitle, Ytitle), 4);
                        actualMenu.Buttons[0] = new MenuButton(new Vector2(Xbutton1, Ybutton2), Ressource.MenuString["Recommencer"], MenuType.reloadlevel);
                        actualMenu.Buttons[2] = new MenuButton(new Vector2(Xbutton0, Ybutton2), Ressource.MenuString["Envoiscore"], MenuType.sendhighscore);
                        actualMenu.Buttons[1] = new MenuButton(new Vector2(Xbutton, Ybutton3), Ressource.MenuString["Changer niv."], MenuType.freeplay);
                        break;
                    }
                case MenuType.defaultcommandJ1:
                    {

                        Ressource.KeyJ1[Ressource.inGameAction.Left] = Keys.Left;
                        Ressource.KeyJ1[Ressource.inGameAction.Right] = Keys.Right;
                        Ressource.KeyJ1[Ressource.inGameAction.Jump] = Keys.Up;
                        Ressource.KeyJ1[Ressource.inGameAction.Pause] = Keys.Escape;
                        Ressource.KeyJ1[Ressource.inGameAction.Shoot] = Keys.NumPad3;

                        actualMenu = new Menu(type, 2, Ressource.BackgroundMenuMain, 0, 5);
                        actualMenu.title = new MenuTitle(new Vector2(Xtitle, Ytitle), 2);
                        actualMenu.CButtons[0] = new ControleButton(new Vector2(Xbutton0, Ybutton0), Ressource.MenuString["Droite"], Ressource.inGameAction.Right);
                        actualMenu.CButtons[1] = new ControleButton(new Vector2(Xbutton0, Ybutton1), Ressource.MenuString["Gauche"], Ressource.inGameAction.Left);
                        actualMenu.CButtons[2] = new ControleButton(new Vector2(Xbutton1, Ybutton0), Ressource.MenuString["Saut"], Ressource.inGameAction.Jump);
                        actualMenu.CButtons[3] = new ControleButton(new Vector2(Xbutton1, Ybutton1), Ressource.MenuString["Pause"], Ressource.inGameAction.Pause);
                        actualMenu.CButtons[4] = new ControleButton(new Vector2(Xbutton0, Ybutton2), Ressource.MenuString["Tir"], Ressource.inGameAction.Shoot);
                        actualMenu.Buttons[1] = new MenuButton(new Vector2(Xbutton1, Ybutton2), Ressource.MenuString["Par defaut"], MenuType.defaultcommandJ1);
                        actualMenu.Buttons[0] = new MenuButton(new Vector2(Xbutton, Ybutton3), Ressource.MenuString["Retour"], MenuType.option);
                        break;
                    }
                case MenuType.defaultcommandJ2:
                    {

                        Ressource.KeyJ2[Ressource.inGameAction.Left] = Keys.Q;
                        Ressource.KeyJ2[Ressource.inGameAction.Right] = Keys.D;
                        Ressource.KeyJ2[Ressource.inGameAction.Jump] = Keys.Space;
                        Ressource.KeyJ2[Ressource.inGameAction.Pause] = Keys.Escape;
                        Ressource.KeyJ2[Ressource.inGameAction.Shoot] = Keys.Z;

                        actualMenu = new Menu(type, 2, Ressource.BackgroundMenuMain, 0, 5);
                        actualMenu.title = new MenuTitle(new Vector2(Xtitle, Ytitle), 2);
                        actualMenu.CButtons[0] = new ControleButton(new Vector2(Xbutton0, Ybutton0), Ressource.MenuString["Droite"], Ressource.inGameAction.Right, 2);
                        actualMenu.CButtons[1] = new ControleButton(new Vector2(Xbutton0, Ybutton1), Ressource.MenuString["Gauche"], Ressource.inGameAction.Left, 2);
                        actualMenu.CButtons[2] = new ControleButton(new Vector2(Xbutton1, Ybutton0), Ressource.MenuString["Saut"], Ressource.inGameAction.Jump, 2);
                        actualMenu.CButtons[3] = new ControleButton(new Vector2(Xbutton1, Ybutton1), Ressource.MenuString["Pause"], Ressource.inGameAction.Pause, 2);
                        actualMenu.CButtons[4] = new ControleButton(new Vector2(Xbutton0, Ybutton2), Ressource.MenuString["Tir"], Ressource.inGameAction.Shoot, 2);
                        actualMenu.Buttons[1] = new MenuButton(new Vector2(Xbutton1, Ybutton2), Ressource.MenuString["Par defaut"], MenuType.defaultcommandJ2);
                        actualMenu.Buttons[0] = new MenuButton(new Vector2(Xbutton, Ybutton3), Ressource.MenuString["Retour"], MenuType.option);
                        break;
                    }
                case MenuType.setpseudo:
                    {
                        actualMenu = new Menu(type, 1, Ressource.BackgroundMenuMain, 0, 0, 0, 1);
                        actualMenu.title = new MenuTitle(new Vector2(Xtitle, YtitleOptions), 2);
                        actualMenu.Buttons[0] = new MenuButton(new Vector2(Xbutton, Ybutton3), Ressource.MenuString["Retour"], MenuType.option);
                        actualMenu.Pseudo[0] = new Pseudo(new Vector2(Xbutton, Ybutton2));
                        break;
                    }
                case MenuType.sendhighscore:
                    {
                        bool b = true;
                        loadfile.Save((Game1.score.score) + Game1.score.timer * 10, Ressource.pseudo);
                        try
                        {
                            Game1.level.Web.send_request(Ressource.pseudo, Game1.score.score + (int)Game1.score.timer * 10, Game1.level.lvl);
                        }
                        catch (Exception)
                        {
                            if (Ressource.parameter[2])
                            {
                                actualMenu.Buttons[2].Text = Ressource.MenuString["Echec"].Item2;
                            }
                            else
                            {
                                actualMenu.Buttons[2].Text = Ressource.MenuString["Echec"].Item1;
                            }
                            b = false;
                        }

                        if (Ressource.parameter[2] && b)
                        {
                            actualMenu.Buttons[2].Text = Ressource.MenuString["Envoye"].Item2;
                        }
                        if (!Ressource.parameter[2] && b)
                        {
                            actualMenu.Buttons[2].Text = Ressource.MenuString["Envoye"].Item1;
                        }
                        actualMenu.Buttons[2].nextMenu = MenuType.none;
                        break;
                    }
                case MenuType.multi:
                    {
                        actualMenu = new Menu(type, 4, Ressource.BackgroundMenuMain);
                        actualMenu.title = new MenuTitle(new Vector2(Xtitle, YtitleOptions), nbPlay);
                        actualMenu.Buttons[0] = new MenuButton(new Vector2(Xbutton, Ybutton0), Ressource.MenuString["Heberger"], MenuType.host);
                        actualMenu.Buttons[1] = new MenuButton(new Vector2(Xbutton, Ybutton1), Ressource.MenuString["Rejoindre"], MenuType.ipjoin);
                        actualMenu.Buttons[3] = new MenuButton(new Vector2(Xbutton, Ybutton2), Ressource.MenuString["Coop"], MenuType.coop);
                        actualMenu.Buttons[2] = new MenuButton(new Vector2(Xbutton, Ybutton3), Ressource.MenuString["Retour"], MenuType.welcome);
                        break;
                    }
                case MenuType.host:
                    {
                        actualMenu = new Menu(type, 2, Ressource.BackgroundMenuMain);
                        actualMenu.title = new MenuTitle(new Vector2(Xtitle, YtitleOptions), nbPlay);
                        actualMenu.Buttons[0] = new MenuButton(new Vector2(Xbutton, Ybutton2), Ressource.MenuString["Jouer"], MenuType.freeplay);
                        actualMenu.Buttons[1] = new MenuButton(new Vector2(Xbutton, Ybutton3), Ressource.MenuString["Retour"], MenuType.welcome);
                        Ressource.parameter[3] = true;
                        Ressource.parameter[4] = true;
                        Ressource.parameter[5] = false;
                        break;
                    }
                case MenuType.ipjoin:
                    {
                        actualMenu = new Menu(type, 2, Ressource.BackgroundMenuMain,0,0,0,0,Xbutton, Ybutton1);
                        actualMenu.title = new MenuTitle(new Vector2(Xtitle, YtitleOptions), nbPlay);
                        actualMenu.Buttons[0] = new MenuButton(new Vector2(Xbutton, Ybutton2), Ressource.MenuString["Rejoindre"], MenuType.join);
                        actualMenu.Buttons[1] = new MenuButton(new Vector2(Xbutton, Ybutton3), Ressource.MenuString["Retour"], MenuType.welcome);
                        break; 
                    }
                case MenuType.join:
                    {
                        actualMenu = new Menu(type, 2, Ressource.BackgroundMenuMain);
                        actualMenu.title = new MenuTitle(new Vector2(Xtitle, YtitleOptions), nbPlay);
                        actualMenu.Buttons[0] = new MenuButton(new Vector2(Xbutton, Ybutton1), Ressource.MenuString["Reessayer"], MenuType.ipjoin);
                        actualMenu.Buttons[1] = new MenuButton(new Vector2(Xbutton, Ybutton2), Ressource.MenuString["Retour"], MenuType.welcome);
                        Ressource.parameter[3] = true;
                        Ressource.parameter[4] = false;
                        Ressource.parameter[5] = true;
                        break;
                    }
                case MenuType.coop:
                    {
                        Ressource.parameter[3] = true;
                        Ressource.parameter[4] = false;
                        Ressource.parameter[5] = false;
                        actualMenu = new Menu(type, 2, Ressource.BackgroundMenuMain);
                        actualMenu.title = new MenuTitle(new Vector2(Xtitle, YtitleOptions), nbPlay);
                        actualMenu.Buttons[0] = new MenuButton(new Vector2(Xbutton, Ybutton2), Ressource.MenuString["Jouer"], MenuType.freeplay);
                        actualMenu.Buttons[1] = new MenuButton(new Vector2(Xbutton, Ybutton3), Ressource.MenuString["Retour"], MenuType.welcome);
                        break;
                    }
                case MenuType.multipause:
                    {
                        actualMenu = new Menu(MenuType.pause, 0, Ressource.BackgroundMenuPause);
                        actualMenu.title = new MenuTitle(new Vector2(Xtitle, Ytitle), 3);
                        break;
                    }
                case MenuType.multiwin:
                    {
                        actualMenu = new Menu(MenuType.win, 0, Ressource.BackgroundMenuPause);
                        actualMenu.title = new MenuTitle(new Vector2(Xtitle, Ytitle), 4);
                        break;
                    }
                case MenuType.multiloose:
                    {
                        actualMenu = new Menu(MenuType.loose, 0, Ressource.BackgroundMenuPause);
                        actualMenu.title = new MenuTitle(new Vector2(Xtitle, Ytitle), 5);
                        break;
                    }
                case MenuType.reseaulost:
                    {
                        actualMenu = new Menu(MenuType.reseaulost, 1, Ressource.BackgroundMenuMain);
                        actualMenu.title = new MenuTitle(new Vector2(Xtitle, Ytitle), 0);
                        actualMenu.Buttons[0] = new MenuButton(new Vector2(Xbutton, Ybutton1), Ressource.MenuString["Retour"], MenuType.welcome);
                        break;
                    }
                case MenuType.uninstall:
                    {
                        //Met le code ici Baptiste pour uninstall
                        try
                        {
                            Process P = Process.Start(Directory.GetCurrentDirectory() + "\\unins000.exe");
                        }
                        catch
                        {
                            Console.WriteLine("marche pas");
                        }

                        Game1.exitgame = true;
                        break;
                    }
                default:

                    {
                        break;
                    }
            }
            #endregion
            return actualMenu;
        }

        //UPDATE & DRAW
        public void Draw(SpriteBatch spriteBatch, MouseState mouse)
        {
            #region Base
            spriteBatch.Draw(this.background,new Rectangle(0,0,Ressource.screenWidth, Ressource.screenHeight), Color.White);

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

            if (setIp != null)
            {
                if (mouse.X >= setIp.pos.X
                    & mouse.X <= setIp.pos.X + setIp.SpriteWidth
                    & mouse.Y >= setIp.pos.Y
                    & mouse.Y <= setIp.pos.Y + setIp.SpriteHeight)
                {
                    color = Color.LightGray;
                }
                else
                {
                    color = Color.White;
                }
                setIp.Draw(spriteBatch, color);
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
            #endregion
            #region win
            if (actualType == MenuType.win)
            {
                //LEFT
                string nbCoin = (Game1.score.score/10).ToString();
                string tpsRestant = (Game1.score.timer).ToString();
                string score = (Game1.score.score + Game1.score.timer * 10).ToString();

                int xpos = (int)(Ressource.screenWidth / 3 - Ressource.SmallMenuPolice.MeasureString("TOTAL : " + score).Length() / 2);

                Vector2 posCoin = new Vector2(xpos - 10, 195);
                Vector2 posChrono = new Vector2(xpos, 245);

                spriteBatch.Draw(Ressource.Gold, posCoin, Color.White);
                spriteBatch.Draw(Ressource.Chrono, posChrono, Color.White);
                spriteBatch.DrawString(Ressource.SmallMenuPolice, "x " + nbCoin, new Vector2(xpos + 40, 200), Color.White);
                spriteBatch.DrawString(Ressource.SmallMenuPolice, tpsRestant + " s", new Vector2(xpos + 40, 240), Color.White);
                spriteBatch.DrawString(Ressource.SmallMenuPolice, "TOTAL : " + score, new Vector2(xpos, 280), Color.White);

                //RIGHT
                List<string> hightscore = loadfile.read_score((Directory.GetCurrentDirectory() + "\\Content\\Score\\hightscore" + Game1.level.lvl + ".txt"));

                int maxpos = (int)(2*Ressource.screenWidth / 3 - Ressource.SmallMenuPolice.MeasureString("Hightscore").Length()/2);

                foreach (string s in hightscore)
                {
                    int tempo = (int)(2*Ressource.screenWidth / 3 - Ressource.SmallMenuPolice.MeasureString(s).Length()/2);
                    if (tempo > maxpos)
                    {
                        maxpos = tempo;
                    }
                }

                Vector2 pos = new Vector2(maxpos, 200);
                spriteBatch.DrawString(Ressource.SmallMenuPolice, "Highscore :", pos, Color.White);
                int c = 0;
                foreach (string s in hightscore)
                {
                    pos.Y += 40;
                    c++;
                    spriteBatch.DrawString(Ressource.SmallMenuPolice, " " + c.ToString() + " - " + s, pos, Color.White);
                }
            }
            #endregion
            #region loose
            if (actualType == MenuType.loose)
            {
                string str;

                if (Game1.score.timer <= 0)
                {

                    if (!Ressource.parameter[2])
                    {
                        str = Ressource.MenuString["Time"].Item1;

                    }
                    else
                    {
                        str = Ressource.MenuString["Time"].Item2;
                    }
                }

                else
                {
                    if (!Ressource.parameter[2])
                    {
                        str = Ressource.MenuString["Nolife"].Item1;
                    }
                    else
                    {
                        str = Ressource.MenuString["Nolife"].Item2;
                    }
                }

                spriteBatch.DrawString(Ressource.MenuPolice, str, new Vector2(Ressource.screenWidth / 2 - Ressource.MenuPolice.MeasureString(str).Length() / 2, 250), Color.White);
            }
            #endregion
            #region host
            if (actualType == MenuType.host)
            {
                string str = "";
                
                if (Ressource.serverCount == 0)
                {
                    if (!Ressource.parameter[2])
                    {
                        str = Ressource.MenuString["Wait"].Item1;

                    }
                    else
                    {
                        str = Ressource.MenuString["Wait"].Item2;
                    }
                }
                else
                {
                    if (!Ressource.parameter[2])
                    {
                        str = Ressource.MenuString["J2 Found"].Item1;

                    }
                    else
                    {
                        str = Ressource.MenuString["J2 Found"].Item2;
                    }
                }
                Server.getIp();
                spriteBatch.DrawString(Ressource.MenuPolice, str, new Vector2(Ressource.screenWidth / 2 - Ressource.MenuPolice.MeasureString(str).Length() / 2, 250), Color.White);
                spriteBatch.DrawString(Ressource.MenuPolice, Ressource.ipJ1, new Vector2(Ressource.screenWidth / 2 - Ressource.MenuPolice.MeasureString(Ressource.ipJ1).Length() / 2, 350), Color.White);
            }
            #endregion 
            #region join
            if (actualType == MenuType.join)
            {
                string str = "";
                if (Ressource.connected)
                {
                    if (!Ressource.parameter[2])
                    {
                        str = Ressource.MenuString["J1 Found"].Item1;
                        Buttons[0].Text = Ressource.MenuString["Attente J1"].Item1;

                    }
                    else
                    {
                        str = Ressource.MenuString["J1 Found"].Item2;
                        Buttons[0].Text = Ressource.MenuString["Attente J1"].Item2;
                    }
                    Buttons[0].nextMenu = MenuType.none;
                }
                else
                {
                    if (!Ressource.parameter[2])
                    {
                        str = Ressource.MenuString["Aucun server"].Item1;

                    }
                    else
                    {
                        str = Ressource.MenuString["Aucun server"].Item2;
                    }
                }
                spriteBatch.DrawString(Ressource.MenuPolice, str, new Vector2(Ressource.screenWidth / 2 - Ressource.MenuPolice.MeasureString(str).Length() / 2, 250), Color.White);
            }
            #endregion 
            #region coop
            if (actualType == MenuType.coop)
            {
                string str = "";
               if (!Ressource.parameter[2])
                    {
                        str = Ressource.MenuString["Coop expl"].Item1;

                    }
                    else
                    {
                        str = Ressource.MenuString["Coop expl"].Item2;
                    }
               spriteBatch.DrawString(Ressource.MenuPolice, str, new Vector2(Ressource.screenWidth / 2 - Ressource.MenuPolice.MeasureString(str).Length() / 2, 250), Color.White);
            }
            #endregion
            #region reseaulost
            if (actualType == MenuType.reseaulost)
            {
                string str = "";
                if (!Ressource.parameter[2])
                {
                    str = Ressource.MenuString["Connection perdue"].Item1;

                }
                else
                {
                    str = Ressource.MenuString["Connection perdue"].Item2;
                }
                spriteBatch.DrawString(Ressource.MenuPolice, str, new Vector2(Ressource.screenWidth / 2 - Ressource.MenuPolice.MeasureString(str).Length() / 2, 250), Color.White);
            }
            #endregion
        }

        public void Update(MouseState mouse, KeyboardState keyboard, ref Menu menu, bool sizeChanged)
        {

            //Musique
            if (Ressource.parameter[0] && MediaPlayer.State != MediaState.Playing)
            {
                MediaPlayer.Play(Ressource.song);
                MediaPlayer.Volume = 1;
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

            if (setIp != null)
            {
                setIp.Update(mouse, ref menu);
            }

        }

    }
}