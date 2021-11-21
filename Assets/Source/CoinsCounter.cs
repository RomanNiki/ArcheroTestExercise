using System;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Source
{
    public class CoinsCounter : MonoBehaviour
    {
        private int _coins;
        public CoinsAddedEvent _coinsAddEvent;
        
        public void AddCoins()
        {
            _coins += Random.Range(1, 20);
            _coinsAddEvent.Invoke(_coins);
        }
    }

    [Serializable]
    public class CoinsAddedEvent : UnityEvent<int>
    {
    }
}