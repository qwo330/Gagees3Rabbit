using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGunCollider : MonoBehaviour
{
    public List<Monster> hitMonsters = new List<Monster>();

    public void Clear_List()
    {
        hitMonsters.Clear();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer.Equals(LayerMask.NameToLayer("Monster")))
        {
            if(!hitMonsters.Contains(other.GetComponent<Monster>()))
            {
                hitMonsters.Add(other.GetComponent<Monster>());
            }
        }
    }
}
