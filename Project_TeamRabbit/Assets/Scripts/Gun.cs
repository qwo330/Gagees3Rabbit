
public enum GunType
{
    Revolver = 1,
    Carbine = 2,
    SniperRifle = 3,
    Shotgun = 4,
    Arrow = 5,
}

public class Gun
{
    public float demage;
    public float speed; // 공격딜레이
    public int range;
    public bool isSingleShot; // 단발:t, 연발:f
    public int getBullet; // 획득탄수
    public int addBullet; // 추가탄수
    public float dropPercent;
}

public class Revolver : Gun
{
    public Revolver()
    {
        demage = 1;
        speed = 1;
        range = 1;
        isSingleShot = true;
        getBullet = 0;
        addBullet = 0;
        dropPercent = 0;
    }
}

public class Carbine : Gun
{
    public Carbine()
    {
        demage = 2;
        speed = 0.25f;
        range = 1;
        isSingleShot = false;
        getBullet = 200;
        addBullet = 100;
        dropPercent = .1f;
    }
}

public class SniperRifle : Gun
{
    public SniperRifle()
    {
        demage = 99999;
        speed = 1;
        range = 1;
        isSingleShot = true;
        getBullet = 10;
        addBullet = 4;
        dropPercent = 0.015f;
    }
}

public class Shotgun : Gun
{
    public Shotgun()
    {
        demage = 1;
        speed = 1;
        range = 5;
        isSingleShot = true;
        getBullet = 20;
        addBullet = 10;
        dropPercent = 0.07f;
    }
}

public class Arrow : Gun
{
    public Arrow()
    {
        demage = 10;
        speed = 1;
        range = 1;
        isSingleShot = true;
        getBullet = 10;
        addBullet = 4;
        dropPercent = 0.06f;
    }
}
