using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GravityTutorial
{
    public class moving_platform
    {
        Texture2D texture;
        public Vector2 position;
        int right;
        int left;
        bool going_right;
        public Rectangle hitbox;

        public moving_platform(Vector2 position, Texture2D texture)
        {
            this.texture = texture;
            this.position = position;
            right = (int)position.X + 40;
            left = (int)position.X - 40;
            going_right = false;
            hitbox = new Rectangle((int)position.X,(int)position.Y,texture.Width, texture.Height);
        }

        public void update()
        {
            hitbox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            if (going_right)
            {
                position = new Vector2((int)position.X + 1, position.Y);
            }
            else
            {
                position = new Vector2((int)position.X - 1, position.Y);
            }
            if (position.X > right)
            {
                going_right = false;
            }
            if (position.X < left)
            {
                going_right = true;
            } 
        }

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(texture, new Rectangle((int)position.X, (int)position.Y,50,50), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0);
        }
    }
}
