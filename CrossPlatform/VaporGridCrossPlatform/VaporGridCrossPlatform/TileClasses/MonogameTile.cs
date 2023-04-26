using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace VaporGridCrossPlatform
{
    public class MonogameTile : Sprite
    {
        public bool IsWalkable { get { return tile.IsWalkable; } }
        public TileState State { get { return tile.state; } set { tile.state = value; } }
        public TileState PrevState { get { return tile.prevTileState; } set { tile.prevTileState = value; } }

        public Tile tile;
        RhythmManager rm;

        public MonogameTile(Game game, Tile t, Camera camera, RhythmManager rm) : base(game, "TileGray", camera)
        {
            this.rm = rm;
            tile = t;
            Game.Components.Add(this);
        }

        public void ResetTile(Tile t)
        {
            tile = t;
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            tile.LoadTile();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            tile.tileUpdate(gameTime, rm.RhythmState);
            updateTileTexture();
        }

        protected void updateTileTexture()
        {
            spriteTexture = tile.getCurrentTexture(rm.RhythmState);
        }

    }
}
