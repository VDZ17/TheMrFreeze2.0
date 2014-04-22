using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using ProjectMercury.Emitters;
using ProjectMercury.Modifiers;
using ProjectMercury.Renderers;
using ProjectMercury; 

namespace GravityTutorial
{
    public class particule
    {
        ParticleEffect p_effect;
        Renderer sr;
        public static List<particule> particules_list = new List<particule>{};
        Vector2 position;

        public particule(ParticleEffect effect, Vector2 position)
        {
            p_effect = effect;
            this.sr = Game1.particleRenderer;
            this.position = position;
        }
        public void Update(GameTime gameTime)
        {
            p_effect.Trigger(position);
            float SecondsPassed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            p_effect.Update(SecondsPassed); 
        }

        public void Draw(Matrix trans)
        {
            sr.RenderEffect(p_effect, ref trans);
        }

    }
}
