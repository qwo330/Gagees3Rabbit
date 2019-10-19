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

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer.Equals(LayerMask.NameToLayer("Monster")))
        {
            other.GetComponent<Monster>().TakeDamage(_damage);
            gameObject.SetActive(false);
        }
    }
}
