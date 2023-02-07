using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] GameObject camSwitcher;
    [SerializeField] private float speed = 5f;
    Vector3 forward;
    Vector3 right;

    private void Start()
    {
        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            Move();            

        }
    }

    void Move()
    {
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 rightMov = right * speed * Time.deltaTime * Input.GetAxis("Horizontal");
        Vector3 upMov = forward * speed * Time.deltaTime * Input.GetAxis("Vertical");

        Vector3 heading = Vector3.Normalize(rightMov + upMov);

        transform.forward = heading;

        transform.position += rightMov;
        transform.position += upMov;
        camSwitcher.GetComponent<CamSwitcher>().isMoveable = false;

    }
}


