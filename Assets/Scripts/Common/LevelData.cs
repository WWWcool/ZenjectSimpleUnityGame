using UnityEngine;

namespace Common
{
    [System.Serializable]
    public class BlockData
    {
        [SerializeField] public Vector2 position;
        [SerializeField] public int toughness;
    }

    [CreateAssetMenu(menuName = "Utils/Data/Level")]
    public class LevelData : ScriptableObject
    {
        public BlockData[] blocks;
    }
}
