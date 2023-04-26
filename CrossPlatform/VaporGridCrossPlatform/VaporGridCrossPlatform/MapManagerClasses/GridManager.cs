using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using VaporGridCrossPlatform.GridClasses;
using VaporGridCrossPlatform.MapManagerClasses;
using VaporGridCrossPlatform.Scenes;

namespace VaporGridCrossPlatform
{
    public class GridManager : Sprite, ISceneComponenet
    {

        Pathfinder pathfinder;
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

        public Vector2 WalkableRange { get { return getLargestWalkableDistance(); } }

        protected int offsetX;
        protected int offsetY;

        int defaultGridWidth;
        int defaultGridHeight;

        public GridManager(Game game, Camera camera, RhythmManager rm) : base(game, "blank", camera)
        {
            pathfinder = new Pathfinder(this);
            tileTextures = new TileTextures(game);
            lvlLoader = new LevelLoader();
            this.rm = rm;
            this.camera = camera;

            //TileSize = new Rectangle(0, 0, (int)tileTextures.tileSpriteSize.X, (int)tileTextures.tileSpriteSize.Y);

            defaultGridWidth = 15;
            defaultGridHeight = 10;
            loadTiles();
        }

        public override void Initialize()
        {
            base.Initialize();

        }

        protected override void LoadContent()
        {
            base.LoadContent();

            setGridPositions(Grid);
        }

        private void loadTiles()
        {

            //the default grid is what is used for randomized level.
            //change this size to change size of randomized level.
            Grid = createGrid(defaultGridWidth, defaultGridHeight);
            NodeGrid = createNodeGrid(defaultGridWidth, defaultGridHeight);
        }

        private Node[,] createNodeGrid(int gridWidth, int gridHeight)
        {
            Node[,] grid = new Node[gridWidth, gridHeight];
            for(int x = 0; x < gridWidth; x++)
            {
                for(int y =0; y < gridHeight; y++)
                {
                    grid[x, y] = new Node(new Vector2(x, y), Grid[x,y].IsWalkable);
                }
            }
            return grid;
        }

        #region gridGeneration

        MonogameTile mg;
        private void createUnWalkableGrid(Vector2 playerPos)
        {
            for (int x = 0; x < GridWidth; x++)
            {
                for (int y = 0; y < GridHeight; y++)
                {
                    mg = Grid[x, y];
                    mg.ResetTile(new UnWalkableTile(mg.Position, new Vector2(x, y), tileTextures));
                }
            }
            Grid[(int)playerPos.X, (int)playerPos.Y].ResetTile(new WalkableTile(mg.Position, playerPos, tileTextures));
        }

        Vector2 currentPos;
        Vector2 dir;
        Vector2 d;
        private Vector2 getRandomDir()
        {
            d = new Vector2(random.Next(-1, 2), random.Next(-1, 2));
            while (d == Vector2.Zero)
            {
                d = new Vector2(random.Next(-1, 2), random.Next(-1, 2));
            }
            return d;
        }
        private bool isValidPos(Vector2 pos)
        {
            if (pos.X >= GridWidth || pos.X < 0 || pos.Y >= GridHeight || pos.Y < 0)
            {
                return false;
            }
            return true;
        }

        #region OtherGridGenerationAlgorithms

        private void CreateDungeonRoomGrid(Vector2 playerPos)
        {
            addRoomToDungeon(createRoom(getRandRoomSize()), playerPos);
        }

        private Vector2 getRandRoomSize()
        {
            return new Vector2(random.Next(3,7), random.Next(3,7));
        }
        private void addRoomToDungeon(int[,] room, Vector2 topLeftCornerTile)
        {
            for (int x = 0; x < room.GetLength(0); x++)
            {
                for (int y = 0; y < room.GetLength(1); y++)
                {
                    if (isValidPos((int)topLeftCornerTile.X + x, (int)topLeftCornerTile.Y + y))
                    {
                        mg = Grid[(int)topLeftCornerTile.X+ x, (int)topLeftCornerTile.Y + y];
                        mg.ResetTile(getTileType(new Vector2(x, y), getRandomTileNum()));
                    }
                }
            }
        }

        private bool isValidPos(int x, int y)
        {
            if(x >= GridWidth || x < 0 || y >= GridHeight || y < 0) 
            {
                return false; 
            }
            return true;
        }

        private int[,] createRoom(Vector2 roomsize)
        {
            int[,] room = new int[(int)roomsize.X, (int)roomsize.Y];
            for(int x = 0; x < roomsize.X; x++)
            {
                for(int y = 0; y < roomsize.Y; y++)
                {
                    room[x, y] = getRandomTileNum();
                }
            }
            return room;
        }

        private int getRandomTileNum()
        {
            switch (random.Next(9))
            {
                default:
                    return 0;
                case 1:
                    return 2;
                case 2:
                    return 3;
            }
        }

        private Tile getTileType(Vector2 pos, int typeNum)
        {
            switch (typeNum)
            {
                default:
                    return new WalkableTile(pos, pos, tileTextures);
                case 1:
                    return new AutoTrapTile(pos, pos, tileTextures);
                case 2:
                    return new DoorTile(pos, pos, tileTextures);
            }
        }

        private void CreateMyAlgoGrid(Vector2 playerPos)
        {
            createUnWalkableGrid(playerPos);
            currentPos = new Vector2(random.Next(GridWidth), random.Next(GridHeight));
            dir = getDirToStart(playerPos, currentPos);
            for (int i = 0; i < GridWidth + GridHeight; i++)
            {
                currentPos = new Vector2(random.Next(GridWidth), random.Next(GridHeight));
                while (isValidPos(currentPos))
                {
                    if (Grid[(int)currentPos.X, (int)currentPos.Y].IsWalkable)
                    {
                        break;
                    }
                    Grid[(int)currentPos.X, (int)currentPos.Y].ResetTile(getRandomWalkableTile(currentPos));
                    currentPos += dir;
                    dir = getDirToStart(playerPos, currentPos);
                }
            }
        }
        private Vector2 getDirToStart(Vector2 startPos, Vector2 curPos)
        {
            Vector2 dir = new Vector2(0, 0);
            if(startPos.X > curPos.X)
            {
                dir.X = 1;
            }
            else if (startPos.X < curPos.X)
            {
                dir.X = -1;
            }
            else if (startPos.Y > curPos.Y)
            {
                dir.Y = 1;
            }
            else if (startPos.Y < curPos.Y)
            {
                dir.Y = -1;
            }
            return dir;
        }
        private void CreateDFAgrid(Vector2 playerPos)
        {
            createUnWalkableGrid(playerPos);
            dir = getRandomDir();
            currentPos = playerPos;
            for (int i = 0; i < GridWidth + GridHeight; i++)
            {
                currentPos = new Vector2(random.Next(GridWidth), random.Next(GridHeight));
                while (isValidPos(currentPos + dir))
                {
                    if (Grid[(int)currentPos.X, (int)currentPos.Y].IsWalkable)
                    {
                        break;
                    }
                    Grid[(int)currentPos.X, (int)currentPos.Y].ResetTile(getRandomWalkableTile(currentPos));
                    currentPos += dir;
                    dir = getRandomDir();
                }
            }
        }
        private Tile getRandomWalkableTile(Vector2 pos)
        {
            switch (random.Next(9))
            {
                default:
                    return new WalkableTile(pos, pos, tileTextures);
                case 1:
                    return new AutoTrapTile(pos, pos, tileTextures);
                case 2:
                    return new DoorTile(pos,pos, tileTextures);
            }
        }

        #endregion
        private void RandomGridHard(Vector2 playerPos)
        {
            for (int x = GridWidth - 1; x > 0 ; x--)
            {
                for (int y = GridHeight - 1; y > 0; y--)
                {
                    mg = Grid[x, y];
                    if (playerPos.X == x && playerPos.Y == y)
                    {
                        mg.ResetTile(new WalkableTile(mg.Position, new Vector2(x,y), tileTextures));
                        continue;
                    }
                    int num = random.Next(0, 20);
                    switch (num)
                    {
                        default:
                                mg.ResetTile(new WalkableTile(mg.Position, new Vector2(x, y), tileTextures));
                            break;
                        case 6:
                                mg.ResetTile(new DoorTile(mg.Position, new Vector2(x, y), tileTextures));
                            break;
                        case 7:
                        case 8:
                        case 18:
                        case 19:
                        case 20:
                        case 17:
                        case 16:
                        case 15:
                            mg.ResetTile(new UnWalkableTile(mg.Position, new Vector2(x, y), tileTextures));
                            break;
                        case 9:
                            mg.ResetTile(new AutoTrapTile(mg.Position, new Vector2(x, y), tileTextures));
                            break;
                    }

                }
            }
        }

        private void RandomGridEasy(Vector2 playerPos)
        {
            for (int x = GridWidth - 1; x > 0; x--)
            {
                for (int y = GridHeight - 1; y > 0; y--)
                {
                    mg = Grid[x, y];
                    if (playerPos.X == x && playerPos.Y == y)
                    {
                        mg.ResetTile(new WalkableTile(mg.Position, new Vector2(x, y), tileTextures));
                        continue;
                    }
                    int num = random.Next(0, 20);
                    switch (num)
                    {
                        default:
                            mg.ResetTile(new WalkableTile(mg.Position, new Vector2(x, y), tileTextures));
                            break;
                        case 7:
                        case 8:
                        case 18:
                        case 19:
                        case 20:
                        case 17:
                            mg.ResetTile(new UnWalkableTile(mg.Position, new Vector2(x, y), tileTextures));
                            break;
                    }
                }
            }
        }

        private void RandomGridMedium(Vector2 playerPos)
        {
            for (int x = GridWidth - 1; x > 0; x--)
            {
                for (int y = GridHeight - 1; y > 0; y--)
                {
                    mg = Grid[x, y];
                    if (playerPos.X == x && playerPos.Y == y)
                    {
                        mg.ResetTile(new WalkableTile(mg.Position, new Vector2(x, y), tileTextures));
                        continue;
                    }
                    int num = random.Next(0, 20);
                    switch (num)
                    {
                        default:
                            mg.ResetTile(new WalkableTile(mg.Position, new Vector2(x, y), tileTextures));
                            break;
                        case 6:
                            mg.ResetTile(new AutoTrapTile(mg.Position, new Vector2(x, y), tileTextures));
                            break;
                        case 7:
                        case 8:
                        case 18:
                        case 19:
                        case 20:
                        case 17:
                        case 16:
                            mg.ResetTile(new UnWalkableTile(mg.Position, new Vector2(x, y), tileTextures));
                            break;
                    }
                }
            }
        }

        List<Node> path;
        private void cleanGrid(Vector2 playerPos)
        {
            for(int x = 0; x < GridWidth; x++)
            {
                for(int y = 0; y < GridHeight; y++)
                {
                    if(x == playerPos.X && y == playerPos.Y)
                    {
                        continue;
                    }
                    mg = Grid[x, y];
                    path = pathfinder.findPath(NodeGrid[x,y], NodeGrid[(int)playerPos.X, (int)playerPos.Y]);
                    if(path.Count <= 0)
                    {
                        mg.ResetTile(new UnWalkableTile(mg.Position, new Vector2(x, y), tileTextures));
                    }
                }
            }
        }
        public void GenerateNewGrid(Vector2 playerPos)
        {
            do
            {
                switch (rm.SongsComplete)
                {
                    case 0:
                        RandomGridEasy(playerPos);
                        break;
                    case 1:
                        RandomGridMedium(playerPos);
                        break;
                    case 2:
                        RandomGridHard(playerPos);
                        break;
                }
                NodeGrid = createNodeGrid(GridWidth, GridHeight);
                //CreateDungeonRoomGrid(playerPos);
                //NodeGrid = createNodeGrid(GridWidth, GridHeight);
                cleanGrid(playerPos);
                NodeGrid = createNodeGrid(GridWidth, GridHeight);
            } while (WalkableRange.X < 5 || WalkableRange.Y < 5);
            
        }
        #endregion
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
                            mg.ResetTile(new WalkableTile(mg.Position, new Vector2(x, y), tileTextures));
                            break;
                        case 0:
                            mg.ResetTile(new WalkableTile(mg.Position, new Vector2(x, y), tileTextures));
                            break;
                        case 1:
                            mg.ResetTile(new UnWalkableTile(mg.Position, new Vector2(x, y), tileTextures));
                            break;
                        case 2:
                            mg.ResetTile(new DoorTile(mg.Position, new Vector2(x, y), tileTextures));
                            break;
                        case 3:
                            mg.ResetTile(new AutoTrapTile(mg.Position, new Vector2(x, y), tileTextures));
                            break;
                    }
                }
            }
            NodeGrid = createNodeGrid(gridWidth, gridHeight);
        }

        private MonogameTile[,] createGrid(int gridWidth, int gridHeight)
        {
            MonogameTile t;
            
            MonogameTile[,] grid = new MonogameTile[gridWidth, gridHeight];
            for (int x = 0; x < gridWidth; x++)
            {
                for (int y = 0; y < gridHeight; y++)
                {
                    
                    t = new MonogameTile(Game, new WalkableTile(Vector2.Zero, new Vector2(x, y), tileTextures), camera, rm);
                    
                    grid[x, y] = t;
                }
            }
            return grid;
        }

        private void setGridPositions(MonogameTile[,] grid)
        {
            TileSize = new Rectangle(0, 0, (int)(tileTextures.tileSpriteSize.X * scale), (int)(tileTextures.tileSpriteSize.Y * scale));
            offsetX = (int)((Game.GraphicsDevice.Viewport.Width / 2) - ((defaultGridWidth * TileSize.Width) / 2));
            offsetY = (int)((Game.GraphicsDevice.Viewport.Height / 2) - ((defaultGridHeight * TileSize.Height) / 2));
            int gridWidth = grid.GetLength(0);
            int gridHeight = grid.GetLength(1);
            Vector2 pos;
            MonogameTile t;
            for (int x = 0; x < gridWidth; x++)
            {
                for (int y = 0; y < gridHeight; y++)
                {
                    pos = new Vector2(x * TileSize.Width + offsetX, y * TileSize.Height + offsetY);
                    t = grid[x, y];
                    t.Position = pos;
                    grid[x, y] = t;
                }
            }
            
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

        private Vector2 getLargestWalkableDistance()
        {
            return getLargestTilePos() - getSmallestTilePos();
        }

        private Vector2 getLargestTilePos()
        {
            for(int x = GridWidth-1; x >= 0 ; x--)
            {
                for(int y = GridHeight -1; y >= 0 ; y--)
                {
                    if (Grid[x, y].IsWalkable)
                    {
                        return new Vector2(x, y);
                    }
                }
            }
            return Vector2.Zero;
        }
        private Vector2 getSmallestTilePos()
        {
            for (int x = 0; x < GridWidth; x++)
            {
                for (int y = 0; y < GridHeight; y++)
                {
                    if (Grid[x, y].IsWalkable)
                    {
                        return new Vector2(x, y);
                    }
                }
            }
            return Vector2.Zero;
        }
    }
}
