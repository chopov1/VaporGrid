using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using VaporGridCrossPlatform.GameUtility;

namespace VaporGridCrossPlatform
{
    public enum PlayerState { Alive, Dead, Respawning }
    public enum InputOutcome { Perfect, Missed}
    public class Player : RhythmSprite
    {
        public int PlayerNumber;
        public int SixteenthBuffer;
        public int comboLives { get; private set; }

        public InputOutcome lastInputState;

        public PlayerController Controller;

        public Vector2 dirToMove;

        RhythmManager rhythmManager;

        public PlayerState State;

        ScoreManager scoreManager;
        List<string> hints;
        Random rnd;

        List<Texture2D> playerTextures;
        public GridManager GridManager { get { return gridManager; } }

        public int EarlyOrLate; /*{ get { return rhythmManager.slowOrFastSixteenth; }
}*/

public Player(Game game, GridManager gm, RhythmManager rhythmManager, int playernumber, Camera camera, ScoreManager sm) : base(game, gm, rhythmManager,"player/AstroNaut1", camera)
        {
            rnd = new Random();
            createHints();
            scoreManager = sm;
            this.rhythmManager = rhythmManager;
            dirToMove = new Vector2(0, 0);
            PlayerNumber = playernumber - 1;
            
            Controller = new PlayerController(this);
            SpriteState = SpriteState.Active;
            State = PlayerState.Alive;
            SixteenthBuffer = 16;

            playerTextures= new List<Texture2D>();

        }

        public override void Initialize()
        {
            base.Initialize();
            gridPos = new Vector2(gridManager.GridWidth / 2, gridManager.GridHeight / 2);
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            playerTextures.Add(Game.Content.Load<Texture2D>("player/AstroNaut1"));
            playerTextures.Add(Game.Content.Load<Texture2D>("player/AstroNaut2"));
            playerTextures.Add(Game.Content.Load<Texture2D>("player/AstroNaut3"));
            playerTextures.Add(Game.Content.Load<Texture2D>("player/AstroNaut4"));
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Controller.Update();
            updateMovement();
            changeTexture();
        }

        bool inputToggle;
        protected virtual void updateMovement()
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

        protected void checkInputAccuracy()
        {
            //after input, we check if they are on beat
            updateInputAnalytics();
            switch (rhythmManager.RhythmState)
            {
                case RhythmState.Quarter:
                    if (!rhythmManager.songPlayer.hasHitQuarterBeat)
                    {
                        rhythmManager.songPlayer.hasHitQuarterBeat = true;
                        rhythmManager.songPlayer.hasHitSixteenthBeat = true;
                        camera.AdjustZoom(.05f);
                        updatePlayerGridPos();
                        increaseComboScore();
                        lastInputState = InputOutcome.Perfect;
                    }
                    break;
                case RhythmState.Sixteenth:
                    if (CanInputOnSixteenth())
                    {
                        rhythmManager.songPlayer.hasHitSixteenthBeat = true;
                        rhythmManager.songPlayer.hasHitQuarterBeat = true;
                        camera.AdjustZoom(.08f);
                        updatePlayerGridPos();
                        increaseComboScoreSixteenth();
                        lastInputState = InputOutcome.Perfect;
                    }
                    break;
                case RhythmState.Offbeat:
                    resetMultiplier();
                    lastInputState = InputOutcome.Missed;
                    //trigger error SFX here?
                    break;

            }
        }

        protected virtual void updateInputAnalytics()
        {
            if (scoreManager.ComboScore > SixteenthBuffer)
            {
                EarlyOrLate = rhythmManager.slowOrFastSixteenth;
            }
            else
            {
                EarlyOrLate = rhythmManager.slowOrFastQuarter;
            }
        }

        
        public void increaseComboScoreSixteenth()
        {
            //tutorial relies on this being +2. If you change this, change double time tutorial
            scoreManager.ComboScore += 2;
            scoreManager.NumberOfMoves++;
        }
        private void increaseComboScore()
        {
            comboLives = 1;
            scoreManager.ComboScore++;
            scoreManager.NumberOfMoves++;
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
            scoreManager.NumberOfMoves= 0;
            scoreManager.prevScore = scoreManager.Score;
            scoreManager.Score = 0;
            scoreManager.ComboScore = 0;
            if(scoreManager.WriteEnabled)
            {
                scoreManager.SaveScore();
            }
        }

        bool shuffle;
        protected void updatePlayerGridPos()
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

        public void ResetPlayerPos(Vector2 startPos)
        {
            gridPos = startPos;
        }

        public void ResetPlayer(Vector2 startPos)
        {
            EarlyOrLate = 3;
            rhythmManager.SongsComplete = 0;
            gridPos = startPos;
            loseScore();
            State = PlayerState.Alive;
        }

        int shown;
        bool hasShown;
        private void showError()
        {
            if(lastInputState == InputOutcome.Missed)
            {
                shown++;
                if(shown >= 2)
                {
                    shown= 0;
                    lastInputState= InputOutcome.Perfect;
                }
                hasShown = true;
            }
        }
        private void changeTexture()
        {
            switch (rhythmManager.RhythmState)
            {
                case RhythmState.Quarter:
                    spriteTexture = playerTextures[0];
                    if (!hasShown)
                    {
                        showError();
                    }
                    break;
                case RhythmState.Sixteenth:
                    spriteTexture = playerTextures[2];
                    break;
                case RhythmState.Offbeat:
                    hasShown = false;
                    spriteTexture = playerTextures[1];
                    break;
            }
        }

        public bool CanInputOnSixteenth()
        {
            if (rhythmManager.RhythmState == RhythmState.Sixteenth && scoreManager.ComboScore > SixteenthBuffer && rhythmManager.songPlayer.hasHitQuarterBeat && !rhythmManager.songPlayer.hasHitSixteenthBeat)
            {
                return true;
            }
            return false;
        }

        public void SetDeathMessage(int deathCode)
        {
            switch(deathCode)
            {
                case 0:
                    scoreManager.PlayerDeathMessage = "You were zapped by a flying saucer.";
                    break;
                case 1:
                    scoreManager.PlayerDeathMessage = "You were burned to crisp by a vent fire.";
                    break;
                case 2:
                    scoreManager.PlayerDeathMessage = "You were exterminated by an alien projectile";
                    break;
            }
            scoreManager.PlayerHint = hints[rnd.Next(hints.Count)];
        }

        private void createHints()
        {
            hints = new List<string>();
            hints.Add("If your combo score is above 16, you are able to move in double time.");
            hints.Add("Try to move onto the square an enemy is on, as its about to move.");
            hints.Add("Enemies are much more dangerous in groups. Sometimes that apple isn't worth it.");
            hints.Add("Enemies always try to find the shortest path to you.");
            hints.Add("Watch out before you enter a portal, you never know what may be on the other side.");
            hints.Add("If an enemy is above a vent as it catches fire, that enemy will be destroyed.");
        }
    }
}
