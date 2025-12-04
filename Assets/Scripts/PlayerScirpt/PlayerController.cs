using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    public LayerMask blockingLayer;
    bool isMoving = false;

    void Start()
    {
        SnapToGrid();
    }

    void Update()
    {
        if (!isMoving)
            HandleInput();
    }

    void HandleInput()
    {
        Vector3 direction = Vector3.zero;

        if (Input.GetKeyDown(KeyCode.W)) direction = Vector3.forward;
        if (Input.GetKeyDown(KeyCode.S)) direction = Vector3.back;
        if (Input.GetKeyDown(KeyCode.A)) direction = Vector3.left;
        if (Input.GetKeyDown(KeyCode.D)) direction = Vector3.right;

        if (direction != Vector3.zero)
            TryMove(direction);
    }

    void TryMove(Vector3 direction)
    {
        // ?i?m xu?t phát raycast, tránh dính collider chính mình
        Vector3 origin = transform.position + direction * 0.1f;

        // Raycast ?úng 1 ô
        if (!Physics.Raycast(origin, direction, out RaycastHit hit, 1f, blockingLayer))
        {
            // không va ch?m ? ?i bình th??ng
            Vector3 target = transform.position + direction;
            target = RoundVector(target);
            StartCoroutine(MoveToPosition(target));
        }
        else
        {
            // g?p box?
            if (hit.collider.CompareTag("Box"))
            {
                var box = hit.collider.GetComponent<BoxController>();

                if (box != null && box.TryPush(direction, moveSpeed))
                {
                    // box ??y ???c ? player ti?n theo
                    Vector3 target = transform.position + direction;
                    target = RoundVector(target);
                    StartCoroutine(MoveToPosition(target));
                }
            }
        }
    }

    IEnumerator MoveToPosition(Vector3 target)
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
