using System.Collections;
using UnityEngine;
using Valve.VR;

public enum ShootingType
{
    Single,
    Auto
}

public class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponSettings weaponSettings;
    [SerializeField] private Transform firePoint;

    private void Update()
    {
        if (GameSettings.Instance.gameSettings.fireAction.GetStateDown(SteamVR_Input_Sources.Any) || Input.GetKeyDown(GameSettings.Instance.gameSettings.fireButton))
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

    [ContextMenu("Выстрелить")]
    private void Shoot()
    {
        if (weaponSettings == null)
            return;

        if (GameSettings.Instance.gameSettings.shootingEffect != null)
            Instantiate(GameSettings.Instance.gameSettings.shootingEffect, firePoint.position, firePoint.rotation);
        RaycastHit hit;
        if (Physics.Raycast(firePoint.position, firePoint.position + firePoint.forward, out hit, weaponSettings.shootingRange, 3))
        {
            Instantiate(GameSettings.Instance.gameSettings.dentPrefab, hit.point, Quaternion.LookRotation(hit.normal));
        }
    }

    public void OnDrawGizmosSelected()
    {
        if (weaponSettings != null && firePoint != null)
            Debug.DrawRay(firePoint.position, firePoint.position + firePoint.forward, Color.red);
    }
}