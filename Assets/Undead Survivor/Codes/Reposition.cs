using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reposition : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area"))
            return;

        Vector3 playerPosition = GameManager.instance.player.transform.position;
        Vector3 thisPosition = transform.position;

        float diffX = Mathf.Abs(playerPosition.x - thisPosition.x);
        float diffY = Mathf.Abs(playerPosition.y - thisPosition.y);

        Vector3 playerDirection = GameManager.instance.player.inputVec;
        float dirX = playerDirection.x < 0 ? -1 : 1;
        float dirY = playerDirection.y < 0 ? -1 : 1;

        switch (transform.tag)
        {
            case "Ground":
                if (diffX > diffY)
                {
                    transform.Translate(Vector3.right * dirX * 40);
                }
                else if (diffY > diffX)
                {
                    transform.Translate(Vector3.up * dirY * 40);
                }
                break;
            case "Enemy":

                break;

        }
    }
}
