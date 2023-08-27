using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using VaporGridCrossPlatform.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaporGridCrossPlatform.UI
{
    public class MenuUI : DrawableGameComponent, ISceneComponenet
    {
        protected SpriteFont HeaderText;
        protected SpriteFont SubText;
        protected SpriteFont SubTextBack;
        protected SpriteFont TitleText;
        protected SpriteFont TitleTextBack;
        protected SpriteFont SmallSolid;
        protected SpriteFont BasicFont;
        protected SpriteBatch spriteBatch;

        protected int Xcenter;
        protected int Ycenter;

        protected Color yellow;
        protected Color orange;
        protected Color red;
        protected Color pink;
        protected Color purple;
        protected Color blue;
        protected Color grey;
        protected Color lightgrey;
        public MenuUI(Game game) : base(game)
        {
            yellow = new Color(255, 211, 25);
            orange = new Color(255, 144, 31);
            red = new Color(255, 41, 117);
            pink = new Color(242, 34, 255);
            purple = new Color(140, 30, 255);
            blue = new Color(89, 203, 232);
            grey = new Color(80, 80, 80);
            lightgrey = new Color(180, 180, 180);
        }

        protected override void LoadContent()
        {
            Xcenter = GraphicsDevice.Viewport.Width / 2;
            Ycenter = GraphicsDevice.Viewport.Height / 2;
            base.LoadContent();

            TitleText = Game.Content.Load<SpriteFont>("Fonts/TitleText");
            TitleTextBack = Game.Content.Load<SpriteFont>("Fonts/TitleTextBack");
            HeaderText = Game.Content.Load<SpriteFont>("Fonts/PixelText");
            SubText = Game.Content.Load<SpriteFont>("Fonts/PixelTextSub");
            SubTextBack = Game.Content.Load<SpriteFont>("Fonts/PixelTextSubBack");
            SmallSolid = Game.Content.Load<SpriteFont>("Fonts/SmallSolidText");
            BasicFont = Game.Content.Load<SpriteFont>("Fonts/BasicFont");
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
        }

        protected void DrawCustomString(SpriteFont font, string s, int Y, Color color)
        {
            spriteBatch.DrawString(font, s, new Vector2(centerText(s, font).X, Y),color);
        }

        protected void DrawCustomStringBacked(SpriteFont font, SpriteFont backFont,string s, int Y, Color backColor, Color color )
        {
            spriteBatch.DrawString(backFont, s, new Vector2(centerText(s, font).X, Y), backColor);
            spriteBatch.DrawString(font, s, new Vector2(centerText(s, font).X, Y), color);
        }

        


        protected Vector2 centerText(string s, SpriteFont font)
        {
            float x = font.MeasureString(s).X / 2;
            float y = font.MeasureString(s).Y / 2;
            return new Vector2(Xcenter - x, Ycenter - y);
        }

        public override void Initialize()
        {
            base.Initialize();
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
