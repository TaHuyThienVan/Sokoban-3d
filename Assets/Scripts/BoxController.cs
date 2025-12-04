using UnityEngine;
using System.Collections.Generic;
using System.Collections;
public class BoxController : MonoBehaviour
{

    public LayerMask blockingLayer;


    private bool boxIsMoving = false;
    public bool TryToPushBox(Vector3 direction,float moveSpeed) // huong bam direction

    {
        

        var targerPosition = transform.position + direction;
        if (!Physics.Raycast(transform.position, direction, out RaycastHit hit, 1f, blockingLayer))
        {
            StartCoroutine(MoveToPosition(targerPosition, moveSpeed)) ; // chay nhieu frame
            return true;

        }
        return false;
        


    }

    IEnumerator MoveToPosition(Vector3 target,float moveSpeed)
    {
        boxIsMoving = true;

        while (Vector3.Distance(transform.position, target) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = target;
        boxIsMoving = false;
    }
}
