using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] GroundCheck _groundCheck;
    [SerializeField] WeaponController _weaponController;
    [SerializeField] GameObject[] _weaponList;

    Rigidbody2D rigi2d;

    bool isGround = false;

    [Header("Parameters")]
    Vector2 dir;
    Vector2 velocity;
    float v, h;
    [SerializeField] float _speed = 5f;
    [SerializeField] float _jumpVelocity = 3f;
    [SerializeField] float _currHP;
    [SerializeField] float _maxHP = 100;

    bool isHit = false;

    bool isInvincible = false;
    float leftTimeInvincible = 0;

    [Header("Effect")]
    [SerializeField] Material playerMat;
    [SerializeField] GameObject shieldEffect;

    private void Awake()
    {
        rigi2d = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        _currHP = _maxHP;
        playerMat.color = new Color(playerMat.color.r, playerMat.color.g, playerMat.color.b, 1);

        _weaponController.Set_CurrentWeapon(GunType.Revolver);

        StartCoroutine(IE_PlayerController());
        StartCoroutine(IE_WeaponSwap());
    }

    public void TakeDamage(float damage)
    {
        if (isInvincible) return;
        if (isHit) return;

        _currHP -= damage;
        if(_currHP <= 0)
        {
            //게임오버
            GameManager.Get.GameOver();

            SoundManager.instance.Play_PlayerDead();
        }
        else
        {
            StartCoroutine(IE_Set_Hit());
        }
    }

    IEnumerator IE_PlayerController()
    {
        while (GameManager.Get.gameState == GameState.Play)
        {
            h = Input.GetAxis("Horizontal");

            if (_groundCheck.isGround)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    rigi2d.AddForce(Vector2.up * _jumpVelocity, ForceMode2D.Impulse);
                }
            }

            velocity = new Vector2(h, rigi2d.velocity.y);

            dir = new Vector2(velocity.x * _speed, velocity.y);

            transform.Translate(dir * Time.deltaTime);

            yield return null;
        }
    }

    IEnumerator IE_WeaponSwap()
    {
        float swapCoolTime = 0;
        float maxCoolTime = 1;
        while(GameManager.Get.gameState == GameState.Play)
        {
            if (swapCoolTime < 0)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    _weaponController.Set_CurrentWeapon(GunType.Revolver);
                    swapCoolTime = maxCoolTime;
                }
                if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    _weaponController.Set_CurrentWeapon(GunType.Carbine);
                    swapCoolTime = maxCoolTime;
                }
                if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    _weaponController.Set_CurrentWeapon(GunType.Shotgun);
                    swapCoolTime = maxCoolTime;
                }
                if (Input.GetKeyDown(KeyCode.Alpha4))
                {
                    _weaponController.Set_CurrentWeapon(GunType.SniperRifle);
                    swapCoolTime = maxCoolTime;
                }
            }
            else swapCoolTime -= Time.deltaTime;

            yield return null;
        }
    }

    public void SetActive_Weapon(int listIndex)
    {
        for(int i = 0; i < _weaponList.Length; i++)
        {
            if (i == listIndex)
            {
                _weaponList[i].SetActive(true);
            }
            else _weaponList[i].SetActive(false);
        }
    }

    public Gun Get_ListWeapon(int listIndex)
    {
        return _weaponList[listIndex].GetComponent<Gun>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer.Equals(LayerMask.NameToLayer("Item")))
        {
            GameObject effect = null;

            SoundManager.instance.Play_ItemDrop();

            switch(other.gameObject.name)
            {
                case "Item_HealPack":
                    if (_currHP <= 0) return;
                    _currHP += 10;
                    if (_currHP > _maxHP) _currHP = _maxHP;
                    effect = ObjectPool.Get.GetObject("HealEffect");
                    effect.transform.position = transform.position;
                    effect.SetActive(true);
                    break;
                case "Item_Shield":
                    if (isInvincible) leftTimeInvincible = 5;
                    else StartCoroutine(IE_Set_Invincible()); 
                    break;
            }

            other.gameObject.SetActive(false);
        }
    }

    IEnumerator IE_Set_Invincible()
    {
        isInvincible = true;
        leftTimeInvincible = 5;
        shieldEffect.SetActive(true);

        while (leftTimeInvincible >= 0)
        {
            leftTimeInvincible -= Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator IE_Set_Hit()
    {
        isHit = true;
        SoundManager.instance.Play_PlayerHit();

        playerMat.color = new Color(playerMat.color.r, playerMat.color.g, playerMat.color.b, 0.5f);

        if (transform.localScale.x < 0) transform.eulerAngles = new Vector3(0, 0, -10);
        else transform.eulerAngles = new Vector3(0, 0, 10);

        yield return new WaitForSeconds(0.1f);
        transform.eulerAngles = new Vector3(0, 0, 0);
        playerMat.color = new Color(playerMat.color.r, playerMat.color.g, playerMat.color.b, 1);
        isHit = false;
    }
}

