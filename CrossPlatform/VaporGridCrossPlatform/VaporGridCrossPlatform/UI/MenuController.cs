using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using VaporGridCrossPlatform;

namespace VaporGridCrossPlatform
{
    public class MenuController : InputHandler
    {
        public bool PressedContinue { get {
                if (isUsingGamepad())
                {
                    return IsButtonPressed(Buttons.Y);
                }
                return PressedKey(Keys.Space); } }
        public bool PressedBack { get {
                if (isUsingGamepad())
                {
                    return IsButtonPressed(Buttons.B);
                }
                return PressedKey(Keys.Back); } }

        public bool PressedRight { get {
                if (isUsingGamepad())
                {
                    return IsDirectionDown("Right", 0);
                }
                return PressedKey(Keys.Right); } }
        public bool PressedLeft { get {
                if (isUsingGamepad())
                {
                    return IsDirectionDown("Left", 0);
                }
                return PressedKey(Keys.Left); } }

    }
}
