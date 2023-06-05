using UnityEditor;
using UnityEngine;
using TMPro;

[CustomEditor(typeof(Weapon))]
public class WeaponEditor : Editor
{
    private Weapon _script;

    private void OnEnable() => _script = (Weapon)target;

    public override void OnInspectorGUI()
    {
		EditorStyles.textField.wordWrap = true;
		
        _script.weaponSettings = (WeaponSettings)EditorGUILayout.ObjectField("��������� ������", _script.weaponSettings, typeof(WeaponSettings), false);
        _script.firePoint = (Transform)EditorGUILayout.ObjectField("����� ����", _script.firePoint, typeof(Transform), true);
        _script.ammoText = (TMP_Text)EditorGUILayout.ObjectField("����� ������", _script.ammoText, typeof(TMP_Text), true);
        _script.ammoTextFormat = EditorGUILayout.TextField("������ ������", _script.ammoTextFormat, GUILayout.Height(100));
        if (!_script.ammoTextFormat.Contains("{0}") || !_script.ammoTextFormat.Contains("{1}"))
			EditorGUILayout.HelpBox("������ ������ ��������� �������� {0}(������� �������), {1}(������������ ���������� ������ � ��������)", MessageType.Error);
		if (GUILayout.Button("����������"))
            _script.Shoot();
    }
}