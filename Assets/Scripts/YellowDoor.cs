using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowDoor : MonoBehaviour
{
    public float speed = 1f;
    public Vector3 PositionDelta = Vector3.zero;

    private Vector3 _closedPosition;
    private Vector3 _openPosition;

    public GameManager gameManager;
    private bool isOpen = false;

    public void Start()
    {
        _closedPosition = transform.position;
        _openPosition = _closedPosition + PositionDelta;
    }

    public void Update()
    {
        if (gameManager.yellowKey && !isOpen)
        {
            Open();
        }
    }

    public void Open()
    {
        StartCoroutine(OpenDoorCoroutine());
    }

    private IEnumerator OpenDoorCoroutine()
    {
        Vector3 goal = _openPosition;
        while (Vector3.Distance(transform.position, goal) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, goal, speed * Time.deltaTime);
            yield return null;
        }

        isOpen = true;
    }
}
