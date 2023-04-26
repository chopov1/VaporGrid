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
using Microsoft.Xna.Framework.Audio;

namespace VaporGridCrossPlatform
{
    public enum SongState { reset, turnOn, running, paused}
    public class RhythmManager : GameComponent, ISceneComponenet
    {
        public RhythmState RhythmState { get { return songPlayer.CurrentRhythmState; } }
        public int Transpose { get { return songPlayer.Transpose; } }
        public bool HasHitQuarterBeat { get { return songPlayer.hasHitQuarterBeat; } set { songPlayer.hasHitQuarterBeat = value; } }
        public bool HasHitSixteenthBeat { get { return songPlayer.hasHitQuarterBeat; } set { songPlayer.hasHitQuarterBeat = value; } }
        public int slowOrFastQuarter { get { return songPlayer.SlowOrFastQuarter(); } }
        public int slowOrFastSixteenth { get { return songPlayer.SlowOrFastSixteenth(); } }
        SongPlayer songPlayer;

        public double bpm;
        public float beat;
        public float inputTime;
        public SongState state;
        public int SongsComplete;

        public RhythmManager(Game game) : base(game)
        {
            game.Components.Add(this);
            bpm = 90;
            state = SongState.reset;
            //songPlayer = new SongPlayer();
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

        public void StartMusic()
        {
            //songPlayer.StartSong();
        }

        public void PauseMusic()
        {
            //songPlayer.StopSong();
        }

        public void SetVolume(float volume)
        {
            //songPlayer.SetVolume(volume);
            SoundEffect.MasterVolume = volume;
        }

        private void checkSongState()
        {
            switch (state) 
            {
                case SongState.reset:
                    //songPlayer.ResetMusic();
                    state = SongState.turnOn;
                    break;
                case SongState.turnOn:
                    state = SongState.running;
                    //songPlayer.StartSong();
                    break;
                case SongState.running:
                    //songPlayer.SongPlayerUpdate();
                    /*if (songPlayer.HasFinishedSong())
                    {
                        SongsComplete++;
                        state = SongState.paused;
                    }*/
                    break;
                case SongState.paused:
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
            //songPlayer.StopSong();
            //songPlayer.ResetMusic();
        }

    }
}
