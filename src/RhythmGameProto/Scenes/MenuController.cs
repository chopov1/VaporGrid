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
        public bool PressedContinue { get {
                if (isUsingGamepad())
                {
                    return IsButtonPressed(Buttons.A);
                }
                return PressedKey(Keys.Space); } }
        public bool PressedBack { get {
                if (isUsingGamepad())
                {
                    return IsButtonPressed(Buttons.B);
                }
                return PressedKey(Keys.Enter); } }

        public bool PressedRight { get {
                if (isUsingGamepad())
                {
                    return IsDirectionDown("Right", 1);
                }
                return PressedKey(Keys.Right); } }
        public bool PressedLeft { get {
                if (isUsingGamepad())
                {
                    return IsDirectionDown("Left", 1);
                }
                return PressedKey(Keys.Left); } }

        public bool PressedT { get {
                if (isUsingGamepad())
                {
                    return IsButtonPressed(Buttons.Y);
                }
                return PressedKey(Keys.T); } }
    }
}
