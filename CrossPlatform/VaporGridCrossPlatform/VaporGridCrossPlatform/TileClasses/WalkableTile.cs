using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaporGridCrossPlatform.GridClasses
{
    public class WalkableTile : Tile
    {
        public WalkableTile(Vector2 pos, Vector2 tileGridPos, TileTextures tt) : base(pos, tileGridPos, tt)
        {
            IsWalkable = true;
        }

        public override void LoadTile()
        {
            base.LoadTile();
            OnBeatTexture = tt.walkableOnB;
            OffBeatTexture = tt.walkableOffB;
        }
    }
}
