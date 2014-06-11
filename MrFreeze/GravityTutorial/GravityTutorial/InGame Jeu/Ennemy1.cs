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


        //Attributs
        int bonusTimer;
        int bonusTimerFrequency;
        int timerHitting;
        int timerHittingFrequency;
        bool rollingHit;
        public int life;
        bool hasJumped;
        int jumpTimer;
        int jumpTimerFrequency;


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
            this.bonusTimerFrequency = 1000;
            this.rollingHit = false;
            this.timerHitting = 0;
            this.timerHittingFrequency = 16;
            this.life = 20;
            this.hasJumped = false;
            this.jumpTimerFrequency = 200;
            this.jumpTimer = 0;
        }

        public void Update(GameTime gameTime, Character player)
        {
            rectangle = new Rectangle((int)position.X, (int)position.Y, width, height);


            //update position
            updatePositionY();
            updatePositionX();


            if (this.state != State1.Rolling && this.state != State1.Spawning && this.state != State1.Jumping && this.state != State1.Hitting && this.state != State1.Bonus)
            {
                Reaching(player);
            }

            jumpTimer++;

            if (this.state != State1.Rolling)
            {
                Hitting(player);
            }


            if (this.state == State1.Rolling && this.rectangle.Intersects(player.rectangle) && this.rollingHit == false && player.CurrentItem != Item.Type.Invincibility)
            {
                this.rollingHit = true;
                player.life_changment = -100;
            }

            Bonus();
            Animate();
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
            #region SPAWNING
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
                        this.velocity.X = 0;
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
            #region JUMPING
            else if (this.state == State1.Jumping)
            {
                this.Timer++;
                if (this.Timer >= this.animationSpeed)
                {
                    this.Timer = 0;
                    this.frameCollumn++;
                    if (this.frameCollumn >= this.nbSprites)
                    {
                        this.frameCollumn = 1;
                        this.state = State1.Running;
                        updateHitbox();
                    }
                }

            }
            #endregion
        }

        void Hitting(Character player)
        {
            Rectangle hitboxHit = new Rectangle((int)this.position.X, (int)this.position.Y, 77, 85);

            if (player.rectangle.Intersects(hitboxHit))
            {
                this.state = State1.Hitting;
                updateHitbox();
                this.velocity.X = 0;
            }
            if (this.state == State1.Hitting && !(player.rectangle.Intersects(hitboxHit)))
            {
                this.state = State1.Stop;
                updateHitbox();
            }

            if (this.state == State1.Hitting && player.rectangle.Intersects(hitboxHit) && player.CurrentItem != Item.Type.Invincibility)
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
            if (name == "Tile1" || name == "Tile2" || name == "Tile5" || name == "Tile6" || name == "Tile16")
            {
                if (rectangle.isOnTopOf(newRectangle))
                {
                    if (this.state != State1.Jumping)
                    {
                        velocity.Y = 0;
                        position.Y = newRectangle.Y - this.height;
                    }
                    else
                    {
                        velocity.Y = 0;
                        velocity.Y += -0.15f;
                        rectangle.Y = newRectangle.Y - this.height;
                    }
                }

                else if (rectangle.isOnLeftOf(newRectangle))
                {
                    if (this.velocity.X > 0)
                    {
                        position.X = newRectangle.X - rectangle.Width;
                    }
                    if (this.state != State1.Spawning && this.state != State1.Rolling && this.state != State1.Bonus && this.jumpTimer >= this.jumpTimerFrequency)
                        Jump();
                }


                else if (rectangle.isOnRightOf(newRectangle))
                {
                    if (this.velocity.X < 0)
                    {
                        position.X = newRectangle.X + newRectangle.Width;
                    }
                    if (this.state != State1.Spawning && this.state != State1.Rolling && this.state != State1.Bonus && this.jumpTimer >= this.jumpTimerFrequency)
                        Jump();
                }
                else if (rectangle.isOnBotOf(newRectangle))
                {
                    if (velocity.Y < 0)
                    {
                        velocity.Y = -velocity.Y;
                    }
                    position.Y = newRectangle.Bottom + velocity.Y;
                    updatePositionY();
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
        }

        void Bonus()
        {
            bonusTimer++;
            if (bonusTimerFrequency <= bonusTimer && (this.state == State1.Stop || this.state == State1.Running))
            {
                this.velocity.X = 0;
                bonusTimer = 0;
                this.state = State1.Bonus;
                updateHitbox();
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
                this.animationSpeed = 12;
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
                this.animationSpeed = 10;
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

        void Jump()
        {
            frameCollumn = 1;
            this.position.Y += -45;
            this.velocity.Y = -5;
            this.state = State1.Jumping;
            updateHitbox();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (this.state == State1.Spawning)
                spriteBatch.Draw(this.texture, rectangle, new Rectangle((this.frameCollumn - 1) * width, 0, width, height), Color.White, 0f, new Vector2(0, 0), this.Effect, 0f);
            else if (this.state == State1.Stop)
                spriteBatch.Draw(this.texture, rectangle, new Rectangle((this.frameCollumn - 1) * width, 64, width, height), Color.White, 0f, new Vector2(0, 0), this.Effect, 0f);
            else if (this.state == State1.Running)
                spriteBatch.Draw(this.texture, rectangle, new Rectangle((this.frameCollumn - 1) * width, 64 + 57, width, height), Color.White, 0f, new Vector2(0, 0), this.Effect, 0f);
            else if (this.state == State1.Bonus)
                spriteBatch.Draw(this.texture, rectangle, new Rectangle((this.frameCollumn - 1) * width, 64 + 57 + 57 + 85 + 93, width, height), Color.White, 0f, new Vector2(0, 0), this.Effect, 0f);
            else if (this.state == State1.Rolling)
                spriteBatch.Draw(this.texture, rectangle, new Rectangle((this.frameCollumn - 1) * width, 489 - 53, width, height), Color.White, 0f, new Vector2(0, 0), this.Effect, 0f);
            else if (this.state == State1.Hitting)
                spriteBatch.Draw(this.texture, rectangle, new Rectangle((this.frameCollumn - 1) * width, 64 + 57 + 57, width, height), Color.White, 0f, new Vector2(0, 0), this.Effect, 0f);
            else if (this.state == State1.Jumping)
                spriteBatch.Draw(this.texture, rectangle, new Rectangle((this.frameCollumn - 1) * width, 489 - 53 - 76 - 93 - 3, width, height - 1), Color.White, 0f, new Vector2(0, 0), this.Effect, 0f);
            else
                spriteBatch.Draw(this.texture, rectangle, new Rectangle((this.frameCollumn - 1) * width, 0, width, height), Color.White, 0f, new Vector2(0, 0), this.Effect, 0f);

        }
    }

}
