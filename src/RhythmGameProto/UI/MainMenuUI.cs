using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhythmGameProto.UI
{
    public class MainMenuUI : MenuUI
    {
        ScoreManager sm;
        public int LevelSelection;

        public MainMenuUI(Game game, ScoreManager sm) : base(game)
        {
            this.sm = sm;
        }

        protected override void drawUI()
        {
            spriteBatch.DrawString(HeaderText, "Welcome To VaporGrid!", new Vector2(100, 100), Color.PeachPuff);
            spriteBatch.DrawString(SubText, "Level Selection <" + LevelSelection + "> ", new Vector2(200, 150), Color.PeachPuff);
            spriteBatch.DrawString(SubText, "Press enter to play selected level", new Vector2(200, 200), Color.PeachPuff);
            spriteBatch.DrawString(SubText, "Press T to play tutorial", new Vector2(300, 300), Color.PeachPuff);
            spriteBatch.DrawString(HeaderText, "Previous Score: " + sm.prevScore, new Vector2(300, 400), Color.PeachPuff);
            spriteBatch.DrawString(HeaderText, "HighScore: " + sm.HighScore, new Vector2(300, 450), Color.PeachPuff);
        }
    }
}
