using System.IO;
using UnityEditor;
using UnityEngine;

public class GameMenu : Editor
{
    private const string FILENAME = "Screenshot";
    private const string FILEEXPANSION = ".png";

    [MenuItem("Игра/Сделать скриншот")]
    private static void TakeScreenshot()
    {
        if (!Application.isPlaying)
        {
            Debug.Log($"<color=#FF7100>Невозможно сделать скриншот пока игра не запущена!</color>");
            return;
        }

        int screenNumber = 1;
        string fileName = FILENAME + screenNumber + FILEEXPANSION;
        while (File.Exists(fileName))
            fileName = FILENAME + (++screenNumber) + FILEEXPANSION;
        ScreenCapture.CaptureScreenshot(fileName);
        Debug.Log($"<color=#FF7100>Скриншот сохранён в папке проекта с названием:</color> {fileName}");
    }

    [MenuItem("Игра/Подготовить проект")]
    private static void PrepareProject()
    {
        if (EditorUtility.DisplayDialog("Подтверждение создания", "Это создаст папки проекта.\nЕсли они уже созданы, то нажмите \"Отмена\"", "Подтвердить", "Отмена"))
        {
            AssetDatabase.CreateFolder("Assets", "Resources");
            AssetDatabase.CreateFolder("Assets", "Scripts");
            AssetDatabase.CreateFolder("Assets/Scripts", "Editor");
            AssetDatabase.CreateFolder("Assets/Resources", "Sprites");
            AssetDatabase.CreateFolder("Assets/Resources", "Audio");
            AssetDatabase.CreateFolder("Assets/Resources", "Animations");
            AssetDatabase.CreateFolder("Assets/Resources", "Prefabs");
            Debug.Log($"<color=#FF7100>Папки проекта успешно созданы!</color>");
        }
    }
}