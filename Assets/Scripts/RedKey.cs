using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedKey : MonoBehaviour
{
    public GameManager gameManager;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.redKey = true;
            Collect();
        }
    }

    private void Collect()
    {
        Destroy(gameObject);
    }

}
