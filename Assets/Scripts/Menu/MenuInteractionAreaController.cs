using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
[RequireComponent(typeof(CanvasGroupFader))]
public class MenuInteractionAreaController : MonoBehaviour
{
    //Hidden Public
    [HideInInspector] public CanvasGroupFader canvasGroupFader;

    [Header("Children Link")]
    [SerializeField] Image interactionImage;
    [SerializeField] TMP_Text classTitle;
    [SerializeField] TMP_Text classDescription;
    [SerializeField] Transform detailedTextItemsParentTransform;

    [Header("Assets Link")]
    [SerializeField] TMP_Text detailedTextItemPrefab;

    private void Awake()
    {
        canvasGroupFader = GetComponent<CanvasGroupFader>();
    }

    public void Setup(ClassesScriptableObjects classesScriptableObjects)
    {
        interactionImage.sprite = classesScriptableObjects.classSprite;

        classTitle.text = classesScriptableObjects.className;
        classDescription.text = classesScriptableObjects.classDescription;

        // Destroy instantiated prefabs if needed
        if (detailedTextItemsParentTransform.childCount > 0)
        {
            for (int i = 0; i < detailedTextItemsParentTransform.childCount; i++)
            {
                Destroy(detailedTextItemsParentTransform.GetChild(i).gameObject);
            }
        }

        // Instantiate prefabs on designated Transform and setup text
        for (int i = 0; i < classesScriptableObjects.classContents.Length; i++)
        {
            TMP_Text tempDescriptionItem = Instantiate(detailedTextItemPrefab, detailedTextItemsParentTransform);
            tempDescriptionItem.text = "- " + classesScriptableObjects.classContents[i];
        }

        canvasGroupFader.DoFadeIn();
    }

}
