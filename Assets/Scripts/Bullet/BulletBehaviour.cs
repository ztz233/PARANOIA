using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public float linearSpeed;
    public float angularSpeed;
    public float linearAccelerate;
    public float angularAccelerate;
    public int damage;

    public float mx = 20f;
    public float lifeCycle = 11f;
    private Rigidbody2D rb;
    public BulletPool pool;
    private Animator anim;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        /*
        linearSpeed += linearAccelerate * Time.fixedDeltaTime;
        if(linearSpeed >= mx)
        {
            linearSpeed = mx;
        }
        else if(linearSpeed <= mx)
        {
            linearSpeed = -mx;
        }
        angularSpeed += angularAccelerate * Time.fixedDeltaTime;
        */

        this.transform.Translate(linearSpeed * Vector2.right * Time.fixedDeltaTime);
        //this.transform.rotation *= Quaternion.Euler(new Vector2(1, 0) * angularAccelerate * Time.fixedDeltaTime);

        if(this.transform.localPosition.x >= lifeCycle ||
            this.transform.localPosition.x <= -lifeCycle ||
            this.transform.localPosition.y >= lifeCycle || 
            this.transform.localPosition.y <= -lifeCycle)
        {
            //pool.RealseItem(this);
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            anim.SetTrigger("hit");
            rb.velocity = Vector2.zero;
            Invoke("Delete", 1f);
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            anim.SetTrigger("hit");
            rb.velocity = Vector2.zero;
            Invoke("Delete", 1f);
        }
    }

    private void Delete()
    {
        Destroy(this.gameObject);
    }

    public int GetDamage()
    {
        return damage;
    }
}
