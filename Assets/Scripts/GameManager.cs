using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public List<ClassesScriptableObjects> classesScriptableObjects;
    public List<ClassesScriptableObjects> cienciasScriptableObjects;
    public List<ClassesScriptableObjects> geografiaScriptableObjects;

    public delegate void ActionWithTransform (Transform _transform, bool _shouldApplyRotation);
    public ActionWithTransform OnTeleportStart;
    public ActionWithTransform OnTeleportEnd;

    public delegate void ActionWithFloat(float _float);
    public ActionWithFloat OnInteractionTimerValueChanged;

    public delegate void ActionWithInt(int _int);
    public ActionWithInt OnRequestedTeleportFromVRMenu;

    public delegate void ObjectControllerAction(InteractableObjectController _gazedObject);
    public ObjectControllerAction OnAnyObjectGazed;

    public delegate void ActionWithScriptableObject(ClassesScriptableObjects classesScriptableObjects);
    public ActionWithScriptableObject OnSelectedClass;

    public delegate void ActionWithTransforms(Transform _previous = null, Transform _next = null);
    public ActionWithTransforms OnTeleportCheck;

    public delegate void ArrowAction(Transform _transform);
    public ArrowAction OnArrowCheck;

    public delegate void ActionWithGameObject(GameObject go = null);
    public ActionWithGameObject OnEventCheck;

    public delegate void ActionWithBoolean(bool value);
    public ActionWithBoolean OnArrowActive;

    public delegate void ActionDefault();
    public ActionDefault OnTutorialEnd;

    public delegate bool ReturnBoolAction();
    public ReturnBoolAction GetArrowOption;

    public void Fire_OnTeleportStart(Transform _teleportPointTransform, bool _shouldApplyRotation)
    {
        OnTeleportStart?.Invoke(_teleportPointTransform, _shouldApplyRotation);
    }

    public void Fire_OnTeleportEnd(Transform _playerTransform,  bool _shouldApplyRotation)
    {
        OnTeleportEnd?.Invoke(_playerTransform, _shouldApplyRotation);
    }

    public void Fire_OnInteractionTimerValueChanged(float _float)
    {
        OnInteractionTimerValueChanged?.Invoke(_float);
    }

    public void Fire_OnAnyObjectGazed(InteractableObjectController _gazedObject)
    {
        OnAnyObjectGazed?.Invoke(_gazedObject);
    }

    public void Fire_OnSelectedClass(ClassesScriptableObjects classesScriptableObjects)
    {
        OnSelectedClass?.Invoke(classesScriptableObjects);
    }

    public void Fire_OnRequestedTeleportFromVRMenu(int _id)
    {
        OnRequestedTeleportFromVRMenu?.Invoke(_id);
    }

    public void Fire_OnTeleportCheck(Transform previousTransform = null, Transform nextTransform = null)
    {
        OnTeleportCheck?.Invoke(previousTransform, nextTransform);
    }

    public void Fire_OnEventCheck(GameObject go = null)
    {
        OnEventCheck?.Invoke(go);
    }

    public void Fire_OnTutorialEnd()
    {
        OnTutorialEnd?.Invoke();
    }

    public void Fire_ArrowCheck(Transform _transform)
    {
        Fire_OnArrowActive(true);
        OnArrowCheck?.Invoke(_transform);
    }

    public void Fire_OnArrowActive(bool _value)
    {
        OnArrowActive?.Invoke(_value);
    }

    public bool Fire_GetArrowOption()
    {
        return GetArrowOption.Invoke();
    }
}
