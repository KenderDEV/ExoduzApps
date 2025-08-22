using UnityEngine;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using System.IO;

public class CopyInstallersPostBuild : IPostprocessBuildWithReport
{
    public int callbackOrder => 0;

    public void OnPostprocessBuild(BuildReport report)
    {
        string sourcePath = Path.Combine(Application.dataPath, "Data/Installers");
        string destinationPath = Path.Combine(Path.GetDirectoryName(report.summary.outputPath), "Data", "Installers");

        if (Directory.Exists(sourcePath))
        {
            if (!Directory.Exists(destinationPath))
            {
                Directory.CreateDirectory(destinationPath);
            }

            foreach (var file in Directory.GetFiles(sourcePath))
            {
                string destFile = Path.Combine(destinationPath, Path.GetFileName(file));
                File.Copy(file, destFile, true);
                Debug.Log($"Copied installer: {file} to {destFile}");
            }
        }
        else
        {
            Debug.LogWarning("No 'Installers' directory found in Assets.");
        }
    }
}

