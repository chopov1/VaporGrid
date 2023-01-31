using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhythmGameProto.UI
{
    public class ScoreUI : MenuUI
    {
        ScoreManager sm;
        public ScoreUI(Game game, ScoreManager sm) : base(game)
        {
            this.sm = sm;   
        }

        protected override void drawUI()
        {
            if (sm.ShowScore)
            {
                if (sm.ShowHighScore)
                {
                    DrawCustomString(HeaderText, "HighScore: " + sm.HighScore, 25, Color.White);
                    //DrawCustomString(HeaderText, "Last Score: " + sm.prevScore, 425, Color.White);
                    
                }
                DrawCustomString(HeaderText, "Total Score: " + sm.Score, 40, Color.White);
                DrawCustomString(HeaderText, "+" + sm.ComboScore, 80, red);
            }
        }

    }
}
