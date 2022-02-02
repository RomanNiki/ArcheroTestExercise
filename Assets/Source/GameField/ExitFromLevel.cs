using Source.Interfaces;
using UnityEngine;

namespace Source.GameField
{
    public class ExitFromLevel : MonoBehaviour
    {
        [SerializeField] private LevelCondition _levelCondition;
        private void OnCollisionEnter(Collision other)
        {
            if (_levelCondition.LevelClear && other.gameObject.GetComponent<IPlayer>() != null) 
                Quite();
        }

        private static void Quite()
        {
            if (UnityEditor.EditorApplication.isPlaying)
            {
                UnityEditor.EditorApplication.isPlaying = false;
            }
            Application.Quit();
        }
    }
}