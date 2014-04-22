using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GravityTutorial
{
    public class gold : Bonus
    {
        //FIELDS
        int nbPoint = 10;

        //CONSTRUCTOR
        public gold(Vector2 position) :
            base(position, Ressource.Gold, Bonus.Type.Gold)
        { 
        }

        //DRAW UPDATE
        public override void Update(Character player, Hud score)
        {
            if (player.rectangle.Collide_object(hitbox) && !hasBeenTaken)
            {
                score.score += nbPoint;
                hasBeenTaken = true;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!hasBeenTaken)
            {
                spriteBatch.Draw(texture, position, Color.White);
            }
            
        }
    }
}
