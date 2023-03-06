using System.IO;
using UnityEngine;

public class CubemapScreenshot : MonoBehaviour
{
	public int imageWidth = 1024;
	public Camera renderCam;
	public bool saveAsJPEG = true;
	public bool faceCameraDirection = true;


	void Update()
	{
		if (Input.GetKeyDown(KeyCode.P))
		{
			byte[] bytes = I360Render.Capture(imageWidth, saveAsJPEG, renderCam, faceCameraDirection);
			if (bytes != null)
			{
				string path = Path.Combine(Application.persistentDataPath, "360render" + (saveAsJPEG ? ".jpeg" : ".png"));
				File.WriteAllBytes(path, bytes);
				Debug.Log("360 render saved to " + path);
			}
		}
	}
}
