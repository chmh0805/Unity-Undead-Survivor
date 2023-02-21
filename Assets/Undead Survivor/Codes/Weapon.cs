using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int id;
    public int prefabId;
    public float damage;
    public int count;
    public float speed;

    private void Start()
    {
        Init();
    }

    void Update()
    {
        switch (id)
        {
            case 0:
                this.transform.Rotate(Vector3.back * speed * Time.deltaTime);
                break;
            default:
                break;
        }

        // for test
        if (Input.GetButtonDown("Jump"))
        {
            LevelUp(30, 5);
        }
    }

    public void LevelUp(float damage, int count)
    {
        this.damage = damage;
        this.count += count;

        if (id == 0)
            Position();
    }

    public void Init()
    {
        switch (id)
        {
            case 0:
                speed = 150;
                Position();
                break;
            default:
                break;
        }
    }

    private void Position()
    {
        for (int index = 0; index < this.count; index++)
        {
            Transform bullet;
            
            if (index < transform.childCount)
            {
                bullet = transform.GetChild(index);
            }
            else
            {
                bullet = GameManager.instance.poolManager.Get(this.prefabId).transform;
                bullet.SetParent(this.transform);
            }

            bullet.localPosition = Vector3.zero; // Location of Player.
            bullet.localRotation = Quaternion.identity;

            Vector3 rotationVector = Vector3.forward * 360 * index / count;
            bullet.Rotate(rotationVector);
            bullet.Translate(bullet.up * 1.25f, Space.World);

            bullet.GetComponent<Bullet>().Init(this.damage, -1); // -1 is Infinity Per.
        }
    }
}
