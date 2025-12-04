using UnityEngine;
using System.Collections;

public class BoxController : MonoBehaviour
{
    public LayerMask blockingLayer;
    bool isMoving = false;

    void Start()
    {
        SnapToGrid();
    }

    public bool TryPush(Vector3 direction, float moveSpeed)
    {
        if (isMoving) return false;

        Vector3 origin = transform.position + direction * 0.1f;

        // box c?ng ch? raycast ?úng 1 ô
        if (Physics.Raycast(origin, direction, out RaycastHit hit, 1f, blockingLayer))
            return false; // tr??c m?t box có v?t ? không ??y ???c

        // box ??y ???c
        Vector3 target = transform.position + direction;
        target = RoundVector(target);

        StartCoroutine(MoveToPosition(target, moveSpeed));
        return true;
    }

    IEnumerator MoveToPosition(Vector3 target, float moveSpeed)
    {
        isMoving = true;

        while (Vector3.Distance(transform.position, target) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = target;
        SnapToGrid();
        isMoving = false;
    }

    void SnapToGrid()
    {
        transform.position = RoundVector(transform.position);
    }

    Vector3 RoundVector(Vector3 pos)
    {
        return new Vector3(
            Mathf.Round(pos.x),
            Mathf.Round(pos.y),
            Mathf.Round(pos.z)
        );
    }
}
