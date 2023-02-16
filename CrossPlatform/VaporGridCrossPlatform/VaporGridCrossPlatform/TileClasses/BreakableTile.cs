using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using VaporGridCrossPlatform.GridClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace VaporGridCrossPlatform
{
    public class BreakableTile : DynamicTile
    {
        enum BrokenState { open, opening ,closed, closing }

        BrokenState openstate;
        Texture2D brokenOnB;
        Texture2D brokenOffB;

        TileAnimation openAnim;
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
            OnBeatTexture = tt.DoorOnB;
            OffBeatTexture = tt.DoorOffB;
            brokenOffB = tt.unWalkableOffB;
            brokenOnB= tt.unWalkableOnB;
            openAnim = new TileAnimation(tt.doorOpenAnim, .1f, false);
        }
        public override void tileUpdate(GameTime gametime)
        {
            base.tileUpdate(gametime);
            openAnim.Update(gametime);
            changeWalkState();
            if (openstate == BrokenState.closed)
            {
                if (tickTile(beatsActive))
                {
                    openstate= BrokenState.opening;
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
                default:
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
                openstate = BrokenState.closing;
            }
            prevIsOccupied = isOccupied;
        }

        public override Texture2D getCurrentTexture()
        {
            switch (openstate)
            {
                case BrokenState.closed:
                    if(openAnim.hasFinished)
                    {
                        openAnim.Reset();
                    }
                    return getClosedTexture();
                case BrokenState.opening:
                    if (!openAnim.hasFinished)
                    {
                        if(openAnim.active == false)
                        {
                            openAnim.StartRev();
                        }
                        return openAnim.getCurrentFrame();
                    }
                    else
                    {
                        openstate = BrokenState.open;
                        return getOpenTexture();
                    }
                case BrokenState.open:
                    if (openAnim.hasFinished)
                    {
                        openAnim.Reset();
                    }
                    return getOpenTexture();
                case BrokenState.closing:
                    if (!openAnim.hasFinished)
                    {
                        if (openAnim.active == false)
                        {
                            openAnim.Start();
                        }
                        return openAnim.getCurrentFrame();
                    }
                    else
                    {
                        openstate = BrokenState.closed;
                        return getClosedTexture();
                    }
                default: return base.getCurrentTexture();
            }
            
        }

        private Texture2D getClosedTexture()
        {
            if (isOnQuarter())
            {
                return brokenOnB;
            }
            else
            {
                return brokenOffB;
            }
        }

        private Texture2D getOpenTexture()
        {
            if (isOnQuarter())
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
