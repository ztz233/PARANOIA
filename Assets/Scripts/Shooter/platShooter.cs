using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platShooter : MonoBehaviour
{
    public BulletPool pool;
    public BulletObj bullet;
    private float currentPos = 0f;
    private float currentLinearVelocity = 0f;
    private float timeCounter = 0f;

    private float initTime;
    public float continueTime;
    public float CoolDown;
    private float continueCounter = 0f;
    private float cdCounter = 0f;

    private void Awake()
    {
        initTime = Random.value % CoolDown;
        pool = new BulletPool();
        pool.bullet = bullet;
        currentPos = this.transform.position.y;
        currentLinearVelocity = bullet.linearSpeed;
    }

    private void FixedUpdate()
    {
        if (timeCounter < initTime)
        {
            timeCounter += Time.fixedDeltaTime;
            return;
        }
        else
        {
            initTime = 0f;
        }//set first shoot time

        if (cdCounter < CoolDown)
        {
            cdCounter += Time.fixedDeltaTime;
            return;
        }

        if (continueCounter < continueTime)
        {
            continueCounter += Time.fixedDeltaTime;
        }
        else
        {
            continueCounter = 0f;
            cdCounter = 0f;
        }

        //currentAngleVelocity += bullet.angularAccelerate * Time.fixedDeltaTime;
        if (currentLinearVelocity > bullet.mx)
        {
            currentLinearVelocity = bullet.mx;
        }

        currentPos = currentLinearVelocity * Time.fixedDeltaTime;
        if (Mathf.Abs(currentPos) > 720f)
        {
            currentPos -= Mathf.Sign(currentPos);
        }

        timeCounter += Time.fixedDeltaTime;

        if (timeCounter > bullet.CoolDown)
        {
            timeCounter -= bullet.CoolDown;
            ShootbyCount(bullet.count, currentPos);
        }
    }

    public void Shooting(float pos)
    {
        var go = pool.GetItem();
        go.transform.position = new Vector2(transform.position.x, transform.position.y + pos);
    }

    private void ShootbyCount(int count, float pos)
    {
        float tmpAngle = (count % 2 == 0) ? pos / 2 : pos;

        for (int i = 0; i < count; ++i)
        {
            tmpAngle += Mathf.Pow(-1, i) * i * 1.5f;
            Shooting(tmpAngle);
        }
    }
}
