using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhythmGameProto.GridClasses
{
    public enum TrapState { activate, active, deactivate, inactive}
    public class TrapTile : DynamicTile
    {
        public TrapState State;

        Texture2D inactiveTexture;
        Texture2D activeTexture;


        public TrapTile(Vector2 pos, Vector2 tileGridPos, TileTextures tt, RhythmManager rm) : base(pos, tileGridPos, tt, rm)
        {
            setupTrapTile();
        }

        protected virtual void setupTrapTile()
        {
            State = TrapState.inactive;
            IsWalkable = true;
            beatsActive = 8;
            beat = 0;
        }

        public override void LoadTile()
        {
            base.LoadTile();
            inactiveTexture = tt.trapInactive;
            activeTexture = tt.trapActive;
        }
        public override Texture2D getCurrentTexture()
        {
            switch (State)
            {
                case TrapState.activate:
                    return inactiveTexture;
                case TrapState.active:
                    return activeTexture;
                case TrapState.deactivate:
                    return activeTexture;
                case TrapState.inactive:
                    return inactiveTexture;
            }
            return inactiveTexture;
        }

        

        public override void tileUpdate()
        {
            base.tileUpdate();
            switch(State)
            {
                case TrapState.activate:
                    if (!rm.songPlayer.IsOnQuarter() && !hasTicked)
                    {
                        hasTicked = true;
                        beat++;
                        if (beat >= 2)
                        {
                            beat = 0;
                            State = TrapState.active;
                        }
                    }
                    break;
                case TrapState.active:
                    if (!rm.songPlayer.IsOnQuarter() && !hasTicked)
                    {
                        hasTicked = true;
                        beat++;
                        if (beat >= beatsActive)
                        {
                            beat = 0;
                            State = TrapState.deactivate;
                        }
                    }
                    break;
                case TrapState.deactivate:
                    if (!rm.songPlayer.IsOnQuarter())
                    {
                        State = TrapState.inactive;
                    }
                    break; 
                case TrapState.inactive:
                    break;
            }
            if (rm.songPlayer.IsOnQuarter())
            {
                hasTicked = false;
            }
        }

        
    }
}
