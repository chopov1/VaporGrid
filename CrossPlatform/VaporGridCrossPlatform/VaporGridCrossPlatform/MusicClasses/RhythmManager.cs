using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;
using VaporGridCrossPlatform.GameUtility;

namespace VaporGridCrossPlatform
{
    public enum SongState { reset, turnOn, running}
    public class RhythmManager : GameComponent, ISceneComponenet
    {
        public RhythmState RhythmState { get { return songPlayer.CurrentRhythmState; } }

        public double bpm;
        public float beat;

        public float inputTime;
        public int Transpose { get { return songPlayer.Transpose; } }

        public SongState state;

        public int SongsComplete;

        public SongPlayer songPlayer;

        public int slowOrFastQuarter { get { return songPlayer.SlowOrFastQuarter(); } }
        public int slowOrFastSixteenth { get { return songPlayer.SlowOrFastSixteenth(); } }

        public RhythmManager(Game game) : base(game)
        {
            game.Components.Add(this);
            bpm = 90;
            state = SongState.reset;
            songPlayer = new SongPlayer();
        }

        public override void Initialize()
        {
            base.Initialize();
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            checkSongState();
        }

        private void checkSongState()
        {
            switch (state) 
            {
                case SongState.reset:
                    songPlayer.ResetMusic();
                    state = SongState.turnOn;
                    break;
                case SongState.turnOn:
                    state = SongState.running;
                    songPlayer.StartSong();
                    break;
                case SongState.running:
                    //songPlayer.SongPlayerUpdate();
                    if (songPlayer.HasFinishedSong())
                    {
                        SongsComplete++;
                        state = SongState.reset;
                    }
                    break;
            }

        }
        public void Load()
        {
            Enabled= true;
       
        }

        public void UnLoad()
        {
            Enabled= false;
            songPlayer.StopSong();
            songPlayer.ResetMusic();
        }

    }
}
