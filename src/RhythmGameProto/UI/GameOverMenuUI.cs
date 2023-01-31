using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhythmGameProto.UI
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
            base.drawUI();
            DrawCustomStringBacked(TitleText, TitleTextBack, "Game Over", 100, pink, purple);
            DrawCustomString(HeaderText, "Score: " + sm.prevScore, 200, red);
            DrawCustomString(HeaderText, "HighScore: " + sm.HighScore, 300, red);
            DrawCustomString(HeaderText, "press space or A to try again", 400, orange);
            DrawCustomString(HeaderText, "press backspace or B to return to main menu", 420, orange);
        }

    }
}
