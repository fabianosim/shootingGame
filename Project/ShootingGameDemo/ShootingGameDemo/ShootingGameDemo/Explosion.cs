using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShootingGameDemo
{
    public class Explosion : Sprite
    {
        public Explosion(Texture2D texture, Vector2 centerOfSprite, Rectangle bounds) : base(texture, centerOfSprite, bounds, 3, 2, 10)
        {

        }

        public bool IsDone()
        {
            return animationPlayedOnce;
        }
    }
}
