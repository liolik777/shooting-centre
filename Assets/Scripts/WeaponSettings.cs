using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon Settings", menuName = "Игра/Оружие/Создать новые настройки оружия")]
public class WeaponSettings : ScriptableObject
{
    public ShootingType shootingType;
    public float shootingDelay = 0.1f;
    public int shootingRange = 20;
}