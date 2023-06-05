using System.Collections;
using UnityEngine;
using Valve.VR;
using TMPro;

public enum ShootingType
{
    Single,
    Auto
}

public class Weapon : MonoBehaviour
{
    public WeaponSettings weaponSettings;
    public Transform firePoint;
    public TMP_Text ammoText;
    public string ammoTextFormat;
    private Clip _clip;

	public  void InjectClip(Clip clip)
	{
		_clip = clip;
	}

	public  void EjectClip()
	{
		_clip = null;
	}

    private void Update()
    {
        if (GameSettings.Instance.gameSettings.isControllerInput)
        {
            if (GameSettings.Instance.gameSettings.fireAction.GetStateDown(SteamVR_Input_Sources.Any))
                StartShoot();
        }
        else
        {
            if (Input.GetKeyDown(GameSettings.Instance.gameSettings.fireButton))
                StartShoot();
        }

        void StartShoot()
        {
            if (weaponSettings.shootingType == ShootingType.Single)
                Shoot();
            else
                StartCoroutine(StartShooting());
        }
    }

    private IEnumerator StartShooting()
    {
        while (GameSettings.Instance.gameSettings.fireAction.GetState(SteamVR_Input_Sources.Any) || Input.GetKey(GameSettings.Instance.gameSettings.fireButton))
        {
            Shoot();
            yield return new WaitForSeconds(weaponSettings.shootingDelay);
        }
    }

    private void UpdateUI()
    {
        if (ammoText == null || _clip == null)
            return;

        ammoText.text = string.Format(ammoTextFormat, _clip.Ammo, _clip.MaxAmmo);
    }

    [ContextMenu("Выстрелить")]
    public void Shoot()
    {
        if (weaponSettings == null || _clip == null)
            return;
		if (_clip.Ammo <= 0)
			return;

        _clip.Ammo--;
        UpdateUI();
        if (GameSettings.Instance.gameSettings.shootingEffect != null)
			Instantiate(GameSettings.Instance.gameSettings.shootingEffect, firePoint.position, firePoint.rotation);
        RaycastHit hit;
        if (Physics.Raycast(firePoint.position, firePoint.forward, out hit, weaponSettings.shootingRange))
        {
			if (hit.collider.CompareTag("Target"))
			{
				Instantiate(GameSettings.Instance.gameSettings.dentPrefab, hit.point, Quaternion.LookRotation(hit.normal)).transform.SetParent(hit.collider.transform);
				int score = hit.collider.gameObject.GetComponent<Target>().GetScore(hit.point);
				Debug.Log("Your score is: " + score);
			}
        }
    }

    public void OnDrawGizmos()
    {
        if (weaponSettings != null && firePoint != null)
            Debug.DrawRay(firePoint.position, firePoint.forward * weaponSettings.shootingRange, Color.red);
    }
}