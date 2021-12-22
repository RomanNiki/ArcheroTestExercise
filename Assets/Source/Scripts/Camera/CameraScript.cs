using UnityEngine;

namespace Source.Scripts.Camera
{
    public class CameraScript : MonoBehaviour
    {
        [SerializeField] private Transform _field;

        private void Start()
        {
            var screenRatio = Screen.width / (float) Screen.height;
            var targetRatio = _field.transform.lossyScale.x / _field.transform.lossyScale.y;

            if (screenRatio >= targetRatio)
            {
                UnityEngine.Camera.main.orthographicSize = _field.transform.lossyScale.y / 2;
            }
            else
            {
                var differenceInSize = targetRatio / screenRatio;
                UnityEngine.Camera.main.orthographicSize = _field.transform.lossyScale.y / 2 * differenceInSize;
            }
        }
    }
}