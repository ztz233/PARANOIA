using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public BulletPool pool;
    public BulletObj bullet;
    private float currentAngle = 0f;
    private float currentAngleVelocity = 0f;
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
        currentAngle = bullet.initRotation;
        currentAngleVelocity = bullet.angularVelocity;
    }

    private void FixedUpdate()
    {
        if(timeCounter < initTime )
        {
            timeCounter += Time.fixedDeltaTime;
            return;
        }
        else
        {
            initTime = 0f;
        }//set first shoot time

        if(cdCounter < CoolDown)
        {
            cdCounter += Time.fixedDeltaTime;
            return;
        }
        
        if(continueCounter < continueTime)
        {
            continueCounter += Time.fixedDeltaTime;
        }
        else
        {
            continueCounter = 0f;
            cdCounter = 0f;
        }

        //currentAngleVelocity += bullet.angularAccelerate * Time.fixedDeltaTime;
        if(currentAngleVelocity > bullet.angularVMax)
        {
            currentAngleVelocity = bullet.angularVMax;
        }

        currentAngle = currentAngleVelocity * Time.fixedDeltaTime;
        if(Mathf.Abs(currentAngle) > 720f)
        {
            currentAngle -= Mathf.Sign(currentAngle) * 360f;
        }

        timeCounter += Time.fixedDeltaTime;

        if(timeCounter > bullet.CoolDown)
        {
            timeCounter -= bullet.CoolDown;
            ShootbyCount(bullet.count, currentAngle);
        }
    }

    public void Shooting(float angle)
    {
        var go = pool.GetItem();
        go.transform.position = transform.position;
        go.transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void ShootbyCount(int count, float angle)
    {
        float tmpAngle = (count % 2 == 0) ? angle / 2 : angle;

        for(int i = 0; i < count; ++i)
        {
            tmpAngle += Mathf.Pow(-1, i) * i * bullet.lineAngle;
            Shooting(tmpAngle);
        }
    }
}
