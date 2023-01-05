using MacksInterestingMovement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace RhythmGameProto
{
    public class MenuController : InputHandler
    {
        public bool PressedEnter { get { return PressedKey(Keys.Enter); } }
        public bool PressedSpace { get { return PressedKey(Keys.Space); } }
    }
}
