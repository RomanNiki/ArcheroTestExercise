using UnityEngine;
using UnityEngine.UI;

namespace Source.UI
{
    public class CoinsOnUI : MonoBehaviour
    {
        [SerializeField] private Text _textBox;
        [SerializeField] private string _template;
        
        public void DisplayCoins(int coins)
        {
            _textBox.text = string.Format(_template, coins);
        }
    }
}