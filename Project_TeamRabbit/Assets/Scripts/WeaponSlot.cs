using System.Collections;
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
        bool isActive = this.type == type;

        if (isActive == true)
        {
            StartCoroutine(PlayCoolTimeEffect());
        }
        else
        {
            imgDisable.fillAmount = 1;
        }
    }

    IEnumerator PlayCoolTimeEffect()
    {
        GameManager.Get.swapCool = true;

        float maxFrame = 60 * GameManager.Get.swapCoolTime;
        float frame = maxFrame;

        while (frame > 0)
        {
            frame--;
            imgDisable.fillAmount = (frame / maxFrame);
            yield return new WaitForEndOfFrame();
        }

        GameManager.Get.swapCool = false;
    }
}
