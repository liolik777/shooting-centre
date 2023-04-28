using UnityEngine;

public class GameSettings : MonoBehaviour
{
    public static GameSettings Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameObject().AddComponent<GameSettings>();
                _instance.gameSettings = Resources.Load<GameSettingsInfo>("GameSettings");
                DontDestroyOnLoad(_instance.gameObject);
            }
            return _instance;
        }
    }

    private static GameSettings _instance;
    public GameSettingsInfo gameSettings;
}