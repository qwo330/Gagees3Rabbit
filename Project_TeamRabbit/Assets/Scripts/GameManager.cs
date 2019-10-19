﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Fields
    public PlayerController player;

    [SerializeField]
    Text txtScore, txtMoney, txtKillCount;

    int money;
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
    }

    void Update()
    {
        if (Input.anyKey)
            SwapWeapon();
    }

    void SwapWeapon()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            WriteLog("권총");
        }
        else if(Input.GetKey(KeyCode.Alpha2))
        {
            WriteLog("카빈");
        }
        else if (Input.GetKey(KeyCode.Alpha3))
        {
            WriteLog("저격");
        }
        else if (Input.GetKey(KeyCode.Alpha4))
        {
            WriteLog("샷건");
        }
        else if (Input.GetKey(KeyCode.Alpha5))
        {
            WriteLog("활");
        }
    }

    public void AddMoney(int value)
    {
        money += value;
    }

    public void ShowMoney()
    {
        txtScore.text = money.ToString();
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
