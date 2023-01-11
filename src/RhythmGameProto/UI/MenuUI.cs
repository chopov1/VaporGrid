using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhythmGameProto.UI
{
    public class MenuUI : DrawableGameComponent, ISceneComponenet
    {
        protected SpriteFont HeaderText;
        protected SpriteFont SubText;
        protected SpriteBatch spriteBatch;
        public MenuUI(Game game) : base(game)
        {

        }

        protected override void LoadContent()
        {
            base.LoadContent();
            HeaderText = Game.Content.Load<SpriteFont>("PixelText");
            SubText = Game.Content.Load<SpriteFont>("PixelTextSub");
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            spriteBatch.Begin();
            drawUI();
            spriteBatch.End();
        }

        protected virtual void drawUI()
        {

        }

        public void Load()
        {
            Enabled = true;
            Visible = true;
        }

        public void UnLoad()
        {
            Enabled = false;
            Visible = false;
        }
    }
}
