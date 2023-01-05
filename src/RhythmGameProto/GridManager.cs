using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct2D1.Effects;
using SharpDX.XAudio2;

namespace RhythmGameProto
{
    public class GridManager : GameComponent, ISceneComponenet
    {
        //public Dictionary<Vector2, MonogameTile> Grid;
        public MonogameTile[,] Grid;
        public int gridHeight;
        public int gridWidth;
        public Rectangle TileSize;
        int buffer;
        Random random = new Random();
        public Node[,] nodeGrid;


        Camera camera;
        RhythmManager rm;
        public GridManager(Game game, Camera camera, RhythmManager rm) : base(game)
        {
            this.rm = rm;
            this.camera = camera;
            TileSize = new Rectangle(0, 0, 32, 32);
            gridHeight = 10;
            gridWidth = 15;
            //make it centered by using Game.GraphicsDevice.Viewport
            buffer = 100;
            setupGrids();
        }

        private void setupGrids()
        {
            Grid = createGrid();
            nodeGrid = createNodeGrid();
        }

        private Node[,] createNodeGrid()
        {
            Node[,] grid = new Node[gridWidth,gridHeight];
            foreach(MonogameTile tile in Grid)
            {
                int x = (int)tile.tile.tileGridPos.X;
                int y = (int)tile.tile.tileGridPos.Y;
                grid[x, y] = new Node(new Vector2(x, y), tile.IsWalkable);
            }
            return grid;
        }

        public void ResetGrid(Vector2 playerPos)
        {
            for (int x =0; x < gridWidth; x++)
            {
                for(int y =0; y < gridHeight; y++)
                {
                    if(playerPos.X == x && playerPos.Y == y)
                    {
                        Grid[x, y].ResetTile(true);
                    }
                    else
                    {
                        int num = random.Next(0, 12);
                        switch (num)
                        {
                            default:
                                Grid[x, y].ResetTile(true);
                                break;
                            case 6:
                            case 7:
                                Grid[x, y].ResetTile(false);
                                break;
                        }
                    }
                }
            }
            nodeGrid = createNodeGrid();
        }

        private MonogameTile[,] createGrid()
        {
            MonogameTile t;
            Vector2 pos;
            MonogameTile[,] grid = new MonogameTile[gridWidth,gridHeight];
            for (int x = 0; x < gridWidth; x++)
            {
                for(int y = 0; y < gridHeight; y++)
                {
                    int num = random.Next(0, 12);
                    pos = new Vector2(x*TileSize.Width + buffer, y*TileSize.Height + buffer);
                    switch (num)
                    {
                        default:
                            t = new MonogameTile(Game, true, new Tile(pos, new Vector2(x,y)), camera, rm);
                            break;
                        case 6:
                        case 7:
                            t = new MonogameTile(Game, false, new Tile(pos, new Vector2(x, y)), camera, rm);
                            break;
                    }
                    t.Position = pos;
                    grid[x,y] = t;
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

            for(int x = -1; x < 1; x++)
            {
                for(int y = -1; y < 1; y++)
                {
                    if(x ==0 && y == 0)
                    {
                        continue;
                    }
                    int checkX = (int)tile.tile.tileGridPos.X + x;
                    int checkY = (int)tile.tile.tileGridPos.Y + y;
                    if(checkX > 0 && checkX <= gridWidth)
                    {
                        if(checkY > 0 && checkY <= gridHeight)
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
                    if ((x==1 && y ==0) || (x == 0 && y == 1) || (x == -1 && y == 0) || (x == 0 && y == -1))
                    {
                        int checkX = (int)node.pos.X + x;
                        int checkY = (int)node.pos.Y + y;
                        if (checkX >= 0 && checkX < gridWidth)
                        {
                            if (checkY >= 0 && checkY < gridHeight)
                            {
                                neighbors.Add(nodeGrid[checkX, checkY]);
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
            foreach(MonogameTile t in Grid)
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
