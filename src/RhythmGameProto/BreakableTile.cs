using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhythmGameProto
{
    public class BreakableTile : Tile
    {
        public BreakableTile(Vector2 pos, Vector2 tileGridPos ) : base(pos, tileGridPos)
        {

        }

        //need to know if player occupies this tile. 
        //Should I have player keep track of this?
        //Yes I think it would be more performant
        //Maybe I make a prevoccupiedtile var in player so that I can set tile states to unoccupied after player leaves


        public override void tileUpdate()
        {
            base.tileUpdate();
            
        }

        public override void OnTileStateChange()
        {
            base.OnTileStateChange();
            checkIfBroken();
        }

        public void checkIfBroken()
        {
            if (prevTileState == TileState.occupied && state == TileState.empty)
            {
                IsWalkable = false;
            }
        }
    }
}
