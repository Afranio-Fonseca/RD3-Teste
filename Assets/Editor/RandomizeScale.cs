using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class RandomizeScale : ScriptableWizard
{
    [SerializeField] private List<Transform> transforms;

    [SerializeField] private Vector2 minMaxX = Vector2.one;
    [SerializeField] private Vector2 minMaxY = Vector2.one;
    [SerializeField] private Vector2 minMaxZ = Vector2.one;


    [MenuItem("RD3/Randomize Transforms Scales")]
    static void CreateWizard()
    {
        ScriptableWizard.DisplayWizard<RandomizeScale>("Randomize Object Scale...", "Done", "Randomize");
    }

    void OnWizardCreate()
    {
        foreach (Transform t in transforms)
        {
            PrefabUtility.RecordPrefabInstancePropertyModifications(t);
        }
       
    }

    void OnWizardOtherButton()
    {
        foreach (Transform t in transforms)
        {
            Undo.RecordObject(t, "Randomized scale");
            
            t.localScale = new Vector3(Random.Range(minMaxX.x, minMaxX.y), Random.Range(minMaxY.x, minMaxY.y), 
                Random.Range(minMaxZ.x, minMaxZ.y));
        }
    }
}
