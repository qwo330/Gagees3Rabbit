using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class namPlayerController : MonoBehaviour
{
    [SerializeField] namGroundCheck gCheck;

    Rigidbody2D rigi2d;

    bool isGround = false;

    Vector2 dir;
    Vector2 velocity;
    float v, h;
    [SerializeField] float speed = 5f;
    [SerializeField] float jumpVelocity = 3f;

    private void Awake()
    {
        rigi2d = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        StartCoroutine(IE_PlayerController());
    }

    IEnumerator IE_PlayerController()
    {
        while(true)
        {
            h = Input.GetAxis("Horizontal");
            
            if(gCheck.isGround)
            {
                if(Input.GetKeyDown(KeyCode.Space))
                {
                    rigi2d.AddForce(Vector2.up * jumpVelocity, ForceMode2D.Impulse);
                }
            }

            velocity = new Vector2(h, rigi2d.velocity.y);

            dir = new Vector2(velocity.x * speed, velocity.y);

            transform.Translate(dir * Time.deltaTime);

            yield return null;
        }
    }

    //Ground Check
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer.Equals(LayerMask.NameToLayer("Ground")))
        {
            isGround = true;
            v = 0;
        }
    }
}
