using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RhythmGameProto.GridClasses
{
    public class TileTextures : DrawableGameComponent
    {
        public Texture2D walkableOnB;
        public Texture2D walkableOffB;

        public Texture2D unWalkableOnB;
        public Texture2D unWalkableOffB;

        public Texture2D trapActive;
        public Texture2D trapInactive;

        public Texture2D testOnB;
        public Texture2D testOffB;

        public Vector2 tileSpriteSize { get { return new Vector2(unWalkableOffB.Width, unWalkableOffB.Height); } }

        public TileTextures(Game game) : base(game)
        {
            Game.Components.Add(this);
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            LoadTileTextures();
        }
        public void LoadTileTextures()
        {
            walkableOnB = Game.Content.Load<Texture2D>("TileGrayLight");
            walkableOffB = Game.Content.Load<Texture2D>("TileGray");

            unWalkableOnB = Game.Content.Load<Texture2D>("TileBlackBlue");
            unWalkableOffB = Game.Content.Load<Texture2D>("TileBlackPurple");

            trapActive = Game.Content.Load<Texture2D>("TileTrapActive");
            trapInactive = Game.Content.Load<Texture2D>("TileTrapInactive");

            testOnB = Game.Content.Load<Texture2D>("TileTest1");
            testOffB = Game.Content.Load<Texture2D>("TileTest2");
        }
    }
}
