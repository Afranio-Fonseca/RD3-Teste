using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(ContentLoaderBttnView))]

public class ContentLoaderBttnController : MonoBehaviour
{
    [SerializeField] private XRSceneType mySceneToLoad;
    public AssetReference scene;

    ContentLoaderController loaderController;
    Button myBttn;

    ContentLoaderBttnView myView;

    [SerializeField] GameObject downloadSceneArea;
    [SerializeField] Button downloadSceneBttn;

    private void Awake()
    {
        loaderController = FindObjectOfType<ContentLoaderController>(true);
        myView = GetComponent<ContentLoaderBttnView>();
        myBttn = GetComponent<Button>();
        myBttn.onClick.AddListener(ClickedBttn);

        if(downloadSceneBttn)
            downloadSceneBttn.onClick.AddListener(ClickedDownloadBttn);
       
    }

   

    private void OnEnable()
    {
        ContentLoaderManager.Instance.CatalogUpdateFinished += CatalogUpdateFinished;
        GameManager.Instance.OnSelectedClass += OnSelectedClass;
    }

    private void OnDisable()
    {
        ContentLoaderManager.Instance.CatalogUpdateFinished -= CatalogUpdateFinished;
        GameManager.Instance.OnSelectedClass -= OnSelectedClass;
    }

    void ResetButtonStates()
    {
        if (scene != null)
        {
            if (downloadSceneBttn)
            {
                myBttn.interactable = false;
                downloadSceneBttn.interactable = false;

                if (downloadSceneArea)
                    downloadSceneArea.SetActive(false);
            }
        }
    }

    void OnSelectedClass(ClassesScriptableObjects classesScriptableObjects)
    {
        ResetButtonStates();
        scene = classesScriptableObjects.sceneReference;
        StartCoroutine(CheckAddressableDownloadState());
    }

    void CatalogUpdateFinished()
    {
       
    }

    IEnumerator CheckAddressableDownloadState()
    {
        var myScene = Addressables.GetDownloadSizeAsync(scene);

        while (myScene.Status != AsyncOperationStatus.Succeeded)
        {
            yield return null;
        }

        if(myScene.Status == AsyncOperationStatus.Succeeded)
        {
            var dataSize = myScene.Result;
            if(dataSize > 0)
            {
                AllowSceneDownload();
            }
            else
            {
                AllowSceneLoad();
            }
        }

        yield break;
    }

    void AllowSceneDownload()
    {
        myBttn.interactable = false;

        downloadSceneBttn.interactable = true;
        downloadSceneArea.SetActive(true);

        myView.AllowSceneDownload();
    }

    void AllowSceneLoad()
    {
        myBttn.interactable = true;
        downloadSceneBttn.interactable = false;
        downloadSceneArea.SetActive(false);

        myView.AllowSceneLoad();
    }

    void ClickedDownloadBttn()
    {
        StartCoroutine(DownloadRoutine());
        downloadSceneBttn.interactable = false;

        myView.ClickedDownloadBttn();
    }

    IEnumerator DownloadRoutine()
    {
        var cacheClearing = Addressables.ClearDependencyCacheAsync(scene, true);

        while(!cacheClearing.IsDone)
        {
            Debug.Log("Clearing cache");
            yield return null;
        }

        Debug.Log("Cache clearing status is: " + cacheClearing.Status);
        var sceneDownload = Addressables.DownloadDependenciesAsync(scene);
        while (!sceneDownload.IsDone)
        {
            var status = sceneDownload.GetDownloadStatus();
            float progress = status.Percent;

            myView.UpdateDownloadProgress((int)(progress * 100));
            yield return null;
        }

        var isSuccess = sceneDownload.Status == AsyncOperationStatus.Succeeded;
        Debug.Log("Download status is " + sceneDownload.Status);
        if (isSuccess == true)
        {
            AllowSceneLoad();
        }
        else
        {
            AllowSceneDownload();
        }
    }

    void ClickedBttn()
    {
        if(!loaderController)
        {
            return;
        }

        if(mySceneToLoad == XRSceneType.Menu)
        {
            ContentLoaderManager.Instance.Fire_OnMenuLoadRequested();
        }
        else
        {
            loaderController.RequestNewContentScene(scene);
        }
        
    }

    public XRSceneType GetMyScene()
	{
        return mySceneToLoad;
	}
}

namespace RD3Client
{
    public class RD3HttpClient: Singleton<RD3HttpClient>
    {
        public string _baseUrl;
        public static string BaseUrl => Instance._baseUrl;


    }
}