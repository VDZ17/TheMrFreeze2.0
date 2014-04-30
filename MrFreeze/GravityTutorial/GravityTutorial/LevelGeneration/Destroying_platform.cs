using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace GravityTutorial
{
    public class Destroying_platform
    {
        Texture2D texture;
        Vector2 position;
        int right;
        int left;
        bool going_right;
        public Rectangle hitbox;
        bool complet;
        bool fissure;
        public bool detruit;

        public Destroying_platform(Vector2 position, Texture2D texture)
        {
            complet = true;
            fissure = false;
            detruit = false;

            this.texture = texture;
            this.position = position;
            right = (int)position.X + 40;
            left = (int)position.X - 40;
            going_right = false;
            hitbox = new Rectangle((int)position.X, (int)position.Y, 50, 50);
        }

        public void update(Bullet bullet)
        {
            if (complet)
            {
                if (bullet.hitbox_bullet.Collide_object(hitbox))
                {
                    texture = Ressource.fissure;
                    fissure = true;
                    complet = false;
                    return;
                }
            }
            else if (fissure)
            {
                if (bullet.hitbox_bullet.Collide_object(hitbox))
                {
                    fissure = false;
                    detruit = true;
                    return;
                }
            }
        }

        public void Draw(SpriteBatch spritebatch)
        {
            if (!(detruit))
            {
                spritebatch.Draw(texture, new Rectangle((int)position.X, (int)position.Y, 50, 50), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0);
            }
        }

    }
}
