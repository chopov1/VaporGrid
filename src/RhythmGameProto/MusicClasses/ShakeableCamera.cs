using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhythmGameProto
{
    public class ShakeableCamera : Camera
    {
        Vector2 shakeOffset;
        int shakeSize;
        int shakesPerSecond;
        float zoomSize;
        float timer;

        public ShakeableCamera(Game game) : base(game)
        {
            shakeSize = 10;
            shakesPerSecond = 10;
            shakeOffset = new Vector2(shakeSize, 0);
        }
        protected override void UpdateCameraMove(GameTime gameTime)
        {
            //tick timer
            timer -= gameTime.ElapsedGameTime.Milliseconds;
            //shake
            if (timer <= 0)
            {
                shakeOffset *= -1;
                cameraMovement += shakeOffset;
            }
            //reset timer
            if (timer <= 0) timer += 1000 / shakesPerSecond;
            base.UpdateCameraMove(gameTime);
        }
    }
}
