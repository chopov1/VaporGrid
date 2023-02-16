using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhythmGameProto
{
    public class BgStar : Sprite
    {
        enum CountState { up, down};
        CountState state;
        public float Transparency;
        float fadeSpeed;
        public Texture2D Texture { get { return spriteTexture; } }

        Random rnd;
        public BgStar(Game game, string texturename, Camera camera) : base(game, texturename, camera)
        {
            Game.Components.Add(this);
            rnd = new Random();
            Transparency = rnd.Next(100)*0.01f;
            state = CountState.up;
            fadeSpeed = 0.01f * rnd.Next(1, 5);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            
            if (Transparency <= 0)
            {
                state = CountState.up;
                getRndPos(this);
            }
            if(Transparency >= 1)
            {
                state = CountState.down;
            }
            switch(state)
            {
                case CountState.up:
                    Transparency+=fadeSpeed;
                    break;
                case CountState.down:
                    Transparency-=fadeSpeed;
                    break;
            }
        }


        private void getRndPos(Sprite s)
        {
            s.Position = new Vector2(rnd.Next(Game.GraphicsDevice.Viewport.Width), rnd.Next(Game.GraphicsDevice.Viewport.Height));

        }
    }
}
