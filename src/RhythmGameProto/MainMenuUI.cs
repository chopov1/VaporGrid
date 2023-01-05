using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhythmGameProto
{
    public class MainMenuUI : MenuUI
    {
        public MainMenuUI(Game game) : base(game)
        {

        }

        protected override void drawUI()
        {
            spriteBatch.DrawString(HeaderText, "Welcome To VaporGrid!", new Vector2(100, 100), Color.PeachPuff);
            spriteBatch.DrawString(SubText, "Press enter to play", new Vector2(200, 200), Color.PeachPuff);
        }
    }
}
