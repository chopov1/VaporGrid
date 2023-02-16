using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhythmGameProto.GridClasses
{
    public class UnWalkableTile : Tile
    {
        public UnWalkableTile(Vector2 pos, Vector2 tileGridPos, TileTextures tt, RhythmManager rm) : base(pos, tileGridPos, tt, rm)
        {
            IsWalkable = false;
        }

        public override void LoadTile()
        {
            base.LoadTile();
            OnBeatTexture = tt.unWalkableOnB;
            OffBeatTexture = tt.unWalkableOffB;
        }
    }
}
