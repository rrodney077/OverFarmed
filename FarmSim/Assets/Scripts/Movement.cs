using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] GameObject camSwitcher;
    [SerializeField] private float speed = 5f;

    private Vector2 movementInput = Vector2.zero;
    Vector3 forward;
    Vector3 right;

    private void Start()
    {
        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;
    }

    public void onMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }
    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        //Vector3 direction = new Vector3(movementInput.x, 0, movementInput.y);
        Vector3 rightMov = right * speed * Time.deltaTime * movementInput.x;
        Vector3 upMov = forward * speed * Time.deltaTime * movementInput.y;

        Vector3 heading = Vector3.Normalize(rightMov + upMov);

        transform.forward = heading;

        Vector3 movement = Vector3.Normalize(heading) * speed * Time.deltaTime;

        //transform.position += rightMov;
        //transform.position += upMov;

        transform.position += movement;
        camSwitcher.GetComponent<CamSwitcher>().isMoveable = false;

    }
}


