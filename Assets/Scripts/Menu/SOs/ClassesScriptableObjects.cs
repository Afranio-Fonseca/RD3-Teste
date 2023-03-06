using UnityEngine;

public enum Subjects { None, Ciências, Geografia };

[CreateAssetMenu(fileName = "ClassData", menuName = "ScriptableObjects/ClassesScriptableObject", order = 1)]
public class ClassesScriptableObjects : ScriptableObject
{
    public int id;

    public Subjects subject = Subjects.None;

    [Range(1,9)]
    public int schoolYear = 1;

    public string className;
    public string classDescription;
    [TextArea(10,20)]
    public string[] classContents;
    public Sprite classSprite;

    public UnityEngine.AddressableAssets.AssetReference sceneReference;
}

