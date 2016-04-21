using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShootingGameDemo
{
    public class SoundManager
    {
        private Song backgroundMusic;
        private SoundEffect laserEffect;
        private SoundEffect explosionEffect;

        public SoundManager(ContentManager content)
        {
            backgroundMusic = content.Load<Song>(@"sounds\song");
            laserEffect = content.Load<SoundEffect>(@"sounds\laser");
            explosionEffect = content.Load<SoundEffect>(@"sounds\blast");
        }

        public void PlayBackgroundMusic()
        {
            if(MediaPlayer.GameHasControl)
            {
                MediaPlayer.Play(backgroundMusic);
                MediaPlayer.IsRepeating = true;
            }
        }

        public void PlayShotSound()
        {
            laserEffect.Play();
        }

        public void PlayExplosionSound()
        {
            explosionEffect.Play();
        }
    }
}
