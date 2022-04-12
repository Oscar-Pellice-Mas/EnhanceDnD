using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChatDisplay : MonoBehaviour
{
    [SerializeField] private TMP_InputField input;
    [SerializeField] private TMP_Text chatText;

    private List<ChatLogs.LogItem> logItems = new List<ChatLogs.LogItem>();

    public void SubmitText()
    {
        ChatLogs.Instance.setMessage(input.text, "User");
        input.text = "";
    }

    private void LateUpdate()
    {
        logItems = ChatLogs.Instance.getChat();
        UpdateChat();
    }

    private void UpdateChat()
    {
        chatText.text = "";
        foreach (ChatLogs.LogItem logItem in logItems)
        {
            chatText.text += "\n" + logItem.ToString();
        }
    }
}
