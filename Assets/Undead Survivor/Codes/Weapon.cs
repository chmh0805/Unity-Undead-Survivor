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

    private float timer;
    private Player player;

    private void Awake()
    {
        this.player = GetComponentInParent<Player>();
    }

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
            case 1:
                timer += Time.deltaTime;
                if (timer > speed)
                {
                    timer = 0f;
                    Fire();
                }
                break;
            default:
                break;
        }

        // for test
        if (Input.GetButtonDown("Jump"))
        {
            LevelUp(10, 1);
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
            case 1:
                speed = 0.3f;
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

            bullet.GetComponent<Bullet>().Init(this.damage, -1, Vector3.zero); // -1 is Infinity Per.
        }
    }

    private void Fire()
    {
        if (!this.player.scanner.nearstTarget)
            return;

        Vector3 targetPosition = this.player.scanner.nearstTarget.position;
        Vector3 dir = (targetPosition - this.transform.position).normalized;

        Transform bullet = GameManager.instance.poolManager.Get(this.prefabId).transform;
        bullet.position = this.transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        bullet.GetComponent<Bullet>().Init(damage, this.count, dir);
    }
}
