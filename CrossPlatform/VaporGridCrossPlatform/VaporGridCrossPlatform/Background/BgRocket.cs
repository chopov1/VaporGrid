using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaporGridCrossPlatform.Background
{
    internal class BgRocket : Sprite
    {
        public float Transparency;
        float speed;
        public Texture2D Texture { get { return spriteTexture; } }

        Animation anim;

        MenuController input;
        Random rnd;
        public BgRocket(Game game, string texturename, Camera camera) : base(game, texturename, camera)
        {
            Game.Components.Add(this);
            Direction = new Vector2(-1, 1);
            input= new MenuController();
            rnd = new Random();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            spriteTexture = Game.Content.Load<Texture2D>("Rocket-Sheet");
            anim = new Animation(spriteTexture, 4, .1f);
            Transparency= 1.0f;
        }
        public override void Initialize()
        {
            base.Initialize();
            anim.Start();
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if(Enabled)
            {
                anim.Update(gameTime);
                input.Update();
                Position += Direction;
            }
            if ((input.PressedBack || input.PressedContinue) && !Visible)
            {
                getRndPos(this);
                Visible= true;
            }
            if(Position.X < -40)
            {
                Visible= false;
            }
        }

        public void DrawAnim(SpriteBatch s)
        {
            if(Visible)
            {
                anim.Draw(Position, s, Transparency);
            }
        }

        private void getRndPos(Sprite s)
        {
            s.Position = new Vector2(rnd.Next(GraphicsDevice.Viewport.Width) + 100, -40);
        }
    }
}
