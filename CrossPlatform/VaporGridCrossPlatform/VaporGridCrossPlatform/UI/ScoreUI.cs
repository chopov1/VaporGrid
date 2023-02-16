using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaporGridCrossPlatform.UI
{
    public class ScoreUI : MenuUI
    {
        ScoreManager sm;
        int Y;
        public ScoreUI(Game game, ScoreManager sm, int y) : base(game)
        {
            this.sm = sm;
            Y = y;
        }

        protected override void drawUI()
        {
            if (sm.ShowScore)
            {
                if (sm.ShowHighScore)
                {
                    DrawCustomString(HeaderText, "HighScore: " + sm.HighScore, 25, Color.AntiqueWhite);
                    //DrawCustomString(HeaderText, "Last Score: " + sm.prevScore, 425, Color.White);
                    
                }
                DrawCustomString(HeaderText, "Total Score: " + sm.Score, Y+30, Color.AntiqueWhite);
                DrawCustomString(HeaderText, "+" + sm.ComboScore, Y, red);
            }
        }

    }
}
