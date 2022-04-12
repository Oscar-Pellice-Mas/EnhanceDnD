using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AnotherFileBrowser.Windows;

public class ChEd_Main : MonoBehaviour
{
    public RawImage rawImage;

    public void OpenFileBrowser()
    {
        var bp = new BrowserProperties();
        bp.filter = "";
    }
}
