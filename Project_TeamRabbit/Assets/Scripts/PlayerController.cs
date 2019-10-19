﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] GroundCheck _groundCheck;
    [SerializeField] WeaponController _weaponController;
    [SerializeField] GameObject[] _weaponList;

    Rigidbody2D rigi2d;

    bool isGround = false;

    [Header("Parameters")]
    Vector2 dir;
    Vector2 velocity;
    float v, h;
    [SerializeField] float _speed = 5f;
    [SerializeField] float _jumpVelocity = 3f;
    [SerializeField] float _currHP;
    [SerializeField] float _maxHP = 100;

    private void Awake()
    {
        rigi2d = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        _weaponController.Set_CurrentWeapon(GunType.Revolver);

        StartCoroutine(IE_PlayerController());
        StartCoroutine(IE_WeaponSwap());
    }

    IEnumerator IE_PlayerController()
    {
        while (true)
        {
            h = Input.GetAxis("Horizontal");

            if (_groundCheck.isGround)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    rigi2d.AddForce(Vector2.up * _jumpVelocity, ForceMode2D.Impulse);
                }
            }

            velocity = new Vector2(h, rigi2d.velocity.y);

            dir = new Vector2(velocity.x * _speed, velocity.y);

            transform.Translate(dir * Time.deltaTime);

            yield return null;
        }
    }

    IEnumerator IE_WeaponSwap()
    {
        float swapCoolTime = 0;
        float maxCoolTime = 1;
        while(true)
        {
            if (swapCoolTime < 0)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    _weaponController.Set_CurrentWeapon(GunType.Revolver);
                    swapCoolTime = maxCoolTime;
                }
                if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    _weaponController.Set_CurrentWeapon(GunType.Carbine);
                    swapCoolTime = maxCoolTime;
                }
                if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    _weaponController.Set_CurrentWeapon(GunType.Shotgun);
                    swapCoolTime = maxCoolTime;
                }
                if (Input.GetKeyDown(KeyCode.Alpha4))
                {
                    _weaponController.Set_CurrentWeapon(GunType.SniperRifle);
                    swapCoolTime = maxCoolTime;
                }
            }
            else swapCoolTime -= Time.deltaTime;

            yield return null;
        }
    }

    public void SetActive_Weapon(int listIndex)
    {
        for(int i = 0; i < _weaponList.Length; i++)
        {
            if (i == listIndex)
            {
                _weaponList[i].SetActive(true);
            }
            else _weaponList[i].SetActive(false);
        }
    }

    public Gun Get_ListWeapon(int listIndex)
    {
        return _weaponList[listIndex].GetComponent<Gun>();
    }
}

