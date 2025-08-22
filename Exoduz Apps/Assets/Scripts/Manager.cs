using UnityEngine;
using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine.UI;


public class Resolutions
{
    public static readonly Vector2Int[] SupportedResolutions = new Vector2Int[]
    {
        new Vector2Int(800, 600),
        new Vector2Int(1000, 600),
        new Vector2Int(1024, 768),
        new Vector2Int(1280, 720),
        new Vector2Int(1366, 768),
        new Vector2Int(1440, 900),
        new Vector2Int(1600, 900),
        new Vector2Int(1920, 1080),
        new Vector2Int(2560, 1440),
        new Vector2Int(3840, 2160)
    };
}

public enum TypesOfApps
{
    None,
    Utilities,
    Games,
    Optimization,
    Other
}
public class Manager : MonoBehaviour
{
    public static Manager Instance { get; private set; }
    public AppDatabase database;

    [SerializeField] private GameObject AppUIPrefab;
    [SerializeField] private Transform AppUIContainer;

    [SerializeField] private Button HomeButton;
    [SerializeField] private Button GameButton;
    [SerializeField] private Button OptimizationButton;
    [SerializeField] private Button UtilitiesButton;
    [SerializeField] private Button OthersButton;
    void Awake()
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
    void Start()
    {
        PrepareButtons();
        PrepareAppsUI();
    }

    void PrepareButtons()
    {
        HomeButton.onClick.AddListener(() => FilterAppsByType(TypesOfApps.None));
        GameButton.onClick.AddListener(() => FilterAppsByType(TypesOfApps.Games));
        OptimizationButton.onClick.AddListener(() => FilterAppsByType(TypesOfApps.Optimization));
        UtilitiesButton.onClick.AddListener(() => FilterAppsByType(TypesOfApps.Utilities));
        OthersButton.onClick.AddListener(() => FilterAppsByType(TypesOfApps.Other));
    }
    void PrepareAppsUI()
    {
        foreach (var app in database.apps)
        {
            GameObject appUI = Instantiate(AppUIPrefab, AppUIContainer);
            AppUI appUIComponent = appUI.GetComponent<AppUI>();
            if (appUIComponent != null)
            {
                appUIComponent.PrepareAppUI(app);
            }
            else
            {
                Debug.LogError("AppUIPrefab does not have an AppUI component.");
            }
        }
    }

    public void FilterAppsByType(TypesOfApps type)
    {
        foreach (Transform child in AppUIContainer)
        {
            Destroy(child.gameObject);
        }
        if (type == TypesOfApps.None)
        {
            PrepareAppsUI();
            return;
        }
        foreach (var app in database.apps)
        {
            if (app.typeApp == type)
            {
                GameObject appUI = Instantiate(AppUIPrefab, AppUIContainer);
                AppUI appUIComponent = appUI.GetComponent<AppUI>();
                if (appUIComponent != null)
                {
                    appUIComponent.PrepareAppUI(app);
                }
                else
                {
                    Debug.LogError("AppUIPrefab does not have an AppUI component.");
                }
            }
        }
    }


    public Vector2Int GetCurrentResolution()
    {
        Debug.Log($"Current resolution: {Screen.width}x{Screen.height}");
        foreach (Vector2Int resolution in Resolutions.SupportedResolutions)
        {
            if (resolution.x == Screen.width && resolution.y == Screen.height)
            {
                return resolution;
            }
        }
        throw new Exception("Not found resolution");
    }


    public void Quit()
    {
        Debug.Log("Application is quitting.");
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}

public class AppData
{
    public string nombre;
    public string descripcion;
    public string version;
    public string linkWeb;
    public string rutaInstaller;
}

[Serializable]
public class AppDataList
{
    public List<AppData> apps;
}
