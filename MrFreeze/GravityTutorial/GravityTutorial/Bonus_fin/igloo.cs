using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GravityTutorial
{
    public class igloo : Bonus
    {
        //FIELDS
        bool has_collide;
        public Vector2 position;
        int Timer;
        int animationSpeed;
        int FrameColumn;
        Rectangle hitbox_igloo;

        //CONSTRUCTOR
        public igloo(Vector2 position) :
            base(position, Ressource.igloo, Bonus.Type.Igloo)
        {
            Timer = 0;
            animationSpeed = 6;
            has_collide = false;
            this.position = position;
            FrameColumn = 1;
            hitbox_igloo = new Rectangle((int)position.X,(int)position.Y,35,34);
        }

        public void animate()
        {
            this.Timer++;
            if (this.Timer == this.animationSpeed)
            {
                this.Timer = 0;
                this.FrameColumn++;
                if (this.FrameColumn >= 6)
                {
                    this.FrameColumn = 1;
                }
            }
        }
        //DRAW UPDATE
        public override void Update(Character player, Hud score)
        {
            animate();
            particule.particleEffects["BeamMeUp"].Trigger(new Vector2((int)position.X + Camera.Transform.Translation.X,(int)position.Y + Camera.Transform.Translation.Y));
            if (player.rectangle.Collide_object(hitbox) && !hasBeenTaken)
            {
                has_collide = true;
                Game1.inGame = false;
                Game1.menu = Game1.menu.ChangeMenu(Menu.MenuType.win);
                Hud.youlose = true;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.texture, hitbox_igloo, new Rectangle((this.FrameColumn - 1) * 35, 0, 35, 34), Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0f);
        }
    
    }
}
