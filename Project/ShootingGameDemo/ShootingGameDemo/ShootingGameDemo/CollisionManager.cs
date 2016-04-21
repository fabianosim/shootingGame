using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShootingGameDemo
{
    public class CollisionManager
    {
        private readonly PlayerShip playerShip;
        private readonly ShotManager shotManager;
        private readonly EnemyManager enemyManager;
        private readonly ExplosionManager explosionManager;

        public CollisionManager(PlayerShip playerShip, ShotManager shotManager, EnemyManager enemyManager, ExplosionManager explosionManager)
        {
            this.playerShip = playerShip;
            this.shotManager = shotManager;
            this.enemyManager = enemyManager;
            this.explosionManager = explosionManager;
        }

        public void Update(GameTime gameTime)
        {
            CheckCollisions();
        }

        public void CheckCollisions()
        {
            CheckShotToPlayer();
            CheckShotToEnemy();
        }

        public void CheckShotToEnemy()
        {
            for (int i = 0; i < shotManager.PlayerShots.Count; i++)
            {
                var shot = shotManager.PlayerShots[i];

                foreach(var enemy in enemyManager.Enemies)
                {
                    if(!enemy.IsDead && shot.BoundingBox.Intersects(enemy.BoundingBox))
                    {
                        enemy.Hit();

                        if(enemy.IsDead)
                        {
                            explosionManager.CreateExplosion(enemy);
                        }

                        shotManager.RemovePlayerShot(shot);
                    }
                }
            }
        }

        public void CheckShotToPlayer()
        {
            for (int i = 0; i < shotManager.EnemyShots.Count;i++)
            {
                var shot = shotManager.EnemyShots[i];

                if (!playerShip.IsDead && shot.BoundingBox.Intersects(playerShip.BoundingBox))
                {
                    playerShip.Hit();

                    if (playerShip.IsDead)
                        explosionManager.CreateExplosion(playerShip);
                    shotManager.RemoveEnemyShot(shot);
                }
            }
        }
    }
}
