using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhythmGameProto
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
            spriteBatch.DrawString(HeaderText, "Game Over", new Vector2(100, 100), Color.PeachPuff);
            spriteBatch.DrawString(HeaderText, "Score: " + sm.prevScore, new Vector2(200, 200), Color.PeachPuff);
            spriteBatch.DrawString(HeaderText, "HighScore: " + sm.HighScore, new Vector2(200, 300), Color.PeachPuff);
            spriteBatch.DrawString(SubText, "press enter to return to main menu", new Vector2(100, 420), Color.PeachPuff);
            spriteBatch.DrawString(SubText, "press space to try again", new Vector2(100, 400), Color.PeachPuff);
        }

    }
}
