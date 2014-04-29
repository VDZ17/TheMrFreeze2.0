using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GravityTutorial.InGame_Jeu
{
    enum Direction1
    {
        Right, Left
    };
    enum State1
    {
        Spawning, Stop, Running, Hitting, Jumping, Bonus, Rolling
    };

    public class Ennemy1
    {

        //DEFINITION
        private Texture2D texture;

        public Vector2 position;
        public Vector2 velocity;

        Direction1 direction;

        //GRAPHICS
        int nbSprites;
        int height;
        int Timer;
        int width;


        //ANIMATION
        int frameCollumn;
        int animationSpeed;
        SpriteEffects Effect;

        //HITBOX
        public Rectangle rectangle;
        State1 state;



        public Ennemy1(Texture2D newTexture, Vector2 newPosition)
        {
            this.state = State1.Spawning;
            this.nbSprites = 10;
            this.frameCollumn = 1;
            this.animationSpeed = 10;
            this.height = 64;
            this.width = 67;
            this.position = newPosition;
            this.texture = newTexture;
            this.Timer = 0;
        }



        public void Update(GameTime gameTime)
        {
            rectangle = new Rectangle((int)position.X, (int)position.Y, width, height);
            //updatePositionX();
            updateHitbox();
            Animate();
            //updatePositionY();

        }

        //Methods for position

        void updatePositionY()
        {
            this.velocity.Y += 0.15f;
            this.position.Y += this.velocity.Y;
        }
        void updatePositionX()
        {

        }

        public void Animate()
        {
                this.Timer++;
                if (this.Timer == this.animationSpeed)
                {
                    this.Timer = 0;
                    this.frameCollumn++;
                    if (this.frameCollumn >= this.nbSprites)
                    {
                        this.frameCollumn = 1;
                    }
                }
        }



        //methods for animation
        public void Collision(Rectangle newRectangle, string name)
        {


        }

        void updateDirection()
        {
            if (this.direction == Direction1.Right)
                this.Effect = SpriteEffects.None;
            else if (this.direction == Direction1.Left)
                this.Effect = SpriteEffects.FlipHorizontally;
        }
        void updateHitbox()
        {
            if (this.state == State1.Spawning)
            {
                this.width = 67;
                this.height = 64;
                this.nbSprites = 10;
                this.animationSpeed = 6;
            }
            else if (this.state == State1.Stop)
            {
                this.width = 67;
                this.height = 56;
                this.nbSprites = 1;
                this.animationSpeed = 10;
            }
            else if (this.state == State1.Running)
            {
                this.width = 93;
                this.height = 57;
                this.nbSprites = 11;
                this.animationSpeed = 6;
            }
            else if (this.state == State1.Jumping)
            {
                this.width = 76;
                this.height = 93;
                this.nbSprites = 6;
                this.animationSpeed = 8;
            }
            else if (this.state == State1.Hitting)
            {
                this.width = 77;
                this.height = 85;
                this.nbSprites = 6;
                this.animationSpeed = 8;
            }
            else if (this.state == State1.Bonus)
            {
                this.width = 81;
                this.height = 76;
                this.nbSprites = 12;
                this.animationSpeed = 6;
            }
            else if (this.state == State1.Rolling)
            {
                this.width = 77;
                this.height = 53;
                this.nbSprites = 9;
                this.animationSpeed = 5;
            }


        }

        public void hit(Character player)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (this.state == State1.Spawning)
                spriteBatch.Draw(this.texture, rectangle, new Rectangle((this.frameCollumn - 1) * width, 0, width, height), Color.White, 0f, new Vector2(0, 0), this.Effect, 0f);

        }


    }

}
