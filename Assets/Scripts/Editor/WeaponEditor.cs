using UnityEditor;
using UnityEngine;
using TMPro;

[CustomEditor(typeof(Weapon))]
public class WeaponEditor : Editor
{
    private Weapon _script;
	private int _ammoToAdd;

    private void OnEnable() => _script = (Weapon)target;

    public override void OnInspectorGUI()
    {
		EditorStyles.textField.wordWrap = true;
		
        _script.weaponSettings = (WeaponSettings)EditorGUILayout.ObjectField("Настройки оружия", _script.weaponSettings, typeof(WeaponSettings), false);
        _script.firePoint = (Transform)EditorGUILayout.ObjectField("Точка дула", _script.firePoint, typeof(Transform), true);
        _script.ammoText = (TMP_Text)EditorGUILayout.ObjectField("Текст патрон", _script.ammoText, typeof(TMP_Text), true);
        _script.ammoTextFormat = EditorGUILayout.TextField("Формат текста", _script.ammoTextFormat, GUILayout.Height(100));
        if (!_script.ammoTextFormat.Contains("{0}") || !_script.ammoTextFormat.Contains("{1}") || !_script.ammoTextFormat.Contains("{2}"))
			EditorGUILayout.HelpBox("Формат должен содержать элементы {0}(текущие патроны), {1}(максимальное количество патрон в магазине), {2}(запас патрон)", MessageType.Error);
		if (GUILayout.Button("Выстрелить"))
            _script.Shoot();
        if (GUILayout.Button("Перезарядиться"))
            _script.Reload();
		
		EditorGUILayout.Space(10);
		_ammoToAdd = EditorGUILayout.IntField("Кол-во патронов для выдачи", _ammoToAdd);
		if (GUILayout.Button("Выдать патроны"))
			_script.AddAmmoStock(_ammoToAdd);
    }
}