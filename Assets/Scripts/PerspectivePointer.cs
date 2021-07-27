using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerspectivePointer : MonoBehaviour
{
    public Camera mainCamera;

    void Update()
    {
        var offset = new Vector2((Input.mousePosition.x / Screen.width) - 0.5f, (Input.mousePosition.y / Screen.height) - 0.5f);
        var clampedPosition = Vector2.ClampMagnitude(new Vector2(offset.x, offset.y), 0.5f);
        mainCamera.transform.position = new Vector3(clampedPosition.x, clampedPosition.y, mainCamera.transform.position.z);
    }
}
