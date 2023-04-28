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
            EditorGUILayout.HelpBox("����� ������ ����� �������� \"GameSettings\"", MessageType.Error);

        string[] directories = assetPath.Split('/');
        if (directories.Length != 3 || directories[1] != "Resources")
            EditorGUILayout.HelpBox("����� ������ ���������� �� ����:  \"Resources/GameSettings\"", MessageType.Error);
    }
}