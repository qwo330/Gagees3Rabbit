using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class namGroundCheck : MonoBehaviour
{
    public bool isGround = false;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.layer.Equals(LayerMask.NameToLayer("Ground")))
        {
            isGround = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer.Equals(LayerMask.NameToLayer("Ground")))
        {
            isGround = false;
        }
    }
}
