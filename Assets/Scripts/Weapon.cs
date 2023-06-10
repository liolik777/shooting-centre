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
    private Clip _clip;
    private bool _shutterIsDistorted;

	public  void InjectClip(Clip clip)
	{
		_clip = clip;
	}

	public  void EjectClip()
	{
		_clip = null;
	}

    public void DistorteShutter()
    {
        if (_clip == null && _shutterIsDistorted)
            _shutterIsDistorted = false;
        if (_clip != null)
        {
            _shutterIsDistorted = true;
            _clip.Ammo--;
        }
    }
    
    private void Update()
    {
        if (GameSettings.Settings.isControllerInput)
        {
            if (GameSettings.Settings.fireAction.GetStateDown(SteamVR_Input_Sources.Any))
                StartShoot();
        }
        else
        {
            if (Input.GetKeyDown(GameSettings.Settings.fireButton))
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
        while (GameSettings.Settings.fireAction.GetState(SteamVR_Input_Sources.Any) || Input.GetKey(GameSettings.Settings.fireButton))
        {
            Shoot();
            yield return new WaitForSeconds(weaponSettings.shootingDelay);
        }
    }

    private void UpdateUI()
    {
        if (ammoText == null || _clip == null)
            return;

        ammoText.text = string.Format(GameSettings.Settings.weaponFormatText, _clip.Ammo, _clip.MaxAmmo);
    }

    [ContextMenu("Выстрелить")]
    public void Shoot()
    {
        if (_clip == null && _shutterIsDistorted)
        {
            SubShoot();
            _shutterIsDistorted = false;
        }
        
        if (weaponSettings == null || _clip == null)
            return;
		if (_clip.Ammo <= 0 || !_shutterIsDistorted)
			return;

        _clip.Ammo--;
        if (_clip.Ammo == 0)
            _shutterIsDistorted = false;
        SubShoot();

        void SubShoot()
        {
            Debug.Log("Shoot");
            UpdateUI();
            if (GameSettings.Settings.shootingEffect != null)
                Instantiate(GameSettings.Settings.shootingEffect, firePoint.position, firePoint.rotation);
            RaycastHit hit;
            if (Physics.Raycast(firePoint.position, firePoint.forward, out hit, weaponSettings.shootingRange))
            {
                Instantiate(GameSettings.Settings.dentPrefab, hit.point, Quaternion.LookRotation(hit.normal)).transform.SetParent(hit.collider.transform);
                if (hit.collider.CompareTag("Target"))
                {
                    int score = hit.collider.gameObject.GetComponent<Target>().GetScore(hit.point);
                    Debug.Log("Your score is: " + score);
                }
            }
        }
    }

    public void OnDrawGizmos()
    {
        if (weaponSettings != null && firePoint != null)
            Debug.DrawRay(firePoint.position, firePoint.forward * weaponSettings.shootingRange, Color.red);
    }
}