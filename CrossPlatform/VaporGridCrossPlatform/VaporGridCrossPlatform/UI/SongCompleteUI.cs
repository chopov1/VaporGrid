using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace VaporGridCrossPlatform.UI
{
    internal class SongCompleteUI : MenuUI
    {
        RhythmManager rm;
        public SongCompleteUI(Game game, RhythmManager rm) : base(game)
        {
            this.rm = rm;
        }

        protected override void drawUI()
        {
            base.drawUI();
            float scale = 1.5f;
            DrawCustomStringBacked(TitleText, TitleTextBack, rm.SongsComplete + " SONG COMPLETE", (int)(400 * (scale * .8)), pink, purple);
        }
    }
}
