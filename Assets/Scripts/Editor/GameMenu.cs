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
}