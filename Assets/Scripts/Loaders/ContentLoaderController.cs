using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEditor;

public class ContentLoaderController : MonoBehaviour
{
    bool needsToRemoveScene = false;

    int previousLoadedScene;

    AsyncOperationHandle<SceneInstance> loadedAdditiveScene;

    List<string> catalogsToUpdate = new List<string>();

    [SerializeField] bool isLocalBuild;


    private void Start()
    {
        needsToRemoveScene = false;

        if (SceneManager.GetActiveScene().buildIndex == PublicConstants.key_MenuSceneIndex)
        {
            StartCoroutine(CheckForCatalogChanges());
            
            Debug.Log("Você está no menu, nenhuma cena aditiva será carregada");
        }
        else
        {
            if (ContentLoaderManager.Instance.addressableSceneRequested == null)
            {
                Debug.Log("Algo deu errado, é necessário uma cena aditiva para carregar");
                ContentLoaderManager.Instance.Fire_OnMenuLoadRequested();
                return;
            }
            //else
            //{
            //    Debug.Log("Cena aditiva está sendo carregada");
            //    RequestNewContentScene(ContentLoaderManager.Instance.addressableSceneRequested);
            //}
        }
    }


    private void OnEnable()
    {
        ContentLoaderManager.Instance.OnContentSceneRequested += OnContentSceneRequested;
        ContentLoaderManager.Instance.OnContentSceneLoaded += OnContentSceneLoaded;
        ContentLoaderManager.Instance.OnMenuLoadRequested += BackToMenu;

        ContentLoaderManager.Instance.CatalogUpdateRequested += CatalogUpdateRequested;
        GameManager.Instance.OnSelectedClass += OnSelectedClass;
    }

    private void OnDisable()
    {
        ContentLoaderManager.Instance.OnContentSceneRequested -= OnContentSceneRequested;
        ContentLoaderManager.Instance.OnContentSceneLoaded -= OnContentSceneLoaded;
        ContentLoaderManager.Instance.OnMenuLoadRequested -= BackToMenu;

        ContentLoaderManager.Instance.CatalogUpdateRequested -= CatalogUpdateRequested;
        GameManager.Instance.OnSelectedClass -= OnSelectedClass;
    }

    IEnumerator CheckForCatalogChanges()
    {
        if(isLocalBuild)
        {
            ContentLoaderManager.Instance.Fire_CatalogUpdateFinished();
            yield break;
        }
        RD3Client.RD3HttpClient.Instance._baseUrl = "http://rd3space.com/fiemgVR/" + Application.version.ToString() + "/1/Android";

        catalogsToUpdate.Clear();
        AsyncOperationHandle<List<string>> checkForUpdateHandle = Addressables.CheckForCatalogUpdates();
        checkForUpdateHandle.Completed += op =>
        {
            catalogsToUpdate.AddRange(op.Result);
        };

        yield return checkForUpdateHandle;
        if (catalogsToUpdate.Count > 0)
        {
            for (int i = 0; i < catalogsToUpdate.Count; i++)
            {
                Debug.Log("update needed in " + catalogsToUpdate[i]);
            }
            ContentLoaderManager.Instance.Fire_CatalogUpdateRequested();
        }
        else
        {
            Debug.Log("No catalogs need updating");
            ContentLoaderManager.Instance.Fire_CatalogUpdateFinished();
        }
    }

    void CatalogUpdateRequested()
    {
        StartCoroutine(UpdateCatalog());
    }

    IEnumerator UpdateCatalog()
    {
        AsyncOperationHandle<List<IResourceLocator>> updateHandle = Addressables.UpdateCatalogs(catalogsToUpdate);
        yield return updateHandle;

        ContentLoaderManager.Instance.Fire_CatalogUpdateFinished();
    }


    void OnSelectedClass(ClassesScriptableObjects classesScriptableObjects)
    {
        StartCoroutine(CheckForCatalogChanges());
    }

    void OnContentSceneRequested(AssetReference _sceneReference)
    {
        //if (needsToRemoveScene)
        //{
        //    RemoveCurrentScene();
        //}

       
    }

    void OnContentSceneLoaded(AssetReference _sceneReference)
    {
        needsToRemoveScene = true;
    }

    void RemoveCurrentScene()
    {
        Addressables.UnloadSceneAsync(loadedAdditiveScene, true).Completed += op =>
        {
            if (op.Status == AsyncOperationStatus.Succeeded)
            {
                Debug.Log("Cena foi descarregada");
            }
        };
    }


    public void RequestNewContentScene(AssetReference _sceneReference)
    {
        ContentLoaderManager.Instance.addressableSceneRequested = _sceneReference;

        //if(SceneManager.GetActiveScene().buildIndex == PublicConstants.key_MenuSceneIndex)
        //{
        //    Debug.Log("O index foi salvo. A cena Base será carregada a seguir, juntamente com sua aditiva");
        //    SceneManager.LoadScene(PublicConstants.key_BaseSceneIndex);
        //    return;
        //}

        ContentLoaderManager.Instance.Fire_OnContentSceneRequested(_sceneReference);
    }

    void BackToMenu()
    {
        PlayerPrefs.SetInt(PublicConstants.key_SceneToLoad, PublicConstants.key_MenuSceneIndex);
        SceneManager.LoadScene(PublicConstants.key_MenuSceneIndex);
    }

    public void PublicMenuLoadRequest()
    {
        ContentLoaderManager.Instance.Fire_OnMenuLoadRequested();
    }
}
