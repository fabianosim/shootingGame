using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShootingGameDemo
{
    public class Sprite
    {
        private readonly Texture2D texture;
        private Vector2 position;
        public Vector2 Velocity { get; set; }
        protected float Speed { get; set; }
        private readonly Rectangle movementBounds;
        private readonly int rows;
        private readonly int columns;
        private readonly double framesPerSecond;
        private int totalFrames;
        private double timeSinceLastFrame;
        private int currentFrame;
        protected bool animationPlayedOnce;

        public Rectangle BoundingBox
        {
            get { return CreateBoundingBoxFromPosition(position); }
        }

        public float Width
        {
            get
            {
                return texture.Width / columns;
            }
        }

        public float Height
        {
            get
            {
                return texture.Height / rows;
            }
        }

        public Vector2 Position
        {
            get { return this.position; }
            set { this.position = value; }
        }

        public Sprite(Texture2D texture, Vector2 position, Rectangle movementBounds) : this(texture, position, movementBounds, 1, 1, 1)
        {
            
        }

        public Sprite(Texture2D texture, Vector2 position, Rectangle movementBounds, int rows, int columns, double framesPerSecond)
        {
            this.texture = texture;
            this.position = position;
            this.movementBounds = movementBounds;
            this.rows = rows;
            this.columns = columns;
            this.framesPerSecond = framesPerSecond;
            totalFrames = rows * columns;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var imageWidth = texture.Width / columns;
            var imageHeight = texture.Height / rows;

            var currentRow = currentFrame / columns;
            var currentColumn = currentFrame % columns;

            var sourceRectangle = new Rectangle(imageWidth * currentColumn, imageHeight * currentRow, imageWidth, imageHeight);
            var destinationRectangle = new Rectangle((int)position.X, (int)position.Y, imageWidth, imageHeight);

            spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);
        }

        public virtual void Update(GameTime gameTime)
        {
            UpdateAnimation(gameTime);
            var newPosition = position + ((Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds)  * Speed);

            if(Blocked(newPosition))
            {
                return;
            }

            position = newPosition;
        }

        private void UpdateAnimation(GameTime gameTime)
        {
            timeSinceLastFrame += gameTime.ElapsedGameTime.TotalSeconds;

            if(timeSinceLastFrame > SecondBetweenFrames())
            {
                currentFrame++;
                timeSinceLastFrame = 0;
            }

            if (currentFrame == totalFrames)
            {
                currentFrame = 0;
                animationPlayedOnce = true;
            }
                
        }

        private double SecondBetweenFrames()
        {
            return 1 / framesPerSecond;
        }

        private bool Blocked(Vector2 newPosition)
        {
            var boundingBox = CreateBoundingBoxFromPosition(newPosition);
            return !movementBounds.Contains(boundingBox);
        }

        private Rectangle CreateBoundingBoxFromPosition(Vector2 position)
        {
            return new Rectangle((int)position.X, (int)position.Y, (int)this.Width, (int)this.Height);
        }
    }
}
