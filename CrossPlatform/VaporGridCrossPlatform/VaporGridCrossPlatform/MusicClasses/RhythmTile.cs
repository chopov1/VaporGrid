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
        public RhythmTile()
        {
        }

        protected bool isOnQuarter(RhythmState state)
        {
            if (state == RhythmState.Quarter)
            {
                return true;
            }
            return false;
        }

        protected bool isOnSixteenth(RhythmState state)
        {
            if (state == RhythmState.Sixteenth)
            {
                return true;
            }
            return false;
        }
    }
}
