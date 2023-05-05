using UnityEngine;
using UnityEditor;

public class OlgaGreetings : EditorWindow
{
	[MenuItem("Игра/Приветствие Ольги")]
    private static void ShowWindow()
	{
		OlgaGreetings window = EditorWindow.GetWindow<OlgaGreetings>("Приветствие");
		window.position = new Rect(100, 100, 550, 300);
		window.Show();
	}
	
	private void OnGUI()
	{
		EditorGUILayout.BeginVertical("box");
		EditorGUILayout.LabelField("О, Ольга, добро пожаловать в проект \"Shooting center\"!", EditorStyles.boldLabel);
		EditorGUILayout.LabelField("Unity будет стараться сделать всё возможное, чтобы облегчить вашу работу.");
		EditorGUILayout.LabelField("Клянусь свято выполнять ваши приказы.");
		EditorGUILayout.LabelField("Клянусь не лагать, не вылетать и отображать всё корректно.");
		EditorGUILayout.LabelField("Приятного времяпрепровождения.", EditorStyles.boldLabel);
		EditorGUILayout.EndVertical();
	}
}

[InitializeOnLoad]
public static class OlgaGreetingsInitializer
{
	static OlgaGreetingsInitializer()
	{
		EditorApplication.delayCall += ShowWindow;
	}
	
	private static void ShowWindow()
	{
		OlgaGreetings window = EditorWindow.GetWindow<OlgaGreetings>("Приветствие");
		window.position = new Rect(100, 100, 550, 300);
		window.Show();
	}
}
