using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace GravityTutorial
{
    public class health
    {
        // Healthbar
        public Texture2D texture_life;
        public Vector2 position_life;
        public Rectangle rectangle_life;

        public health()
        {
            texture_life = Ressource.healthbar;
            position_life = new Vector2(150, 150);
            rectangle_life = new Rectangle(0, 0, texture_life.Width, texture_life.Height);
        }

        public void update(int damage)
        {
            if (damage < 0)
            {
                rectangle_life.Width -= damage;
            }
            else
            {
                for (int i = 0; i < damage; i++)
                {
                    if (rectangle_life.Width >= texture_life.Width)
                    {
                        break;
                    }
                    rectangle_life.Width++;
                }
            }

        }
        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture_life, new Vector2(150, 150), rectangle_life, Color.White);
        }
    }
}
