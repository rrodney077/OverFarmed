using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiles : MonoBehaviour
{
    [SerializeField] GameObject tempCan;
    [SerializeField] Transform playerCheck;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] LayerMask seedLayer;
    [SerializeField] LayerMask plantLayer;

    public bool isSelectable = false;
    public bool isAvailable = false;

    private Color baseColor;
    private Color newColor = Color.cyan;

    // Start is called before the first frame update
    void Start()
    {
        baseColor = GetComponent<Renderer>().material.color;
    }

    // Update is called once per frame
    void Update()
    {
        CheckPlant();

        if (Physics.CheckSphere(playerCheck.position, .6f, playerLayer) && isAvailable)
        {
            isSelectable = true;
            gameObject.GetComponent<Renderer>().material.color = newColor;
            tempCan.GetComponent<Canvas>().enabled = true;

        }
        else
        {
            isSelectable = false;
            gameObject.GetComponent<Renderer>().material.color = baseColor;
            tempCan.GetComponent<Canvas>().enabled = false;
        }
    }

    void CheckPlant()
    {
        if (Physics.CheckSphere(playerCheck.position, .5f, seedLayer) || Physics.CheckSphere(playerCheck.position, .5f, plantLayer))
        {
            isAvailable = false;
        }
        else
        {
            isAvailable = true;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(playerCheck.position, .6f);

        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(playerCheck.position, .5f);
    }

}
