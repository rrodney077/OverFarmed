using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    //[SerializeField] GameObject camSwitcher;
    [SerializeField] private float speed = 5f;

    [SerializeField] LayerMask boxObjectLayer;
    
    [SerializeField] Transform boxCheck;
    [SerializeField] Transform objectHolder;
    public bool touchingBox;
    

    [Header("For Handling Seed Types")]
    public bool isPotatoSeed;
    public bool isCornSeed;
    public bool isCarrotSeed;
    [SerializeField] GameObject potatoSeed;
    [SerializeField] GameObject cornSeed;
    [SerializeField] GameObject carrotSeed;
    [SerializeField] LayerMask seedLayer;
    [SerializeField] LayerMask plantLayer;
    public bool isholding;   


    [Header("for planting/lifting seeds and plants")]
    GameObject[] tiles;
    List<Collider> seedColliders = new List<Collider>();
    List<Collider> plantColliders = new List<Collider>();
    public bool canLiftPlant;
    public bool isHoldingPlant;

    [Header("For selling plants")]
    [SerializeField] LayerMask SaleLayer;
    GameObject scoreManager;
    List<Collider> currentPlant = new List<Collider>();

    private Vector2 movementInput = Vector2.zero;
    Vector3 forward;
    Vector3 right;

    private void Start()
    {
        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;

        tiles = GameObject.FindGameObjectsWithTag("Soil");

        scoreManager = GameObject.Find("ScoreManager");
    }

    public void onMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }
    // Update is called once per frame
    void Update()
    {
        Move();
        ItemOverlap();
        SeedOverlap();
        SeedManager();
        plantOverlap();
        PlantManager();
        
    }

    void Move()
    {
        //Vector3 direction = new Vector3(movementInput.x, 0, movementInput.y);
        Vector3 rightMov = right * speed * Time.deltaTime * movementInput.x;
        Vector3 upMov = forward * speed * Time.deltaTime * movementInput.y;

        Vector3 heading = Vector3.Normalize(rightMov + upMov);

        transform.forward = heading;

        //Vector3 movement = Vector3.Normalize(heading) * speed * Time.deltaTime;

        transform.position += rightMov;
        transform.position += upMov;
        //transform.position += movement;
        
        
        //camSwitcher.GetComponent<CamSwitcher>().isMoveable = false;

    }
    void ItemOverlap()
    {
        if (Physics.CheckBox(boxCheck.position, boxCheck.transform.localScale, boxCheck.transform.rotation.normalized, boxObjectLayer, QueryTriggerInteraction.Ignore))
        {
            touchingBox = true;
        }
        else
        {
            touchingBox = false;
        }  
    }
    void SeedOverlap()
    {        
        if (Physics.OverlapBox(objectHolder.position, objectHolder.transform.localScale, objectHolder.rotation.normalized, seedLayer, QueryTriggerInteraction.Ignore).Length != 0)
        {
            seedColliders.Add(Physics.OverlapBox(objectHolder.position, objectHolder.transform.localScale, objectHolder.rotation.normalized, seedLayer, QueryTriggerInteraction.Ignore)[0]);
            isholding = true;                   
        }
        else
        {
            seedColliders.Clear();
            isholding = false;
            
        }
    }

    void SeedManager()
    {
        RaycastHit hit;
        if (touchingBox && Physics.Raycast(boxCheck.position, Vector3.forward, out hit))
        {
            if (hit.collider.tag == "PotatoBox")
            {
                isPotatoSeed = true;
                isCarrotSeed = false;
                isCornSeed = false;
            }
            else if (hit.collider.tag == "CornBox")
            {
                isCornSeed = true;
                isPotatoSeed = false;
                isCarrotSeed = false;
            }
            else if (hit.collider.tag == "CarrotBox")
            {
                isCarrotSeed = true;
                isPotatoSeed = false;
                isCornSeed = false;
            }
            else
            {
                isPotatoSeed = false;
                isCarrotSeed = false;
                isCornSeed = false;
            }
        }
    }
   
    void PlantManager()
    {
        if(Physics.OverlapBox(boxCheck.position, boxCheck.transform.localScale, boxCheck.transform.rotation.normalized, plantLayer, QueryTriggerInteraction.Ignore).Length != 0 && !isholding)
        {
            plantColliders.Add(Physics.OverlapBox(boxCheck.position, boxCheck.transform.localScale, boxCheck.transform.rotation.normalized, plantLayer, QueryTriggerInteraction.Ignore)[0]);
            canLiftPlant = true;
        }
        else
        {            
            plantColliders.Clear();
            canLiftPlant = false;
            
        }
    }
    void plantOverlap()
    {
        RaycastHit hit;
        
        if (Physics.Raycast(objectHolder.position, Vector3.up, out hit, plantLayer))
        {
            currentPlant.Add(hit.collider);
            Debug.DrawRay(objectHolder.position, Vector3.up, Color.red);
            isholding = true;
            isHoldingPlant = true;
        }
        else
        {
            isHoldingPlant = false;
            currentPlant.Clear();

        }

    }
    /*void SellPlant()
    {
        if(isHoldingPlant && Physics.CheckBox(objectHolder.position, objectHolder.localScale, objectHolder.rotation.normalized, SaleLayer, QueryTriggerInteraction.Collide))   
        {
            Destroy(currentPlant[0]);
            scoreManager.GetComponent<Score>().score += 10;
            currentPlant.Clear();
        }
    }*/
    public void Interact()
    {
        if (touchingBox && isPotatoSeed && !isholding)
        {
            Instantiate(potatoSeed, objectHolder.position, objectHolder.rotation, potatoSeed.transform.parent = transform);
        }
        else if (touchingBox && isCornSeed && !isholding)
        {
            Instantiate(cornSeed, objectHolder.position, objectHolder.rotation, potatoSeed.transform.parent = transform);
        }
        else if (touchingBox && isCarrotSeed && !isholding)
        {
            Instantiate(carrotSeed, objectHolder.position, objectHolder.rotation, potatoSeed.transform.parent = transform);
        }

        if (!isholding && canLiftPlant)
        {
            plantColliders[0].transform.position = new Vector3(objectHolder.position.x, objectHolder.position.y + 1.5f, objectHolder.position.z);
            plantColliders[0].transform.SetParent(transform);
        }
        foreach (GameObject g in tiles)
        {
            if (isholding && g.GetComponent<Tiles>().isSelectable)
            {
                seedColliders[0].gameObject.transform.position = new Vector3(g.transform.position.x, g.transform.position.y + .7f,g.transform.position.z);
                seedColliders[0].gameObject.transform.SetParent(g.transform);
            }
        }
        if (isHoldingPlant && Physics.CheckBox(objectHolder.position, objectHolder.localScale, objectHolder.rotation.normalized, SaleLayer, QueryTriggerInteraction.Ignore))
        {
            /* currentPlant[0].GetComponent<MeshRenderer>().enabled = false;
             currentPlant[0].GetComponent<Collider>().enabled = false;*/
            Destroy(currentPlant[0].gameObject);
            scoreManager.GetComponent<Score>().score += 10;
            
            
        }
    }
    private void OnDrawGizmosSelected()
    {
        Debug.DrawRay(transform.position, Vector3.forward, Color.green);
        Gizmos.color = Color.red;
        Gizmos.DrawCube(boxCheck.transform.position, boxCheck.transform.localScale);
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(objectHolder.position, objectHolder.transform.localScale);
    }
}


