using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Diagnostics;
using System.IO;
using Unity.VisualScripting;

public class AppUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI TitleApp;
    [SerializeField] private TextMeshProUGUI VersionApp;
    [SerializeField] private TextMeshProUGUI DescriptionApp;
    [SerializeField] private Button InstallButton;
    [SerializeField] private Button LinkButton;

    public void PrepareAppUI(AppDataConfig appData)
    {
        if (appData == null)
        {
            UnityEngine.Debug.LogError("AppData is null");
            return;
        }

        TitleApp.text = appData.nameApp;
        VersionApp.text = $"Version: {appData.version}";
        DescriptionApp.text = appData.description;

        InstallButton.onClick.AddListener(() => InstallApp(appData.installerName));
        LinkButton.onClick.AddListener(() => OpenLink(appData.webLink));
    }

    void InstallApp(string installerName)
    {
        string installersFolder = Path.Combine(Application.dataPath, "..", "Data/Installers");
        string ruteComplete = Path.Combine(installersFolder, installerName);

        if (File.Exists(ruteComplete))
        {
            if (installerName.EndsWith(".exe") == false && installerName.EndsWith(".msi") == false)
            {
                Message.Instance.Show(MessageTypeText.Info, "Esta App no tiene un instalador, no se puede instalar.\nVe al link para descargarla.");
                return;
            }
            Process.Start(ruteComplete);
            UnityEngine.Debug.Log($"Starting installer: {ruteComplete}");
        }
        else
        {
            Message.Instance.Show(MessageTypeText.Info, "Esta App no tiene un instalador, no se puede instalar.\nVe al link para descargarla.");
            return;
        }
    }

    void OpenLink(string link)
    {
        if (string.IsNullOrEmpty(link))
        {
            UnityEngine.Debug.LogError("Link is empty");
            return;
        }

        Application.OpenURL(link);
    }
}
