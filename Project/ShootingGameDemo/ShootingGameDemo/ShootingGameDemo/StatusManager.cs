using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShootingGameDemo
{
    public class StatusManager
    {
        public int Score { get; set; }
        public int Lives { get; set; }
        private readonly SpriteFont gameFont;
        private readonly Rectangle bounds;
        private readonly EnemyManager enemyManager;
        private readonly Texture2D lifeTexture;

        public StatusManager(SpriteFont gameFont, Rectangle bounds, EnemyManager enemyManager, Texture2D lifeTexture)
        {
            this.gameFont = gameFont;
            this.bounds = bounds;
            this.enemyManager = enemyManager;
            this.lifeTexture = lifeTexture;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var scale = .25f;

            for (int i = 0; i <= Lives;i++)
            {
                var xPosition = (lifeTexture.Width / 2) * scale * (i-1);
                spriteBatch.Draw(lifeTexture, new Vector2(xPosition, 10), new Rectangle(0, 0, lifeTexture.Width / 2, lifeTexture.Height / 2),
                    Color.White, 0.0f, Vector2.Zero, scale, SpriteEffects.None, 0.0f);
            }

            DrawScore(spriteBatch);
        }

        private void DrawScore(SpriteBatch spriteBatch)
        {
            var scoreText = string.Format("Score: {0}", Score);
            var scoreDimensions = gameFont.MeasureString(scoreText);
            var scoreX = bounds.Width - scoreDimensions.X - 10;
            var scoreY = 10;

            spriteBatch.DrawString(gameFont, scoreText, new Vector2(scoreX, scoreY), Color.White);
        }

        public void UpdateScore()
        {
            int kills = enemyManager.GetKillCount();
            Score += (kills * 1000);
        }
    }
}
