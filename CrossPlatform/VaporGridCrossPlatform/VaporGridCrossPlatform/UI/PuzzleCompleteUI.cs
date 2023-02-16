using VaporGridCrossPlatform.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace VaporGridCrossPlatform
{
    public class PuzzleCompleteUI : MenuUI
    {
        ScoreManager sm;
        int levelNum;
        public bool ShowDisplay;
        public PuzzleCompleteUI(Game game, ScoreManager sm) : base(game)
        {
            ShowDisplay= true;
            this.sm = sm;
        }

        public void setLevelNum(int lvlNum)
        {
            levelNum= lvlNum;
        }

        protected override void drawUI()
        {
            if(ShowDisplay)
            {
                float scale = 1.3f;
                base.drawUI();
                DrawCustomStringBacked(TitleText, TitleTextBack, "Puzzle " + levelNum +" Complete", (int)(100 * (scale * .8f)), pink, purple);
                DrawCustomString(HeaderText, "You Completed this puzzle in " + sm.prevNumOfMoves + " moves", (int)(200 * scale), red);
                DrawCustomString(HeaderText, "The lowest number of moves this has taken is " + sm.LowestNumberOfMoves[levelNum], (int)(300 * scale), red);
                DrawCustomString(HeaderText, "play again", (int)(400 * scale), yellow);
                DrawCustomString(HeaderText, "main menu", (int)(450 * scale), blue);
            }
        }
    }
}
