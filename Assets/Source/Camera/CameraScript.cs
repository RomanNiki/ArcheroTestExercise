﻿using UnityEngine;

namespace Source.Camera
{
    public class CameraScript : MonoBehaviour
    {
        [SerializeField] private Vector2 _defaultResolution = new Vector2(1280, 720);
        [Range(0f, 1f)] [SerializeField] private float _widthOrHeight;

        private UnityEngine.Camera componentCamera;

        private float initialSize;
        private float targetAspect;

        private float initialFov;
        private float horizontalFov = 120f;

        private void Start()
        {
            componentCamera = GetComponent<UnityEngine.Camera>();
            initialSize = componentCamera.orthographicSize;

            targetAspect = _defaultResolution.x / _defaultResolution.y;

            initialFov = componentCamera.fieldOfView;
            horizontalFov = CalcVerticalFov(initialFov, 1 / targetAspect);
        }

        private void Update()
        {
            if (componentCamera.orthographic)
            {
                float constantWidthSize = initialSize * (targetAspect / componentCamera.aspect);
                componentCamera.orthographicSize = Mathf.Lerp(constantWidthSize, initialSize, _widthOrHeight);
            }
            else
            {
                float constantWidthFov = CalcVerticalFov(horizontalFov, componentCamera.aspect);
                componentCamera.fieldOfView = Mathf.Lerp(constantWidthFov, initialFov, _widthOrHeight);
            }
        }

        private float CalcVerticalFov(float hFovInDeg, float aspectRatio)
        {
            float hFovInRads = hFovInDeg * Mathf.Deg2Rad;

            float vFovInRads = 2 * Mathf.Atan(Mathf.Tan(hFovInRads / 2) / aspectRatio);

            return vFovInRads * Mathf.Rad2Deg;
        }
    }
}