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
        public int player_Height;
        public int player_Width;
        bool spawn;
        bool jump;
        bool stop;
        bool attack;
        public int life;

        //BULLETS
        public List<Bullet> Bullets;
        int defaultNbBullet;
        int nbBullet;
        int timerBullet;
        Color bulletColor;


        //HEALTH
        public int life_changment;
        public bool isDead;

        //DEFINITION
        Texture2D texture;
        public Vector2 position;
        public Vector2 velocity;
        float defaultSpeed;
        float speed;

        //SAUT
        public bool hasJumped;
        public bool hasJumped2;
        public bool hasDoubleJumped;
        public int saut;
        int defaultSaut;
        bool cooldownDoubleJump;

        //HITBOX
        public Rectangle rectangle;

        //ANIMATION
        int frameCollumn;
        SpriteEffects Effect;
        Direction Direction;
        int Timer;
        int AnimationSpeed;
        Color color;
        //int AnimationSpeedJump = 7;

        //BONUS
        public Item.Type CurrentItem;
        public int timerBonus;
        int currentTimerBonus;
        bool IsTimerBonusOn;
        Item.Type PreviousItem;
        int TimerColor;
        Keys defaultRight;
        Keys defaultLeft;
        Keys Left;
        Keys Right;


        public Character(Texture2D newTexture, Vector2 newPosition)
        {
            texture = newTexture;

            life_changment = 0;

            Bullets = new List<Bullet> { };
            defaultNbBullet = 1;
            timerBullet = 0;
            bulletColor = Color.White;

            isDead = false;
            hasJumped2 = true;
            spawn = true;
            position = newPosition;
            hasJumped = true;
            hasDoubleJumped = false;
            this.Timer = 0;
            this.frameCollumn = 1;
            cooldownDoubleJump = false;
            color = Color.White;


            defaultSaut = 6;
            saut = 6;

            defaultSpeed = 5f;
            speed = 5f;

            timerBonus = 20 * 60;
            currentTimerBonus = 0;
            IsTimerBonusOn = false;

            PreviousItem = Item.Type.None;
            CurrentItem = Item.Type.None;
            this.life = 150;

            defaultLeft = Ressource.KeyJ1[Ressource.inGameAction.Left];
            defaultRight = Ressource.KeyJ1[Ressource.inGameAction.Right];
            Left = defaultLeft;
            Right = defaultRight;
        }

        public void UpdateBullet()
        {
            foreach (Bullet bullet in Bullets)
            {

                bullet.position += bullet.velocity;
                bullet.hitbox_bullet = new Rectangle((int)position.X + (int)bullet.velocity.X, (int)position.Y + (int)bullet.velocity.Y, bullet.texture.Width, bullet.texture.Height);

                if (Vector2.Distance(bullet.position, this.position) > 1000)
                {
                    bullet.IsVisible = false;
                }
            }
            for (int i = 0; i < Bullets.Count; i++)
            {
                if (!(Bullets[i].IsVisible))
                {
                    Bullets.RemoveAt(i);
                    i--;
                }

            }
        }

        public void Shoot()
        {
            Bullet newBullet;
            if (this.Effect == SpriteEffects.None)
            {
                newBullet = new Bullet(Ressource.Bullet, new Vector2(10, 0), this.Effect);
            }
            else
            {
                newBullet = new Bullet(Ressource.Bullet, new Vector2(-10, 0), this.Effect);
            }
            newBullet.position = new Vector2(this.position.X + 10, this.position.Y - 15);
            newBullet.IsVisible = true;

            if (Bullets.Count < nbBullet)
            {
                if (CurrentItem == Item.Type.MultiShot)
                {
                    if (timerBullet % 10 == 0)
                    {
                        Bullets.Add(newBullet);
                    }
                }
                else
                {
                    Bullets.Add(newBullet);
                    if (Ressource.parameter[1])
                        Ressource.shot2.Play();

                }


            }
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
                        this.position.Y += 23;
                        this.position.X += 20;
                        frameCollumn = 1;
                    }
                }
                #endregion
                else
                {
                    this.frameCollumn++;
                    if (this.frameCollumn >= this.nbr_sprite)
                    {
                        if (stop == false && attack == false)
                            this.frameCollumn = 2;
                        else
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

                    if (attack)
                    {
                        nbr_sprite = 16;
                        player_Height = 43;
                        player_Width = 51;
                        AnimationSpeed = 3;
                    }

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
            rectangle.Width = player_Width;
            rectangle.Height = player_Height;
        }

        public void Update(GameTime gameTime, SoundEffectInstance effect)
        {
            particule.particleEffects["Snow"].Trigger(new Vector2(position.X + Camera.Transform.Translation.X, 0));
            life_changment = 0;

            //DEFINITION
            rectangle = new Rectangle((int)position.X, (int)position.Y, player_Width, player_Height);
            //rectangle = new Rectangle((int)position.X, (int)position.Y, 50, 50);

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
            if (Keyboard.GetState().IsKeyUp(Right) && (Keyboard.GetState().IsKeyUp(Left)))
            {
                stop = true;
                Animate();
            }

            //BONUS
            if (CurrentItem != PreviousItem && CurrentItem != Item.Type.None)
            {
                //Musiques invincible
                if (CurrentItem == Item.Type.Invincibility && Ressource.parameter[0])
                {
                    MediaPlayer.Stop();
                    MediaPlayer.Play(Ressource.song2);
                    MediaPlayer.Volume = 0.5f;
                }
                else if (PreviousItem == Item.Type.Invincibility && CurrentItem != Item.Type.Invincibility && Ressource.parameter[0])
                {
                    MediaPlayer.Stop();
                    MediaPlayer.Play(Ressource.song);
                    MediaPlayer.Volume = 1;
                }
                currentTimerBonus = 0;
                IsTimerBonusOn = true;
            }

            if (IsTimerBonusOn)
            {
                currentTimerBonus++;
            }

            if (currentTimerBonus > timerBonus)
            {
                CurrentItem = Item.Type.None;
                IsTimerBonusOn = false;
            }
            PreviousItem = CurrentItem;

            if (CurrentItem == Item.Type.MoonJump)
            {
                saut = (int)(1.3 * defaultSaut);
            }
            else
            {
                saut = defaultSaut;
            }

            if (CurrentItem == Item.Type.MultiShot)
            {
                nbBullet = 5;
                bulletColor = Color.Yellow;
            }
            else
            {
                nbBullet = defaultNbBullet;
                bulletColor = Color.White;
            }
            timerBullet++;

            if (CurrentItem == Item.Type.SuperSpeed)
            {
                speed = 2 * defaultSpeed;
            }
            else if (CurrentItem == Item.Type.SlowSpeed)
            {
                speed = 3f;
            }
            else
            {
                speed = defaultSpeed;
            }

            if (CurrentItem == Item.Type.Invincibility)
            {
                TimerColor++;
                if (TimerColor < 10)
                {
                    color = Color.Red;
                }
                else if (TimerColor < 20)
                {
                    color = Color.Orange;
                }
                else if (TimerColor < 30)
                {
                    color = Color.Yellow;
                }
                else if (TimerColor < 40)
                {
                    color = Color.LimeGreen;
                }
                else if (TimerColor < 50)
                {
                    color = Color.LightBlue;
                }
                else if (TimerColor < 60)
                {
                    color = Color.Pink;
                }
                else
                {
                    TimerColor = 0;
                }
            }
            else
            {
                color = Color.White;
            }

            if (CurrentItem == Item.Type.ReverseDirection)
            {
                Left = defaultRight;
                Right = defaultLeft;
            }
            else
            {
                Left = defaultLeft;
                Right = defaultRight;
            }



            // SET UP
            if (Keyboard.GetState().IsKeyDown(Right))
            {
                stop = false;
                velocity.X = speed;
                this.Direction = Direction.Right;
                this.Animate();
            }
            else if (Keyboard.GetState().IsKeyDown(Left))
            {
                stop = false;
                velocity.X = -speed;
                this.Direction = Direction.Left;
                this.Animate();
            }
            else
            {
                stop = true;
                velocity.X = 0f;
            }

            //PAUSE
            if (Keyboard.GetState().IsKeyDown(Ressource.KeyJ1[Ressource.inGameAction.Pause]))
            {
                stop = true;
                Game1.inGame = false;
            }

            //TIR
            if (Keyboard.GetState().IsKeyDown(Ressource.KeyJ1[Ressource.inGameAction.Shoot]))
            {
                Shoot();
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
            if (Keyboard.GetState().IsKeyDown(Ressource.KeyJ1[Ressource.inGameAction.Jump]) && !hasJumped)
            {
                jump = true;
                position.Y -= 5f;
                velocity.Y = -saut;
                hasJumped = true;
                cooldownDoubleJump = true;
            }
            if (Keyboard.GetState().IsKeyUp(Ressource.KeyJ1[Ressource.inGameAction.Jump]) && cooldownDoubleJump)
            {
                cooldownDoubleJump = false;
            }

            if (Keyboard.GetState().IsKeyDown(Ressource.KeyJ1[Ressource.inGameAction.Jump]) && hasJumped && !hasJumped2 && CurrentItem == Item.Type.DoubleJump && !cooldownDoubleJump)
            {
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
                MediaPlayer.Volume = 1;
            }


            else if (Ressource.parameter[0] == false)
            {
                MediaPlayer.Stop();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Subtract))
            {
                life_changment += -1;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Add))
            {
                life_changment += 1;
            }

            //SORTIE ECRAN
            if (position.Y > 1500)
            {
                life_changment = -life;
            }


            if (Hud.youlose)
            {
                Game1.inGame = false;
                Game1.menu = Game1.menu.ChangeMenu(Menu.MenuType.loose);
            }

            sprite_update(spawn, attack, stop, jump);
            UpdateBullet();
        }

        //COLLISION
        public void Collision(Rectangle obstacle, int xoffset, int yoffset, SoundEffectInstance effect, string name)
        {
            Rectangle player_plus_1 = new Rectangle((int)position.X + (int)velocity.X, (int)position.Y + saut, player_Height, player_Width);
            //Rectangle playerRunning = new Rectangle((int)position.X, (int)position.Y, player_Height, player_Width);

            if (name == "Tile1" || name == "Tile2" || name == "Tile5" || name == "Tile6" || name == "Tile16")
            {
                if (rectangle.isOnTopOf(obstacle))
                {
                    if (name == "Tile5")
                    {
                        if (stop)
                        {
                            position.X = obstacle.X;
                        }
                    }
                    if (Ressource.parameter[1] && this.hasJumped == false)
                    {
                        if (Keyboard.GetState().IsKeyDown(Left) || Keyboard.GetState().IsKeyDown(Right))
                        {
                            if (effect.State != SoundState.Playing)
                                effect.Play();
                        }
                        else
                        {
                            effect.Pause();
                        }
                    }
                    else
                    {
                        effect.Pause();
                    }

                    if (hasJumped)
                    {
                        effect.Pause();
                    }

                    if (Keyboard.GetState().IsKeyDown(Left) || Keyboard.GetState().IsKeyDown(Right))
                    {
                        if (jump)
                            jump = false;

                        if (Keyboard.GetState().IsKeyDown(Ressource.KeyJ1[Ressource.inGameAction.Jump]))
                        {
                            jump = true;
                        }


                    }


                    hasJumped = false;
                    hasJumped2 = false;

                    velocity.Y = 0;
                    hasDoubleJumped = false;

                    if (Keyboard.GetState().IsKeyDown(Ressource.KeyJ1[Ressource.inGameAction.Jump]) == true)
                    {
                        effect.Pause();
                    }
                    this.sprite_update(spawn, attack, stop, jump);

                    if (!jump)
                        position.Y = obstacle.Y - rectangle.Height;
                    else
                        rectangle.Y = obstacle.Y - rectangle.Height;
                }

                //COLISION

                else if (rectangle.isOnLeftOf(obstacle))
                {
                    if(!jump)
                    position.X = obstacle.X - rectangle.Width;
                }
                else if (rectangle.isOnRightOf(obstacle))
                {
                    if (!jump)
                    position.X = obstacle.X + obstacle.Width;
                }
                else if (player_plus_1.isOnBotOf(obstacle))
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

                rectangle = new Rectangle((int)position.X, (int)position.Y, player_Width, player_Height);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (spawn)
            {
                spriteBatch.Draw(this.texture, rectangle, new Rectangle((this.frameCollumn - 1) * player_Width, 0, player_Width, player_Height), color, 0f, new Vector2(0, 0), this.Effect, 0f);
            }
            else if (stop)
            {
                if (attack)
                {
                    player_Height = 43;
                    player_Width = 51;
                    spriteBatch.Draw(this.texture, rectangle, new Rectangle(player_Width, 64 + 41 + 43, player_Width, player_Height), color, 0f, new Vector2(0, 0), this.Effect, 0f);
                }
                else
                {
                    player_Height = 41;
                    player_Width = 32;
                    spriteBatch.Draw(this.texture, rectangle, new Rectangle((this.frameCollumn - 1) * player_Width, 64, player_Width, player_Height), color, 0f, new Vector2(0, 0), this.Effect, 0f);
                }
            }
            else if (jump)
            {

                if (attack)
                {
                    spriteBatch.Draw(this.texture, rectangle, new Rectangle((this.frameCollumn - 1) * player_Width, 64 + 41 + 43 + 43 + 55, player_Width, player_Height), color, 0f, new Vector2(0, 0), this.Effect, 0f);
                }
                else
                {
                    spriteBatch.Draw(this.texture, rectangle, new Rectangle((this.frameCollumn - 1) * player_Width, 64 + 41 + 43 + 43 + 1, player_Width, player_Height - 1), color, 0f, new Vector2(0, 0), this.Effect, 0f);
                }
            }
            else
            {
                if (attack)
                {
                    spriteBatch.Draw(this.texture, rectangle, new Rectangle((this.frameCollumn - 1) * player_Width, 64 + 41 + 43, player_Width, player_Height), color, 0f, new Vector2(0, 0), this.Effect, 0f);
                }
                else
                {
                    spriteBatch.Draw(this.texture, rectangle, new Rectangle((this.frameCollumn - 1) * player_Width, 64 + 41, player_Width, player_Height), color, 0f, new Vector2(0, 0), this.Effect, 0f);
                }
            }
            foreach (Bullet bullet in Bullets)
            {
                bullet.Draw(spriteBatch, bulletColor);
            }
        }
    }

}