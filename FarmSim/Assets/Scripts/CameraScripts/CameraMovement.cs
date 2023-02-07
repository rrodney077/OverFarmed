using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] GameObject camSwitcher;

    [SerializeField] GameObject upTarget;
    [SerializeField] GameObject downTarget;
    [SerializeField] GameObject leftTarget;
    [SerializeField] GameObject rightTarget;

    // camera movement booleans
    public bool canMoveUp = false;
    public bool canMoveDown = false;
    public bool canMoveLeft = false;
    public bool canMoveRight = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveUpHandler();

        if (canMoveUp)
        {
            MoveUp();
            
        }

        if (canMoveDown)
        {
            MoveDown();
            
        }

        if (canMoveLeft)
        {
            MoveLeft();
            
        }

        if (canMoveRight)
        {
            MoveRight();
            
        }
    }
    private void MoveUpHandler()
    {
        // Distance between camera and targets
        float upTargetDist = Vector3.Distance(upTarget.transform.position, transform.position);
        float downTargetDist = Vector3.Distance(downTarget.transform.position, transform.position);
        float leftTargetDist = Vector3.Distance(leftTarget.transform.position, transform.position);
        float rightTargetDist = Vector3.Distance(rightTarget.transform.position, transform.position);

        // Camera movement logic
        if (upTargetDist < 1f)
        {
            upTarget.transform.position += new Vector3(0, 0, 4);
            canMoveUp = false;
        }

        if(downTargetDist < 1f)
        {
            downTarget.transform.position -= new Vector3(0, 0, 4);
            canMoveDown = false;
        }

        if (leftTargetDist < 1f)
        {
            leftTarget.transform.position -= new Vector3(4, 0, 0);
            canMoveLeft = false;
        }

        if (rightTargetDist < 1f)
        {
            rightTarget.transform.position += new Vector3(4, 0, 0);
            canMoveRight = false;
        }
    }

    private void MoveUp()
    {
        transform.position = Vector3.Lerp(transform.position, upTarget.transform.position, Time.deltaTime * 2f);
        downTarget.transform.position = transform.position - new Vector3(0 ,0, 4);
        leftTarget.transform.position = transform.position - new Vector3(4, 0, 0);
        rightTarget.transform.position = transform.position + new Vector3(4, 0, 0);
        
    }
    private void MoveDown()
    {
        transform.position = Vector3.Lerp(transform.position, downTarget.transform.position, Time.deltaTime * 2f);
        upTarget.transform.position = transform.position + new Vector3(0, 0, 4);
        leftTarget.transform.position = transform.position - new Vector3(4, 0, 0);
        rightTarget.transform.position = transform.position + new Vector3(4, 0, 0);
        
    }
    private void MoveLeft()
    {
        transform.position = Vector3.Lerp(transform.position, leftTarget.transform.position, Time.deltaTime * 2f);
        upTarget.transform.position = transform.position + new Vector3(0, 0, 4);
        downTarget.transform.position = transform.position - new Vector3(0, 0, 4);
        rightTarget.transform.position = transform.position + new Vector3(4, 0, 0);
        
    }
    private void MoveRight()
    {
        transform.position = Vector3.Lerp(transform.position, rightTarget.transform.position, Time.deltaTime * 2f);
        upTarget.transform.position = transform.position + new Vector3(0, 0, 4);
        downTarget.transform.position = transform.position - new Vector3(0, 0, 4);
        leftTarget.transform.position = transform.position - new Vector3(4, 0, 0);
        
    }
    // GUI button Inputs
    public void MoveUpGUI()
    {
        canMoveUp = true;
        canMoveDown = false;
        canMoveLeft = false;
        canMoveRight = false;
        camSwitcher.GetComponent<CamSwitcher>().isMoveable = true;
    }
    public void MoveDownGUI()
    {
        canMoveDown = true;
        canMoveUp = false;
        canMoveLeft = false;
        canMoveRight = false;
        camSwitcher.GetComponent<CamSwitcher>().isMoveable = true;
    }
    public void MoveLeftGUI()
    {
        canMoveLeft = true;
        canMoveUp = false;
        canMoveDown = false;
        canMoveRight = false;
        camSwitcher.GetComponent<CamSwitcher>().isMoveable = true;
    }
    public void MoveRightGUI()
    {
        canMoveRight = true;
        canMoveUp = false;
        canMoveDown = false;
        canMoveLeft = false;
        camSwitcher.GetComponent<CamSwitcher>().isMoveable = true;
    }


}
