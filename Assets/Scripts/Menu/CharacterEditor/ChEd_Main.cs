using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AnotherFileBrowser.Windows;
using UnityEngine.Networking;
using TMPro;
using System.IO;

public class ChEd_Main : MonoBehaviour
{
    public RawImage rawImage;

    [SerializeField] private TMP_InputField nameInput;

    private void ChangeName()
    {
        // Call when change from input field
        CharacterEditorMenu.Instance.basicPc.Name = nameInput.text;
    }

    public void OpenFileBrowser()
    {
        var bp = new BrowserProperties();
        bp.filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
        bp.filterIndex = 0;

        new FileBrowser().OpenFileBrowser(bp, path =>
        {
            //Load image from local path with UWR
            StartCoroutine(LoadImage(path));
        });
    }

    IEnumerator LoadImage(string path)
    {
        using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(path))
        {
            yield return uwr.SendWebRequest();

            if (uwr.result == UnityWebRequest.Result.ConnectionError || uwr.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log(uwr.error);
            }
            else
            {
                Texture2D uwrTexture = DownloadHandlerTexture.GetContent(uwr);
                rawImage.texture = uwrTexture;
                
                //Sprite itemBGSprite = Resources.Load<Sprite>("_Defaults/Item Images/_Background");
                //Texture2D itemBGTex = itemBGSprite.texture;
                byte[] textureBytes = uwrTexture.EncodeToPNG();
                File.WriteAllBytes(Application.dataPath + "/Images/Player_" + CharacterEditorMenu.Instance.basicPc.Name 
                    + ".png", textureBytes);

                CharacterEditorMenu.Instance.basicPc.Sprite = Resources.Load<Sprite>(Application.dataPath + "/Images/Player_" 
                    + CharacterEditorMenu.Instance.basicPc.Name + ".png") ;
                /*var bytes = rawImage.texture.EncodeToPNG();
                var file = new File.Open(Application.dataPath + "/" + fileName, FileMode.Create);
                var binary = new BinaryWriter(file);
                binary.Write(bytes);
                file.Close();*/
            }
        }
    }


}
