using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class EndGamePopupController : MonoBehaviour
    {
        [SerializeField] private InputField _input;
        [SerializeField] private Text _yourScore;

        public Action<string> onClick;

        public string yourScore
        {
            get
            {
                return _yourScore.text;
            }
            set
            {
                this._yourScore.text = "Your score: " + value;
            }
        }

        public void Show(int score)
        {
            yourScore = score.ToString();
            gameObject.SetActive(true);
        }

        public void OnClick()
        {
            onClick(_input.text);
            gameObject.SetActive(false);
        }
    }
}