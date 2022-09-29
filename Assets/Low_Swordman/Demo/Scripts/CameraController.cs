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
        pos.y = (FieldManager.instance._gridWidth * 14f - FieldManager.instance._gridHeight * 4f * 0.35f);
        pos.x = (FieldManager.instance._gridHeight * 5.1f - 2f) * 0.5f;
        pos.z = -10f;

        float pixels = 128f / Screen.width;
        float size = 0.5f * pixels * Screen.height;
        mainCamera.orthographicSize = size;
        transform.position = pos;
    }
}
