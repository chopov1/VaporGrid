using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhythmGameProto.GridClasses
{
    public class AutoTrapTile : TrapTile
    {
        int beatsInactive;
        public AutoTrapTile(Vector2 pos, Vector2 tileGridPos, TileTextures tt, RhythmManager rm) : base(pos, tileGridPos, tt, rm)
        {

        }

        protected override void setupTrapTile()
        {
            State = TrapState.inactive;
            IsWalkable = true;
            beatsActive = 3;
            beatsInactive= 4;
            beat = 0;
        }

        

        public override void tileUpdate()
        {
            base.tileUpdate();
            switch (State)
            {
                case TrapState.activate:
                    if (tickTile(2))
                    {
                        State = TrapState.active;
                    }
                    break;
                case TrapState.active:
                    if (tickTile(beatsActive))
                    {
                        State = TrapState.deactivate;
                    }
                    break;
                case TrapState.deactivate:
                    if (!rm.songPlayer.IsOnQuarter())
                    {
                        State = TrapState.inactive;
                    }
                    break;
                case TrapState.inactive:
                    if (tickTile(beatsInactive))
                    {
                        State = TrapState.activate;
                    }
                    break;
            }
            if (rm.songPlayer.IsOnQuarter())
            {
                hasTicked = false;
            }
        }
    }
}
