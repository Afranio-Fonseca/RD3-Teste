using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuMainAreaController : MonoBehaviour
{
    [SerializeField] ClassesScriptableObjects[] allScriptableObjects;

    [SerializeField] List<Button> openCienciasBttn = new List<Button>();

    [SerializeField] HighlightsController highlightsController;

    [SerializeField] MenuSubjectAreaController subjectAreaController;
    [SerializeField] MenuInteractionAreaController interactionAreaController;

    private void Awake()
    {
        openCienciasBttn[0].onClick.AddListener(ChooseSubjectCiencias);
        if (!highlightsController)
        {
            highlightsController.GetComponent<HighlightsController>();
        }
    }

    private void Start()
    {
        GameManager.Instance.classesScriptableObjects = new List<ClassesScriptableObjects>(allScriptableObjects);

        GameManager.Instance.cienciasScriptableObjects = new List<ClassesScriptableObjects>();
        GameManager.Instance.cienciasScriptableObjects.Clear();
        GameManager.Instance.geografiaScriptableObjects = new List<ClassesScriptableObjects>();
        GameManager.Instance.cienciasScriptableObjects.Clear();

        for (int i = 0; i < allScriptableObjects.Length; i++)
        {
            // Check if current SO is highlight
            if (highlightsController)
            {
                highlightsController.CheckIfSceneIsHighlight(allScriptableObjects[i]);
            }

            // Separate SOs in their respective subject list
            if (allScriptableObjects[i].subject == Subjects.Ciências)
            {
                GameManager.Instance.cienciasScriptableObjects.Add(allScriptableObjects[i]);
            }
            if (allScriptableObjects[i].subject == Subjects.Geografia)
            {
                GameManager.Instance.geografiaScriptableObjects.Add(allScriptableObjects[i]);
            }
            
        }
    }

    void OnEnable()
    {
        GameManager.Instance.OnSelectedClass += OnSelectedClass;
    }

    void OnDisable()
    {
        GameManager.Instance.OnSelectedClass -= OnSelectedClass;
    }

    public void ChooseSubjectCiencias()
    {
        OpenSubjectArea(Subjects.Ciências);
    }

    void OpenSubjectArea(Subjects subject)
    {
        subjectAreaController.Setup(subject);
    }

    void OnSelectedClass(ClassesScriptableObjects classesScriptableObjects)
    {
        interactionAreaController.Setup(classesScriptableObjects);
    }
}
