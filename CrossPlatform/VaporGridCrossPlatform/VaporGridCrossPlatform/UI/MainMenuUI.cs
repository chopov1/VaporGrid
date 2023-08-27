using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaporGridCrossPlatform.UI
{
    public class MainMenuUI : MenuUI
    {
        ScoreManager sm;
        public int LevelSelection;

        Texture2D qrCode;

        public MainMenuUI(Game game, ScoreManager sm) : base(game)
        {
            this.sm = sm;
        }

        protected override void drawUI()
        {
           
            DrawCustomStringBacked(TitleText, TitleTextBack, "VaporGrid", 120, pink, purple);
            DrawCustomStringBacked(SubText,SubTextBack ,"Game Mode:", 380, yellow, orange);
            drawGameModeOptions(420);
            DrawCustomStringBacked(SubText,SubTextBack, "Press SPACE to play", 580, yellow, orange);
            DrawCustomStringBacked(SubText, SubTextBack, "Use ARROW KEYS to change game mode", 620, yellow, orange);
            //DrawCustomStringBacked(SubText, SubTextBack, "(Use only the right joysticks and buttons)", 280, yellow, orange);
            //DrawCustomStringBacked(SubText, SubTextBack, "Press T to play tutorial", 300, yellow, orange);
            DrawCustomString(HeaderText, "Previous Score: " + sm.prevScore, 700, red);
            DrawCustomString(HeaderText, "High Score: " + sm.HighScore, 750, red);
            DrawCustomString(HeaderText, "Highest Songs Completed: " + sm.HighestSongsCompleted, 800, red);
            spriteBatch.Draw(qrCode, new Vector2(100,920), Color.AntiqueWhite);
            spriteBatch.DrawString(BasicFont, "Leave Feedback\nGet In Touch", new Vector2(100, 880), Color.AntiqueWhite);
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            qrCode = Game.Content.Load<Texture2D>("playtestQR");
        }
        void drawGameModeOptions(int y)
        {
            switch(LevelSelection)
            {
                case 0:
                    DrawCustomStringBacked(SubText, SubTextBack, "Endless", y, red, orange);
                    DrawCustomStringBacked(SubText, SubTextBack, "Tutorial", y+40, lightgrey, grey);
                    DrawCustomStringBacked(SubText, SubTextBack, "Puzzle <" + 1 + ">", y + 80, lightgrey, grey);
                    DrawCustomStringBacked(SubText, SubTextBack, "Puzzle <" + 2 + ">", y + 120, lightgrey, grey);
                    break;
                case 1:
                    DrawCustomStringBacked(SubText, SubTextBack, "Endless", y, lightgrey, grey);
                    DrawCustomStringBacked(SubText, SubTextBack, "Tutorial", y+40, red, orange);
                    DrawCustomStringBacked(SubText, SubTextBack, "Puzzle <" + 1 + ">", y + 80, lightgrey, grey);
                    DrawCustomStringBacked(SubText, SubTextBack, "Puzzle <" + 2 + ">", y + 120, lightgrey, grey);
                    break;
                case 2:
                    DrawCustomStringBacked(SubText, SubTextBack, "Endless", y, lightgrey, grey);
                    DrawCustomStringBacked(SubText, SubTextBack, "Tutorial", y + 40, lightgrey, grey);
                    DrawCustomStringBacked(SubText, SubTextBack, "Puzzle <" + 1 + ">", y+80, red, orange);
                    DrawCustomStringBacked(SubText, SubTextBack, "Puzzle <" + 2 + ">", y+120, lightgrey, grey);
                    break;
                case 3:
                    DrawCustomStringBacked(SubText, SubTextBack, "Endless", y, lightgrey, grey);
                    DrawCustomStringBacked(SubText, SubTextBack, "Tutorial", y + 40, lightgrey, grey);
                    DrawCustomStringBacked(SubText, SubTextBack, "Puzzle <" + 1 + ">", y+80, lightgrey, grey);
                    DrawCustomStringBacked(SubText, SubTextBack, "Puzzle <" + 2 + ">", y+120, red, orange);
                    break;
                default:
                    DrawCustomStringBacked(SubText, SubTextBack, "Puzzle <" + (LevelSelection - 1) + ">", y, yellow, orange);
                    break;
            }
        }

    }
}
