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
    private int _ammoStock;
    private int _ammo;

    private void Update()
    {
        if (GameSettings.Instance.gameSettings.isControllerInput)
        {
            if (GameSettings.Instance.gameSettings.fireAction.GetStateDown(SteamVR_Input_Sources.Any))
                StartShoot();
            if (GameSettings.Instance.gameSettings.reloadAction.GetStateDown(SteamVR_Input_Sources.Any))
                Reload();
        }
        else
        {
            if (Input.GetKeyDown(GameSettings.Instance.gameSettings.fireButton))
                StartShoot();
            if (Input.GetKeyDown(GameSettings.Instance.gameSettings.reloadButton))
                Reload();
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
        if (ammoText == null)
            return;

        ammoText.text = string.Format(ammoTextFormat, _ammo, weaponSettings.maxAmmo, _ammoStock);
    }

	public void AddAmmoStock(int ammoToAdd)
	{
		_ammoStock += ammoToAdd;
	}

    [ContextMenu("Выстрелить")]
    public void Shoot()
    {
        if (weaponSettings == null || _ammo <= 0)
            return;

        _ammo--;
        UpdateUI();
        if (GameSettings.Instance.gameSettings.shootingEffect != null)
            Instantiate(GameSettings.Instance.gameSettings.shootingEffect, firePoint.position, firePoint.rotation);
        RaycastHit hit;
        if (Physics.Raycast(firePoint.position, firePoint.position + firePoint.forward, out hit, weaponSettings.shootingRange, 3))
        {
            Instantiate(GameSettings.Instance.gameSettings.dentPrefab, hit.point, Quaternion.LookRotation(hit.normal));
			int score = hit.gameObject.GetComponent<Target>().GetScore(hit.point);
			Debug.Log(score);
        }
    }

    [ContextMenu("Перезарядиться")]
    public void Reload()
    {
        if (_ammoStock <= 0)
            return;

        if (_ammo + _ammoStock >= weaponSettings.maxAmmo)
        {
            _ammoStock -= weaponSettings.maxAmmo - _ammo;
            _ammo = weaponSettings.maxAmmo;
        }
        else
        {
            _ammoStock = 0;
            _ammo += _ammoStock;
        }
        UpdateUI();
    }

    public void OnDrawGizmosSelected()
    {
        if (weaponSettings != null && firePoint != null)
            Debug.DrawRay(firePoint.position, firePoint.position + firePoint.forward, Color.red);
    }
}