using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShootingGameDemo
{
    public class PlayerShip : Sprite
    {
        private MouseState previousMousePosition;
        private readonly ShotManager shotManager;
        private const double TimeBetweenShotsInSeconds = 0.3;
        private double timeSinceLastFireInSeconds = 0;

        public bool IsDead { get; set; }

        public PlayerShip(Texture2D texture, Vector2 position, Rectangle movementBounds, ShotManager shotManager)
            : base(texture, position, movementBounds, 2, 2, 7)
        {
            this.shotManager = shotManager;
            Speed = 300;
        }

        public override void Update(GameTime gameTime)
        {
            timeSinceLastFireInSeconds += gameTime.ElapsedGameTime.TotalSeconds;
            HandleControllerInput();
            //UpdateVelocityFromMouse();
            //UpdateVelocityFromController();
            //UpdateVelocityFromTouch();

            base.Update(gameTime);
        }

        private void HandleControllerInput()
        {
            var keyboardState = Keyboard.GetState();
            UpdateVelocityFromKeyboard(keyboardState);
            CheckForShotFromKeyboard(keyboardState);
        }

        private void CheckForShotFromKeyboard(KeyboardState keyboardState)
        {
            if(keyboardState.IsKeyDown(Keys.Space) && CanFireShot())
            {
                shotManager.FirePlayerShot(CalculateShotPosition());
                timeSinceLastFireInSeconds = 0;
            }
        }

        private bool CanFireShot()
        {
            return (timeSinceLastFireInSeconds > TimeBetweenShotsInSeconds) && (!IsDead);
        }

        private void UpdateVelocityFromKeyboard(KeyboardState keyboardState)
        {
            var keyDictionary = new Dictionary<Keys, Vector2>()
            {
                { Keys.Left, new Vector2(-1,0)},
                { Keys.Right, new Vector2(1,0)},
                //{ Keys.Up, new Vector2(0,-1)},
                //{ Keys.Down, new Vector2(0,1)}
            };

            var velocity = Vector2.Zero;

            foreach (var key in keyDictionary)
            {
                if (keyboardState.IsKeyDown(key.Key))
                {
                    velocity += key.Value;
                }
            }

            if (velocity != Vector2.Zero)
            {
                velocity.Normalize();
            }

            Velocity = velocity;
        }

        private void UpdateVelocityFromController()
        {
            var buttonDictionary = new Dictionary<Buttons, Vector2>()
            {
                { Buttons.DPadLeft, new Vector2(-1,0)},
                { Buttons.DPadRight, new Vector2(1,0)},
                { Buttons.DPadUp, new Vector2(0,-1)},
                { Buttons.DPadDown, new Vector2(0,1)}
            };

            var controllerState = GamePad.GetState(PlayerIndex.One);

            var velocity = Vector2.Zero;

            foreach (var button in buttonDictionary)
            {
                if(controllerState.IsButtonDown(button.Key))
                {
                    velocity += button.Value;
                }
            }

            if (velocity != Vector2.Zero)
            {
                velocity.Normalize();
            }

            Velocity = velocity;
        }

        private void UpdateVelocityFromMouse()
        {
            var velocity = new Vector2(Mouse.GetState().X - previousMousePosition.X,
                                        Mouse.GetState().Y - previousMousePosition.Y);

            if(velocity != Vector2.Zero)
            {
                velocity.Normalize();
            }

            Velocity = velocity;
            previousMousePosition = Mouse.GetState();
        }

        private void UpdateVelocityFromTouch()
        {
            var state = TouchPanel.GetState();
            var velocity = Vector2.Zero;

            if (state.Count == 0)
            {
                Velocity = velocity;
                return;
            }

            var position = state.FirstOrDefault().Position;

            if(position.X > this.Position.X + this.Width)
            {
                velocity += new Vector2(1, 0);
            }

            if (position.X < this.Position.X)
            {
                velocity += new Vector2(-1, 0);
            }

            if (position.Y > this.Position.Y + this.Width)
            {
                velocity += new Vector2(0, 1);
            }

            if(position.Y < this.Position.Y)
            {
                velocity += new Vector2(0, -1);
            }

            if(velocity != Vector2.Zero)
            {
                velocity.Normalize();
            }

            Velocity = velocity * Speed;
        }

        public void Hit()
        {
            IsDead = true;
        }

        private Vector2 CalculateShotPosition()
        {
            return Position + new Vector2(Width / 2, 0);
        }
    }
}
