using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(CanvasGroupFader))]
public class MenuSubjectAreaController : MonoBehaviour
{
    //Hidden Public
    [HideInInspector] public CanvasGroupFader canvasGroupFader;

    //Private
    List<ClassesScriptableObjects> currentClasses = new List<ClassesScriptableObjects>();

    //Private Serialized Fields
    [Header("Children Link")]
    [SerializeField] TMP_Text subjectTitleText;
    [SerializeField] ToggleGroup classYearToggleGroup;
    [SerializeField] TMP_InputField searchInputField;

    [Tooltip("Assign Transform that classItemPrefabs will be instatiated as Children")]
    [SerializeField] Transform classItemsParentTransform;

    [Header("Assets Link")]
    [SerializeField] ClassItem classItemPrefab;

    [SerializeField] List<ClassItem> spawnedClasses = new List<ClassItem>();

    Toggle[] yearSelectionToggles;

    int filteredYear;
    string currentSearchText = "";
    private void Awake()
    {
        canvasGroupFader = GetComponent<CanvasGroupFader>();

        yearSelectionToggles = classYearToggleGroup.GetComponentsInChildren<Toggle>();

        for (int i = 0; i < yearSelectionToggles.Length; i++)
        {
            int iCopy = i;
            yearSelectionToggles[i].onValueChanged.AddListener(delegate
            {
                InteractedWithYearToggle(iCopy + 1);
            });
        }
    }

    public void Setup(Subjects subject)
    {
        //Setup text based on Enum name
        subjectTitleText.text = subject.ToString();

        //Clear and assign correct List of SOs
        if(currentClasses.Count > 0)
        {
            currentClasses.Clear();
            currentClasses = new List<ClassesScriptableObjects>();
        }
        

        if(subject == Subjects.Ciências)
        {
            currentClasses.AddRange(GameManager.Instance.cienciasScriptableObjects);
        }
        else if (subject == Subjects.Geografia)
        {
            currentClasses.AddRange(GameManager.Instance.geografiaScriptableObjects);
        }

        // Destroy instantiated class items if needed

        if (classItemsParentTransform.childCount > 0)
        {
            for (int i = 0; i < classItemsParentTransform.childCount; i++)
            {
                Destroy(classItemsParentTransform.GetChild(i).gameObject);
            }
            spawnedClasses.Clear();
        }

        // Instantiate Class Items on designated Transform
        for (int i = 0; i < currentClasses.Count; i++)
        {
            ClassItem tempClassItem = Instantiate(classItemPrefab, classItemsParentTransform);
            tempClassItem.Setup(currentClasses[i]);
            spawnedClasses.Add(tempClassItem);
        }

        filteredYear = 0;
        classYearToggleGroup.SetAllTogglesOff();
        searchInputField.text = "";

        canvasGroupFader.DoFadeIn();
    }

    public void SearchTool(string _text)
    {
        currentSearchText = _text;

        string _nonCaptionText = _text.ToLower();

        for (int i = 0; i < spawnedClasses.Count; i++)
        {
            if (_nonCaptionText.Length < 1)
            {
                spawnedClasses[i].gameObject.SetActive(CheckIfClassIsInCorrectSchoolYear(spawnedClasses[i].classScriptable.schoolYear));
            }
            else
            {
                string _nonCaptionClassName = spawnedClasses[i].classScriptable.className.ToLower();

                if (_nonCaptionClassName.Contains(_nonCaptionText))
                {
                    spawnedClasses[i].gameObject.SetActive(CheckIfClassIsInCorrectSchoolYear(spawnedClasses[i].classScriptable.schoolYear));
                }
                else
                {
                    spawnedClasses[i].gameObject.SetActive(false);
                }
            }
        }
    }

    public void InteractedWithYearToggle(int _toggleOrder)
    {
        Debug.Log("interacted with toggle number; " + _toggleOrder);
        SetYearToggle(_toggleOrder);
    }

    public void SetYearToggle(int _classYear)
    {
        Debug.Log("Are any of toggles on ?" + classYearToggleGroup.AnyTogglesOn());
        if(!classYearToggleGroup.AnyTogglesOn())
        {
            filteredYear = 0;
        }
        else
        {
            filteredYear = _classYear;
        }
        SearchTool(currentSearchText);
    }

    bool CheckIfClassIsInCorrectSchoolYear(int classSschoolYear)
    {
        if (filteredYear > 0)
        {
            if (classSschoolYear == filteredYear)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return true;
        }
    }
}
