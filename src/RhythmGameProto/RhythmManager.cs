using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;
using SharpDX.XAudio2;

namespace RhythmGameProto
{
    public enum SongState { reset, turnOn, running}
    public class RhythmManager : DrawableGameComponent, ISceneComponenet
    {
        public double bpm;
        public float beat;

        public float inputTime;
        public int Transpose { get { return songPlayer.Transpose; } }

        public SongState state;
        string songname;
        string songname2;

        SpriteFont font;
        Texture2D noteTexture;
        Texture2D offbeatNote;
        Texture2D downbeatNote;
        Texture2D halfNote;
        SpriteBatch spriteBatch;

        public SongPlayer songPlayer;

        public RhythmManager(Game game) : base(game)
        {
            game.Components.Add(this);
            bpm = 90;
            
            state = SongState.reset;
            songPlayer = new SongPlayer();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            font = Game.Content.Load<SpriteFont>("Font1");
            offbeatNote = Game.Content.Load<Texture2D>("MusicNoteGrey");
            downbeatNote = Game.Content.Load<Texture2D>("MusicNoteYellow");
            halfNote= Game.Content.Load<Texture2D>("MusicNotePurple");
            noteTexture = offbeatNote;
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
        }
        public override void Initialize()
        {
            base.Initialize();
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            changeNoteTexture();
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
                        state = SongState.reset;
                    }
                    break;
            }

        }

        private void changeNoteTexture()
        {
            if (songPlayer.IsOnQuarter())
            {
                noteTexture = downbeatNote;
            }
            else
            {
                noteTexture = offbeatNote;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            spriteBatch.Begin();
            //spriteBatch.Draw(noteTexture, new Vector2(300, 30), Color.White);
            /*spriteBatch.DrawString(font, songPlayer.SampleCount.ToString(), new Vector2(10, 10), Color.White);
            spriteBatch.DrawString(font, songPlayer.InputOnBeat().ToString(), new Vector2(200, 10), Color.White);
            spriteBatch.DrawString(font, songPlayer.hasHitBeat.ToString(), new Vector2(200, 20), Color.White);*/
            spriteBatch.End();
        }

        public void Load()
        {
            Enabled= true;
            Visible= true;
        }

        public void UnLoad()
        {
            Enabled= false;
            Visible= false;
            songPlayer.StopSong();
            songPlayer.ResetMusic();
        }
    }
}
