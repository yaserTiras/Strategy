using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : Singleton<CameraController>
{
    public static CameraController Instance;
    [HideInInspector] public Camera mainCamera;


    public override void Initialize()
    {
        mainCamera = GetComponent<Camera>();
        SetCamera();
    }

    private void SetCamera()
    {
        Vector3 pos;
        pos.y = (FieldManager.instance._gridHeight * 14.8f);
        pos.x = (FieldManager.instance._gridWidth * 9.5f) * 0.5f;
        pos.z = -10f;
        float sceneWidth = 408f;
        float pixels = sceneWidth / Screen.width;
        float size = 0.5f * pixels * Screen.height;
        mainCamera.orthographicSize = size;
        transform.position = pos;
    }
}
