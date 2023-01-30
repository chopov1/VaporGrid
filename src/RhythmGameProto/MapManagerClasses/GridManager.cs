using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RhythmGameProto.GridClasses;
using RhythmGameProto.MapManagerClasses;
using SharpDX.Direct2D1.Effects;
using SharpDX.XAudio2;

namespace RhythmGameProto
{
    public class GridManager : GameComponent, ISceneComponenet
    {

        LevelLoader lvlLoader;
        //public Dictionary<Vector2, MonogameTile> Grid;
        public MonogameTile[,] Grid;
        public Rectangle TileSize;
        Random random = new Random();
        public Node[,] NodeGrid;

        Camera camera;
        RhythmManager rm;

        TileTextures tileTextures;

        public int GridWidth { get { return Grid.GetLength(0); } }
        public int GridHeight { get { return Grid.GetLength(1); } }

        protected int offsetX;
        protected int offsetY;

        int defaultGridWidth;
        int defaultGridHeight;

        public GridManager(Game game, Camera camera, RhythmManager rm) : base(game)
        {
            lvlLoader = new LevelLoader();
            tileTextures = new TileTextures(game);
            this.rm = rm;
            this.camera = camera;
            TileSize = new Rectangle(0, 0, 32, 32);

            defaultGridWidth = 15;
            defaultGridHeight = 10;
            setupDefaultGrid();

        }

        public override void Initialize()
        {
            base.Initialize();
            
        }

        private void setupDefaultGrid()
        {
            offsetX = (int)((Game.GraphicsDevice.Viewport.Width / 2) - ((defaultGridWidth * TileSize.Width) / 2));
            offsetY = (int)((Game.GraphicsDevice.Viewport.Height / 2) - ((defaultGridHeight * TileSize.Height) / 2));
            //the default grid is what is used for randomized level.
            //change this size to change size of randomized level.
            Grid = createGrid(defaultGridWidth, defaultGridHeight);
            NodeGrid = createNodeGrid(defaultGridWidth, defaultGridHeight);
        }

        private Node[,] createNodeGrid(int gridWidth, int gridHeight)
        {
            Node[,] grid = new Node[gridWidth, gridHeight];
            foreach (MonogameTile tile in Grid)
            {
                int x = (int)tile.tile.tileGridPos.X;
                int y = (int)tile.tile.tileGridPos.Y;
                grid[x, y] = new Node(new Vector2(x, y), tile.IsWalkable);
            }
            return grid;
        }

        public void RandomGrid(Vector2 playerPos)
        {
            for (int x = GridWidth - 1; x > 0 ; x--)
            {
                for (int y = GridHeight - 1; y > 0; y--)
                {
                    MonogameTile mg = Grid[x, y];
                    if (playerPos.X == x && playerPos.Y == y)
                    {
                        mg.ResetTile(new WalkableTile(mg.Position, new Vector2(x,y), tileTextures, rm));
                        continue;
                    }
                    int num = random.Next(0, 20);
                    switch (num)
                    {
                        default:
                                mg.ResetTile(new WalkableTile(mg.Position, new Vector2(x, y), tileTextures, rm));
                            break;
                        case 6:
                                mg.ResetTile(new BreakableTile(mg.Position, new Vector2(x, y), tileTextures, rm));
                            break;
                        case 7:
                        case 8:
                        case 18:
                        case 19:
                        case 20:
                        case 17:
                            mg.ResetTile(new UnWalkableTile(mg.Position, new Vector2(x, y), tileTextures, rm));
                            break;
                        case 9:
                            mg.ResetTile(new AutoTrapTile(mg.Position, new Vector2(x, y), tileTextures, rm));
                            break;
                    }

                }
            }
            NodeGrid = createNodeGrid(GridWidth, GridHeight);
        }

        public void loadLevel(int lvlNum)
        {
            UnloadGrid();
            if(Grid != null)
            {
                Array.Clear(Grid);
            }
            int[,] level = lvlLoader.levelList[lvlNum].tileInfo;
            int gridWidth = level.GetLength(0);
            int gridHeight = level.GetLength(1);
            Grid = createGrid(gridWidth, gridHeight);
            for (int x = 0; x < gridWidth; x++)
            {
                for(int y =0; y < gridHeight; y++)
                {
                    MonogameTile mg = Grid[x, y];
                    switch (level[x,y])
                    {
                        default:
                            mg.ResetTile(new WalkableTile(mg.Position, new Vector2(x, y), tileTextures, rm));
                            break;
                        case 0:
                            mg.ResetTile(new WalkableTile(mg.Position, new Vector2(x, y), tileTextures, rm));
                            break;
                        case 1:
                            mg.ResetTile(new UnWalkableTile(mg.Position, new Vector2(x, y), tileTextures, rm));
                            break;
                        case 2:
                            mg.ResetTile(new BreakableTile(mg.Position, new Vector2(x, y), tileTextures, rm));
                            break;
                        case 3:
                            mg.ResetTile(new AutoTrapTile(mg.Position, new Vector2(x, y), tileTextures, rm));
                            break;
                    }
                }
            }
            NodeGrid = createNodeGrid(gridWidth, gridHeight);
        }

        private MonogameTile[,] createGrid(int gridWidth, int gridHeight)
        {
            MonogameTile t;
            Vector2 pos;
            MonogameTile[,] grid = new MonogameTile[gridWidth, gridHeight];
            for (int x = 0; x < gridWidth; x++)
            {
                for (int y = 0; y < gridHeight; y++)
                {
                    pos = new Vector2(x * TileSize.Width + offsetX, y * TileSize.Height + offsetY);
                    t = new MonogameTile(Game, new WalkableTile(pos, new Vector2(x, y), tileTextures, rm), camera, rm);
                    t.Position = pos;
                    grid[x, y] = t;
                }
            }
            return grid;
        }

        public bool isWalkable(Vector2 gridPos)
        {
            if (Grid[(int)gridPos.X, (int)gridPos.Y].IsWalkable)
            {
                return true;
            }
            return false;
        }

        public List<MonogameTile> getNeighbors(MonogameTile tile)
        {
            List<MonogameTile> neighbors = new List<MonogameTile>();

            for (int x = -1; x < 1; x++)
            {
                for (int y = -1; y < 1; y++)
                {
                    if (x == 0 && y == 0)
                    {
                        continue;
                    }
                    int checkX = (int)tile.tile.tileGridPos.X + x;
                    int checkY = (int)tile.tile.tileGridPos.Y + y;
                    if (checkX > 0 && checkX <= GridWidth)
                    {
                        if (checkY > 0 && checkY <= GridHeight)
                        {
                            neighbors.Add(Grid[checkX, checkY]);
                        }
                    }
                }
            }

            return neighbors;
        }

        public List<Node> getNodeNeighbors(Node node)
        {
            List<Node> neighbors = new List<Node>();
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if (x == 1 && y == 0 || x == 0 && y == 1 || x == -1 && y == 0 || x == 0 && y == -1)
                    {
                        int checkX = (int)node.pos.X + x;
                        int checkY = (int)node.pos.Y + y;
                        if (checkX >= 0 && checkX < GridWidth)
                        {
                            if (checkY >= 0 && checkY < GridHeight)
                            {
                                neighbors.Add(NodeGrid[checkX, checkY]);
                            }
                        }
                    }
                    else
                    {
                        continue;
                    }

                }
            }
            return neighbors;
        }
        public void Load()
        {
            foreach (MonogameTile t in Grid)
            {
                t.Load();
            }
        }

        private void UnloadGrid()
        {
            if (Grid == null)
            {
                return;
            }
            foreach (MonogameTile t in Grid)
            {
                t.UnLoad();
            }
        }

        public void UnLoad()
        {
            
            UnloadGrid();
        }
    }
}
