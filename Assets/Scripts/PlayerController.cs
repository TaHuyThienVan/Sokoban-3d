using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    bool isMoving = false;

    Vector3 targerPosition;
    [SerializeField] float moveSpeed = 5f;
    
    void Update()
    {
       
        if (isMoving) return;
        HandleInput();

    }


    void HandleInput()
    {

        Vector3 movement = Vector3.zero;

        if (Input.GetKeyDown(KeyCode.W)) movement = new Vector3(0, 0, 1);
        if (Input.GetKeyDown(KeyCode.S)) movement = new Vector3(0, 0, -1);
        if (Input.GetKeyDown(KeyCode.A)) movement = new Vector3(-1, 0, 0);
        if (Input.GetKeyDown(KeyCode.D)) movement = new Vector3(1, 0, 0);

        if (movement != Vector3.zero)
        {
            TryToMove(movement);
        }
    }

    void TryToMove(Vector3 direction)
    {
        targerPosition = transform.position + direction;
        StartCoroutine(MoveToPosition(targerPosition)); // chay nhieu frame

    }

    IEnumerator MoveToPosition(Vector3 target)
    {
        isMoving = true;

        while ((transform.position - target).sqrMagnitude > 0.0001f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = target;
        isMoving = false;
    }
}
