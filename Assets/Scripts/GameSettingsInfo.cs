using UnityEngine;
using Valve.VR;

[CreateAssetMenu(fileName = "GameSettings", menuName = "Игра/Создать новые настройки игры")]
public class GameSettingsInfo : ScriptableObject
{
    public ParticleSystem shootingEffect;
    public SteamVR_Action_Boolean fireAction;
    public GameObject dentPrefab;
    public KeyCode fireButton;
    public bool isControllerInput;
	public string weaponFormatText = "";
}

