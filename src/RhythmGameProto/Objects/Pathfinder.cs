using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using RhythmGameProto;

namespace RhythmGameProto
{
    public class Node
    {
        public Vector2 pos;
        public bool iswalkable;
        public Node(Vector2 position, bool walkable)
        {
            pos = position;
            iswalkable = walkable;
        }

        public Node parent;
        public int gCost;
        public int hCost;
        public int fCost { get { return gCost + hCost; } }


    }
    public class Pathfinder
    {
        GridManager gridManager;
        Enemy enemy;

        public Pathfinder(GridManager gm, Enemy enemy)
        {
            gridManager = gm;
            this.enemy = enemy;
        }

        public List<Node> findPath(Node begin, Node destination)
        {
            List<Node> finalPath = new List<Node>();
            List<Node> openSet = new List<Node>();
            HashSet<Node> closedSet = new HashSet<Node>();
            openSet.Add(begin);

            while (openSet.Count > 0)
            {
                Node currentnode = openSet[0];
                for (int i = 1; i < openSet.Count; i++)
                {
                    if (openSet[i].fCost < currentnode.fCost || openSet[i].fCost == currentnode.fCost && openSet[i].hCost < currentnode.hCost)
                    {
                        currentnode = openSet[i];
                    }
                }
                openSet.Remove(currentnode);
                closedSet.Add(currentnode);

                if (currentnode == destination)
                {
                    finalPath = RetracePath(begin, destination);
                }

                //current node has positon 0 0?
                List<Node> neighborList = gridManager.getNodeNeighbors(currentnode);
                foreach (Node neighbor in neighborList)
                {
                    if (!neighbor.iswalkable || closedSet.Contains(neighbor))
                    {
                        continue;
                    }

                    int newMovementCostToNeighbor = currentnode.gCost + getDistance(currentnode, neighbor);
                    if (newMovementCostToNeighbor < neighbor.gCost || !openSet.Contains(neighbor))
                    {
                        neighbor.gCost = newMovementCostToNeighbor;
                        neighbor.hCost = getDistance(neighbor, destination);
                        neighbor.parent = currentnode;
                        if (!openSet.Contains(neighbor))
                        {
                            openSet.Add(neighbor);
                        }
                    }
                }
            }

            return finalPath;
        }

        List<Node> RetracePath(Node start, Node end)
        {
            List<Node> path = new List<Node>();
            Node currentNode = end;
            while (currentNode != start)
            {
                path.Add(currentNode);
                currentNode = currentNode.parent;
            }
            path.Reverse();
            return path;
        }

        int getDistance(Node node1, Node node2)
        {
            int distX = (int)MathF.Abs(node1.pos.X - node2.pos.X);
            int distY = (int)MathF.Abs(node1.pos.Y - node2.pos.Y);

            return distX + distY;
        }

    }
}
