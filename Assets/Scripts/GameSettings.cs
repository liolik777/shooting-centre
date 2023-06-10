using UnityEngine;

public class GameSettings : MonoBehaviour
{
	public static GameSettingsInfo Settings
	{
		get
		{
			if (_settings == null)
				_settings = Resources.Load<GameSettingsInfo>("GameSettings");
			return _settings;
		}
	}
    private static GameSettingsInfo _settings;
}