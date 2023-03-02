using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightsController : MonoBehaviour
{
    [SerializeField] int totalHighlights = 3;

    [Header("Children Link")]
    [SerializeField] GameObject highlightArea;
    [SerializeField] Transform classItemsParentTransform;
    
    [Header("Assets Link")]
    [SerializeField] ClassItem classItemPrefab;

    private void Awake()
    {
        if (highlightArea)
        {
            highlightArea.SetActive(false);
        }

        //for (int i = 0; i < totalHighlights; i++)
        //{
        //    PlayerPrefs.DeleteKey("Highlight" + i.ToString());
        //}
        //PlayerPrefs.DeleteKey("lastHighlightNumber");

        ContentLoaderManager.Instance.totalHighlights = totalHighlights;
    }


    public void CheckIfSceneIsHighlight(ClassesScriptableObjects classesScriptableObjects)
    {
        for (int i = 0; i < totalHighlights; i++)
        {
            if (PlayerPrefs.HasKey("Highlight" + i.ToString()))
            {
                int highLightedClassId = PlayerPrefs.GetInt("Highlight" + i.ToString());
                Debug.Log("Retrieved Highlight" + i.ToString() + " key. Its value is " + highLightedClassId);
                //Check if SO is a highlight

                Debug.Log(" is value " + classesScriptableObjects.id + " equal to " + highLightedClassId + "? " + (classesScriptableObjects.id == highLightedClassId).ToString());
                if (classesScriptableObjects.id == highLightedClassId)
                {
                    Setup(classesScriptableObjects);
                }

            }
        }
    }

    public void Setup(ClassesScriptableObjects classesScriptableObjects)
    {
        Debug.Log("Should be setting up");
        // activate highlight area GameObject if not already active
        if(highlightArea)
        {
            if (!highlightArea.activeInHierarchy)
            {
                Debug.Log("Should activate highlight");
                highlightArea.SetActive(true);
            }

        }

        // Instantiate Class Items on designated Transform
        Debug.Log("Should instantiate class");
        ClassItem tempClassItem = Instantiate(classItemPrefab, classItemsParentTransform);
        tempClassItem.Setup(classesScriptableObjects);
    }
}
