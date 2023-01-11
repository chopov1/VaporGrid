using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhythmGameProto.UI
{
    public class TutorialUI : MenuUI
    {
        string[] header;
        string[,] subtext;
        string[] instructions;

        public int Stage;
        public TutorialUI(Game game) : base(game)
        {
            header = new string[4];
            subtext = new string[4,5];
            instructions = new string[4];
            header[0] = "Movement";
            header[1] = "Score";
            header[2] = "Double Time";
            header[3] = "Enemies";

            instructions[0] = "Reach the EXIT tile to continue";
            instructions[1] = "Obtain a score of 5 to continue";
            instructions[2] = "Perform a double time move to continue";
            instructions[3] = "End the game to exit the tutorial";

            subtext[0, 0] = "Press the arrow shown on the tile you wish to move to";
            subtext[0, 1] = "You must input ON BEAT or your character will not move";
            subtext[0, 2] = "";
            subtext[0, 3] = "";
            subtext[0, 4] = "";

            subtext[1, 0] = "The number with a + next to it is your Combo";
            subtext[1, 1] = "Combo increases as you move to the beat";
            subtext[1, 2] = "If you try to move offbeat twice, your Combo will reset";
            subtext[1, 3] = "Collect blue power cells to add your Combo to your Score";
            subtext[1, 4] = "Your total score will not reset untill the game ends";

            subtext[2, 0] = "A double time move can be made on the sixteenth note immediatly after a successful move";
            subtext[2, 1] = "Your character will flash yellow on that sixteenth note";
            subtext[2, 2] = "Double time moves increase combo by 2";
            subtext[2, 3] = "When your Combo is above 16, you can perform double time moves";
            subtext[2, 4] = "Your Combo is locked at 17 for this tutorial";

            subtext[3, 0] = "When an enemy turns red it is about to move";
            subtext[3, 1] = "If an enemy enters the same tile as you, it is Game Over.";
            subtext[3, 2] = "";
            subtext[3, 3] = "";
            subtext[3, 4] = "";
        }

        protected override void drawUI()
        {
            spriteBatch.DrawString(HeaderText, header[Stage], new Vector2(150, 200), Color.PeachPuff);
            spriteBatch.DrawString(SubText, subtext[Stage, 0], new Vector2(100, 300), Color.PeachPuff);
            spriteBatch.DrawString(SubText, subtext[Stage, 1], new Vector2(100, 350), Color.PeachPuff);
            spriteBatch.DrawString(SubText, subtext[Stage, 2], new Vector2(100, 400), Color.PeachPuff);
            spriteBatch.DrawString(SubText, subtext[Stage, 3], new Vector2(100, 450), Color.PeachPuff);
            spriteBatch.DrawString(SubText, subtext[Stage, 4], new Vector2(100, 500), Color.PeachPuff);
            spriteBatch.DrawString(HeaderText, instructions[Stage], new Vector2(100, 550), Color.PeachPuff);
        }
    }
}
