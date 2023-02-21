using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantGrowth : MonoBehaviour
{
    [SerializeField] GameObject plant;
    float time;
    int potatoTime = 10;
    int cornTime = 5;
    int carrotTime = 20;

    // Start is called before the first frame update
    void Start()
    {
        if(gameObject.tag == "PotatoSeed")
        {
            time = potatoTime;
        }
        else if (gameObject.tag == "CornSeed")
        {
            time = cornTime;
        }
        else if (gameObject.tag == "CarrotSeed")
        {
            time = carrotTime;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        Growth();
    }
    void Growth()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, .5f) && hit.collider.tag == "Soil")
        {
            Debug.DrawRay(transform.position, Vector3.down, Color.red, hit.distance);
            if (time > 0)
            {
                time -= 1 * Time.deltaTime;
                print((int)time);
            }
            else if (time <= 0)
            {
                Instantiate(plant, transform.position, Quaternion.Euler(0, 0, 0), null);
                Destroy(gameObject);
            }
        }
    }
}
