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
		_script.weaponSettings = (WeaponSettings)EditorGUILayout.ObjectField("��������� ������", _script.weaponSettings, typeof(WeaponSettings), false);
        _script.firePoint = (Transform)EditorGUILayout.ObjectField("����� ����", _script.firePoint, typeof(Transform), true);
        _script.ammoText = (TMP_Text)EditorGUILayout.ObjectField("����� ������", _script.ammoText, typeof(TMP_Text), true);
        if (GUILayout.Button("����������"))
            _script.Shoot();
    }
}