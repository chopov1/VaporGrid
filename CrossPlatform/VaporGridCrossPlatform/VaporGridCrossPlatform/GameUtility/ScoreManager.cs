using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaporGridCrossPlatform
{
    public class ScoreManager : DrawableGameComponent, ISceneComponenet
    {

        public string PlayerDeathMessage;
        public string PlayerHint;

        public int Score;
        public int prevScore;
        public int HighScore;
        public int ComboScore;
        public int HighestSongsCompleted;

        public bool ShowScore;
        public bool ShowHighScore;
        public bool WriteEnabled;

        public int prevNumOfMoves;
        public int NumberOfMoves;
        public int[] LowestNumberOfMoves;

        FileReader fr;
        string[] scoreFxNames = { "ScoreFXc", "ScoreFXc#", "ScoreFXd", "ScoreFXd#", "ScoreFXe", "ScoreFXf", "ScoreFXf#", "ScoreFXg", "ScoreFXg#", "ScoreFXa", "ScoreFXa#", "ScoreFXb" };
        List<string> scoreData;
        List<string> moveData;

        List<SoundEffect> soundEffects;
        int[] orderedNums = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };

        Random rnd;
        RhythmManager Rm;
        public ScoreManager(Game game, RhythmManager rm) : base(game)
        {
            Rm = rm;
            game.Components.Add(this);
            fr = new FileReader();
            rnd = new Random();
            ShowScore = true;
            ShowHighScore= true;
            PlayerDeathMessage = "Pressing the blue button exits the current level.";
        }

        SoundEffect getSoundToPlay()
        {
            /*int index = rnd.Next(0, 8);
            keyIndicies = getKeyIndicies(Rm.Transpose);*/
            return soundEffects[Rm.Transpose];
        }

        int[] getKeyIndicies(int distanceFromC)
        {
            //get which index should be put into the array first using distancefromc
            //add each incrementing index to the array that follows the minor scale pattern
            //if index gets to high then start from the beginning
            int[] ints = new int[8];
            int counter = 0;
            int index;
            for (int i =0; i < 8; i++)
            {

                if (counter > 11)
                {
                    counter = 0;
                }
                if (counter + distanceFromC > 11)
                {
                    index = (counter + distanceFromC) - 11;
                    //gotta either add or subtract dist from c if it goes out of range
                }
                else
                {
                    index = counter + distanceFromC;
                }
                ints[i] = orderedNums[index];
                switch (counter)
                {
                    case 0:
                    case 5:
                    case 8:
                    case 3:
                    case 10:
                        counter += 2;
                        break;
                    case 2:
                    case 7:
                        counter += 1;
                        break;
                    default:
                        break;
                }
            }

            return ints;
        }
        protected override void LoadContent()
        {
            base.LoadContent();
            soundEffects = setupSFX();
            scoreData = fr.ReadFile(fr.scorePath);
            HighScore = int.Parse(scoreData[0]);
            prevScore= int.Parse(scoreData[1]);
            HighestSongsCompleted= int.Parse(scoreData[2]);
            loadMoveData();
            NumberOfMoves = int.Parse(moveData[1]);
            soundEffects = setupSFX();
        }

        private void loadMoveData()
        {
            moveData = fr.ReadFile(fr.movePath);
            int length = moveData.Count;
            LowestNumberOfMoves = new int[length];
            for(int i =0; i < moveData.Count; i++)
            {
                LowestNumberOfMoves[i] = int.Parse(moveData[i]);
            }
        }

        private List<SoundEffect> setupSFX()
        {
            List<SoundEffect> sfx = new List<SoundEffect>();
            for (int i = 0; i <= 11; i++)
            {
                sfx.Add((Game.Content.Load<SoundEffect>(scoreFxNames[i])));
            }
            return sfx;
        }

        SoundEffectInstance fx;
        public void PlayScoreSFX()
        {
            fx = getSoundToPlay().CreateInstance();
            fx.Volume = .4f;
            fx.Play();
        }

        public void IncreaseScore()
        {
            Score += ComboScore;
            if(WriteEnabled)
            {
                if (Score >= HighScore)
                {
                    HighScore = Score;
                    SaveScore();
                }
            }
            PlayScoreSFX();
        }

        public void SaveMoves(int levelNum)
        {
            prevNumOfMoves = NumberOfMoves;
            if(NumberOfMoves <= LowestNumberOfMoves[levelNum])
            {
                LowestNumberOfMoves[levelNum] = NumberOfMoves;
                moveData[levelNum] = LowestNumberOfMoves[levelNum].ToString();
                if (WriteEnabled)
                {
                    fr.writeFile(fr.movePath, moveData);
                }
            }
        }

        public void SaveScore()
        {
            scoreData[0] = HighScore.ToString();
            scoreData[1] = prevScore.ToString();
            scoreData[2] = HighestSongsCompleted.ToString();
            if (WriteEnabled)
            {
                fr.writeFile(fr.scorePath, scoreData);
            }
        }
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            //spriteBatch.Begin();
            /*if(ShowScore)
            {
                if (ShowHighScore)
                {
                    spriteBatch.DrawString(font, "HighScore: " + HighScore, new Vector2(200, 25), Color.White);
                    spriteBatch.DrawString(font, "Last Score: " + prevScore, new Vector2(200, 425), Color.White);
                }
                spriteBatch.DrawString(font, "Score: " + Score, new Vector2(600, 100), Color.White);
                spriteBatch.DrawString(font, "+" + ComboScore, new Vector2(600, 200), Color.White);
            }*/
            //draw score
            //spriteBatch.End();
        }

        
        public void Load()
        {
            Enabled = true;
            Visible= true;
        }

        public void UnLoad()
        {
            Enabled= false;
            Visible= false;
        }
    }
}
