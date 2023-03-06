using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsManager : MonoBehaviour
{
    [SerializeField] List<GameObject> eventPoints = new List<GameObject>();

    private void OnEnable()
    {
        GameManager.Instance.OnEventCheck += ManageEventPoints;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnEventCheck -= ManageEventPoints;
    }

    public void ManageEventPoints(GameObject eventGameObject = null)
    {
        for (int i = 0; i < eventPoints.Count; i++)
        {
            eventPoints[i].SetActive(false);
        }

        if (eventGameObject != null)
        {
            int index = eventPoints.IndexOf(eventGameObject);
            eventPoints[index].SetActive(true);
        }
    }
}
