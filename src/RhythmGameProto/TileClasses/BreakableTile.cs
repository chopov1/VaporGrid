using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RhythmGameProto.GridClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhythmGameProto
{
    public class BreakableTile : DynamicTile
    {
        enum BrokenState { open, closed }

        BrokenState openstate;
        Texture2D brokenOnB;
        Texture2D brokenOffB;
        public BreakableTile(Vector2 pos, Vector2 tileGridPos, TileTextures tt, RhythmManager rm) : base(pos, tileGridPos, tt, rm)
        {
            IsWalkable = true;
            openstate = BrokenState.open;
            beatsActive = 8;
        }

        //need to know if player occupies this tile. 
        //Should I have player keep track of this?
        //Yes I think it would be more performant
        //Maybe I make a prevoccupiedtile var in player so that I can set tile states to unoccupied after player leaves

        public override void LoadTile()
        {
            base.LoadTile();
            OnBeatTexture = tt.testOnB;
            OffBeatTexture = tt.testOffB;
            brokenOffB = tt.unWalkableOffB;
            brokenOnB= tt.unWalkableOnB;
        }
        public override void tileUpdate()
        {
            base.tileUpdate();
            changeWalkState();
            if (openstate == BrokenState.closed)
            {
                if (tickTile(beatsActive))
                {
                    openstate= BrokenState.open;
                }
            }
        }

        private void changeWalkState()
        {
            switch (openstate)
            {
                case BrokenState.open:
                    if (!IsWalkable)
                    {
                        IsWalkable = true;
                    }
                    break;
                case BrokenState.closed:
                    if (IsWalkable)
                    {
                        IsWalkable = false;
                    }
                    break;
            }
        }


        bool prevIsOccupied;
        bool isOccupied;
        public void checkIfBroken(Vector2 playerPos)
        {
            if (IsUnderObject(playerPos) && !isOccupied)
            {
                isOccupied = true;
            }
            else if (!IsUnderObject(playerPos) && isOccupied)
            {
                isOccupied = false;
            }
            if (prevIsOccupied && !isOccupied)
            {
                openstate = BrokenState.closed;
            }
            prevIsOccupied = isOccupied;
        }

        public override Texture2D getCurrentTexture()
        {
            switch (openstate)
            {
                case BrokenState.closed:
                    if (rm.songPlayer.IsOnQuarter())
                    {
                        return brokenOnB;
                    }
                    else
                    {
                        return brokenOffB;
                    }
                case BrokenState.open:
                    if (rm.songPlayer.IsOnQuarter())
                    {
                        return OnBeatTexture;
                    }
                    else
                    {
                        return OffBeatTexture;
                    }
                    
                default: return base.getCurrentTexture();
            }
            
        }
    }
}
