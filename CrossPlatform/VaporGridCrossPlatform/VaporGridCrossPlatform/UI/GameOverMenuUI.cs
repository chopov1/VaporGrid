using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaporGridCrossPlatform.UI
{
    public class GameOverMenuUI : MenuUI
    {
        ScoreManager sm;
        public GameOverMenuUI(Game game, ScoreManager sm) : base(game)
        {
            this.sm = sm;
        }

        protected override void drawUI()
        {
            float scale = 1.4f;
            base.drawUI();
            DrawCustomStringBacked(TitleText, TitleTextBack, "Game Over", (int )(100 * (scale * .8)), pink, purple);
            DrawCustomString(HeaderText, "Score: " + sm.prevScore, (int)(200 * scale), red);
            DrawCustomString(HeaderText, "HighScore: " + sm.HighScore, (int)(300 * scale), red);
            DrawCustomString(HeaderText, "play again", (int)(400 * scale), yellow);
            DrawCustomString(HeaderText, "main menu", (int)(450 * scale), blue);
        }

    }
}
