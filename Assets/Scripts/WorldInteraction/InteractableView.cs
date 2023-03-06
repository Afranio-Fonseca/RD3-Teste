using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InteractableView : MonoBehaviour
{
    [SerializeField] bool lookAtPlayer = true;
    [SerializeField] bool useAnim = false;

    [SerializeField] Transform canvasPivot;

    [SerializeField] Image iconImage;
    [SerializeField] Sprite iconSprite;
    [SerializeField] Animator anim;

    [SerializeField] GameObject loadingBar;
    [SerializeField] Image loadingFill;
    [SerializeField] TMP_Text featureText;

    bool xrSceneLoaded;

    private void Awake()
    {
        if(!canvasPivot && lookAtPlayer)
        {
            canvasPivot = transform.parent.parent;
        }
    }

    private void OnEnable()
    {
        ContentLoaderManager.Instance.OnContentSceneLoaded += OnContentSceneLoaded;
        if (ContentLoaderManager.Instance.isOnXr)
        {
            CanvasLookAtPlayer();
        }
    }

    private void OnDisable()
    {
        ContentLoaderManager.Instance.OnContentSceneLoaded -= OnContentSceneLoaded;
    }

    private void Start()
    {
        ChangeLoadingBarState(false);
    }

    void OnContentSceneLoaded(UnityEngine.AddressableAssets.AssetReference _sceneReference)
    {
        CanvasLookAtPlayer();
    }

    public void ChangeLoadingBarState(bool _value)
    {
        if (!useAnim)
        {
            if (loadingBar)
                loadingBar.SetActive(_value);
        }
        else
        {
            if (anim)
                anim.gameObject.SetActive(_value);
        }
    }

    public void UpdateLoadingFill(float _value)
    {
        if (!useAnim)
        {
            if (loadingFill)
                loadingFill.fillAmount = _value;
        }
        else
        {
            if (anim)
                anim.SetFloat("animTime", _value);
        }
    }

    void CanvasLookAtPlayer()
    {
        if (canvasPivot && lookAtPlayer)
        {
            canvasPivot.LookAt(FindObjectOfType<PlayerController>(true).GetComponent<Transform>());
        }
    }
}
