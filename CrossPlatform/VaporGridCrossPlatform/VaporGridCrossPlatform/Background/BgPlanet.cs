using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaporGridCrossPlatform.Background
{
    internal class BgPlanet : Sprite
    {
        Texture2D[] frames;
        enum CountState { up, down };
        CountState state;
        public float Transparency;
        float fadeSpeed;
        public Texture2D Texture { get { return spriteTexture; } }

        Animation anim;

        Random rnd;
        public BgPlanet(Game game, string texturename, Camera camera) : base(game, texturename, camera)
        {
            Game.Components.Add(this);
            rnd = new Random();
            Transparency = rnd.Next(100) * 0.01f;
            state = CountState.up;
            fadeSpeed = 0.001f * rnd.Next(1, 5);
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            spriteTexture = Game.Content.Load<Texture2D>("bluePlanet/BluePlanetSheet");
            anim = new Animation(spriteTexture, 6, .1f);
        }
        public override void Initialize()
        {
            base.Initialize();
            anim.Start();
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            anim.Update(gameTime);
            if (Transparency <= 0)
            {
                state = CountState.up;
                getRndPos(this);
            }
            if (Transparency >= 1)
            {
                state = CountState.down;
            }
            switch (state)
            {
                case CountState.up:
                    Transparency += fadeSpeed;
                    break;
                case CountState.down:
                    Transparency -= fadeSpeed;
                    break;
            }
        }

        public void DrawAnim(SpriteBatch s)
        {
            anim.Draw(Position, s,Transparency);
        }

        private void getRndPos(Sprite s)
        {
            s.Position = new Vector2(rnd.Next(Game.GraphicsDevice.Viewport.Width), rnd.Next(Game.GraphicsDevice.Viewport.Height));
        }

    }
}
