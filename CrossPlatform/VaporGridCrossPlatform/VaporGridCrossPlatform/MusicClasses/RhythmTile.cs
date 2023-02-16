using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VaporGridCrossPlatform.GridClasses;

namespace VaporGridCrossPlatform
{
    public class RhythmTile
    {
        protected RhythmManager rhythmManager;
        public RhythmTile(RhythmManager rm)
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
