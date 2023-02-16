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
           
            DrawCustomStringBacked(TitleText, TitleTextBack, "Welcome To VaporGrid!", 120, pink, purple);
            DrawCustomStringBacked(SubText,SubTextBack ,"Game Mode:", 380, yellow, orange);
            drawGameModeOptions(420);
            DrawCustomStringBacked(SubText,SubTextBack, "Press Yellow button to play", 480, yellow, orange);
            DrawCustomStringBacked(SubText, SubTextBack, "Use Joystick to change game mode", 520, yellow, orange);
            //DrawCustomStringBacked(SubText, SubTextBack, "(Use only the right joysticks and buttons)", 280, yellow, orange);
            //DrawCustomStringBacked(SubText, SubTextBack, "Press T to play tutorial", 300, yellow, orange);
            DrawCustomString(HeaderText, "Previous Score: " + sm.prevScore, 600, red);
            DrawCustomString(HeaderText, "HighScore: " + sm.HighScore, 650, red);
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
                    break;
                case 1:
                    DrawCustomStringBacked(SubText, SubTextBack, "Tutorial", y, blue, orange);
                    break;
                default:
                    DrawCustomStringBacked(SubText, SubTextBack, "Puzzle <" + (LevelSelection - 1) + ">", y, yellow, orange);
                    break;
            }
        }

    }
}
