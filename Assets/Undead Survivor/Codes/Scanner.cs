using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    public float scanRange;
    public LayerMask targetLayer;
    public RaycastHit2D[] targets;
    public Transform nearstTarget;

    private void FixedUpdate()
    {
        targets = Physics2D.CircleCastAll(this.transform.position, scanRange, Vector2.zero, 0, targetLayer);
        this.nearstTarget = GetNearstTarget();
    }

    private Transform GetNearstTarget()
    {
        Transform nearst = null;
        float diff = 1000;

        foreach (RaycastHit2D target in this.targets) {
            Vector3 thisPosition = this.transform.position;
            Vector3 targetPosition = target.transform.position;
            float currentDiff = Vector3.Distance(thisPosition, targetPosition);

            if (currentDiff < diff)
            {
                diff = currentDiff;
                nearst = target.transform;
            }
        }


        return nearst;
    }
}
