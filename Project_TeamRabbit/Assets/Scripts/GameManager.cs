using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameState
{
    Play,
    Clear,
    Fail,
    Pause,
}

public class GameManager : MonoBehaviour
{
    #region Fields
    public PlayerController player;
    public GameState gameState = GameState.Play;
    public float swapCoolTime = 1f;

    [SerializeField]
    Text txtScore, txtKillCount, txtDistance;

    [Header("UI")]
    [SerializeField] GameObject titleUI;
    [SerializeField] GameObject resultUI;
    [SerializeField]
    Image imgLife;

    [SerializeField]
    Image bgLife;

    [SerializeField]
    Vector2 life_Offset = new Vector2(2, -12.4f);

    [SerializeField]
    WeaponSlot[] slots;

    public bool swapCool = false;
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
        ObjectPool.Get.AddObjectPool("ShotGun");
        ObjectPool.Get.AddObjectPool("GroundHit");
        ObjectPool.Get.AddObjectPool("ZombieHit");
        ObjectPool.Get.AddObjectPool("HealEffect");
        ObjectPool.Get.AddObjectPool("ShieldBlue");
        ObjectPool.Get.AddObjectPool("Zombie");
        ObjectPool.Get.AddObjectPool("ZombieThrow");
        ObjectPool.Get.AddObjectPool("Zombie_Bone");
    }

    void Update()
    {
        ShowLife();
    }

    public void ShowLife()
    {
        Vector2 pos = player.transform.position;
        bgLife.transform.position = pos + life_Offset;
        // 임시 세팅
        imgLife.fillAmount = 0.7f;
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
        if (swapCool)
            return;

        foreach (var slot in slots)
            slot.SlotActive(type);
    }

    public void AddScore(int value)
    {
        score += value;
    }

    public void ShowScore()
    {
        txtScore.text = score.ToString();
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
        txtDistance.text = distance.ToString();
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