using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace GravityTutorial
{
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
            else
            {
                ActualKey = "!!!";
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
}
