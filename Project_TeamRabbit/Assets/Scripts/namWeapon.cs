using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class namWeapon : MonoBehaviour
{
    [SerializeField] namPlayerController player;
    [SerializeField] Transform weaponDir;

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

            if(!(transform.eulerAngles.z < 90 && transform.eulerAngles.z > -90))
            {
                weaponDir.localEulerAngles = new Vector3(0, 0, 180);
                weaponDir.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                weaponDir.localEulerAngles = new Vector3(0, 0, 0);
                weaponDir.localScale = new Vector3(1, 1, 1);
            }

            if(Input.GetMouseButtonDown(0))
            {
                Fire();
            }

            yield return null;
        }
    }

    void Fire()
    {
        
    }
}
