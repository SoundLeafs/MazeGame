using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueKey : MonoBehaviour
{
    public GameManager gameManager;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.blueKey = true;
            Collect();
        }
    }

    private void Collect()
    {
        Destroy(gameObject);
    }

}
