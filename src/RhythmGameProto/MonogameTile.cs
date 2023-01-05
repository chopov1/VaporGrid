using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;

namespace RhythmGameProto
{
    public class MonogameTile : Sprite
    {
        public bool IsWalkable { get { return tile.IsWalkable; } }
        public TileState State { get { return tile.state; } set { tile.state = value; } }
        public TileState prevState { get { return tile.prevTileState; } set { tile.prevTileState = value; } }

        public Tile tile;
        RhythmManager rm;

        Texture2D currentTexture1;
        Texture2D currentTexture2;

        Texture2D walkableTexture1;
        Texture2D walkableTexture2;
        Texture2D unWalkableTexture1;
        Texture2D unWalkableTexture2;

        private string walkableTile;
        private string walkableTile2;
        private string unwalkableTile;
        private string unwalkableTile2;

        string bct;
        public MonogameTile(Game game, bool isWalkable, Tile t, Camera camera, RhythmManager rm) : base(game, "TileGray", camera)
        {
            walkableTile = "TileGray";
            walkableTile2 = "TileGrayLight";
            unwalkableTile = "TileBlackPurple";
            unwalkableTile2 = "TileBlackBlue";
            this.rm = rm;
            tile = t;
            tile.IsWalkable = isWalkable;
            if (!tile.IsWalkable)
            {
                tile.state = TileState.neverwalkable;
            }
            Game.Components.Add(this);
        }

        public void ResetTile(bool isWalkable)
        {
            tile.IsWalkable = isWalkable;
            if(isWalkable)
            {
                tile.state = TileState.empty;
                currentTexture1 = walkableTexture1;
                currentTexture2 = walkableTexture2;
            }
            else{
                tile.state = TileState.neverwalkable;
                currentTexture1 = unWalkableTexture1;
                currentTexture2= unWalkableTexture2;
            }
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            walkableTexture1 = Game.Content.Load<Texture2D>(walkableTile);
            walkableTexture2 = Game.Content.Load<Texture2D>(walkableTile2);
            unWalkableTexture1 = Game.Content.Load<Texture2D>(unwalkableTile);
            unWalkableTexture2 = Game.Content.Load<Texture2D>(unwalkableTile2);
            ResetTile(IsWalkable);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            tile.tileUpdate();
            changeTexture();
        }

        private void changeTexture()
        {
            if (rm.songPlayer.IsOnQuarter())
            {
                spriteTexture = currentTexture2;
            }
            else
            {
                spriteTexture = currentTexture1;
            }
        }
        
    }
}
