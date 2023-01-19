using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhythmGameProto.MapManagerClasses
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

        public LevelLoader()
        {
            levelList = new List<LevelData>();
            levelList.Add(createTutorial());
            levelList.Add(createLevel());
            levelList.Add(createLevel2());
        }

        private LevelData createTutorial()
        {
            int[,] collectables = new int[15, 3];
            int[,] tutorial = new int[15, 3];
            tutorial[7, 1] = 1;
            tutorial[9, 1] = 1;
            LevelData data = new LevelData(tutorial, collectables);
            return data;
        }
        private LevelData createLevel()
        {
            
            int[,] level = new int[15,10];
            level[0, 0] = 1;
            level[1, 1] = 1;
            level[2, 2] = 1;
            level[3, 3] = 2; 
            level[4, 4] = 3;

            int[,] objs = new int[15, 10];
            objs[6, 6] = 3;
            objs[6, 8] = 1;
            objs[0, 6] = 2;
            objs[0, 5] = 2;
            objs[0, 4] = 2;

            LevelData data = new LevelData(level, objs);
            return data;
        }

        private LevelData createLevel2()
        {
            int[,] level = new int[15, 10];
            level[4, 0] = 1;
            level[4, 1] = 1;
            level[4, 2] = 1;
            level[4, 3] = 2;
            level[5, 4] = 3;

            int[,] objs = new int[15, 10];
            objs[7, 7] = 3;
            objs[3, 0] = 1;
            objs[4, 4] = 2;
            objs[1, 9] = 2;

            LevelData data = new LevelData(level, objs);

            return data;
        }
    }
}
