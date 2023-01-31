using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhythmGameProto.GameUtility
{
    public class BackgroundStars : DrawableGameComponent
    {
        Texture2D star;
        List<BgStar> sprites;
        Camera camera;
        SpriteBatch sb;
        
        public BackgroundStars(Game game, Camera camera) : base(game)
        {
            this.camera= camera;
            sprites= new List<BgStar>();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            star = Game.Content.Load<Texture2D>("BackgroundStar");
            sb = new SpriteBatch(Game.GraphicsDevice);
            setupStars();
        }
        void setupStars()
        {
            sprites = new List<BgStar>();
            for(int i =0; i < 10; i++)
            {
                sprites.Add(new BgStar(Game, "BackgroundStar", camera));
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            sb.Begin();
            foreach (BgStar s in sprites)
            {
                sb.Draw(s.Texture, s.Position, Color.White * s.Transparency);
            }
            sb.End();
        }


    }
}
