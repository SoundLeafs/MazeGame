using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class MoveToObject : MonoBehaviour
{
	public Transform player;
	private NavMeshAgent _agent;
    private Transform currentTarget;
    [SerializeField] private State _state;

    public enum State
    {
        Waypoint,
        GetCoins,
        GetSpeed,
        Attack,
    }


    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        currentTarget = player;

    }

    // Update is called once per frame
    void Update()
    {
        _agent.destination = player.position;
		
    }



}
