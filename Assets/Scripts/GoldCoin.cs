using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCoin : MonoBehaviour
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
            gameManager.money += 100;
            gameManager.UpdateMoney();
            Collect();
        }

        else if (other.CompareTag("Enemy"))
        {
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
