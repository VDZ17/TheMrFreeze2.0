using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace GravityTutorial
{
    public enum Direction
    {
        Right,
        Left
    };

    public class Character
    {
        //VAR
        int nbr_sprite;
        int player_Height;
        int player_Width;
        bool spawn;
        bool jump;
        bool stop;
        bool attack;

        //HEALTH
        public int life_changment;

        //DEFINITION
        Texture2D texture;
        public Vector2 position;
        public Vector2 velocity;

        //SAUT
        public bool hasJumped;
        public bool hasJumped2;
        public bool hasDoubleJumped;
        public int saut = 6;
        bool cooldownDoubleJump;

        //HITBOX
        public Rectangle rectangle;

        //ANIMATION
        int frameCollumn;
        int frameLine;
        SpriteEffects Effect;
        Direction Direction;
        int Timer;
        int AnimationSpeed;
        //int AnimationSpeedJump = 7;

        //BONUS
        public Item.Type CurrentItem;

        public Character(Texture2D newTexture, Vector2 newPosition)
        {
            texture = newTexture;

            life_changment = 0;


            hasJumped2 = true;
            spawn = true;
            position = newPosition;
            hasJumped = true;
            hasDoubleJumped = false;
            this.Timer = 0;
            this.frameCollumn = 1;
            this.frameLine = 1;
            cooldownDoubleJump = false;

            CurrentItem = Item.Type.DoubleJump;

        }


        public void Animate()
        {
            this.Timer++;
            if (this.Timer == this.AnimationSpeed)
            {
                this.Timer = 0;
                #region spawning
                if (spawn)
                {
                    this.frameCollumn++;
                    if (this.frameCollumn >= this.nbr_sprite)
                    {
                        spawn = false;
                    }
                }
                #endregion
                else
                {
                    this.frameCollumn++;
                    if (this.frameCollumn >= this.nbr_sprite)
                    {
                        this.frameCollumn = 1;
                    }
                }
            }
        }

        void sprite_update(bool spawn, bool attack, bool stop, bool jump)
        {
            if (spawn)
            {
                nbr_sprite = 28;
                player_Height = 64;
                player_Width = 64;
                AnimationSpeed = 9;
            }
            else
            {
                if (stop)
                {
                    nbr_sprite = 4;
                    player_Height = 41;
                    player_Width = 32;
                    AnimationSpeed = 3;

                }
                else
                {
                    if (jump)
                    {
                        nbr_sprite = 19;
                        player_Height = 55;
                        player_Width = 33;
                        AnimationSpeed = 3;

                        if (attack)
                        {
                            nbr_sprite = 19;
                            player_Height = 55;
                            player_Width = 42;
                            AnimationSpeed = 3;
                        }
                    }
                    else
                    {
                        if (attack)
                        {
                            nbr_sprite = 16;
                            player_Height = 43;
                            player_Width = 51;
                            AnimationSpeed = 3;
                        }
                        else
                        {
                            nbr_sprite = 16;
                            player_Height = 43;
                            player_Width = 41;
                            AnimationSpeed = 3;
                        }
                    }
                }
            }
        }

        public void Update(GameTime gameTime, SoundEffectInstance effect)
        {
            life_changment = 0;
            //DEFINITION
            rectangle = new Rectangle((int)position.X, (int)position.Y, 44, 50);

            //RESPAWN
            if (Keyboard.GetState().IsKeyDown(Keys.R))
            {
                position = Vector2.One;
                velocity.Y = 0;
                spawn = true;
            }

            position += velocity;

            if (velocity.Y != 0)
                this.hasJumped = true;


            if (spawn)
            {
                Animate();
            }
            if (Keyboard.GetState().IsKeyUp(Ressource.Key[Ressource.inGameAction.Right]) && (Keyboard.GetState().IsKeyUp(Ressource.Key[Ressource.inGameAction.Left])))
            {
                stop = true;
                Animate();
            }

            // SET UP
            if (Keyboard.GetState().IsKeyDown(Ressource.Key[Ressource.inGameAction.Right]))
            {
                stop = false;
                velocity.X = 5f;
                this.Direction = Direction.Right;
                this.Animate();
            }
            else if (Keyboard.GetState().IsKeyDown(Ressource.Key[Ressource.inGameAction.Left]))
            {
                stop = false;
                velocity.X = -5f;
                this.Direction = Direction.Left;
                this.Animate();
            }
            else
            {
                stop = true;
                velocity.X = 0f;
            }

            //PAUSE
            if (Keyboard.GetState().IsKeyDown(Ressource.Key[Ressource.inGameAction.Pause]))
            {
                stop = true;
                Game1.inGame = false;
            }

            //TIR
            if (Keyboard.GetState().IsKeyDown(Ressource.Key[Ressource.inGameAction.Shoot]))
            {
                if (hasJumped)
                {
                    jump = true;
                    attack = true;
                }
                else
                {
                    jump = false;
                    attack = true;
                }
            }
            else
            {
                if (hasJumped)
                {
                    jump = true;
                    attack = false;
                }
                else
                {
                    attack = false;
                    jump = false;
                }
            }

            //SAUT
            if (Keyboard.GetState().IsKeyDown(Ressource.Key[Ressource.inGameAction.Jump]) && hasJumped == false)
            {
                jump = true;
                position.Y -= 5f;
                velocity.Y = -saut;
                hasJumped = true;
                cooldownDoubleJump = true;
            }
            if (Keyboard.GetState().IsKeyUp(Ressource.Key[Ressource.inGameAction.Jump]) && cooldownDoubleJump)
            {
                cooldownDoubleJump = false;
            }

            if (Keyboard.GetState().IsKeyDown(Ressource.Key[Ressource.inGameAction.Jump]) && hasJumped && !hasJumped2 && CurrentItem == Item.Type.DoubleJump && !cooldownDoubleJump)
            {
                position.Y -= 5f;
                velocity.Y = -saut;
                hasJumped2 = true;
            }
            float i = 1;
            velocity.Y += 0.15f * i;


            //SWITCH POUR GERER EFFECT SUR LE SPRITE
            switch (this.Direction)
            {
                case Direction.Right:
                    this.Effect = SpriteEffects.None;
                    break;
                case Direction.Left:
                    this.Effect = SpriteEffects.FlipHorizontally;
                    break;
            }


            //MUSIQUE
            if (Ressource.parameter[0] && MediaPlayer.State != MediaState.Playing)
            {
                MediaPlayer.Play(Ressource.song);
                MediaPlayer.Volume = 0.1f;
            }

            else if (Ressource.parameter[0] == false)
            {
                MediaPlayer.Stop();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                life_changment = -1;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Z))
            {
                life_changment = 1;
            }


            sprite_update(spawn, attack, stop, jump);
        }

        //COLLISION
        public void Collision(Rectangle obstacle, int xoffset, int yoffset, SoundEffectInstance effect, string name)
        {
            Rectangle player_plus_1 = new Rectangle((int)position.X + (int)velocity.X, (int)position.Y + saut, player_Height, player_Width);
            if (name == "Tile1" || name == "Tile2")
            {
                if (rectangle.isOnTopOf(obstacle))
                {

                    if (Ressource.parameter[1] && this.hasJumped == false)
                    {
                        if (Keyboard.GetState().IsKeyDown(Ressource.Key[Ressource.inGameAction.Left]) || Keyboard.GetState().IsKeyDown(Ressource.Key[Ressource.inGameAction.Right]))
                        {
                            if (effect.State != SoundState.Playing)
                                effect.Play();
                        }
                        else
                        {
                            effect.Resume();
                            effect.Pause();
                        }
                    }
                    else
                    {
                        effect.Resume();
                        effect.Pause();
                    }

                    if (hasJumped)
                    {
                        effect.Resume();
                        effect.Pause();
                    }

                    rectangle.Y = obstacle.Y - rectangle.Height;
                    velocity.Y = 0;
                    hasJumped = false;
                    hasDoubleJumped = false;
                    if (Keyboard.GetState().IsKeyDown(Ressource.Key[Ressource.inGameAction.Jump]) == true)
                    {
                        effect.Resume();
                        effect.Pause();
                    }

                }

                //COLISION

                if (rectangle.isOnLeftOf(obstacle) && Keyboard.GetState().IsKeyDown(Ressource.Key[Ressource.inGameAction.Right]))
                {
                    position.X = obstacle.X - rectangle.Width - 3;

                }
                if (rectangle.isOnRightOf(obstacle) && Keyboard.GetState().IsKeyDown(Ressource.Key[Ressource.inGameAction.Left]))
                {
                    position.X = obstacle.X + obstacle.Width + 3;

                }
                if (player_plus_1.isOnBotOf(obstacle))
                {
                    if (velocity.Y < 0)
                    {
                        velocity.Y = -velocity.Y;
                    }
                    position.Y = obstacle.Bottom + velocity.Y;
                }

                if (this.velocity.Y > 0)
                {
                    effect.Resume();
                    effect.Pause();
                }




                if (position.X < 0)
                { position.X = 0; }
                if (position.X > xoffset - rectangle.Width)
                { position.X = xoffset - rectangle.Width; }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (spawn)
            {
                spriteBatch.Draw(this.texture, rectangle, new Rectangle((this.frameCollumn - 1) * player_Width, 0, player_Width, player_Height), Color.White, 0f, new Vector2(0, 0), this.Effect, 0f);
            }
            else if (stop)
            {
                spriteBatch.Draw(this.texture, rectangle, new Rectangle((this.frameCollumn - 1) * player_Width, 64, player_Width, player_Height), Color.White, 0f, new Vector2(0, 0), this.Effect, 0f);
            }
            else if (jump)
            {

                if (attack)
                {
                    spriteBatch.Draw(this.texture, rectangle, new Rectangle((this.frameCollumn - 1) * player_Width, 64 + 41 + 43 + 43 + 55, player_Width, player_Height), Color.White, 0f, new Vector2(0, 0), this.Effect, 0f);
                }
                else
                {
                    spriteBatch.Draw(this.texture, rectangle, new Rectangle((this.frameCollumn - 1) * player_Width, 64 + 41 + 43 + 43 + 1, player_Width, player_Height - 1), Color.White, 0f, new Vector2(0, 0), this.Effect, 0f);
                }
            }
            else
            {
                if (attack)
                {
                    spriteBatch.Draw(this.texture, rectangle, new Rectangle((this.frameCollumn - 1) * player_Width, 64 + 41 + 43, player_Width, player_Height), Color.White, 0f, new Vector2(0, 0), this.Effect, 0f);
                }
                else
                {
                    spriteBatch.Draw(this.texture, rectangle, new Rectangle((this.frameCollumn - 1) * player_Width, 64 + 41, player_Width, player_Height), Color.White, 0f, new Vector2(0, 0), this.Effect, 0f);
                }
            }
        }
    }

}