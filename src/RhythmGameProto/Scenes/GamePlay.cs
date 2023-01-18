using Microsoft.Xna.Framework;
using RhythmGameProto;
using RhythmGameProto.GridClasses;

namespace RhythmGameProto.Scenes
{
    public class GamePlay : Scene
    {
        protected GridManager gridManager;
        protected Player player;
        protected ArrowIndicators arrowIndicators;
        protected EnemySpawner enemySpawner;
        protected PowerUpSpawner powerUpSpawner;
        protected Camera camera;
        protected FileReader fileReader;
        protected DynamicTileManager dynamicTileManager;

        int level;
        public GamePlay(Game game, SceneManager manager, RhythmManager rm, ScoreManager sm, int level) : base(game, manager, rm, sm)
        {
            this.level= level;
        }

        public override void SetupScene()
        {
            setupGame();
            base.SetupScene();
        }

        private void setupGame()
        {
            fileReader = new FileReader();
            camera = new Camera(Game);
            Game.Components.Add(camera);

            gridManager = new GridManager(Game, camera, rm);
            if(level != 0)
            {
                gridManager.loadLevel(level);
            }
            Game.Components.Add(gridManager);

            player = new Player(Game, gridManager, rm, 1, camera, scoreManager);
            Game.Components.Add(player);
            enemySpawner = new EnemySpawner(Game, gridManager, rm, player, camera, 8);
            Game.Components.Add(enemySpawner);
            powerUpSpawner = new PowerUpSpawner(Game, gridManager, rm, player, camera, 8);
            Game.Components.Add(powerUpSpawner);
            arrowIndicators = new ArrowIndicators(Game, gridManager, player, camera);
            Game.Components.Add(arrowIndicators);

            dynamicTileManager = new DynamicTileManager(Game, gridManager, player, enemySpawner);
            Game.Components.Add(dynamicTileManager);

            addCompsToList();
        }

        private void addCompsToList()
        {
            addComponentToScene(camera);
            addComponentToScene(gridManager);
            addComponentToScene(enemySpawner);
            addComponentToScene(powerUpSpawner);
            addComponentToScene(player);
            addComponentToScene(scoreManager);
            addComponentToScene(arrowIndicators);
            addComponentToScene(rm);
            addComponentToScene(dynamicTileManager);
        }


        protected override void SceneUpdate()
        {
            base.SceneUpdate();
            checkPlayerState();
        }

        public override void loadScene()
        {
            base.loadScene();
            ResetGamePlay();
            rm.state = SongState.reset;
            scoreManager.ShowScore = true;
            scoreManager.ShowHighScore = false;
            scoreManager.WriteEnabled = true;
        }

        protected void ResetGamePlay()
        {
            if(level == 0)
            {
                gridManager.RandomGrid(player.gridPos);
            }
            dynamicTileManager.AddTiles();
            player.ResetPlayer(new Vector2(0, 1));
            enemySpawner.spawnState = SpawnState.reset;
            powerUpSpawner.spawnState = SpawnState.reset;
            //rm.state = SongState.reset;
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
    }
}
