using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GravityTutorial
{

    public class Pseudo : Button
    {
        string newPseudo;
        string newPseudoMsg;
        string currentPseudoMsg;

        int maxCount;
        int cursorCount;
        int currentCount;

        KeyboardState previousKeyboard;

        //CONSTRUCTOR
        public Pseudo(Vector2 pos) :
            base(pos, Ressource.MenuString["Valider"])
        {
            newPseudo = "";
            previousKeyboard = Keyboard.GetState();

            maxCount = 60;
            cursorCount = 40;
            currentCount = 0;

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
            //CURSOR
            currentCount++;
            string cursor = "";
            if (newPseudo.Length > 0 && newPseudo[newPseudo.Length - 1] == '|')
            {
                newPseudo = newPseudo.Remove(newPseudo.Length - 1);
            }

            if (currentCount > maxCount)
            {
                currentCount = 0;
            }

            if (currentCount < cursorCount)
            {
                cursor = "|";
            }


            //VALIDATE
            if (((mouse.LeftButton == ButtonState.Pressed
                && mouse.X >= pos.X
                && mouse.X <= pos.X + SpriteWidth
                && mouse.Y >= pos.Y
                && mouse.Y <= pos.Y + SpriteHeight
                && menu.cooldown) || Keyboard.GetState().IsKeyDown(Keys.Enter))
                && newPseudo.Length >= 3)
            {
                Ressource.pseudo = newPseudo;
                newPseudo = "";
                menu.cooldown = false;
            }

            //CHECK KEYBOARD
            KeyboardState keyboard = Keyboard.GetState();

            if (keyboard != previousKeyboard)
            {


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
            newPseudo = newPseudo + cursor;
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            spriteBatch.DrawString(Ressource.MenuPolice, currentPseudoMsg + Ressource.pseudo, new Vector2(Ressource.screenWidth / 2 - Ressource.MenuPolice.MeasureString(currentPseudoMsg + Ressource.pseudo).Length() / 2, pos.Y - 300), Color.White);

            spriteBatch.DrawString(Ressource.MenuPolice, newPseudoMsg, new Vector2(Ressource.screenWidth / 2 - Ressource.MenuPolice.MeasureString(newPseudoMsg).Length() / 2, pos.Y - 200), Color.White);

            spriteBatch.Draw(Ressource.TextBox, new Rectangle((int)pos.X + 25, (int)pos.Y - 100, 450, 60), Color.White);
            spriteBatch.DrawString(Ressource.MenuPolice, newPseudo, new Vector2(pos.X + 50, pos.Y - 110), Color.White);

            spriteBatch.Draw(Ressource.Button, this.hitbox, new Rectangle(0, 0, this.SpriteWidth, this.SpriteHeight), color);
            spriteBatch.DrawString(Ressource.MenuPolice, Text, new Vector2(pos.X + 95, pos.Y), Color.White);
        }
    }
}
