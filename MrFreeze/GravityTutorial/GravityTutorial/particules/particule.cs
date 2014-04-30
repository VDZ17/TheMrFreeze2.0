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
        public static Dictionary<string, ParticleEffect> particleEffects = new Dictionary<string, ParticleEffect>();
        public static Renderer particleRenderer;

        public particule()
        {
            particleEffects = new Dictionary<string, ParticleEffect>(); 
            particleRenderer = new SpriteBatchRenderer
            {
                GraphicsDeviceService = Game1.graphics_particle
            };
        }

        public void LoadContent(ContentManager content)
        {
            foreach (KeyValuePair<string, ParticleEffect> effectPair in particleEffects)
            {
                effectPair.Value.LoadContent(content);
                effectPair.Value.Initialise();
            }
            particleRenderer.LoadContent(content);
        }

        public void Update(GameTime gameTime)
        {
            foreach (KeyValuePair<string, ParticleEffect> effectPair in particleEffects)
            {
                //effectPair.Value.Initialise();
                effectPair.Value.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
            }
        }

        public void Draw()
        {
            foreach (KeyValuePair<string, ParticleEffect> effectPair in particleEffects)
            {
                particleRenderer.RenderEffect(effectPair.Value);
            }
        }

    }
}
