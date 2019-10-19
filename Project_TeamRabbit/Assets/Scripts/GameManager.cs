using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Fields
    public PlayerController player;

    [SerializeField]
    Text txtScore, txtKillCount, txtDistance;

    [Header("UI")]
    WeaponSlot[] slots;

    int distance;
    int killCount;
    int score;
    #endregion

    static GameManager instance;
    public static GameManager Get
    {
        get
        {
            return instance;
        }
    }

    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this);

        ObjectPool.Get.CreateObjectPool();
        ObjectPool.Get.AddObjectPool("Bullet");
        //ObjectPool.Get.AddObjectPool(GunType.Carbine + "Bullet");
        //ObjectPool.Get.AddObjectPool(GunType.SniperRifle + "Bullet");
        //ObjectPool.Get.AddObjectPool(GunType.Shotgun + "Bullet");
        //ObjectPool.Get.AddObjectPool(GunType.Arrow + "Bullet");
        ObjectPool.Get.AddObjectPool("Blood");
    }

    public void ShowRemainBullet()
    {
        for (int i = 0; i < 4; i++)
        {
            Gun gun = player.Get_ListWeapon(i);
            slots[i].ShowRemainBullet(gun.BulletCount);
        }
    }

    public void ChangeWeapon(GunType type)
    {
        foreach (var slot in slots)
            slot.SlotActive(type);
    }

    public void AddScore(int value)
    {
        score += value;
    }

    public void ShowScore()
    {
        txtScore.text = score + "pts";
    }

    public void AddKillCount()
    {
        killCount++;
    }

    public void ShowKillcount()
    {
        txtKillCount.text = killCount.ToString();
    }

    public void ShowDistance()
    {
        txtDistance.text = distance + "m";
    }

    /// <summary>
    /// 에디터에서만 디버그 로그를 찍는다.
    /// </summary>
    public static void WriteLog(object message, Object context = null)
    {
#if UNITY_EDITOR
        Debug.Log(message, context);
#endif
    }
}