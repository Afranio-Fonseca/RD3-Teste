using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientController : MonoBehaviour
{
    [SerializeField] GameObject sceneParent;
    [SerializeField] Transform firstSpawn;

    private void OnEnable()
    {
        GameManager.Instance.OnTutorialEnd += StartAmbient;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnTutorialEnd -= StartAmbient;
    }

    void StartAmbient()
    {
        sceneParent.SetActive(true);
        GameManager.Instance.Fire_OnTeleportStart(firstSpawn, true);
    }
}
