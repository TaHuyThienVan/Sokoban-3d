using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    bool isMoving = false;

    
    [SerializeField] float moveSpeed = 5f;
    public LayerMask blockingLayer;
    
    void Update()
    {
      
        HandleInput();

    }


    void HandleInput()
    {


        if (isMoving) return;

        Vector3 movement = Vector3.zero;

        if (Input.GetKeyDown(KeyCode.W)) movement = Vector3.forward;
        if (Input.GetKeyDown(KeyCode.S)) movement = Vector3.back;
        if (Input.GetKeyDown(KeyCode.A)) movement = Vector3.left;
        if (Input.GetKeyDown(KeyCode.D)) movement = Vector3.right;

        if (movement != Vector3.zero)
        {
            TryToMove(movement);
        }
    }

    void TryToMove(Vector3 direction) // huong bam direction

    {
        var targetPosition = transform.position + direction;
        if (!Physics.Raycast(transform.position, direction, out RaycastHit hit, 1f, blockingLayer))
        {
            StartCoroutine(MoveToPosition(targetPosition)); // chay nhieu frame

        }
        else if(hit.collider.CompareTag("Box"))
        {
            // do sth
            var box = hit.collider.GetComponent<BoxController>();
            if(box!= null&& box.TryToPushBox(direction,moveSpeed))
            {
                StartCoroutine(MoveToPosition(targetPosition));
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
        isMoving = false;
    }
}
