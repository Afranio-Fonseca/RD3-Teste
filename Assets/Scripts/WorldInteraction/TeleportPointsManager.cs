using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPointsManager : MonoBehaviour
{
    [SerializeField] List<Transform> teleportPoints = new List<Transform>();

    private void OnEnable()
    {
        GameManager.Instance.OnTeleportCheck += ManageTeleportPoints;
    }
    private void OnDisable()
    {
        GameManager.Instance.OnTeleportCheck -= ManageTeleportPoints;
    }

    public void ManageTeleportPoints(Transform previous = null, Transform next = null)
    {
        for (int i = 0; i < teleportPoints.Count; i++)
        {
            teleportPoints[i].gameObject.SetActive(false);
        }
        
        if (previous != null)
        {
            int index = teleportPoints.IndexOf(previous);
            teleportPoints[index].gameObject.SetActive(true);
        }

        if (next != null)
        {
            int index = teleportPoints.IndexOf(next);
            teleportPoints[index].gameObject.SetActive(true);
        }
    }
}
