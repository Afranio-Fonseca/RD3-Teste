using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportSenderFromVrMenu : MonoBehaviour
{
    public int teleportSectionId;

    [SerializeField] GameObject[] deactivateWhenTeleported;

    public void RequestTeleportFromVrMenu()
    {
        if(deactivateWhenTeleported.Length >= 1)
        {
            for (int i = 0; i < deactivateWhenTeleported.Length; i++)
            {
                deactivateWhenTeleported[i].SetActive(false);
            }
        }
        GameManager.Instance.Fire_OnRequestedTeleportFromVRMenu(teleportSectionId);
    }
}
