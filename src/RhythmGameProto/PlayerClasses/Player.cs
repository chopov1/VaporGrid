using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RhythmGameProto;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhythmGameProto
{
    public enum PlayerState { Alive, Dead, Respawning }
    public class Player : GridSprite
    {
        public int PlayerNumber;
        public int SixteenthBuffer;

        public PlayerController Controller;

        public Vector2 dirToMove;

        RhythmManager rhythmManager;

        public PlayerState State;

        ScoreManager scoreManager;

        string beatTextureString;
        Texture2D texture;
        Texture2D beatTexture;
        Texture2D sixteenthTexture;
        public GridManager GridManager { get { return gridManager; } }

        public Player(Game game, GridManager gm, RhythmManager rhythmManager, int playernumber, Camera camera, ScoreManager sm) : base(game, gm, "MushroomGuy", camera)
        {
            scoreManager = sm;
            beatTextureString = "MushroomGuyJumping";
            this.rhythmManager = rhythmManager;
            dirToMove = new Vector2(0, 0);
            PlayerNumber = playernumber - 1;
            gridPos = new Vector2(gridManager.GridWidth / 2, gridManager.GridHeight / 2);
            Controller = new PlayerController(this);
            SpriteState = SpriteState.Active;
            State = PlayerState.Alive;
            SixteenthBuffer = 16;

        }

        protected override void LoadContent()
        {
            base.LoadContent();
            beatTexture = Game.Content.Load<Texture2D>(beatTextureString);
            sixteenthTexture = Game.Content.Load<Texture2D>("MushroomGuyGold");
            texture = spriteTexture;
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Controller.Update();
            updateMovement();
            changeTexture();
        }

        bool inputToggle;
        private void updateMovement()
        {
            switch (Controller.State)
            {
                case InputState.HasInput:
                    //moveState = MoveState.Move;
                    if(inputToggle)
                    {
                        checkInputAccuracy();
                        inputToggle= false;
                    }
                    else
                    {
                        camera.AdjustZoom(-.02f);
                    }
                    //Controller.State = InputState.NoInput;
                    break;
                case InputState.NoInput:
                    inputToggle = true;
                    camera.AdjustZoom(-.02f);
                    break;
            }
        }

        private void checkInputAccuracy()
        {
            if (rhythmManager.songPlayer.InputOnQuarter() && rhythmManager.songPlayer.hasHitQuarterBeat == false)
            {
                rhythmManager.songPlayer.hasHitQuarterBeat = true;
                rhythmManager.songPlayer.hasHitSixteenthBeat = true;
                camera.AdjustZoom(.05f);
                updatePlayerGridPos();
                increaseComboScore();
            }
            else if (canInputOnSixteenth())
            {
                rhythmManager.songPlayer.hasHitSixteenthBeat = true;
                rhythmManager.songPlayer.hasHitQuarterBeat = true;
                camera.AdjustZoom(.08f);
                updatePlayerGridPos();
                increaseComboScoreSixteenth();
            }
            else
            {
                resetMultiplier();
            }
        }

        int comboLives;
        public void increaseComboScoreSixteenth()
        {
            //tutorial relies on this being +2. If you change this, change double time tutorial
            scoreManager.ComboScore += 2;
        }
        private void increaseComboScore()
        {
            comboLives = 1;
            scoreManager.ComboScore++;
        }
        private void resetMultiplier()
        {
            if (comboLives <= 0)
            {
                scoreManager.ComboScore = 0;
            }
            comboLives--;
        }

        public void IncreaseScore()
        {
            scoreManager.IncreaseScore();
        }
        private void loseScore()
        {
            scoreManager.prevScore = scoreManager.Score;
            scoreManager.Score = 0;
            scoreManager.ComboScore = 0;
            scoreManager.saveScore();
        }

        bool shuffle;
        private void updatePlayerGridPos()
        {
            switch (dirToMove)
            {
                case Vector2(1, 0):
                    gridPos.X++;
                    shuffle = true;
                    break;
                case Vector2(0, 1):
                    gridPos.Y++;
                    shuffle = true;
                    break;
                case Vector2(-1, 0):
                    gridPos.X--;
                    shuffle = true;
                    break;
                case Vector2(0, -1):
                    gridPos.Y--;
                    shuffle = true;
                    break;
                default:
                    break;
            }
            dirToMove = Vector2.Zero;

            if (shuffle)
            {
                Controller.Indicies = Controller.shuffleKeys(Controller.Indicies);
                shuffle = false;
            }
        }

        public void ResetPlayer(Vector2 startPos)
        {
            rhythmManager.SongsComplete = 0;
            gridPos = startPos;
            loseScore();
            State = PlayerState.Alive;
        }

        private void changeTexture()
        {
            if (rhythmManager.songPlayer.IsOnQuarter())
            {
                spriteTexture = beatTexture;
            }
            else if (canInputOnSixteenth())
            {
                spriteTexture = sixteenthTexture;
            }
            else
            {
                spriteTexture = texture;
            }
        }

        private bool canInputOnSixteenth()
        {
            if (rhythmManager.songPlayer.InputOnSixteenth() && scoreManager.ComboScore > SixteenthBuffer && rhythmManager.songPlayer.hasHitQuarterBeat && !rhythmManager.songPlayer.hasHitSixteenthBeat)
            {
                return true;
            }
            return false;
        }
    }
}
