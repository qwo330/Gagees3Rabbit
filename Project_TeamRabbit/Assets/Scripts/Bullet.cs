using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float _damage = 0f;

    public void Set_Damage(float damage)
    {
        _damage = damage;
    }

    public float Get_Damage() { return _damage; }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer.Equals(LayerMask.NameToLayer("Monster")))
        {
            other.GetComponent<Monster>().TakeDamage(_damage);

            //GameObject effect = ObjectPool.Get.GetObject("Blood");
            GameObject effect = ObjectPool.Get.GetObject("ZombieHit");
            effect.transform.position = transform.position;
            effect.SetActive(true);
        }
        else
        {
            GameObject effect = ObjectPool.Get.GetObject("GroundHit");
            effect.transform.position = transform.position;
            effect.SetActive(true);
        }

        ObjectPool.Get.ReturnObject(gameObject);
        gameObject.SetActive(false);
    }
}
