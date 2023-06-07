using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpeedCube : MonoBehaviour
{
    public GameManager gameManager;
    private MeshRenderer meshRenderer;
    private new Collider collider;


    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        collider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.collectedSpeedcubes++;
            gameManager.AddSpeed();
            Collect();
        }
    }

    private void Collect()
    {
        //Destroy(gameObject); changed this to disable mesh as it was causing issues

        meshRenderer.enabled = false;
        collider.enabled = false;
    }


}
