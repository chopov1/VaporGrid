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
    public class DoorTile : DynamicTile
    {
        enum DoorState { open, opening ,closed, closing, cooldown }

        DoorState openstate;
        Texture2D brokenOnB;
        Texture2D brokenOffB;

        Texture2D cooldownTexture;

        TileAnimation openAnim;
        int beatsInactive;
        public DoorTile(Vector2 pos, Vector2 tileGridPos, TileTextures tt, RhythmManager rm) : base(pos, tileGridPos, tt, rm)
        {
            IsWalkable = true;
            openstate = DoorState.open;
            beatsActive = 4;
            beatsInactive = 6;
        }

        public override void LoadTile()
        {
            base.LoadTile();
            OnBeatTexture = tt.DoorReady;
            OffBeatTexture = tt.DoorBasic;
            cooldownTexture= tt.DoorCooldown;
            brokenOffB = tt.unWalkableOffB;
            brokenOnB= tt.unWalkableOnB;
            openAnim = new TileAnimation(tt.doorOpenAnim, .1f, false);
        }
        public override void tileUpdate(GameTime gametime)
        {
            base.tileUpdate(gametime);
            openAnim.Update(gametime);
            changeWalkState();
            switch (openstate)
            {
                case DoorState.closed:
                    if (tickTile(beatsActive))
                    {
                        openstate = DoorState.opening;
                    }
                    break;
                case DoorState.cooldown:
                    if (tickTile(beatsInactive))
                    {
                        openstate = DoorState.open;
                    }
                    break;
            }
        }

        private void changeWalkState()
        {
            switch (openstate)
            {
                case DoorState.open:
                    if (!IsWalkable)
                    {
                        IsWalkable = true;
                    }
                    break;
                case DoorState.closed:
                    if (IsWalkable)
                    {
                        IsWalkable = false;
                    }
                    break;
                case DoorState.cooldown:
                    if (!IsWalkable)
                    {
                        IsWalkable = true;
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
        public void checkIfActivated(Vector2 playerPos)
        {
            if (IsUnderObject(playerPos) && !isOccupied)
            {
                isOccupied = true;
            }
            else if (!IsUnderObject(playerPos) && isOccupied)
            {
                isOccupied = false;
            }
            if (prevIsOccupied && !isOccupied && openstate == DoorState.open)
            {
                openstate = DoorState.closing;
            }
            prevIsOccupied = isOccupied;
        }

        public override Texture2D getCurrentTexture()
        {
            switch (openstate)
            {
                case DoorState.closed:
                    if(openAnim.hasFinished)
                    {
                        openAnim.Reset();
                    }
                    return getClosedTexture();
                case DoorState.opening:
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
                        openstate = DoorState.cooldown;
                        return getOpenTexture();
                    }
                case DoorState.open:
                    if (openAnim.hasFinished)
                    {
                        openAnim.Reset();
                    }
                    return getOpenTexture();
                case DoorState.closing:
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
                        openstate = DoorState.closed;
                        return getClosedTexture();
                    }
                case DoorState.cooldown: 
                    return getCooldownTexture();
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

        private Texture2D getCooldownTexture()
        {
            if (isOnQuarter())
            {
                return cooldownTexture;
            }
            else
            {
                return OffBeatTexture;
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
