using System;

namespace LeaderBoard
{
    [Serializable]
    public class LeaderBoardItem
    {
        public string name;
        public int score;
    }

    [Serializable]
    public class LeaderBoardItems
    {
        public LeaderBoardItem[] items;
    }
}