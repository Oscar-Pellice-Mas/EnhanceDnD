using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class ChatLogs : MonoBehaviour
{
    private static ChatLogs _instance;
    public static ChatLogs Instance { get { return _instance; } }


    private List<LogItem> logItems = new List<LogItem>();

    private void Awake()
    {
        // Instance creation
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        _instance = this;
    }

    public class LogItem
    {
        private string message;
        private System.DateTime time;
        private string user;

        public LogItem(string message, string user)
        {
            this.message = message;
            this.user = user;
            time = System.DateTime.Now;
        }

        override
        public string ToString()
        {
            return "[" + time.ToString() + "] " + user + ": " + message;
        }
    }

    public void setMessage(string message, string user)
    {
        LogItem log = new LogItem(message, user);
        logItems.Add(log);
    }

    public List<LogItem> getChat()
    {
        return logItems;
    }
}
