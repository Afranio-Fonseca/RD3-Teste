using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentLoaderBttnView : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Text downloadText;

    public void AllowSceneDownload()
    {
        downloadText.text = "Atualizar";
    }

    public void AllowSceneLoad()
    {

    }

    public void ClickedDownloadBttn()
    {

    }

    public void UpdateDownloadProgress(int _progress)
    {
        downloadText.text = _progress.ToString();
    }
}
