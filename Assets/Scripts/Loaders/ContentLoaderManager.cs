using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.AddressableAssets.ResourceLocators;
public class ContentLoaderManager : Singleton<ContentLoaderManager>
{
    public int totalHighlights;

    public bool isOnXr;

    public delegate void SceneRequests(AssetReference _sceneReference);
    public SceneRequests OnContentSceneRequested;
    public SceneRequests OnContentSceneLoaded;

    public delegate void SimpleRequests();
    public SimpleRequests OnMenuLoadRequested;
    
    public SimpleRequests CatalogUpdateRequested;
    public SimpleRequests CatalogUpdateFinished;

    public AssetReference addressableSceneRequested;

    public void Fire_OnContentSceneRequested(AssetReference _sceneReference)
    {
        for (int i = 0; i < GameManager.Instance.classesScriptableObjects.Count; i++)
        {
            if(GameManager.Instance.classesScriptableObjects[i].sceneReference.ToString().Contains(_sceneReference.ToString()))
            {
                SaveHighlight(GameManager.Instance.classesScriptableObjects[i]);
                Debug.Log("MATCH FOR HIGHLIGHT on id " + GameManager.Instance.classesScriptableObjects[i].sceneReference);
            }
            else
            {
                Debug.Log(_sceneReference + " is not the same as " + GameManager.Instance.classesScriptableObjects[i].sceneReference);
            }
        }

        //SceneManager.sceneLoaded += LocalContentSceneLoaded;
        //SceneManager.LoadScene(2);

        Addressables.LoadSceneAsync(_sceneReference, LoadSceneMode.Single).Completed += SceneLoadComplete;
        OnContentSceneRequested?.Invoke(_sceneReference);
    }


    void SceneLoadComplete(AsyncOperationHandle<SceneInstance> obj)
    {
        if (obj.Status == AsyncOperationStatus.Succeeded)
        {
            SceneManager.sceneLoaded += XRSceneLoaded;
            SceneManager.LoadScene(PublicConstants.key_BaseSceneIndex, LoadSceneMode.Additive);
        }
        else
        {
            ContentLoaderManager.Instance.Fire_OnMenuLoadRequested();
        }
    }

    void LocalContentSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= LocalContentSceneLoaded;

        SceneManager.sceneLoaded += XRSceneLoaded;
        SceneManager.LoadScene(PublicConstants.key_BaseSceneIndex, LoadSceneMode.Additive);
    }
    void XRSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("XR SCENE LOADED");
        isOnXr = true;
        ContentLoaderManager.Instance.Fire_OnContentSceneLoaded(null);
        SceneManager.sceneLoaded -= XRSceneLoaded;
    }
    public void Fire_OnContentSceneLoaded(AssetReference _sceneReference)
    {
        OnContentSceneLoaded?.Invoke(_sceneReference);
        
    }

    public void Fire_OnMenuLoadRequested()
    {
        isOnXr = false;
        OnMenuLoadRequested?.Invoke();
    }

    public void Fire_CatalogUpdateRequested()
    {
        CatalogUpdateRequested?.Invoke();
    }
    public void Fire_CatalogUpdateFinished()
    {
        CatalogUpdateFinished?.Invoke();
    }

    void SaveHighlight(ClassesScriptableObjects classesScriptableObjects)
    {
        if (PlayerPrefs.HasKey("lastHighlightNumber"))
        {
            int lastHighlightNumber = PlayerPrefs.GetInt("lastHighlightNumber");

            int newHighlightNumber = lastHighlightNumber + 1;

            if (newHighlightNumber >= totalHighlights)
            {
                newHighlightNumber = 0;
            }

            CheckIfHighlightExistsAndSavePlayerPrefs(newHighlightNumber, classesScriptableObjects.id);

        }
        else
        {
            PlayerPrefs.SetInt("lastHighlightNumber", 0);
            PlayerPrefs.SetInt("Highlight" + 0.ToString(), classesScriptableObjects.id);

            Debug.Log("No Key found, making class with id: " + classesScriptableObjects.id + " a highlight");
        }
    }

    void CheckIfHighlightExistsAndSavePlayerPrefs(int prefsIndex, int objectId)
    {
        bool isClassHighlighted = false;
        for (int i = 0; i < totalHighlights; i++)
        {
            if(PlayerPrefs.GetInt("Highlight" + i) == objectId)
            {
                isClassHighlighted = true;
            }
        }

        if(!isClassHighlighted)
        {
            PlayerPrefs.SetInt("Highlight" + prefsIndex.ToString(), objectId);

            PlayerPrefs.SetInt("lastHighlightNumber", prefsIndex);

            Debug.Log("Key found, saved class with id: " + objectId + " as highlight on index" + prefsIndex);
        }
    }
}
