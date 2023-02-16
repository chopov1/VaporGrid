using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaporGridCrossPlatform.UI
{
    internal class PuzzleFailUI : MenuUI
    {
        public bool ShowDisplay;
        public PuzzleFailUI(Game game) : base(game)
        {

        }

        protected override void drawUI()
        {
            if(ShowDisplay)
            {
                float scale = 1.3f;
                base.drawUI();
                DrawCustomStringBacked(TitleText, TitleTextBack, "Game Over", (int)(100 * (scale*.8)), pink, purple);
                DrawCustomString(HeaderText, "play again", (int)(400 * scale), yellow);
                DrawCustomString(HeaderText, "main menu", (int)(450 * scale), blue);
            }
        }
    }
}
