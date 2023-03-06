using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum XRSceneType { None = -1, Menu = 0, BaseScene = 1, Scene3D_Addressable = 2 }

public static class PublicConstants
{
	// AR
	public const string key_SceneToLoad = "SceneToLoad";
	public const int key_MenuSceneIndex = (int)XRSceneType.Menu;
	public const int key_BaseSceneIndex = (int)XRSceneType.BaseScene;
}