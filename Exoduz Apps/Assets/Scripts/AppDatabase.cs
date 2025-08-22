using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "AppDataBase", menuName = "Exoduz Apps/Database", order = 2)]
public class AppDatabase : ScriptableObject
{
    public List<AppDataConfig> apps = new List<AppDataConfig>();
}
