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
    Image imgDisable;

    public void ShowRemainBullet(int count)
    {
        txtBulletCount.text = count.ToString();
    }

    public void SlotActive(GunType type)
    {
        imgDisable.gameObject.SetActive(this.type != type);
    }

    void PlayCoolTimeEffect()
    {
        //imgDisable.fillAmount = 
    }
}
