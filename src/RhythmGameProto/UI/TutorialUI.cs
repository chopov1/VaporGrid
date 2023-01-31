﻿using Microsoft.Xna.Framework;
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
            subtext = new string[4,3];
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
          

            subtext[1, 0] = "The red number increases as you move on beat";
            subtext[1, 1] = "If you try to move offbeat twice, this number will reset";
            subtext[1, 2] = "Collect apples to add your the red number to your Score";

            subtext[2, 0] = "Double time moves can be made when your highlighted yellow";
            subtext[2, 1] = "You will not flash yellow untill the red number is above 16";
            subtext[2, 2] = "";
           

            subtext[3, 0] = "When an enemy turns red it is about to move";
            subtext[3, 1] = "If an enemy enters the same tile as you, it is Game Over.";
            subtext[3, 2] = "";

        }

        protected override void drawUI()
        {
            DrawCustomStringBacked(TitleText, TitleTextBack, header[Stage], 100, pink, purple);
            DrawCustomString(SmallSolid, subtext[Stage, 0], 325, Color.White);
            DrawCustomString(SmallSolid, subtext[Stage, 1], 400, Color.White);
            DrawCustomString(SmallSolid, subtext[Stage, 2], 475, Color.White);
            DrawCustomString(HeaderText, instructions[Stage], 520, orange);
            
        }
    }
}
