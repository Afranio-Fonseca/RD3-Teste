using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClassItem : MonoBehaviour
{
    [HideInInspector] public ClassesScriptableObjects classScriptable;

    public TMP_Text titleText;
    public TMP_Text descriptionText;
    public Image image;

    [SerializeField] Button selectButton;

    private void Awake()
    {
        selectButton.onClick.AddListener(SelectClass);    
    }

    public void Setup(ClassesScriptableObjects _classScriptable)
    {
        classScriptable = _classScriptable;

        titleText.text = _classScriptable.className;
        descriptionText.text = _classScriptable.classDescription;

        image.sprite = _classScriptable.classSprite;

    }

    void SelectClass()
    {
        GameManager.Instance.Fire_OnSelectedClass(classScriptable);
    }
}
