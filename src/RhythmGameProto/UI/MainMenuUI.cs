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
            spriteBatch.DrawString(TitleTextBack, "Welcome To VaporGrid!", new Vector2(centerText("Welcome To VaporGrid!", TitleTextBack).X, 100), pink);
            spriteBatch.DrawString(TitleText, "Welcome To VaporGrid!", new Vector2(centerText("Welcome To VaporGrid!", TitleText).X, 100), purple);
            DrawCustomStringBacked(SubText,SubTextBack ,"Game Mode:", 180, yellow, orange);
            drawGameModeOptions();
            DrawCustomStringBacked(SubText,SubTextBack, "Press enter to play selected level", 250, yellow, orange);
            DrawCustomStringBacked(SubText, SubTextBack, "Press T to play tutorial", 300, yellow, orange);
            DrawCustomString(HeaderText, "Previous Score: " + sm.prevScore, 400, red);
            DrawCustomString(HeaderText, "HighScore: " + sm.HighScore, 450, red);
        }

        void drawGameModeOptions()
        {
            switch(LevelSelection)
            {
                case 0:
                    DrawCustomStringBacked(SubText, SubTextBack, "Endless", 200, yellow, orange);
                    break;
                default:
                    DrawCustomStringBacked(SubText, SubTextBack, "Puzzle <" + LevelSelection + ">", 200, yellow, orange);
                    break;
            }
        }

    }
}
