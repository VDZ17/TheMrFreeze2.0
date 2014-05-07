using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace GravityTutorial
{
    public class Item
    {
        public Vector2 position;

        public Texture2D texture;
        public Rectangle hitbox;

        public Type type;
        public bool hasBeenTaken;

        int nb_texture;
        int texture_size;

        public enum Type
        {
            Invincibility,
            DoubleJump,
            MoonJump,
            MultiShot,
            SuperSpeed,
            SlowSpeed,
            ReverseDirection,
            /*IceNova,
            Shield,
            TimeStop,*/
            None,
        }

        public Item(Vector2 pos, Texture2D texture, Type type, int nb_texture = 0)
        {
            this.position = pos;
            this.texture = texture;
            hitbox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            this.type = type;
            this.nb_texture = nb_texture;
            hasBeenTaken = false;
            texture_size = 70;
        }

        //UPDATE & DRAW
        public void Update(Character player, Hud score)
        {
            if (player.rectangle.Intersects(hitbox) && !hasBeenTaken)
            {
                player.CurrentItem = type;
                hasBeenTaken = true;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!hasBeenTaken)
            {
                spriteBatch.Draw(texture, position, new Rectangle((nb_texture - 1) * texture_size, 0, texture_size, texture_size), Color.White);
            }
        }
    }
}
