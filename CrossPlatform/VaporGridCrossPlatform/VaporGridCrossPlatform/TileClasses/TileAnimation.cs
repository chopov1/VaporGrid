using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaporGridCrossPlatform
{
    public enum AnimPlayMode { forward, reverse}
    public class TileAnimation 
    {
        AnimPlayMode PlayMode;
        List<Texture2D> textures;
        int frames;
        int frame;
        float frameTime;
        float frameTimeLeft;
        public bool active { get; private set; }
        public bool looping;
        public bool hasFinished { get; private set; }
        public TileAnimation(List<Texture2D> textures, float frameTime, bool isLooping)
        {
            active = true;
            looping = isLooping;
            this.textures = textures;
            frames = textures.Count;
            this.frameTime = frameTime;
            frameTimeLeft = frameTime;
            hasFinished= false;
        }

        public void Start()
        {
            frame = 0;
            PlayMode= AnimPlayMode.forward;
            active = true;
        }

        public void StartRev()
        {
            frame = textures.Count -1;
            PlayMode= AnimPlayMode.reverse;
            active = true;
        }

        public void Stop()
        {
            active = false;
        }

        public void Reset()
        {
            frameTimeLeft = frameTime;
            hasFinished = false;
        }

        public virtual void Update(GameTime gameTime)
        {
            if (!active)
            {
                return;
            }

            frameTimeLeft -= (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (frameTimeLeft <= 0)
            {
                frameTimeLeft += frameTime;
                switch (PlayMode)
                {
                    case AnimPlayMode.forward:
                        playForward();
                        break;
                    case AnimPlayMode.reverse: playReverse(); break;
                }
            }
        }

        private void playReverse()
        {
            if (frame <= 0 && looping == false)
            {
                frame = textures.Count - 1;
                Stop();
                hasFinished = true;
            }
            else if (frame <= 0)
            {
                frame = textures.Count - 1;
            }
            else
            {
                frame--;
                
            }
        }

        private void playForward()
        {
            frame = (frame + 1) % frames;
            if (frame >= frames - 1 && looping == false)
            {
                Stop();
                hasFinished = true;
            }
        }

        public Texture2D getCurrentFrame()
        {
            return textures[frame];
        }

    }
}
