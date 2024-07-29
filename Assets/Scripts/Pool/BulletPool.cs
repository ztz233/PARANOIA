using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BulletPool
{
    public BulletObj bullet;
    private ObjectPool<BulletBehaviour> pool;

    public BulletBehaviour GetItem()
    {
        return pool.Get();
    }

    public void RealseItem(BulletBehaviour bh)
    {
        pool.Release(bh);
    }

    public BulletPool()
    {
        pool = new ObjectPool<BulletBehaviour>(OnCreateItem, OnGetItem, OnReleaseItem, OnDestoryItem);
    }

    private BulletBehaviour OnCreateItem()
    {
        var bh = GameObject.Instantiate(bullet.prefab).AddComponent<BulletBehaviour>();
        initBullet(bh);
        bh.pool = this;

        return bh;
    }

    private void initBullet(BulletBehaviour bh)
    {
        bh.linearSpeed = bullet.linearSpeed;
        bh.linearAccelerate = bullet.linearAccelerate;
        bh.angularSpeed = bullet.angularSpeed;
        bh.angularAccelerate = bullet.angularSpeed;
        bh.mx = bullet.mx;
    }

    private void OnDestoryItem(BulletBehaviour bh)
    {
        GameObject.Destroy(bh.gameObject);
    }

    private void OnReleaseItem(BulletBehaviour bh)
    {
        bh.gameObject.SetActive(false);
    }

    private void OnGetItem(BulletBehaviour bh)
    {
        initBullet(bh);
        bh.gameObject.SetActive(true);
    }
}
