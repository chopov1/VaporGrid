using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhythmGameProto
{
    public class Sprite : DrawableGameComponent, ISceneComponenet
    {
        public Vector2 Position, Direction, Origin;
        public Rectangle Rect;

        protected SpriteBatch spriteBatch;
        SpriteEffects effect;

        protected Texture2D spriteTexture;
        public string TextureName;
        float scale;
        protected float rotation;

        protected Camera camera;
        public Sprite(Game game, string texturename, Camera camera) : base(game)
        {
            this.camera = camera;
            TextureName = texturename;
            scale = 1;
            rotation = 0;
            Position = new Vector2(100, 100);
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            spriteTexture = Game.Content.Load<Texture2D>(TextureName);
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            effect = SpriteEffects.None;
            this.Origin = new Vector2(this.spriteTexture.Width / 2, this.spriteTexture.Height / 2);
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            updateRectangeForDrawing();
        }
        private void updateRectangeForDrawing()
        {
            Rect.X = (int)Position.X;
            Rect.Y = (int)Position.Y;
            Rect.Width = (int)(spriteTexture.Width * this.scale);
            Rect.Height = (int)(spriteTexture.Height * this.scale);
        }

        public override void Draw(GameTime gameTime)
        {
            
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, camera.Transform);
            spriteBatch.Draw(spriteTexture,
                Rect,
                null,
                Color.White,
                rotation,
                this.Origin,
                effect,
                0);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public void Load()
        {
            Enabled= true;
            Visible= true;
        }

        public void UnLoad()
        {
            Enabled= false;
            Visible= false;
        }
    }
}
