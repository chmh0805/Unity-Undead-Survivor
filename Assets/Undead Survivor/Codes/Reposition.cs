using UnityEngine;

public class Reposition : MonoBehaviour
{
    Collider2D coll;

    private void Awake()
    {
        coll = GetComponent<Collider2D>();
    }

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
                else if (diffX < diffY)
                {
                    transform.Translate(Vector3.up * dirY * 40);
                }
                break;
            case "Enemy":
                if (coll.enabled)
                {
                    transform.Translate(playerDirection * 20 + new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0));
                }
                break;

        }
    }
}
