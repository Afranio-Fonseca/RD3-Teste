using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrientationMode : MonoBehaviour
{
    public ScreenOrientation sceneOrientation;

    private void Awake()
    {
        Screen.orientation = sceneOrientation;
    }
}
