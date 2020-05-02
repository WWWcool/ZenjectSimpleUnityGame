using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class GameUIController : MonoBehaviour
    {
        [SerializeField] private Text _lifeCount;
        [SerializeField] private Text _scoreCount;

        public int lifeCount
        {
            get
            {
                return int.Parse(this._lifeCount.text);
            }
            set
            {
                this._lifeCount.text = "Lifes: " + value.ToString();
            }
        }
        public int scoreCount
        {
            get
            {
                return int.Parse(this._scoreCount.text);
            }
            set
            {
                this._scoreCount.text = "Score: " + value.ToString();
            }
        }

        public void UpdateUI(int lifes, int score)
        {
            lifeCount = lifes;
            scoreCount = score;
        }
    }
}