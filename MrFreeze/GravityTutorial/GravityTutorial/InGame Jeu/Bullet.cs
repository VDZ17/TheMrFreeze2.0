using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace GravityTutorial
{
    public class Bullet
    {
        public Texture2D texture;
        public Rectangle hitbox_bullet;

        public Vector2 position;
        public Vector2 velocity;
        public Vector2 origin;

        public bool IsVisible;
        int Timer;
        int AnimationSpeed;
        int frameLine;
        int frameCollumn;
        int nbr_sprite;
        int height = 55;
        int width = 62;

        public Bullet(Texture2D newTexture, Vector2 newVelocity)
        {
            velocity = newVelocity;
            texture = newTexture;
            IsVisible = false;
            Timer = 0;
            nbr_sprite = 10;
            frameLine = 0;
            AnimationSpeed = 100;
            frameCollumn = 0;
        }

        public void animate_bullet()
        {
            this.Timer++;
            if (this.Timer == this.AnimationSpeed)
            {
                this.Timer = 0;
                if (this.frameCollumn >= this.nbr_sprite)
                {
                    this.frameCollumn = this.nbr_sprite;
                }
                else
                {
                    this.frameCollumn++;
                }
            }
        }

        public void Update(CollisionTiles Tile)
        {
            animate_bullet();
            hitbox_bullet = new Rectangle((int)position.X, (int)position.Y, width, height);
            if (this.hitbox_bullet.Collide_object(Tile.Rectangle))
            {
                this.IsVisible = false;
            }
        }



        public void Draw(SpriteBatch spritBatch, SpriteEffects effect)
        {
            spritBatch.Draw(this.texture, hitbox_bullet, new Rectangle((this.frameCollumn - 1) * width, 0, width, height), Color.White, 0f, new Vector2(0, 0), effect, 0f);
        }
    }
}
