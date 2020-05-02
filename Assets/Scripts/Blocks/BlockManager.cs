using System;
using System.Collections.Generic;
using Common;
using Zenject;

namespace Blocks
{
    public class BlockManager
    {
        public Action<int> onScoreAdd = (score) => {};
        public Action onLevelEnd = () => {};
        private List<Block> _blocks = null;

        private Block.Factory _blockFactory;
        private Settings _settings;

        [Inject]
        public void Construct(Block.Factory factory, Settings settings)
        {
            _blockFactory = factory;
            _settings = settings;
        }

        public bool InitLevel(int levelIndex)
        {
            if (_settings.levels.Count <= levelIndex)
                return false;

            if (_blocks == null)
            {
                _blocks = new List<Block>();
            }
            
            _blocks.Clear();
            var level = _settings.levels[levelIndex];
            for(var i = 0; i < level.blocks.Length; i++)
            {
                var block = _blockFactory.Create();
                block.transform.position = level.blocks[i].position;
                block.toughness = level.blocks[i].toughness;
                block.Init();
                block.onCollision += OnCollision;
                _blocks.Add(block);
            }

            return true;
        }

        private void OnCollision(Block block)
        {
            block.toughness--;
            if (block.toughness <= 0)
            {
                onScoreAdd.Invoke(50);
                _blocks.Remove(block);
                block.DestroyBlock();
                if(_blocks.Count <= 0)
                    onLevelEnd.Invoke();
            }
            else
            {
                onScoreAdd.Invoke(30);
                block.SetColor(block.toughness);
            }
        }
        
        [Serializable]
        public class Settings
        {
            public List<LevelData> levels;
        }
    }
}