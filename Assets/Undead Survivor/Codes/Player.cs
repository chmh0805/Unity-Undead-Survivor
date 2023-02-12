using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    MonoBehaviour: A class that has the things you need to configure your game logic.
*/
public class Player : MonoBehaviour
{
    public Vector2 inputVec;
    public float speed;

    Rigidbody2D rigid;


    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        // 1. Add Force
        // rigid.AddForce(inputVec);

        // 2. Control Speed
        // rigid.velocity = inputVec;

        // 3. Move Location
        Vector2 nextVec = inputVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
    }
}
