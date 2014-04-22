using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GravityTutorial.InGame_Jeu
{
    enum Direction
    {
        Right, Left
    };
    enum State
    {
        Walking, Taking
    };

    public class Ennemy3
    {

        //DEFINITION
        Texture2D texture;

        public Vector2 position;
        public Vector2 velocity;

        Direction direction;

        //GRAPHICS
        int nbSprites;
        int height;
        int width;


        //ANIMATION
        int frameCollumn;
        int Timer;
        int animationSpeed;
        SpriteEffects Effect;

        //HITBOX
        public Rectangle rectangle;
        State state;
        public bool hasHit;
        int timerHit;
        int speedHit;



        public Ennemy3(Texture2D newTexture, Vector2 newPosition)
        {
            texture = newTexture;
            position = newPosition;
            this.frameCollumn = 1;
            this.height = 77;
            this.width = 55;
            this.nbSprites = 9;
            this.velocity.X = 3f;
            this.direction = Direction.Right;
            this.Timer = 0;
            this.animationSpeed = 5;
            this.hasHit = false;

            this.speedHit = 500;
            this.timerHit = 0;
        }



        public void Update(GameTime gameTime)
        {

            //Moves
            updatePositionX();
            updatePositionY();
            this.Animate();

            //Update the hitbox
            rectangle = new Rectangle((int)position.X, (int)position.Y, width, height);

            //Update the state (animation)
            updateHitbox(state);
        }

        //Methods for position
        public void Patrol(Rectangle block, string name)
        {
            Rectangle anticipation = new Rectangle((int)this.position.X + (int)this.velocity.X, (int)position.Y + (int)this.velocity.Y, this.height, this.width);
            if (anticipation.isOnLeftOf(block))
            {
                //Obstacle at the right : change the direction of the patrol
                direction = Direction.Left;
                updateDirection(direction);
                this.frameCollumn = 1;
                this.velocity.X = -3f;

            }
            else if (anticipation.isOnRightOf(block))
            {
                //Obstacle at the left : change the direction of the patrol
                direction = Direction.Right;
                updateDirection(direction);
                this.frameCollumn = 1;
                this.velocity.X = 3f;
            }
        }
        void updatePositionY()
        {
            this.velocity.Y += 0.15f;
            this.position.Y += this.velocity.Y;
        }
        void updatePositionX()
        {
            if (state == State.Walking)
                this.position.X += this.velocity.X;
        }

        public void Animate()
        {
            if (state == State.Walking)
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
            else if (state == State.Taking)
            {
                this.Timer++;
                if (this.Timer == this.animationSpeed)
                {
                    this.Timer = 0;
                    this.frameCollumn++;

                    if (this.frameCollumn >= this.nbSprites)
                        this.frameCollumn = nbSprites;
                }
            }
        }



        //methods for animation
        public void Collision(Rectangle newRectangle, string name)
        {
            if (name == "Tile1" || name == "Tile2" || name == "Tile12")
            {
                if (rectangle.isOnTopOf(newRectangle))
                {
                    rectangle.Y = newRectangle.Y - rectangle.Height;
                    velocity.Y = 0;
                    velocity.Y += -0.15f;
                }
                if (rectangle.isOnLeftOf(newRectangle))
                {
                    position.X = newRectangle.X - rectangle.Width;
                }

                if (rectangle.isOnRightOf(newRectangle))
                {
                    position.X = newRectangle.X + newRectangle.Width;
                }
            }
        }
        void updateDirection(Direction direction)
        {
            switch (direction)
            {
                case Direction.Right:
                    this.Effect = SpriteEffects.None;
                    break;
                case Direction.Left:
                    this.Effect = SpriteEffects.FlipHorizontally;
                    break;
                default:
                    this.Effect = SpriteEffects.None;
                    break;
            }
        }
        void updateHitbox(State state)
        {
            if (state == State.Walking)
            {
                this.height = 77;
                this.width = 55;
                this.nbSprites = 9;
            }

            else if (state == State.Taking)
            {
                this.height = 73;
                this.width = 82;
                this.nbSprites = 4;
            }
        }

        //Hit
        public void hit(Character player)
        {
            if (this.rectangle.Intersects(player.rectangle))
            {
                state = State.Taking;
                updateHitbox(state);
                hasHit = true;
                player.position.X = position.X;
            }
            //else
            //{
            //    state = State.Walking;
            //    updateHitbox(state);
            //    hasHit = false;
            //}

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (state == State.Walking)
            {
                spriteBatch.Draw(this.texture, rectangle, new Rectangle((this.frameCollumn - 1) * width, 0, width, height), Color.White, 0f, new Vector2(0, 0), this.Effect, 0f);
            }
            else if (state == State.Taking)
            {
                spriteBatch.Draw(this.texture, rectangle, new Rectangle((this.frameCollumn - 1) * width, 77+1, width, height), Color.White, 0f, new Vector2(0, 0), this.Effect, 0f);
            }

        }


    }

}
