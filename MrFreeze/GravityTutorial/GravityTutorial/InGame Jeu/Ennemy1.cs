﻿using System;
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


        //Attributs
        int bonusTimer;
        int bonusTimerFrequency;
        int timerHitting;
        int timerHittingFrequency;
        bool rollingHit;
        public int life;

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
            this.bonusTimer = 0;
            this.bonusTimerFrequency = 400;
            this.rollingHit = false;
            this.timerHitting = 0;
            this.timerHittingFrequency = 16;
            this.life = 10;
        }

        public void Update(GameTime gameTime)
        {
            rectangle = new Rectangle((int)position.X, (int)position.Y, width, height);

            //Is running after the character
            if (this.state != State1.Bonus && this.state != State1.Rolling && this.state != State1.Hitting)
                Reaching(Level.Heroes[0]);

            //Damages while Rolling
            if (this.state == State1.Rolling)
            {
                if (this.rectangle.Intersects(Level.Heroes[0].rectangle) && rollingHit == false)
                {
                    rollingHit = true;
                    Level.Heroes[0].life_changment = -100;
                }
            }

            if (this.state != State1.Rolling)
                Hitting(Level.Heroes[0]);

            updatePositionY();
            updatePositionX();
            Bonus();
            Animate();
            Console.WriteLine(this.state);

        }

        void updatePositionY()
        {
            this.velocity.Y += 0.15f;
            this.position.Y += this.velocity.Y;
        }

        void updatePositionX()
        {
            this.position.X += this.velocity.X;
        }

        void Animate()
        {
            #region SPAWN
            if (this.state == State1.Spawning)
            {
                this.Timer++;
                if (this.Timer >= this.animationSpeed)
                {
                    this.Timer = 0;
                    this.frameCollumn++;
                    if (this.frameCollumn >= this.nbSprites)
                    {
                        this.frameCollumn = 1;
                        this.state = State1.Stop;
                        updateHitbox();
                    }
                }
            }
            #endregion
            #region RUNNING
            else if (this.state == State1.Running)
            {
                this.Timer++;
                if (this.Timer >= this.animationSpeed)
                {
                    this.Timer = 0;
                    this.frameCollumn++;
                    if (this.frameCollumn >= this.nbSprites)
                    {
                        this.frameCollumn = 2;
                    }
                }
            }
            #endregion
            #region STOP
            else if (this.state == State1.Stop)
            {
                this.Timer++;
                if (this.Timer >= this.animationSpeed)
                {
                    this.Timer = 0;
                    this.frameCollumn++;
                    if (this.frameCollumn >= this.nbSprites)
                    {
                        this.frameCollumn = 1;
                    }
                }
            }
            #endregion
            #region BONUS
            else if (this.state == State1.Bonus)
            {
                this.Timer++;
                if (this.Timer >= this.animationSpeed)
                {
                    this.Timer = 0;
                    this.frameCollumn++;
                    if (this.frameCollumn >= this.nbSprites)
                    {
                        this.frameCollumn = 1;
                        this.state = State1.Rolling;
                        updateHitbox();
                        Rollling();
                    }
                }
            }
            #endregion
            #region ROLLING
            else if (this.state == State1.Rolling)
            {
                this.Timer++;
                if (this.Timer >= this.animationSpeed)
                {
                    this.Timer = 0;
                    this.frameCollumn++;
                    if (this.frameCollumn >= this.nbSprites)
                    {
                        this.frameCollumn = 1;
                        this.state = State1.Stop;
                        this.position.Y += -10;
                        this.rollingHit = false;
                    }
                }
            }
            #endregion
            #region HITTING
            else if (this.state == State1.Hitting)
            {
                this.Timer++;
                if (this.Timer >= this.animationSpeed)
                {
                    this.Timer = 0;
                    this.frameCollumn++;
                    if (this.frameCollumn >= this.nbSprites)
                    {
                        this.frameCollumn = 1;
                    }
                }
            }

            #endregion
        }

        void Hitting(Character player)
        {
            //if (player.position.X + player.player_Width >= this.position.X && 
            //    player.position.X <= this.position.X + this.width)

            if (player.rectangle.Intersects(this.rectangle))
            {
                if (this.state != State1.Hitting)
                    this.position.Y += -23;

                this.state = State1.Hitting;
                updateHitbox();
            }

            if (this.state == State1.Hitting && !(player.position.X + player.player_Width >= this.position.X && player.position.X <= this.position.X + this.width))
            {
                this.position.Y += 23;
                this.state = State1.Stop;
                updateHitbox();
            }
            if (this.state == State1.Hitting)
            {
                timerHitting++;
                if (timerHitting >= timerHittingFrequency)
                {
                    timerHitting = 0;
                    player.life_changment += -10;
                }
            }
        }

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
                    if (this.velocity.X > 0)
                        position.X = newRectangle.X - rectangle.Width;
                }

                if (rectangle.isOnRightOf(newRectangle))
                {
                    if (this.velocity.X < 0)
                        position.X = newRectangle.X + newRectangle.Width;
                    this.velocity.X = 0;
                }
            }

        }

        void Reaching(Character player)
        {
            if (player.position.X + player.player_Width < this.position.X)
            {
                this.velocity.X = -2f;
                if (this.state != State1.Running)
                    this.state = State1.Running;
                updateDirection();
                updateHitbox();
            }
            else if (player.position.X > this.position.X + this.width)
            {
                this.velocity.X = 2f;
                if (this.state != State1.Running)
                    this.state = State1.Running;
                updateDirection();
                updateHitbox();
            }
            else
            {
                this.velocity.X = 0;
            }
        }

        void Bonus()
        {
            bonusTimer++;
            if (bonusTimerFrequency <= bonusTimer)
            {
                this.velocity.X = 0;
                bonusTimer = 0;
                if (this.state == State1.Hitting)
                    this.position.Y += -9;
                else if (this.state == State1.Running)
                    this.position.Y += -14;
                this.state = State1.Bonus;
                updateHitbox();
                this.Animate();
            }
        }

        void Rollling()
        {
            if (this.direction == Direction1.Left)
            {
                this.velocity.X = -7f;
            }
            else if (this.direction == Direction1.Right)
            {
                this.velocity.X = 7f;
            }
        }

        void updateDirection()
        {
            if (this.velocity.X > 0)
                this.direction = Direction1.Right;
            else if (this.velocity.X < 0)
                this.direction = Direction1.Left;

            if (this.direction == Direction1.Right)
                this.Effect = SpriteEffects.None;
            else if (this.direction == Direction1.Left)
                this.Effect = SpriteEffects.FlipHorizontally;
        }

        void updateHitbox()
        {
            #region SPAWN
            if (this.state == State1.Spawning)
            {
                this.width = 67;
                this.height = 64;
                this.nbSprites = 10;
                this.animationSpeed = 6;
            }
            #endregion
            #region STOP
            else if (this.state == State1.Stop)
            {
                this.width = 67;
                this.height = 56;
                this.nbSprites = 1;
                this.animationSpeed = 10;
            }
            #endregion
            #region RUNNING
            else if (this.state == State1.Running)
            {
                this.width = 93;
                this.height = 57;
                this.nbSprites = 11;
                this.animationSpeed = 6;
            }
            #endregion
            #region JUMPING
            else if (this.state == State1.Jumping)
            {
                this.width = 76;
                this.height = 93;
                this.nbSprites = 6;
                this.animationSpeed = 8;
            }
            #endregion
            #region HITTING
            else if (this.state == State1.Hitting)
            {
                this.width = 77;
                this.height = 85;
                this.nbSprites = 6;
                this.animationSpeed = 8;
            }
            #endregion
            #region BONUS
            else if (this.state == State1.Bonus)
            {
                this.width = 81;
                this.height = 76;
                this.nbSprites = 12;
                this.animationSpeed = 6;
            }
            #endregion
            #region ROLLING
            else if (this.state == State1.Rolling)
            {
                this.width = 77;
                this.height = 53;
                this.nbSprites = 9;
                this.animationSpeed = 8;
            }
            #endregion
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle fixY = this.rectangle;
            fixY.Y = this.rectangle.Y + 5;

            if (this.state == State1.Spawning)
                spriteBatch.Draw(this.texture, rectangle, new Rectangle((this.frameCollumn - 1) * width, 0, width, height), Color.White, 0f, new Vector2(0, 0), this.Effect, 0f);
            else if (this.state == State1.Stop)
                spriteBatch.Draw(this.texture, fixY, new Rectangle((this.frameCollumn - 1) * width, 64, width, height), Color.White, 0f, new Vector2(0, 0), this.Effect, 0f);
            else if (this.state == State1.Running)
                spriteBatch.Draw(this.texture, fixY, new Rectangle((this.frameCollumn - 1) * width, 64 + 57, width, height), Color.White, 0f, new Vector2(0, 0), this.Effect, 0f);
            else if (this.state == State1.Bonus)
                spriteBatch.Draw(this.texture, rectangle, new Rectangle((this.frameCollumn - 1) * width, 64 + 57 + 57 + 85 + 93, width, height), Color.White, 0f, new Vector2(0, 0), this.Effect, 0f);
            else if (this.state == State1.Rolling)
                spriteBatch.Draw(this.texture, rectangle, new Rectangle((this.frameCollumn - 1) * width, 489 - 53, width, height), Color.White, 0f, new Vector2(0, 0), this.Effect, 0f);
            else if (this.state == State1.Hitting)
                spriteBatch.Draw(this.texture, rectangle, new Rectangle((this.frameCollumn - 1) * width, 64 + 57 + 57, width, height), Color.White, 0f, new Vector2(0, 0), this.Effect, 0f);
        }
    }

}
