using Microsoft.Xna.Framework;
using RhythmGameProto.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhythmGameProto
{
    internal class Tutorial : Scene
    {
        enum TutorialStage {movement, score, doubleTime, enemies};
        TutorialStage tutorialStage;

        Player player;
        Camera camera;
        GridManager gridManager;
        ArrowIndicators arrowIndicators;

        int width;
        int height;

        Collectable continueTile;
        PowerUpSpawner scoreSpawner;
        EnemySpawner enemySpawner;

        TutorialUI display;

        public Tutorial(Game game, SceneManager manager, RhythmManager rm, ScoreManager sm) : base(game, manager, rm, sm)
        {
            width = 15;
            height = 3;
        }

        public override void SetupScene()
        {
            setupTutorial();
            base.SetupScene();
        }

        private void setupTutorial()
        {
            tutorialStage = TutorialStage.movement;

            display = new TutorialUI(Game);
            Game.Components.Add(display);

            camera = new Camera(Game);
            Game.Components.Add(camera);

            gridManager = new GridManager(Game, camera, rm, width,height, false);
            Game.Components.Add(gridManager);

            player = new Player(Game, gridManager, rm, 1, camera, scoreManager);
            Game.Components.Add(player);
            player.gridPos = new Vector2(0, 0);

            continueTile = new Collectable(Game, gridManager, "ExitTutorial", camera, player);
            Game.Components.Add(continueTile);
            scoreSpawner = new PowerUpSpawner(Game, gridManager, rm, player, camera, 1);
            Game.Components.Add(scoreSpawner);
            enemySpawner = new EnemySpawner(Game, gridManager, rm, player, camera, 1);
            Game.Components.Add(enemySpawner);

            arrowIndicators = new ArrowIndicators(Game, gridManager, player, camera);
            Game.Components.Add(arrowIndicators);
            
            addCompsToList();

            stageChangedUpdated = false;
        }

        private void addCompsToList()
        {
            addComponentToScene(display);
            addComponentToScene(camera);
            addComponentToScene(gridManager);
            addComponentToScene(player);
            addComponentToScene(continueTile);
            addComponentToScene(scoreManager);
            addComponentToScene(arrowIndicators);
            addComponentToScene(rm);
            addComponentToScene(scoreSpawner);
            addComponentToScene(enemySpawner);
        }

        protected override void SceneUpdate()
        {
            base.SceneUpdate();
            if (!stageChangedUpdated)
            {
                changeStage();
            }
            switch (tutorialStage)
            {
                case TutorialStage.movement:
                    if (completedMovement())
                    {
                        tutorialStage = TutorialStage.score;
                        stageChangedUpdated = false;
                    }
                    break;
                case TutorialStage.score:
                    if (completedScore())
                    {
                        tutorialStage = TutorialStage.doubleTime;
                        stageChangedUpdated = false;
                    }
                    break;
                case TutorialStage.doubleTime:
                    if (completedDoubleTime())
                    {
                        tutorialStage = TutorialStage.enemies;
                        stageChangedUpdated = false;
                    }
                    break;
                case TutorialStage.enemies:
                    if (completedEnemies())
                    {
                        tutorialStage = TutorialStage.movement;
                        stageChangedUpdated = false;
                        returnToMenu();
                    }
                    break;
            }
            
        }

        bool stageChangedUpdated;
        private void changeStage()
        {
            display.Stage = (int)tutorialStage;
            switch (tutorialStage)
            {
                case TutorialStage.movement:
                    continueTile.Load();
                    enemySpawner.UnLoad();
                    scoreSpawner.UnLoad();
                    break;
                case TutorialStage.score:
                    scoreManager.ComboScore = 0;
                    scoreSpawner.Load();
                    scoreManager.ShowScore = true;
                    scoreManager.ShowHighScore = false;
                    scoreSpawner.BufferCount = 15;
                    break;
                case TutorialStage.doubleTime:
                    scoreSpawner.UnLoad();
                    scoreManager.ComboScore = player.SixteenthBuffer;
                    break;
                case TutorialStage.enemies:
                    scoreManager.ShowScore = false;
                    enemySpawner.Load();
                    enemySpawner.BufferCount = 15;
                    break;
            }
            stageChangedUpdated = true;
        }

        private bool completedEnemies()
        {
            if(player.State == PlayerState.Dead)
            {
                return true;
            }
            return false;
        }

        private bool completedDoubleTime()
        {
            //This is reliant on double time inputs giving +2
            //And 17 being the point at which you can do double time!!
            if (scoreManager.ComboScore == player.SixteenthBuffer + 2)
            {
                scoreManager.ComboScore = player.SixteenthBuffer + 1;
            }
            if (scoreManager.ComboScore < player.SixteenthBuffer + 1)
            {
                scoreManager.ComboScore = player.SixteenthBuffer + 1;
            }
            if(scoreManager.ComboScore == player.SixteenthBuffer + 3)
            {
                return true;
            }
            return false;
        }

        private bool completedScore()
        {
            if(scoreManager.Score > 5)
            {
                return true;
            }
            return false;
        }
        private bool completedMovement()
        {
            if (continueTile.Collected)
            {
                continueTile.Collected = false;
                continueTile.UnLoad();
                
                return true;
            }
            return false;
        }

        private void returnToMenu()
        {
            //TODO: not sure why need to call Unload manually on arrowIndicators only. 
            arrowIndicators.UnLoad();
            sceneManager.ChangeScene(this, sceneManager.mainMenu);
        }

        public override void loadScene()
        {
            player.ResetPlayer();
            base.loadScene();
            rm.state = SongState.reset;
            scoreManager.ShowScore = false;
            scoreManager.WriteEnabled = false;
            continueTile.gridPos = new Vector2(width-1,height-1);
        }


    }
}
