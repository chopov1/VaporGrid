using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;


namespace VaporGridCrossPlatform
{
    
    public class FileReader
    {
        
        public string scorePath = "GameData/scoreData.txt";
        public string movePath = "GameData/moveData.txt";
        public string songsCompletePath = "GameData/completeData.txt";
        List<string> data = new List<string>();
        public FileReader()
        {
            
        }

        public List<string> ReadFile(string path)
        {
            data = File.ReadAllLines(path).ToList();
            return data;
        }

        public void writeFile(string path, List<string> content)
        {
            
            File.WriteAllLines(path, content);
            
        }
    }
}
