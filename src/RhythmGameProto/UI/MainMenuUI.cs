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
        public MainMenuUI(Game game) : base(game)
        {

        }

        protected override void drawUI()
        {
            spriteBatch.DrawString(HeaderText, "Welcome To VaporGrid!", new Vector2(100, 100), Color.PeachPuff);
            spriteBatch.DrawString(SubText, "Press enter to play", new Vector2(200, 200), Color.PeachPuff);
            spriteBatch.DrawString(SubText, "Press T to play tutorial", new Vector2(300, 300), Color.PeachPuff);
        }
    }
}
