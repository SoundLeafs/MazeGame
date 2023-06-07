using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMove : MonoBehaviour
{
	private UnityEngine.AI.NavMeshAgent _agent;
	
	void Start()
	{
		_agent = GetComponent<UnityEngine.AI.NavMeshAgent>();	
	}
	
	void Update()
	{
		if(Vector3.Distance(_agent.destination, transform.position) < 2f)
		{
			//generate random position an assign it to float variable randomX and RandomZ
			float randomX = Random.Range(-10f,10f);
			float randomZ = Random.Range(-10f,10f);
			
			//need transform initial position + the randomX position
			Vector3 randomPosition = new Vector3(transform.position.x + randomX,
												transform.position.y,
												transform.position.z + randomZ);
												
			//agent destination is equal to randomposition which we generated above									
			_agent.destination = randomPosition;									
		}
	}
}
