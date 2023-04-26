using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaporGridCrossPlatform.GridClasses
{
    public enum TrapState { activate, active, deactivate, inactive}
    public class TrapTile : DynamicTile
    {
        public TrapState State;

        Texture2D inactiveTexture;
        Texture2D activatingTexture;

        TileAnimation activeAnim;
        TileAnimation activatingAnim;

        public TrapTile(Vector2 pos, Vector2 tileGridPos, TileTextures tt) : base(pos, tileGridPos, tt)
        {
            setupTrapTile();
        }

        protected virtual void setupTrapTile()
        {
            State = TrapState.inactive;
            IsWalkable = true;
            beatsActive = 8;
            beat = 0;
        }

        public override void LoadTile()
        {
            base.LoadTile();
            inactiveTexture = tt.trapInactive;
            activatingTexture = tt.trapActivating;
            activatingAnim = new TileAnimation(tt.trapActivatingAnim, .1f,  false);
            activatingAnim.looping = false;
            activeAnim = new TileAnimation(tt.trapActiveAnim, .1f, true);
        }
        public override Texture2D getCurrentTexture(RhythmState state)
        {
            switch (State)
            {
                case TrapState.activate:
                    return activatingTexture;
                case TrapState.active:
                    if (!activatingAnim.hasFinished)
                    {
                        if (activatingAnim.active == false)
                        {
                            activatingAnim.Start();
                        }
                        return activatingAnim.getCurrentFrame();
                    }
                    else
                    {
                        if(activeAnim.active == false)
                        {
                            activeAnim.Start();
                        }
                        return activeAnim.getCurrentFrame();
                    }
                case TrapState.deactivate:
                   
                    return activeAnim.getCurrentFrame();
                case TrapState.inactive:
                    if (activeAnim.active)
                    {
                        activeAnim.Stop();
                        activeAnim.Reset();
                    }
                    if (activatingAnim.hasFinished)
                    {
                        activatingAnim.Stop();
                        activatingAnim.Reset();
                    }
                    return inactiveTexture;
            }
            return inactiveTexture;
        }

        public override void tileUpdate(GameTime gametime, RhythmState state)
        {
            activatingAnim.Update(gametime);
            activeAnim.Update(gametime);
            base.tileUpdate(gametime, state);
            switch(State)
            {
                case TrapState.activate:
                    if (!isOnQuarter(state) && !hasTicked)
                    {
                        hasTicked = true;
                        beat++;
                        if (beat >= 2)
                        {
                            beat = 0;
                            State = TrapState.active;
                        }
                    }
                    break;
                case TrapState.active:
                    if (!isOnQuarter(state) && !hasTicked)
                    {
                        hasTicked = true;
                        beat++;
                        if (beat >= beatsActive)
                        {
                            beat = 0;
                            State = TrapState.deactivate;
                        }
                    }
                    break;
                case TrapState.deactivate:
                    if (!isOnQuarter(state))
                    {
                        State = TrapState.inactive;
                    }
                    break; 
                case TrapState.inactive:
                    break;
            }
            if (isOnQuarter(state))
            {
                hasTicked = false;
            }
        }

    }
}
