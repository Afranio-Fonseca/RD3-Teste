using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Manager360Background : MonoBehaviour
{
    public Camera bgCam;
    public Transform player;
    public Texture2D tex;
    public MeshRenderer mesh;

    private void OnEnable()
    {
        GameManager.Instance.OnTeleportEnd += OnTeleportEnded;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnTeleportEnd -= OnTeleportEnded;
    }
    void OnTeleportEnded(Transform _playerTransform, bool _shouldApplyRotation)
    {
        print("teleport");
        transform.position = player.position;
        byte[] bytes = I360Render.Capture(2048, true, bgCam);
        if(bytes != null)
        {
            //string path = Path.Combine(Application.persistentDataPath, "360render" + ".jpeg");
            //File.WriteAllBytes(path, bytes);
            tex = new Texture2D(1, 1);
            tex.LoadImage(bytes);
            mesh.material.mainTexture = tex;
        }
    }
}
