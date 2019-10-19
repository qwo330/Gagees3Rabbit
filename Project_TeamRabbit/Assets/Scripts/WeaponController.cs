using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] PlayerController player;
    [SerializeField] Transform armTr;

    [Header("Weapon")]
    [SerializeField] Gun currWeapon;
    float fireCoolTime = 0;

    Vector3 leftDir, rightDir;

    private void Awake()
    {
        Vector3 playerScale = player.transform.localScale;
        leftDir = new Vector3(-1 * playerScale.x, playerScale.y, 1);
        rightDir = new Vector3(playerScale.x, playerScale.y, 1);
    }

    void Start()
    {
        StartCoroutine(IE_WeaponProcess());
    }

    public void Set_CurrentWeapon(GunType gunCode)
    {
        switch (gunCode)
        {
            case GunType.Revolver:
                currWeapon = player.Get_ListWeapon(0);
                player.SetActive_Weapon(0);
                break;
            case GunType.Carbine:
                currWeapon = player.Get_ListWeapon(1);
                player.SetActive_Weapon(1);
                break;
            case GunType.SniperRifle:
                currWeapon = player.Get_ListWeapon(3);
                player.SetActive_Weapon(3);
                break;
            case GunType.Shotgun:
                currWeapon = player.Get_ListWeapon(2);
                player.SetActive_Weapon(2);
                break;
        }
        GameManager.Get.ChangeWeapon(gunCode);
    }

    IEnumerator IE_WeaponProcess()
    {
        while (true)
        {
            Vector3 mouseScreenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector3 lookAt = mouseScreenPosition;

            float AngleRad = Mathf.Atan2(lookAt.y - transform.position.y, lookAt.x - transform.position.x);

            float AngleDeg = (180 / Mathf.PI) * AngleRad;

            transform.rotation = Quaternion.Euler(0, 0, AngleDeg);

            //마우스 위치에 따른 무기 및 팔 방향 보정
            if (mouseScreenPosition.x - player.transform.position.x > 0)
            {
                player.transform.localScale = rightDir;
                armTr.localEulerAngles = new Vector3(0, 0, 0);
            }
            else
            {
                player.transform.localScale = leftDir;
                armTr.localEulerAngles = new Vector3(0, 0, 180);
            }

            Fire_Ready();
            if (fireCoolTime > 0) fireCoolTime -= Time.deltaTime;

            yield return null;
        }
    }

    void Fire_Ready()
    {
        switch (currWeapon.Type)
        {
            case GunType.Revolver:
                if(Input.GetMouseButtonDown(0))
                {
                    Fire();
                }
                break;
            case GunType.Shotgun:
                if (Input.GetMouseButton(0))
                {
                    if (fireCoolTime <= 0)
                    {
                        Fire_ShotGun();
                        fireCoolTime = currWeapon.Delay;
                    }
                }
                break;
            default:
                if(Input.GetMouseButton(0))
                {
                    if(fireCoolTime <= 0)
                    {
                        Fire();
                        fireCoolTime = currWeapon.Delay;
                    }
                }
                break;
        }
    }

    void Fire()
    {
        Vector2 dir = currWeapon.MuzzleEnd.position - currWeapon.MuzzleStart.position;
        dir.Normalize();

        GameObject bullet = ObjectPool.Get.GetObject(currWeapon.Get_BulletName());
        bullet.GetComponent<Bullet>().Set_Damage(currWeapon.Demage);
        bullet.transform.position = currWeapon.MuzzleStart.position;
        bullet.transform.rotation = transform.rotation;
        bullet.SetActive(true);

        bullet.GetComponent<Rigidbody2D>().AddForce(dir * currWeapon.BulletSpeed, ForceMode2D.Impulse);
    }

    void Fire_ShotGun()
    {

    }
}
