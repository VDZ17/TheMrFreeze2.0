using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace GravityTutorial
{
    public class Tile
    {
        public string Tile_name;
        public Vector2 position_tile;
        protected Texture2D texture;
        private Rectangle rectangle;
        public Rectangle Rectangle
        {
            get { return rectangle; }  
            protected set {rectangle = value;}
        }
        private static ContentManager content;
        public static ContentManager Content
        {
            protected get { return content; }
            set { content = value; }
        }

        public Tile()
        {

        }

        public void Draw(SpriteBatch spritBatch)
        {
            spritBatch.Draw(texture, rectangle, null, Color.White,0f,Vector2.Zero,SpriteEffects.None,0);
        }
    }
    
    public class CollisionTiles : Tile
    {
        public CollisionTiles(int i, Rectangle newRectangle)
        {
            Tile_name = ("Tile" + i);
            position_tile = new Vector2(newRectangle.X, newRectangle.Y);
            if (i != 12 && i != 13)
                texture = Content.Load<Texture2D>("Tile" + i);
            this.Rectangle = newRectangle;
        }
    }
}