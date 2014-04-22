using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace GravityTutorial
{
    public class Bonus
    {
        public Vector2 position;
        public Texture2D texture;
        public Rectangle hitbox;
        public Type type;
        public bool hasBeenTaken;

        public enum Type 
        { 
            Gold, Igloo,
        }

        public Bonus(Vector2 pos, Texture2D texture, Type type)
        {
            this.position = pos;
            hitbox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            this.texture = texture;
            this.type = type;
            hasBeenTaken = false;
        }

        //UPDATE & DRAW
        public virtual void Update(Character player, Hud score)
        {
            
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            
        }
    }
}
