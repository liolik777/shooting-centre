using UnityEngine;
using Valve.VR;

[CreateAssetMenu(fileName = "GameSettings", menuName = "Игра/Создать новые настройки игры")]
public class GameSettingsInfo : ScriptableObject
{
    public ParticleSystem shootingEffect;
    public SteamVR_Action_Boolean fireAction;
    public SteamVR_Action_Boolean reloadAction;
    public GameObject dentPrefab;
    public KeyCode fireButton;
    public KeyCode reloadButton;
    public bool isControllerInput;
}

