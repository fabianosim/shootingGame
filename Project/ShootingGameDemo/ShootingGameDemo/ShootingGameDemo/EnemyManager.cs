using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShootingGameDemo
{
    public class EnemyManager
    {
        private readonly Texture2D texture;
        private readonly Rectangle bounds;
        private readonly ShotManager shotManager;
        private List<Enemy> enemies = new List<Enemy>();

        public IEnumerable<Enemy> Enemies
        {
            get { return enemies; }
        }

        public EnemyManager(Texture2D texture, Rectangle bounds, ShotManager shotManager)
        {
            this.texture = texture;
            this.bounds = bounds;
            this.shotManager = shotManager;

            CreateEnemy();
        }

        private void CreateEnemy()
        {
            var position = RandomPosition();
            var enemy = new Enemy(texture, position, bounds, shotManager);
            enemies.Add(enemy);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(var enemy in enemies)
            {
                enemy.Draw(spriteBatch);
            }
        }

        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if(enemies[i].IsDead)
                {
                    enemies.Remove(enemies[i]);
                    CreateEnemy();
                }

                enemies[i].Update(gameTime);
            }
        }

        private Vector2 RandomPosition()
        {
            var random = new Random();
            var xPosition = random.Next(bounds.Width - texture.Width + 1);
            return new Vector2(xPosition, 20);
        }

        public int GetKillCount()
        {
            return enemies.Where(e => e.IsDead).Count();
        }
    }
}
