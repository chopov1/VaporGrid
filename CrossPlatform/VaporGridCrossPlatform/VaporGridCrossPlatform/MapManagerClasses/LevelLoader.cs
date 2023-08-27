using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace VaporGridCrossPlatform.MapManagerClasses
{
    public class LevelData
    {
        public LevelData(int[,] tileInfo, int[,] objectInfo)
        {
            this.tileInfo = tileInfo;
            this.objectInfo = objectInfo;
        }
        public int[,] tileInfo;
        public int[,] objectInfo;
    }
    public class LevelLoader
    {
        public List<LevelData> levelList;
        List<string> levelPaths;
        List<string> objectPaths;
        int numberOfLevels;
        public LevelLoader()
        {
            numberOfLevels = 2;
            levelPaths = getLevelPaths();
            objectPaths= getObjectPaths();
            levelList = new List<LevelData>();
            levelList.Add(createTutorial());
            for(int i = 1; i <= numberOfLevels; i++)
            {
                levelList.Add(createLevel(i));
            }
        }

        private List<string> getLevelPaths()
        {
            List<string> levelPaths = new List<string>();
            levelPaths.Add("GameData/level1.txt");
            levelPaths.Add("GameData/level2.txt");
            return levelPaths;
        }

        private List<string> getObjectPaths()
        {
            List<string> objectPaths = new List<string>();
            objectPaths.Add("GameData/objs1.txt");
            objectPaths.Add("GameData/objs2.txt");
            return objectPaths;
        }

        private int[,] parseFile(string file)
        {
            string[] data = File.ReadAllLines(file);
            List<string[]> strings = new List<string[]>();
            foreach (string line in data)
            {
                strings.Add(line.Split(','));
            }
            int[,] nums = new int[strings[0].Length, strings.Count];
            for(int y = 0; y < strings.Count; y++)
            {
                for(int x =0; x < strings[y].Length; x++)
                {
                    nums[x,y] = int.Parse(strings[y][x]);
                }
            }
            return nums;
        }
        private LevelData createTutorial()
        {
            int[,] collectables = new int[15, 3];
            int[,] tutorial = new int[15, 3];
            tutorial[9, 0] = 1;
            tutorial[9, 1] = 1;
            //making a 1x1 hall to teach player. should probobly make tutorial into a .txt file
            tutorial[0, 1] = 1;
            tutorial[0, 2] = 1;
            tutorial[1, 1] = 1;
            tutorial[1, 2] = 1;
            tutorial[2, 1] = 1;
            tutorial[2, 2] = 1;
            tutorial[3, 1] = 1;
            tutorial[3, 2] = 1;
            tutorial[4, 1] = 1;
            tutorial[4, 2] = 1;
            tutorial[5, 1] = 1;
            tutorial[5, 2] = 1;
            LevelData data = new LevelData(tutorial, collectables);
            return data;
        }
        private LevelData createLevel(int levelNumber)
        {
            
            int[,] level = parseFile(levelPaths[levelNumber-1]);

            int[,] objs = parseFile(objectPaths[levelNumber-1]);

            LevelData data = new LevelData(level, objs);
            return data;
        }
       
    }
}
