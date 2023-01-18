using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RhythmGameProto.GridClasses;
using SharpDX.Direct2D1.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RhythmGameProto
{
    public enum TileState { empty, occupied, neverwalkable }
    public class Tile
    {
        public bool IsWalkable;
        public TileState state;
        public TileState prevTileState;
        public Vector2 tilePos;
        public Vector2 tileGridPos;

        public Texture2D OnBeatTexture;
        public Texture2D OffBeatTexture;

        protected TileTextures tt;
        protected RhythmManager rm;
        public Tile(Vector2 pos, Vector2 tileGridPos, TileTextures tt, RhythmManager rm)
        {
            this.rm = rm;
            tilePos = pos;
            state = TileState.empty;
            this.tileGridPos = tileGridPos;
            this.tt = tt;
            if(tt != null)
            {
                LoadTile();
            }
        }

        public virtual void LoadTile()
        {

        }
        public virtual void OnTileStateChange()
        {
            prevTileState = state;
        }

        public virtual void tileUpdate()
        {
          
        }

        public virtual Texture2D getCurrentTexture()
        {
            if (rm.songPlayer.IsOnQuarter())
            {
                return OnBeatTexture;
            }
            else
            {
                return OffBeatTexture;
            }
        }

        

    }
}
