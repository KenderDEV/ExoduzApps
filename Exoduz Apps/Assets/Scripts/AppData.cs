using UnityEngine;
[System.Serializable]
[CreateAssetMenu(fileName = "NuevaApp", menuName = "Exoduz Apps/App Data", order = 1)]
public class AppDataConfig : ScriptableObject
{
    public string nameApp;
    [TextArea] public string description;
    public TypesOfApps typeApp;
    public string version;
    public string webLink;
    public string installerName;
}
