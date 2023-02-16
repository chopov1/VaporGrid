using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaporGridCrossPlatform.UI
{
    internal class GameplayUI : MenuUI
    {
        Player p;
        public GameplayUI(Game game, Player p) : base(game)
        {
            this.p = p;
        }

        protected override void drawUI()
        {
            base.drawUI();
            float scale = 1.5f;
            DrawCustomStringBacked(TitleText, TitleTextBack, accuracy(), (int)(100 * (scale * .8)), pink, purple);
        }

        //make this go away if no input, and also say something else on sixteenths or if you get a combo going maybe

        private string accuracy()
        {
            
            switch (p.EarlyOrLate)
            {
                case 0:
                    return "Perfect";
                case 1:
                    return "To early";
                case 2:
                    return "to late";
                case 3:
                    return "start!";
                default:
                    return "";

            }
        }
    }
}
