using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaporGridCrossPlatform
{
    public class RhythmGameComponent : GameComponent
    {
        protected RhythmManager rhythmManager;
        public RhythmGameComponent(Game game, RhythmManager rm) : base(game)
        {
            rhythmManager = rm;
        }

        protected bool isOnQuarter()
        {
            if (rhythmManager.RhythmState == RhythmState.Quarter)
            {
                return true;
            }
            return false;
        }

        protected bool isOnSixteenth()
        {
            if (rhythmManager.RhythmState == RhythmState.Sixteenth)
            {
                return true;
            }
            return false;
        }
    }
}
