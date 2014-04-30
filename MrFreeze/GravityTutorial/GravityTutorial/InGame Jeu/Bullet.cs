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

        public bool explose;
        public bool IsVisible;
        int Timer;
        int AnimationSpeed;
        int frameLine;
        int frameCollumn;
        int nbr_sprite;
        int height = 55;
        int width = 62;
        SpriteEffects effect_stable;

        public Bullet(Texture2D newTexture, Vector2 newVelocity, SpriteEffects effect)
        {
            velocity = newVelocity;
            texture = newTexture;
            IsVisible = false;
            Timer = 0;
            nbr_sprite = 10;
            frameLine = 0;
            AnimationSpeed = 500;
            frameCollumn = 0;
            effect_stable = effect;
            explose = false;
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


        public void Update(Destroying_platform Tile)
        {
            animate_bullet();
            hitbox_bullet = new Rectangle((int)position.X, (int)position.Y, width, height);
            if (this.hitbox_bullet.isOnRightOf(Tile.hitbox) || this.hitbox_bullet.isOnLeftOf(Tile.hitbox))
            {
                this.IsVisible = false;
                if (Vector2.Distance(position, Level.Heroes[0].position) > 10 || Vector2.Distance(position, Level.Heroes[0].position) < 10)
                {
                    particule.particleEffects["BasicExplosion"].Trigger(new Vector2(hitbox_bullet.Center.X + Camera.Transform.Translation.X, hitbox_bullet.Center.Y + Camera.Transform.Translation.Y));
                }
            }
        }
        public void Update(moving_platform Tile)
        {
            animate_bullet();
            hitbox_bullet = new Rectangle((int)position.X, (int)position.Y, width, height);
            if (this.hitbox_bullet.isOnRightOf(Tile.hitbox) || this.hitbox_bullet.isOnLeftOf(Tile.hitbox))
            {
                this.IsVisible = false;
                if (Vector2.Distance(position, Level.Heroes[0].position) > 10 || Vector2.Distance(position, Level.Heroes[0].position) < 10)
                {
                        particule.particleEffects["BasicExplosion"].Trigger(new Vector2(hitbox_bullet.Center.X + Camera.Transform.Translation.X, hitbox_bullet.Center.Y + Camera.Transform.Translation.Y));
                }
            }
        }
        public void Update(CollisionTiles Tile)
        {
            animate_bullet();
            hitbox_bullet = new Rectangle((int)position.X, (int)position.Y, width, height);
            if (this.hitbox_bullet.isOnRightOf(Tile.Rectangle) || this.hitbox_bullet.isOnLeftOf(Tile.Rectangle))
            {
                this.IsVisible = false;
                if (Vector2.Distance(position, Level.Heroes[0].position) > 10 || Vector2.Distance(position, Level.Heroes[0].position) < 10)
                {
                        particule.particleEffects["BasicExplosion"].Trigger(new Vector2(hitbox_bullet.Center.X + Camera.Transform.Translation.X, hitbox_bullet.Center.Y + Camera.Transform.Translation.Y));

                }
            }
        }



        public void Draw(SpriteBatch spritBatch)
        {
            spritBatch.Draw(this.texture, hitbox_bullet, new Rectangle((this.frameCollumn - 1) * width, 0, width, height), Color.White, 0f, new Vector2(0, 0), effect_stable, 0f);
        }
    }
}
