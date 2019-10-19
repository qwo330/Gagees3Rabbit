using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSlot : MonoBehaviour
{
    [SerializeField]
    Text txtBulletCount;

    [SerializeField]
    GunType type;

    [SerializeField]
    GameObject goDisable;

    public void ShowRemainBullet()
    {
        // todo: player에서 각 무기의 남은 탄수 획득
        //int index = (int)type;
        //txtBulletCount.text = 
    }

    public void SlotActive(GunType type)
    {
        goDisable.SetActive(this.type != type);
    }
}
