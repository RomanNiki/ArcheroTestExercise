using UnityEngine;
using Random = UnityEngine.Random;

namespace Source
{
    public class CoinsCounter : MonoBehaviour
    {
        private int _coins;
        public delegate void OnCoinsAddDelegate(int count);
        public OnCoinsAddDelegate OnCoinsAdd;
        public static CoinsCounter Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        public void AddCoins()
        {
            _coins += Random.Range(1, 20);
            OnCoinsAdd?.Invoke(_coins);
        }
    }
}