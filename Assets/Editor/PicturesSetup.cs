using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using TMPro;

public class PicturesSetup : ScriptableWizard
{
    public List<Sprite> pictures;
    public List<Image> images;
    public List<string> refTexts;
    public List<TextMeshProUGUI> texts;

    [MenuItem("RD3/Setup Pictures")]
    static void CreateWizard()
    {
        ScriptableWizard.DisplayWizard<PicturesSetup>("Setup Pictures", "Done", "Setup");
    }

    void OnWizardCreate()
    {
        if(images != null && pictures != null || texts != null && refTexts != null)
        {
            for (int i = 0; i < images.Count; i++)
            {
                Undo.RecordObject(images[i], "Sprite Changed in " + images[i].name);
                PrefabUtility.RecordPrefabInstancePropertyModifications(images[i]);
            }

            for (int i = 0; i < texts.Count; i++)
            {
                Undo.RecordObject(texts[i], "Text Changed in " + texts[i].name);
                PrefabUtility.RecordPrefabInstancePropertyModifications(texts[i]);
            }
        }
    }

    void OnWizardOtherButton()
    {
        if (texts != null && refTexts != null)
        {
            for (int i = 0; i < pictures.Count; i++)
            {
                images[i].sprite = pictures[i];
            }
        }
      
        if(texts != null && refTexts != null)
        {
            for (int i = 0; i < refTexts.Count; i++)
            {
                texts[i].text = "Foto: " + refTexts[i];
            }
        }
    }

    
}
