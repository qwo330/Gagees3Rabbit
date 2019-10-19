﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] PlayerController player;
    [SerializeField] Transform armTr;

    [Header("Weapon")]
    [SerializeField] Transform sampleFireStart;
    [SerializeField] Transform sampleFireEnd;
    [SerializeField] GameObject bullet;
    [SerializeField] float bulletSampleSpeed = 50f;

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
            if (!(transform.eulerAngles.z < 90 && transform.eulerAngles.z > -90))
            {
                player.transform.localScale = leftDir;
                armTr.localEulerAngles = new Vector3(0, 0, 180);
            }
            else
            {
                player.transform.localScale = rightDir;
                armTr.localEulerAngles = new Vector3(0, 0, 0);
            }

            if (Input.GetMouseButtonDown(0))
            {
                Fire();
            }

            yield return null;
        }
    }

    void Fire()
    {
        Vector2 dir = sampleFireEnd.position - sampleFireStart.position;
        dir.Normalize();

        GameObject newOne = Instantiate(bullet);
        newOne.transform.position = sampleFireStart.position;
        newOne.transform.rotation = transform.rotation;
        newOne.SetActive(true);

        newOne.GetComponent<Rigidbody2D>().AddForce(dir * bulletSampleSpeed, ForceMode2D.Impulse);
    }
}
