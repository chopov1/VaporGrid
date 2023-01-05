using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RhythmGameProto
{
    public enum TileState { empty, occupied, neverwalkable}
    public class Tile
    {
        public bool IsWalkable;
        public TileState state;
        public TileState prevTileState;
        public Vector2 tilePos;
        public Vector2 tileGridPos;
         
        public Tile(Vector2 pos, Vector2 tileGridPos)
        {
            tilePos = pos;
            state = TileState.empty;
            this.tileGridPos = tileGridPos;
        }

        public virtual void OnTileStateChange()
        {
            prevTileState = state;
        }

        public virtual void tileUpdate()
        {
            switch (state)
            {
                case TileState.empty:
                    IsWalkable = true;
                    break;
                case TileState.occupied:
                    IsWalkable= false;
                    break;
                case TileState.neverwalkable:
                    IsWalkable = false;
                    break;
            }
        }
    }
}
