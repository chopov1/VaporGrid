using Microsoft.Xna.Framework;
using VaporGridCrossPlatform;
using VaporGridCrossPlatform.GridClasses;
using VaporGridCrossPlatform.UI;
using System.Collections.Generic;
using System;
using System.Threading;
using System.Timers;

namespace VaporGridCrossPlatform
{
    public class EndlessGamePlay : Scene
    {
        protected Camera camera;
        protected Player player;
        protected ArrowIndicators arrowIndicators;
        protected GridManager gridManager;
        protected DynamicTileManager dynamicTileManager;
        protected List<EnemySpawner> enemySpawners;
        protected EnemySpawner enemySpawner;
        protected SpittingEnemySpawner spittingEnemySpawner;
        protected PowerUpSpawner powerUpSpawner;
        protected PortalSpawner portalSpawner;
        ScoreUI display;
        GameplayUI rhythmDisplay;
        SongCompleteUI songCompleteDisplay;
        MenuController input;
        Random rnd;
        int currentCompletedSongs;
        //pause the game for a sec and let the player know they did a good job and are moving up difficulty
        System.Timers.Timer completedTimer;
        public EndlessGamePlay(Game game, SceneManager manager, RhythmManager rm, ScoreManager sm) : base(game, manager, rm, sm)
        {
            enemySpawners = new List<EnemySpawner>();
            input = new MenuController();
            rnd = new Random();
            
        }


        public override void SetupScene()
        {
            setupGame();
            base.SetupScene();
        }

        protected virtual void setupPlayer()
        {

            player = new Player(Game, gridManager, rm, 1, camera, scoreManager);
            Game.Components.Add(player);
        }

        protected virtual void setupGame()
        {
            camera = new Camera(Game);
            Game.Components.Add(camera);

            gridManager = new GridManager(Game, camera, rm);

            Game.Components.Add(gridManager);
            setupPlayer();
            enemySpawner = new EnemySpawner(Game, gridManager, rm, camera, player, 8, 32);
            Game.Components.Add(enemySpawner);
            enemySpawners.Add(enemySpawner);
            spittingEnemySpawner = new SpittingEnemySpawner(Game, gridManager, rm, camera, player, 8, 32);
            Game.Components.Add(spittingEnemySpawner);
            enemySpawners.Add(spittingEnemySpawner);
            powerUpSpawner = new PowerUpSpawner(Game, gridManager, rm, camera, player, 8,  16);
            Game.Components.Add(powerUpSpawner);
            portalSpawner = new PortalSpawner(Game, gridManager, rm, camera, player, 1, 32);
            Game.Components.Add(portalSpawner);


            arrowIndicators = new ArrowIndicators(Game, gridManager, rm, player, camera);
            Game.Components.Add(arrowIndicators);

            dynamicTileManager = new DynamicTileManager(Game, gridManager, player, enemySpawners);
            Game.Components.Add(dynamicTileManager);

            display = new ScoreUI(Game, scoreManager, 810);
            Game.Components.Add(display);

            rhythmDisplay = new GameplayUI(Game, player, rm);
            Game.Components.Add(rhythmDisplay);

            songCompleteDisplay = new SongCompleteUI(Game, rm);
            Game.Components.Add(songCompleteDisplay);

            addCompsToList();
        }
        protected virtual void addCompsToList()
        {
            addComponentToScene(camera);
            addComponentToScene(gridManager);
            addComponentToScene(enemySpawner);
            addComponentToScene(spittingEnemySpawner);
            addComponentToScene(powerUpSpawner);
            addComponentToScene(portalSpawner);
            addComponentToScene(player);
            addComponentToScene(scoreManager);
            addComponentToScene(arrowIndicators);
            addComponentToScene(rm);
            addComponentToScene(dynamicTileManager);
            addComponentToScene(display);
            addComponentToScene(rhythmDisplay);
            addComponentToScene(songCompleteDisplay);
        }


        protected override void SceneUpdate()
        {
            base.SceneUpdate();
            checkForExitInput();
            checkPlayerState();
            checkForSongCompletion();
        }

        private void checkForSongCompletion()
        {
            if(rm.SongsComplete > currentCompletedSongs)
            {
                currentCompletedSongs= rm.SongsComplete;
                if(currentCompletedSongs >= scoreManager.HighestSongsCompleted)
                {
                    scoreManager.HighestSongsCompleted= currentCompletedSongs;
                    scoreManager.SaveScore();
                }
                advanceStage();
            }
        }

        private void setTimer(float time)
        {
            completedTimer = new System.Timers.Timer(time);
            completedTimer.AutoReset = false;
        }

        private void onCompletedTimerEnd(Object source, ElapsedEventArgs e)
        {
            setDifficulty();
            //rm.StartMusic();
            stopTimer();
            songCompleteDisplay.UnLoad();
            rm.state = SongState.reset;
        }
        private void advanceStage()
        {
            songCompleteDisplay.Load();
            //rm.PauseMusic();
            setTimer(5000);
            startTimer();
        }

        private void startTimer()
        {
            completedTimer.Elapsed += onCompletedTimerEnd;
            completedTimer.Start();
        }

        private void stopTimer()
        {
            completedTimer.Stop();
            completedTimer.Elapsed -= onCompletedTimerEnd;
            completedTimer.Dispose();
        }

        private void checkForExitInput()
        {
            input.Update();
            if (input.PressedBack)
            {
                ResetGamePlay();
                arrowIndicators.UnLoad();
                sceneManager.ChangeScene(this, sceneManager.gameOver);
            }
        }

        public override void loadScene()
        {
            base.loadScene();
            VolumeSet();
            ResetGamePlay();
            rm.state = SongState.reset;
            scoreManager.ShowScore = true;
            scoreManager.ShowHighScore = false;
            scoreManager.WriteEnabled = true;
        }

        protected virtual void VolumeSet()
        {
            rm.SetVolume(1);
        }

        protected void setDifficulty()
        {
            switch (currentCompletedSongs)
            {
                case 0:
                    enemySpawner.SpawnBuffer = 32;
                    if (!enemySpawner.Enabled)
                    {
                        enemySpawner.Load();
                    }
                    if (spittingEnemySpawner.Enabled)
                    {
                        spittingEnemySpawner.UnLoad();
                    }
                    break;
                case 1:
                    enemySpawner.SpawnBuffer = 32;
                    if (!enemySpawner.Enabled)
                    {
                        enemySpawner.Load();
                    }
                    if (spittingEnemySpawner.Enabled)
                    {
                        spittingEnemySpawner.UnLoad();
                    }
                    break;
                case 2:
                    enemySpawner.SpawnBuffer = 32;
                    if (!enemySpawner.Enabled)
                    {
                        enemySpawner.Load();
                    }
                    if (spittingEnemySpawner.Enabled)
                    {
                        spittingEnemySpawner.UnLoad();
                    }
                    break;
                case 3:
                    spittingEnemySpawner.SpawnBuffer = 32;
                    if (enemySpawner.Enabled)
                    {
                        enemySpawner.UnLoad();
                    }
                    if (!spittingEnemySpawner.Enabled)
                    {
                        spittingEnemySpawner.Load();
                    }
                    break;
                case 4:
                    spittingEnemySpawner.SpawnBuffer = 32;
                    if (enemySpawner.Enabled)
                    {
                        enemySpawner.UnLoad();
                    }
                    if (!spittingEnemySpawner.Enabled)
                    {
                        spittingEnemySpawner.Load();
                    }
                    break;
            }
            songCompleteDisplay.UnLoad();
            spawnEdgeBuffer = 3;
            player.ResetPlayerPos(new Vector2(rnd.Next(spawnEdgeBuffer, gridManager.GridWidth - spawnEdgeBuffer), rnd.Next(spawnEdgeBuffer, gridManager.GridHeight - spawnEdgeBuffer)));
            gridManager.GenerateNewGrid(player.gridPos);
            dynamicTileManager.AddTiles();
            resetSpawners();
        }

        private void resetSpawners()
        {
            foreach (Spawner s in enemySpawners)
            {
                s.spawnState = SpawnState.reset;
            }
            powerUpSpawner.spawnState = SpawnState.reset;
            portalSpawner.spawnState = SpawnState.reset;
        }

        int spawnEdgeBuffer;
        protected virtual void ResetGamePlay()
        {
            currentCompletedSongs= 0;
            spawnEdgeBuffer = 3;
            player.ResetPlayer(new Vector2(rnd.Next(spawnEdgeBuffer, gridManager.GridWidth - spawnEdgeBuffer), rnd.Next(spawnEdgeBuffer, gridManager.GridHeight - spawnEdgeBuffer)));
            gridManager.GenerateNewGrid(player.gridPos);
            dynamicTileManager.AddTiles();
            setDifficulty();
            resetSpawners();
        }
        protected virtual void checkPlayerState()
        {
            switch (player.State)
            {
                case PlayerState.Dead:
                    ResetGamePlay();
                    arrowIndicators.UnLoad();
                    sceneManager.ChangeScene(this, sceneManager.gameOver);
                    break;
                case PlayerState.Alive:
                    break;
            }
        }

        public override void closeScene()
        {
            base.closeScene();
        }
    }
}
