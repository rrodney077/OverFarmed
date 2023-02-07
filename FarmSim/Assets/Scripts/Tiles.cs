using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiles : MonoBehaviour
{
    [SerializeField] GameObject tempCan;

    private bool isSelectable = false;

    GameObject player;

    private Color baseColor;
    private Color newColor = Color.cyan;

    // Start is called before the first frame update
    void Start()
    {
        baseColor = GetComponent<Renderer>().material.color;

        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        ColourHandler();
        SelectionHandler();
    }
    private void OnMouseDown()
    {
        Debug.Log("Goku???");
    }

    void ColourHandler()
    {
        float distance;
        distance = Vector3.Distance(player.transform.position, transform.position);
        

        if (distance <= 1.8f)
        {
            isSelectable = true;
            gameObject.GetComponent<Renderer>().material.color = newColor;
        }
        else
        {
            isSelectable = false;
            gameObject.GetComponent<Renderer>().material.color = baseColor;
            
        }
    }
    void SelectionHandler()
    {
        if (isSelectable)
        {
            tempCan.SetActive(true);
        }
        else
        {
            tempCan.SetActive(false);
        }
    }
}
