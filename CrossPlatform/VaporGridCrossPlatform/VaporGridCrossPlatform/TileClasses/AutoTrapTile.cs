using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaporGridCrossPlatform.GridClasses
{
    //make a cooldown for reactivating this tile. Also make the time it as active much shorter maybe for art do green if it can be activated and red if it is on cooldown for the little pad in the middle
    public class AutoTrapTile : TrapTile
    {
        int beatsInactive;
        Random rnd;
        public AutoTrapTile(Vector2 pos, Vector2 tileGridPos, TileTextures tt) : base(pos, tileGridPos, tt)
        {
            
        }

        protected override void setupTrapTile()
        {
            State = TrapState.inactive;
            IsWalkable = true;
            rnd = new Random();
            beatsActive = rnd.Next(2, 5);
            beatsInactive= rnd.Next(2, 5);
            beat = rnd.Next(beatsActive);
        }

        

        public override void tileUpdate(GameTime gametime, RhythmState state)
        {
            base.tileUpdate(gametime, state);
            switch (State)
            {
                case TrapState.activate:
                    if (tickTile(2, state))
                    {
                        State = TrapState.active;
                    }
                    break;
                case TrapState.active:
                    if (tickTile(beatsActive, state))
                    {
                        State = TrapState.deactivate;
                    }
                    break;
                case TrapState.deactivate:
                    if (!isOnQuarter(state))
                    {
                        State = TrapState.inactive;
                    }
                    break;
                case TrapState.inactive:
                    if (tickTile(beatsInactive, state))
                    {
                        State = TrapState.activate;
                    }
                    break;
            }
            if (isOnQuarter(state))
            {
                hasTicked = false;
            }
        }
    }
}
