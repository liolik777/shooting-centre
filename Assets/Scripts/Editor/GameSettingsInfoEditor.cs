using UnityEditor;

[CustomEditor(typeof(GameSettingsInfo))]
public class GameSettingsInfoEditor : Editor
{
    private GameSettingsInfo _script;

    private void OnEnable() => _script = (GameSettingsInfo)target;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        string assetPath = AssetDatabase.GetAssetPath(_script);
        if (!assetPath.EndsWith("GameSettings.asset"))
            EditorGUILayout.HelpBox("Ассет должен иметь название \"GameSettings\"", MessageType.Error);

        string[] directories = assetPath.Split('/');
        if (directories.Length != 3 || directories[1] != "Resources")
            EditorGUILayout.HelpBox("Ассет должен находитьс€ по пути:  \"Resources/GameSettings\"", MessageType.Error);
		
		if (!_script.weaponFormatText.Contains("{0}") || !_script.weaponFormatText.Contains("{1}"))
			EditorGUILayout.HelpBox("‘ормат должен содержать элементы {0}(текущие патроны), {1}(максимальное количество патрон в магазине)", MessageType.Error);
    }
}