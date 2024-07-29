using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    public float speed;
    private float dirY;
    private float boundary = 3f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMove();
    }

    private void UpdateMove()
    {
        dirY = Input.GetAxis("Vertical");
        rb.velocity = new Vector2(0, dirY * speed);
        if(this.transform.localPosition.y >= boundary)
        {
            if(dirY >= 0)
            {
                rb.velocity = Vector2.zero;
            }
        }
        else if(this.transform.localPosition.y <= -boundary)
        {
            if(dirY <= 0)
            {
                rb.velocity = Vector2.zero;
            }
        }

        if(dirY != 0)
        {
            anim.SetBool("isRun", true);
        }
        else
        {
            anim.SetBool("isRun", false);
        }
    }
}
