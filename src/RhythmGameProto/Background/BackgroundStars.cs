using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RhythmGameProto.Background;
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
        List<BgStar> stars;
        Camera camera;
        SpriteBatch sb;

        List<BgPlanet> planets;
        
        public BackgroundStars(Game game, Camera camera) : base(game)
        {
            this.camera= camera;
            stars= new List<BgStar>();
            planets= new List<BgPlanet>();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            star = Game.Content.Load<Texture2D>("BackgroundStar");
            sb = new SpriteBatch(Game.GraphicsDevice);
            setupStars();
            setupPlanets();
        }
        void setupStars()
        {
            stars = new List<BgStar>();
            for(int i =0; i < 10; i++)
            {
                stars.Add(new BgStar(Game, "BackgroundStar", camera));
            }
        }

        void setupPlanets()
        {
            planets.Add(new BgPlanet(Game, "bluePlanet/bluePlanet1", camera));
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            sb.Begin();
            foreach (BgStar s in stars)
            {
                sb.Draw(s.Texture, s.Position, Color.White * s.Transparency);
            }
            foreach(BgPlanet s in planets)
            {
                s.DrawAnim(sb);
            }
            sb.End();
        }


    }
}
