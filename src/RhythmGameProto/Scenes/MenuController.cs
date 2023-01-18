using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using RhythmGameProto;

namespace RhythmGameProto
{
    public class MenuController : InputHandler
    {
        public bool PressedEnter { get { return PressedKey(Keys.Enter); } }
        public bool PressedSpace { get { return PressedKey(Keys.Space); } }

        public bool PressedRight { get { return PressedKey(Keys.Right); } }
        public bool PressedLeft { get { return PressedKey(Keys.Left); } }

        public bool PressedT { get { return PressedKey(Keys.T); } }
    }
}
