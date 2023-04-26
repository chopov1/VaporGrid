using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaporGridCrossPlatform.GridClasses
{
    public class DynamicTile : Tile
    {

        protected int beatsActive;
        protected int beat;
        protected bool hasTicked;

        public DynamicTile(Vector2 pos, Vector2 tileGridPos, TileTextures tt) : base(pos, tileGridPos, tt)
        {

        }
        public bool IsUnderObject(Vector2 pos)
        {
            if (pos == tileGridPos)
            {
                return true;
            }
            return false;
        }

        protected bool tickTile(int changeThresh, RhythmState state)
        {
            if(isOnQuarter(state) && hasTicked)
            {
                hasTicked = false;
            }
            if (!isOnQuarter(state) && !hasTicked)
            {
                hasTicked = true;
                beat++;
                if (beat >= changeThresh)
                {
                    beat = 0;
                    return true;
                }
            }
            return false;
        }
    }
}
