using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//попытка подстроить под расширение экрана
public class Cam : MonoBehaviour
{
    private float _defaultHeight;
    private float _defaultWidth;

    private void Start()
    {
        _defaultHeight = Camera.main.orthographicSize;
        _defaultWidth = Camera.main.orthographicSize * Camera.main.aspect;
    }

    private void Update()
    {
        Camera.main.orthographicSize = _defaultWidth / Camera.main.aspect;
    }
}
