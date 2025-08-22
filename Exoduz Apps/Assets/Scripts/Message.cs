using TMPro;
using UnityEngine;
public enum MessageTypeText
{
    Info,
    Warning,
    Error
}
public class Message : MonoBehaviour
{
    public static Message Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [SerializeField] private TextMeshProUGUI MessageTypeText;
    [SerializeField] private TextMeshProUGUI MessageContentText;
    [SerializeField] private GameObject MessagePanel;

    public void Show(MessageTypeText type, string content)
    {
        MessageTypeText.text = type.ToString();
        MessageContentText.text = content;
        MessagePanel.SetActive(true);
    }
}
