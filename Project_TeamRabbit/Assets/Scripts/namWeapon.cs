using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class namWeapon : MonoBehaviour
{
    [SerializeField] namPlayerController player;

    void Start()
    {
        StartCoroutine(IE_WeaponProcess());
    }

    IEnumerator IE_WeaponProcess()
    {
        while (true)
        {


            yield return null;
        }
    }
}
