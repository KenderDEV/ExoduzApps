using UnityEngine;
using UnityEditor;

public class AppDatabaseWindow : EditorWindow
{
    private AppDatabase database;

    [MenuItem("Exoduz Apps/Administrador de Apps")]
    public static void ShowWindow()
    {
        GetWindow<AppDatabaseWindow>("Administrador de Apps");
    }

    private void OnGUI()
    {
        GUILayout.Label("Base de Datos de Exoduz Apps", EditorStyles.boldLabel);

        database = (AppDatabase)EditorGUILayout.ObjectField("Database", database, typeof(AppDatabase), false);

        if (database == null)
        {
            EditorGUILayout.HelpBox("Asigna un AppDatabase para empezar.", MessageType.Info);
            return;
        }

        if (GUILayout.Button("Agregar nueva App"))
        {
            CrearNuevaApp();
        }

        if (GUILayout.Button("Guardar Base de Datos"))
        {
            EditorUtility.SetDirty(database);
            AssetDatabase.SaveAssets();
            Debug.Log("Base de datos guardada");
        }

        if (GUILayout.Button("Recargar Base de Datos"))
        {
            string path = AssetDatabase.GetAssetPath(database);
            database = AssetDatabase.LoadAssetAtPath<AppDatabase>(path);
            Debug.Log("Base de datos recargada");
        }

        EditorGUILayout.Space();

        for (int i = 0; i < database.apps.Count; i++)
        {
            EditorGUILayout.BeginVertical("box");
            database.apps[i] = (AppDataConfig)EditorGUILayout.ObjectField("App", database.apps[i], typeof(AppDataConfig), false);
            if (database.apps[i] != null)
            {
                EditorGUILayout.LabelField("Nombre:", database.apps[i].nameApp);
                EditorGUILayout.LabelField("Versión:", database.apps[i].version);
            }

            if (GUILayout.Button("Eliminar"))
            {
                database.apps.RemoveAt(i);
                break;
            }
            EditorGUILayout.EndVertical();
        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(database);
        }
    }

    private void CrearNuevaApp()
    {
        string path = EditorUtility.SaveFilePanelInProject("Guardar nueva App", "NuevaApp.asset", "asset", "Escribe un nombre para la nueva App");
        if (!string.IsNullOrEmpty(path))
        {
            AppDataConfig nuevaApp = ScriptableObject.CreateInstance<AppDataConfig>();
            AssetDatabase.CreateAsset(nuevaApp, path);
            AssetDatabase.SaveAssets();

            database.apps.Add(nuevaApp);
            EditorUtility.SetDirty(database);
        }
    }
}
