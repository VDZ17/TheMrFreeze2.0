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
        #region Fields
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
        public Color bulletColor;


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
        public int frameCollumn;
        public int frameRow;
        public SpriteEffects Effect;
        public Direction Direction;
        int Timer;
        int AnimationSpeed;
        public Color color;

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
        Keys shootKey;
        Keys jumpKey;

        //Key Multi
        bool Kjump;
        bool Kright;
        bool Kleft;
        bool Kshoot;


        public int player;
        #endregion


        public Character(Texture2D newTexture, Vector2 newPosition, Keys left, Keys right, Keys jump, Keys shoot, int player)
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

            defaultLeft = left;
            defaultRight = right;

            this.Left = defaultLeft;
            this.Right = defaultRight;
            this.shootKey = shoot;
            this.jumpKey = jump;

            this.player = player;
            Kjump = false;
            Kright = false;
            Kleft = false;
            Kshoot = false;
            
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

        void pull_position_X(Rectangle obstacle)
        {
            if (player == 2 && !Ressource.parameter[4])
            {
                //Left + telep
                if (position.X <= Camera.center.X - Camera.viewport.Width / 2 && rectangle.isOnLeftOf(obstacle))
                {
                    position.X = Level.Heroes[0].position.X;
                    position.Y = Level.Heroes[0].position.Y;
                    rectangle = new Rectangle((int)position.X, (int)position.Y, player_Width, player_Height);
                    spawn = true;
                }
                //Left
                else if (position.X < Camera.center.X - Camera.viewport.Width / 2 && !rectangle.isOnLeftOf(obstacle))
                    position.X = Camera.center.X - Camera.viewport.Width / 2;

                //Right+ telep
                if (position.X + player_Width >= Camera.center.X + Camera.viewport.Width / 2 && rectangle.isOnRightOf(obstacle))
                {
                    position.X = Level.Heroes[0].position.X;
                    position.Y = Level.Heroes[0].position.Y;
                    rectangle = new Rectangle((int)position.X, (int)position.Y, player_Width, player_Height);
                    spawn = true;
                }
                //Right
                else if (position.X + player_Width > Camera.center.X + Camera.viewport.Width / 2)
                    position.X = Camera.center.X + Camera.viewport.Width / 2 - player_Width;

            }
        }

        public void Animate()
        {
            this.Timer++;
            if (this.Timer >= this.AnimationSpeed)
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
                else if (jump)
                {
                    if (frameCollumn < 10)
                        frameCollumn++;
                }
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
                        AnimationSpeed = 4;

                        if (attack)
                        {
                            nbr_sprite = 19;
                            player_Height = 55;
                            player_Width = 42;
                            AnimationSpeed = 4;
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

        public void Update(GameTime gameTime, SoundEffectInstance effect, string s = "")
        {
            #region bool keyboard
            Kjump = false;
            Kright = false;
            Kleft = false;
            Kshoot = false;

            if (Ressource.parameter[4] && s != "")
            {
                
                bool[] k = Game1.Level.StrToKeyboard(s);
                //K/left/right/jump/shoot
                if (k[0])
                {
                    Kleft = k[1];
                    Kright = k[2];
                    Kjump = k[3];
                    Kshoot = k[4];
                }

            }
            #endregion
            #region definition
            particule.particleEffects["Snow"].Trigger(new Vector2(position.X + Camera.Transform.Translation.X, 0));
            life_changment = 0;

            
            rectangle = new Rectangle((int)position.X, (int)position.Y, player_Width, player_Height);

            position += velocity;

            if (velocity.Y != 0)
                this.hasJumped = true;


            if (spawn)
            {
                Animate();
            }
            if ((Keyboard.GetState().IsKeyUp(Right) || (!Kright && Ressource.parameter[4])) && (Keyboard.GetState().IsKeyUp(Left) || (!Kleft && Ressource.parameter[4])))
            {
                if (!spawn)
                    frameCollumn = 1;
                stop = true;
                Animate();
            }
            #endregion
            #region bonus
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
            #endregion
            #region set up
            if ((Keyboard.GetState().IsKeyDown(Right) || Kright) && !spawn)
            {
                stop = false;

                if (player == 1 || (player == 2 && this.position.X + this.player_Width < (Camera.center.X - Camera.viewport.Width / 2) + Camera.viewport.Width))
                    velocity.X = speed;
                else
                    velocity.X = 0;
                this.Direction = Direction.Right;
                this.Animate();
            }

            else if ((Keyboard.GetState().IsKeyDown(Left) || Kleft) && !spawn)
            {
                stop = false;
                if (player == 1 || (player == 2 && this.position.X > (Camera.center.X - Camera.viewport.Width / 2)))
                    velocity.X = -speed;
                else
                    velocity.X = 0;

                this.Direction = Direction.Left;
                this.Animate();
            }
            else
            {
                stop = true;
                velocity.X = 0f;
            }
            #endregion
            #region pause
            if (Keyboard.GetState().IsKeyDown(Ressource.KeyJ1[Ressource.inGameAction.Pause]))
            {
                stop = true;
                Game1.inGame = false;
            }
            #endregion
            #region tir
            if ((Keyboard.GetState().IsKeyDown(shootKey) || Kshoot) && !spawn)
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
            #endregion
            #region saut
            if ((Keyboard.GetState().IsKeyDown(jumpKey) || Kjump) && !hasJumped && !spawn)
            {
                //va sauter
                if (!jump)
                    frameCollumn = 1;

                jump = true;
                position.Y -= 5f;
                velocity.Y = -saut;
                hasJumped = true;
                cooldownDoubleJump = true;
            }
            if ((Keyboard.GetState().IsKeyUp(jumpKey) || (!Kjump && Ressource.parameter[4])) && cooldownDoubleJump)
            {
                cooldownDoubleJump = false;
            }

            if ((Keyboard.GetState().IsKeyDown(jumpKey) || Kjump) && hasJumped && !hasJumped2 && CurrentItem == Item.Type.DoubleJump && !cooldownDoubleJump)
            {
                velocity.Y = -saut;
                hasJumped2 = true;
            }
            float i = 1;

            velocity.Y += 0.15f * i;
#endregion
            #region effet sprite
            switch (this.Direction)
            {
                case Direction.Right:
                    this.Effect = SpriteEffects.None;
                    break;
                case Direction.Left:
                    this.Effect = SpriteEffects.FlipHorizontally;
                    break;
            }
            #endregion
            #region musique
            if (Ressource.parameter[0] && MediaPlayer.State != MediaState.Playing)
            {
                MediaPlayer.Play(Ressource.song);
                MediaPlayer.Volume = 1;
            }
            

            else if (Ressource.parameter[0] == false)
            {
                MediaPlayer.Stop();
            }
            #endregion
            #region tomber
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
            #endregion
        }

        //COLLISION
        public void Collision(Rectangle obstacle, int xoffset, int yoffset, SoundEffectInstance effect, string name)
        {
            Rectangle player_plus_1 = new Rectangle((int)position.X + (int)velocity.X, (int)position.Y + saut, player_Height, player_Width);

            pull_position_X(obstacle);

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
                        if ((Keyboard.GetState().IsKeyDown(Left) || Kleft) || (Keyboard.GetState().IsKeyDown(Right) || Kright))
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

                    if ((Keyboard.GetState().IsKeyDown(Left) || Kleft) || (Keyboard.GetState().IsKeyDown(Right) || Kright))
                    {
                        if (jump)
                            jump = false;

                        if ((Keyboard.GetState().IsKeyDown(jumpKey) || Kjump))
                        {
                            if (!jump)
                                position.Y += -12;
                            jump = true;
                        }


                    }


                    hasJumped = false;
                    hasJumped2 = false;

                    velocity.Y = 0;
                    hasDoubleJumped = false;

                    if ((Keyboard.GetState().IsKeyDown(jumpKey) || Kjump))
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
                    position.X = obstacle.X - rectangle.Width;
                }
                else if (rectangle.isOnRightOf(obstacle))
                {
                    position.X = obstacle.X + obstacle.Width;
                }
                else if (player_plus_1.isOnBotOf(obstacle))
                {
                    if (velocity.Y < 0)
                        velocity.Y = -velocity.Y;
                    position.Y = obstacle.Bottom + velocity.Y;
                }

                if (this.velocity.Y > 0)
                {
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
                frameRow = 0;
                spriteBatch.Draw(this.texture, rectangle, new Rectangle((this.frameCollumn - 1) * player_Width, 0, player_Width, player_Height), color, 0f, new Vector2(0, 0), this.Effect, 0f);
            }
            else if (stop)
            {
                if (attack)
                {
                    player_Height = 43;
                    player_Width = 51;
                    frameRow = 64 + 41 + 43;
                    spriteBatch.Draw(this.texture, rectangle, new Rectangle(player_Width, 64 + 41 + 43, player_Width, player_Height), color, 0f, new Vector2(0, 0), this.Effect, 0f);
                }
                else
                {
                    player_Height = 41;
                    player_Width = 32;
                    frameRow = 64;
                    spriteBatch.Draw(this.texture, rectangle, new Rectangle((this.frameCollumn - 1) * player_Width, 64, player_Width, player_Height), color, 0f, new Vector2(0, 0), this.Effect, 0f);
                }
            }
            else if (jump)
            {

                if (attack)
                {
                    frameRow = 64 + 41 + 43 + 43 + 55;
                    spriteBatch.Draw(this.texture, rectangle, new Rectangle((this.frameCollumn - 1) * player_Width, 64 + 41 + 43 + 43 + 55, player_Width, player_Height), color, 0f, new Vector2(0, 0), this.Effect, 0f);
                }
                else
                {
                    frameRow = 64 + 41 + 43 + 43 + 1;
                    spriteBatch.Draw(this.texture, rectangle, new Rectangle((this.frameCollumn - 1) * player_Width, 64 + 41 + 43 + 43 + 1, player_Width, player_Height - 1), color, 0f, new Vector2(0, 0), this.Effect, 0f);
                }
            }
            else
            {
                if (attack)
                {
                    frameRow = 64 + 41 + 43;
                    spriteBatch.Draw(this.texture, rectangle, new Rectangle((this.frameCollumn - 1) * player_Width, 64 + 41 + 43, player_Width, player_Height), color, 0f, new Vector2(0, 0), this.Effect, 0f);
                }
                else
                {
                    frameRow = 64 + 41;
                    spriteBatch.Draw(this.texture, rectangle, new Rectangle((this.frameCollumn - 1) * player_Width, 64 + 41, player_Width, player_Height), color, 0f, new Vector2(0, 0), this.Effect, 0f);
                }
            }
            foreach (Bullet bullet in Bullets)
            {
                bullet.Draw(spriteBatch, bulletColor);
            }

            if (Ressource.parameter[3])
            {
                if (player == 1)
                    spriteBatch.Draw(Ressource.Cursor1, new Rectangle((int)position.X + player_Width / 4,
                        (int)position.Y - 43 - 15, 25, 43), Color.White);
                else if (player == 2)
                    spriteBatch.Draw(Ressource.Cursor2, new Rectangle((int)position.X + player_Width / 4,
                        (int)position.Y - 43 - 15, 25, 43), Color.White);
            }

        }
    }

}