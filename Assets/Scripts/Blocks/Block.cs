using System;
using UnityEngine;
using Zenject;

namespace Blocks
{
    public class Block : MonoBehaviour
    {
        [SerializeField] private Color[] _colors;
        
        public int toughness = 0;
        public Action<Block> onCollision;

        public void Init()
        {
            SetColor(toughness);
        }
        
        public void SetColor(int index)
        {
            if(index < _colors.Length)
            {
                var sprite = GetComponent<SpriteRenderer>();
                sprite.color = _colors[index];
            }
        }

        public void DestroyBlock()
        {
            Destroy(gameObject);
        }
        
        private void OnCollisionEnter2D(Collision2D collision)
        {
            onCollision.Invoke(this);
        }
        
        public class Factory : PlaceholderFactory<Block>
        {
        }
    }
}
