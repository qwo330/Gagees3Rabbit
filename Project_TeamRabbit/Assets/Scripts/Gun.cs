using UnityEngine;

public enum GunType
{
    Revolver = 1,
    Carbine = 2,
    SniperRifle = 3,
    Shotgun = 4,
    Arrow = 5,
}

[System.Serializable]
public class Gun : MonoBehaviour
{
    public float Demage;
    public float Speed; // 공격딜레이
    public int Range;
    public bool IsSingleShot; // 단발:t, 연발:f
    public int GetBullet; // 획득탄수
    public int AddBullet; // 추가탄수
    public float DropPercent;

    public GunType Type;
    public Transform MuzzlePos;
}

//public class Revolver : Gun
//{
//    public void SetData()
//    {
//        Demage = 1;
//        Speed = 1;
//        Range = 1;
//        IsSingleShot = true;
//        GetBullet = 0;
//        AddBullet = 0;
//        DropPercent = 0;

//        Type = GunType.Revolver;
//    }
//}

//public class Carbine : Gun
//{
//    public void SetData()
//    {
//        Demage = 2;
//        Speed = 0.25f;
//        Range = 1;
//        IsSingleShot = false;
//        GetBullet = 200;
//        AddBullet = 100;
//        DropPercent = .1f;

//        Type = GunType.Carbine;
//    }
//}

//public class SniperRifle : Gun
//{
//    public void SetData()
//    {
//        Demage = 99999;
//        Speed = 1;
//        Range = 1;
//        IsSingleShot = true;
//        GetBullet = 10;
//        AddBullet = 4;
//        DropPercent = 0.015f;

//        Type = GunType.SniperRifle;
//    }
//}

//public class Shotgun : Gun
//{
//    public void SetData()
//    {
//        Demage = 1;
//        Speed = 1;
//        Range = 5;
//        IsSingleShot = true;
//        GetBullet = 20;
//        AddBullet = 10;
//        DropPercent = 0.07f;

//        Type = GunType.Shotgun;
//    }
//}

//public class Arrow : Gun
//{
//    public void SetData()
//    {
//        Demage = 10;
//        Speed = 1;
//        Range = 1;
//        IsSingleShot = true;
//        GetBullet = 10;
//        AddBullet = 4;
//        DropPercent = 0.06f;

//        Type = GunType.Arrow;
//    }
//}