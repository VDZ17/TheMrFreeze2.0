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

        //CONSTRUCTOR
        public igloo(Vector2 position) :
            base(position, Ressource.igloo, Bonus.Type.Igloo)
        {
            has_collide = false;
            this.position = position;
        }

        //DRAW UPDATE
        public override void Update(Character player, Hud score)
        {
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
            spriteBatch.Draw(texture, position, Color.White);
        }
    
    }
}
