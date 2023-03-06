using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

[RequireComponent(typeof(CanvasGroupFader))]
public class LoadingPanelController : MonoBehaviour
{
	CanvasGroupFader fader;
	private void Awake()
	{
		fader = this.GetComponent<CanvasGroupFader>();
	}
	void Start()
	{
		ContentLoaderManager.Instance.OnContentSceneRequested += OnAddScene_Start;
		ContentLoaderManager.Instance.OnContentSceneLoaded += OnAddScene_End;
	}
	private void OnAddScene_Start(AssetReference _sceneReference)
	{
		fader.DoFadeIn();
	}

	private void OnAddScene_End(AssetReference _sceneReference)
	{
		fader.DoFadeOut(true);
	}

	private void OnDestroy()
	{
		ContentLoaderManager.Instance.OnContentSceneRequested -= OnAddScene_Start;
		ContentLoaderManager.Instance.OnContentSceneLoaded -= OnAddScene_End;
	}
}
