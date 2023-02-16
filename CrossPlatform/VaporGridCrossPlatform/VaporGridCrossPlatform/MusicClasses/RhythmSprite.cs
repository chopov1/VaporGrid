using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaporGridCrossPlatform
{
    public class RhythmSprite : GridSprite
    {
        protected RhythmManager rhythmManager;

        public RhythmSprite(Game game, GridManager gm, RhythmManager rm,string texturename, Camera camera) : base(game, gm, texturename, camera)
        {
            rhythmManager= rm;
        }

        protected bool isOnQuarter()
        {
            if(rhythmManager.RhythmState == RhythmState.Quarter)
            {
                return true;
            }
            return false;
        }

        protected bool isOnSixteenth()
        {
            if(rhythmManager.RhythmState == RhythmState.Sixteenth)
            {
                return true;
            }
            return false;
        }
    }
}
