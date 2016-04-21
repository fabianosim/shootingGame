using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShootingGameDemo
{
    public class ShotManager
    {
        private readonly Texture2D texture;
        private readonly Rectangle bounds;
        private List<Shot> enemyShots = new List<Shot>();
        private List<Shot> playerShots = new List<Shot>();
        private readonly SoundManager soundManager;

        private IEnumerable<Shot> AllShots
        {
            get
            {
                return enemyShots.Union(playerShots);
            }
        }

        public IList<Shot> EnemyShots
        {
            get { return enemyShots; }
        }

        public IList<Shot> PlayerShots
        {
            get { return playerShots; }
        }

        public ShotManager(Texture2D texture, Rectangle bounds, SoundManager soundManager)
        {
            this.texture = texture;
            this.bounds = bounds;
            this.soundManager = soundManager;
        }

        public void FireEnemyShot(Vector2 shotPosition)
        {
            FireShot(shotPosition, 1, enemyShots); //1 pra ir pra baixo
        }

        public void FirePlayerShot(Vector2 shotPosition)
        {
            FireShot(shotPosition, -1, playerShots); //-1 pra ir pra cima
        }

        public void FireShot(Vector2 shotPosition, int yDirection, List<Shot> shotList)
        {
            var inflatedBounds = bounds;
            inflatedBounds.Inflate(10, 10);
            var shot = new Shot(texture, shotPosition, inflatedBounds);
            shot.Velocity = new Vector2(0, yDirection);
            shotList.Add(shot);

            soundManager.PlayShotSound();
        }

        public void Update(GameTime gameTime)
        {
            foreach (var shot in AllShots)
                shot.Update(gameTime);

            for (int i = 0; i < enemyShots.Count; i++)
            {
                if (!bounds.Contains(enemyShots[i].BoundingBox))
                    enemyShots.Remove(enemyShots[i]);
            }

            for (int i = 0; i < playerShots.Count; i++)
            {
                if (!bounds.Contains(playerShots[i].BoundingBox))
                    playerShots.Remove(playerShots[i]);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var shot in AllShots)
                shot.Draw(spriteBatch);
        }

        public void RemovePlayerShot(Shot shot)
        {
            this.playerShots.Remove(shot);
        }

        public void RemoveEnemyShot(Shot shot)
        {
            enemyShots.Remove(shot);
        }
    }
}
