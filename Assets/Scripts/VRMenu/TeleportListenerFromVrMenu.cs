using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportListenerFromVrMenu : MonoBehaviour
{
    public VRMenuTeleportWithId[] vRMenuTeleportsWithId;

    private void OnEnable()
    {
        GameManager.Instance.OnRequestedTeleportFromVRMenu += OnRequestedTeleportFromVRMenu;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnRequestedTeleportFromVRMenu -= OnRequestedTeleportFromVRMenu;
    }

    void OnRequestedTeleportFromVRMenu(int id)
    {
        for (int i = 0; i < vRMenuTeleportsWithId.Length; i++)
        {
            if(vRMenuTeleportsWithId[i].id == id)
            {
                if(vRMenuTeleportsWithId[i].teleportOntoInteractableObject)
                {
                    // activates interactivePointsParent so the logic on the ObjectController will deactivate the model Object when teleport ends.
                    vRMenuTeleportsWithId[i].interactableObjectController.interactivePointsParent.SetActive(true);
                }
                GameManager.Instance.Fire_OnTeleportStart(vRMenuTeleportsWithId[i].teleportPoint, vRMenuTeleportsWithId[i].applyTransformRotation);
            }
        }
        
    }
}

[System.Serializable]
public class VRMenuTeleportWithId
{
    public int id;
    public bool applyTransformRotation;

    [Header("Se for verdadeiro, linkar o InteractableObject para que ele seja desativado ao teleportar")]
    public bool teleportOntoInteractableObject;
    public InteractableObjectController interactableObjectController;

    public Transform teleportPoint;
}

