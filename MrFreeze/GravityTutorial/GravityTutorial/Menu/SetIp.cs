using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GravityTutorial
{

    public class SetIp : Button
    {
        string ip;
        string currentIp;

        int maxCount;
        int cursorCount;
        int currentCount;

        KeyboardState previousKeyboard;

        //CONSTRUCTOR
        public SetIp(Vector2 pos) :
            base(pos, Ressource.MenuString["Changer"])
        {
            ip = "";
            previousKeyboard = Keyboard.GetState();

            maxCount = 60;
            cursorCount = 40;
            currentCount = 0;

            if (Ressource.parameter[2])
            {
                currentIp = "IP to join : ";
            }
            else
            {
                currentIp = "IP a rejoindre : ";
            }
        }

        //DRAW & UPDATE
        public void Update(MouseState mouse, ref Menu menu)
        {
            //CURSOR
            currentCount++;
            string cursor = "";
            if (ip.Length > 0 && ip[ip.Length - 1] == '|')
            {
                ip = ip.Remove(ip.Length - 1);
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
                && ip.Length >= 10)
            {
                Ressource.ipJ2 = ip;
                ip = "";
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
                        if (k >= Keys.NumPad0 && k <= Keys.NumPad9)
                        {
                            ip += (char)(k - (int)Keys.NumPad0 + (int)'0');
                        }
                        if (k == Keys.Decimal || k == Keys.OemPeriod)
                        {
                            ip += '.';
                        }

                        if (k == Keys.Back && ip.Length > 0)
                        {
                            ip = ip.Remove(ip.Length - 1);
                        }
                    }
                }

                previousKeyboard = keyboard;
            }
            ip = ip + cursor;
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            spriteBatch.DrawString(Ressource.MenuPolice, currentIp + Ressource.ipJ2, new Vector2(Ressource.screenWidth / 2 - Ressource.MenuPolice.MeasureString(currentIp + Ressource.ipJ2).Length() / 2, pos.Y - 200), Color.White);

            spriteBatch.Draw(Ressource.TextBox, new Rectangle((int)pos.X + 25, (int)pos.Y - 100, 450, 60), Color.White);
            spriteBatch.DrawString(Ressource.MenuPolice, ip, new Vector2(pos.X + 50, pos.Y - 110), Color.White);

            spriteBatch.Draw(Ressource.Button, this.hitbox, new Rectangle(0, 0, this.SpriteWidth, this.SpriteHeight), color);
            spriteBatch.DrawString(Ressource.MenuPolice, Text, new Vector2(pos.X + 95, pos.Y), Color.White);
        }
    }
}
