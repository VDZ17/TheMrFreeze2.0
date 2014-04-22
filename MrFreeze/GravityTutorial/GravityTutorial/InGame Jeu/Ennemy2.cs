using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GravityTutorial.InGame_Jeu
{
    public enum Direction2
    {
        Right, Left
    };
    enum State2
    {
        Waiting, hitting, hammering
    };

    public class Ennemy2
    {

        //DEFINITION
        Texture2D texture;

        public Vector2 position;
        public Vector2 velocity;

        public Direction2 direction;

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
        State2 state;


        //Attributs
        bool finalHit;
        public bool firstHit;


        public Ennemy2(Texture2D newTexture, Vector2 newPosition)
        {
            texture = newTexture;
            position = newPosition;
            this.frameCollumn = 1;
            this.height = 45;
            this.width = 40;
            this.nbSprites = 1;
            this.velocity.X = 0;
            this.velocity.Y = 0.15f;
            this.direction = Direction2.Left;
            this.Timer = 0;
            this.animationSpeed = 1;
            this.finalHit = false;
            this.firstHit = false;
        }



        public void Update(GameTime gameTime)
        {
            updatePositionY();
            //Update the hitbox
            rectangle = new Rectangle((int)position.X, (int)position.Y, width, height);
            Animate();

            updateDirection(this.direction, Level.Heroes.ElementAt(0));
        }



        public void Animate()
        {
            if (this.state == State2.Waiting)
            {
                this.Timer = 0;
                this.frameCollumn = 1;
            }
            else
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
        }
        void updatePositionY()
        {
            this.velocity.Y += 0.15f;
            this.position.Y += this.velocity.Y;
        }



        //methods for animation
        public void Collision(Rectangle newRectangle, string name)
        {
            if (name == "Tile1" || name == "Tile2")
            {
                if (rectangle.isOnTopOf(newRectangle))
                {
                    velocity.Y = 0;
                    velocity.Y += -0.15f;
                    rectangle.Y = newRectangle.Y - rectangle.Height;
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

        void updateDirection(Direction2 direction, Character megaman)
        {
            if (megaman.position.X > this.position.X)
                this.direction = Direction2.Right;

            else
                this.direction = Direction2.Left;


            switch (direction)
            {
                case Direction2.Right:
                    this.Effect = SpriteEffects.FlipHorizontally;
                    break;
                case Direction2.Left:
                    this.Effect = SpriteEffects.None;
                    break;
                default:
                    this.Effect = SpriteEffects.None;
                    break;
            }
        }

        void updateHitbox(State2 state)
        {
            if (state == State2.Waiting)
            {
                this.nbSprites = 1;
                this.height = 45;
                this.width = 40;
                this.animationSpeed = 1;
            }
            else if (state == State2.hitting)
            {
                this.nbSprites = 6;
                this.height = 54;
                this.width = 81;
                this.animationSpeed = 5;
            }
            else if (state == State2.hammering)
            {
                this.nbSprites = 8;
                this.height = 79;
                this.width = 73;
                this.animationSpeed = 5;
            }

        }

        //Hit

        int timerAttack1 = 0;
        int speedAttack1 = 45;

        public void hit(Character player)
        {
            if (this.direction == Direction2.Right)
            {
                if (player.position.X < this.position.X + 50 && player.position.Y + player.player_Height > this.position.Y && finalHit == false)
                {
                    this.state = State2.hitting;
                    updateHitbox(state);
                    this.firstHit = true;
                    this.finalHit = true;
                }
            }
            else if (this.direction == Direction2.Left)
            {
                if ( player.position.X > this.position.X - 50 && player.position.Y + player.player_Height > this.position.Y && finalHit == false)
                {
                    this.state = State2.hitting;
                    updateHitbox(state);
                    this.firstHit = true;
                    this.finalHit = true;
                }
            }
            if (firstHit)
            {
                timerAttack1++;
                if (timerAttack1 >= speedAttack1)
                {
                    timerAttack1 = 0;
                    firstHit = false;
                    this.state = State2.Waiting;
                    updateHitbox(state);
                }
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (this.state == State2.Waiting)
                spriteBatch.Draw(this.texture, rectangle, new Rectangle((this.frameCollumn - 1) * width, 0, width, height), Color.White, 0f, new Vector2(0, 0), this.Effect, 0f);
            else if (this.state == State2.hitting)
                spriteBatch.Draw(this.texture, rectangle, new Rectangle((this.frameCollumn - 1) * width, 45 + 79 - 3, width, height), Color.White, 0f, new Vector2(0, 0), this.Effect, 0f);

        }

    }

}
