using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float height;
    [SerializeField] float tutorialTime;
    [SerializeField] Camera myCamera;
    [SerializeField] Image loadingBar;
    [SerializeField] CanvasGroup playerModal;
    [SerializeField] bool usePlayerArrow;

    Transform currentSceneFirstSpawn;
    private void Awake()
    {
        myCamera = Camera.main;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        OnContentSceneLoaded(null);
    }

    private void Start()
    {
        StartCoroutine(StartTutorialRoutine());
    }
    private void OnEnable()
    {
        GameManager.Instance.OnTeleportStart += OnTeleportStart;
        GameManager.Instance.GetArrowOption += GetArrowOption;
        //GameManager.Instance.OnInteractionTimerValueChanged += UpdateLoadingBar;
        ContentLoaderManager.Instance.OnContentSceneLoaded += OnContentSceneLoaded;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnTeleportStart -= OnTeleportStart;
        GameManager.Instance.GetArrowOption -= GetArrowOption;
        //GameManager.Instance.OnInteractionTimerValueChanged -= UpdateLoadingBar;
        ContentLoaderManager.Instance.OnContentSceneLoaded -= OnContentSceneLoaded;
    }

    void OnTeleportStart(Transform _point, bool _shouldApplyRotation)
    {
        Vector3 teleportPosition = _point.position;
        teleportPosition.y = _point.position.y + height;

        transform.position = teleportPosition;

        if (_shouldApplyRotation)
        {
            transform.eulerAngles = new Vector3(0, _point.eulerAngles.y - myCamera.transform.localEulerAngles.y, 0);
        }
        GameManager.Instance.Fire_OnTeleportEnd(transform, _shouldApplyRotation);
    }

    void OnContentSceneLoaded(UnityEngine.AddressableAssets.AssetReference _scene)
    {
        //currentSceneFirstSpawn = GameObject.Find("FirstSpawn - FOUND BY CODE").GetComponent<Transform>();

        if (currentSceneFirstSpawn)
        {
            GameManager.Instance.Fire_OnTeleportStart(currentSceneFirstSpawn, true);
        }
    }

    public void UpdateLoadingBar(float value)
    {
        loadingBar.fillAmount = value;
    }

    IEnumerator StartTutorialRoutine()
    {
        yield return new WaitForSeconds(tutorialTime);
        StartCoroutine(Tween.TweenAlpha(playerModal, 0, 1, 2));
    }

    public bool GetArrowOption()
    {
        return usePlayerArrow;
    }
}
