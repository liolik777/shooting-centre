using UnityEditor;

[CustomEditor(typeof(WeaponSettings))]
public class WeaponSettingsEditor : Editor
{
    private WeaponSettings _script;

    private void OnEnable() => _script = (WeaponSettings)target;

    public override void OnInspectorGUI()
    {
        _script.shootingType = (ShootingType)EditorGUILayout.EnumPopup("Тип стрельбы", _script.shootingType);
        if (_script.shootingType == ShootingType.Auto)
            _script.shootingDelay = EditorGUILayout.Slider("Задержка между выстрелами", _script.shootingDelay, 0.01f, 0.3f);
        _script.shootingRange = EditorGUILayout.IntSlider("Расстояние выстрела", _script.shootingRange, 50, 500);
    }
}