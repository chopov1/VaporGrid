using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using VaporGridCrossPlatform.GridClasses;
using static System.Formats.Asn1.AsnWriter;

namespace VaporGridCrossPlatform
{
    public enum TileState { empty, occupied, neverwalkable }
    public class Tile : RhythmTile
    {
        public bool IsWalkable;
        public TileState state;
        public TileState prevTileState;
        public Vector2 tilePos;
        public Vector2 tileGridPos;

        public Texture2D OnBeatTexture;
        public Texture2D OffBeatTexture;

        protected TileTextures tt;
        public Tile(Vector2 pos, Vector2 tileGridPos, TileTextures tt, RhythmManager rm) : base(rm)
        {
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

        public virtual void tileUpdate(GameTime gametime)
        {
          
        }

        public virtual Texture2D getCurrentTexture()
        {
            if (rhythmManager.RhythmState == RhythmState.Quarter)
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
