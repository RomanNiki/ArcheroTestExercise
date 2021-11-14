using UnityEngine;
using UnityEngine.UI;

namespace Source.UI
{
    public class CoinsOnUI : MonoBehaviour
    {
        [SerializeField] private Text _textBox;
        private void OnEnable()
        {
            CoinsCounter.Instance.OnCoinsAdd += CoinsDisplay;
        }

        private void OnDisable()
        {
            CoinsCounter.Instance.OnCoinsAdd -= CoinsDisplay;
        }

        private void CoinsDisplay(int coins)
        {
            _textBox.text = "Coins:    " + coins;
        }
    }
}