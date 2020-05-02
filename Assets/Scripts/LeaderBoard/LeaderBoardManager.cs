using System.Collections.Generic;
using UI;
using UnityEngine;
using Zenject;

namespace LeaderBoard
{
    public class LeaderBoardManager
    {
        public const string LEADER_BOARD_KEY = "LeaderBoardKey";

        private List<LeaderBoardItem> _items;
        
        private LeaderBoardController _leaderBoardController;

        [Inject]
        public void Construct(LeaderBoardController leaderBoardController)
        {
            _leaderBoardController = leaderBoardController;
        }
        
        public void LoadLB()
        {
            _items = new List<LeaderBoardItem>();
            if(PlayerPrefs.HasKey(LEADER_BOARD_KEY))
            {
                var data = PlayerPrefs.GetString(LEADER_BOARD_KEY);
                var items = JsonUtility.FromJson<LeaderBoardItems>(data);
                _items.AddRange(items.items);
                _leaderBoardController.UpdateUI(_items);
            }
        }
        
        public bool IsScoreHigh(int score)
        {
            var res = _items.Count < 8;
            for (var i = 0; i < _items.Count; i++)
            {
                if (_items[i].score < score)
                {
                    res = true;
                    break;
                }
            }
            return res;
        }

        public void AddNewScore(string name, int score)
        {
            bool needUpdate = true;
            int index = -1;
            for(var i = 0; i < _items.Count; i++)
            {
                if(_items[i].score < score)
                {
                    index = i;
                    break;
                }
            }

            if(index != -1)
            {
                _items.Insert(index, new LeaderBoardItem(){name = name, score = score});
                if(_items.Count > 8)
                {
                    _items.RemoveAt(8);
                }
            }
            else
            {
                if(_items.Count < 8)
                {
                    _items.Add(new LeaderBoardItem(){name = name, score = score});
                }
                else
                {
                    needUpdate = false;
                }
            }

            if(needUpdate)
            {
                var data = JsonUtility.ToJson(new LeaderBoardItems() { items = ((List<LeaderBoardItem>)_items).ToArray() });
                PlayerPrefs.SetString(LEADER_BOARD_KEY, data);
                _leaderBoardController.UpdateUI(_items);
            }
        }
    }
}
