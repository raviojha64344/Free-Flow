using System.Collections.Generic;

namespace FreeFlow.GameData
{
    /**/
    [System.Serializable]
    public class Level
    {
        public int boardValue;
        public List<LevelData> Levels;
    }

    [System.Serializable]
    public class LevelData
    {
        public int levelNum;
        public List<Pair> pairData;
    }

    [System.Serializable]
    public class Pair
    {
        public int pairValue;
        public List<int> pairs;
    }
}
