using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIState3 : MonoBehaviour
{
    //Script to replace move to object

    public Transform player;
    private NavMeshAgent _agent;
    private Transform currentTarget;
    [SerializeField] private State _state;
    public Transform[] waypoints;
    private float distanceToPlayer;
    private float distanceToTarget;
    public int moneyLimit;
    public float speedCubeLimit;
    public GameManager gameManager;

    public Transform[] speedCubePositions;
    public Transform[] coinPositions;



    public int arrayIndex = 0;

    private Vector3 previousPosition;
    private Vector3 currentPosition;
    private float movementSpeed;

    public Animator animator;


    public enum State
    {
        Waypoint,
        //head to waypoints in the assigned order of the array
        GetCoin,
        //head towards the closest coin until all gone
        GetSpeed,
        //head towards the closest speed object until all gone
        Attack,
        //head towards the player if they are within X range
        Attacking,
        //when within 1meter do animation
    }


    // Start is called before the first frame update
    public void Start()
    {
        _agent = GetComponent<NavMeshAgent>();

        // Find all objects tagged with "waypoint"
        GameObject[] waypointObjects = GameObject.FindGameObjectsWithTag("Waypoint3");
        // Create an array of Transforms to store the waypoints
        waypoints = new Transform[waypointObjects.Length];
        for (int i = 0; i < waypointObjects.Length; i++)
        {
            waypoints[i] = waypointObjects[i].transform;
        }

        GameObject[] coinObjects = GameObject.FindGameObjectsWithTag("Coin");
        coinPositions = new Transform[coinObjects.Length];
        for (int i = 0; i < coinObjects.Length; i++)
        {
            coinPositions[i] = coinObjects[i].transform;
        }

        GameObject[] speedCubeObjects = GameObject.FindGameObjectsWithTag("SpeedCube");
        speedCubePositions = new Transform[speedCubeObjects.Length];
        for (int i = 0; i < speedCubeObjects.Length; i++)
        {
            speedCubePositions[i] = speedCubeObjects[i].transform;
        }

        _state = State.Waypoint;
        currentTarget = player;

        // Initialize previous position to the starting position of the character
        previousPosition = transform.position;



        NextState();


    }

    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.position);
        //update distance to player for use elsewhere
        distanceToTarget = Vector3.Distance(transform.position, currentTarget.position);

        _agent.destination = currentTarget.position;
        //head towards current target

        AnimationCheck();

    }

    public void AnimationCheck()
    {
        // Update current position
        currentPosition = transform.position;

        // Calculate movement vector
        Vector3 movementVector = currentPosition - previousPosition;

        // Calculate movement speed
        movementSpeed = movementVector.magnitude / Time.deltaTime;

        // Update previous position
        previousPosition = currentPosition;

        // Control animations based on movement speed
        if (movementSpeed > 3f) // Adjust the threshold value as needed
        {
            // Character is moving, play walking animation
            animator.SetBool("IsWalking", true);
        }
        else
        {
            // Character is not moving, play idle animation
            animator.SetBool("IsWalking", false);
        }
    }

    private void NextState()
    {


        switch (_state)
        {
            case State.Waypoint:
                StartCoroutine(WaypointState());
                break;
            case State.GetCoin:
                StartCoroutine(GetCoinState());
                break;
            case State.GetSpeed:
                StartCoroutine(GetSpeedState());
                break;
            case State.Attack:
                StartCoroutine(AttackState());
                break;
            case State.Attacking:
                StartCoroutine(AttackingState());
                break;
        }
    }

    private IEnumerator WaypointState()
    {

        Debug.Log("Waypoint Search");
        arrayIndex = 0;
        currentTarget = waypoints[arrayIndex];



        while (_state == State.Waypoint)
        {

            //if we are close to player then attack him, if we are far then return to waypoint
            if (distanceToPlayer < 40f)
            {
                _state = State.Attack;
            }

            //if player has more than X speed variable then we will collect speed, if there isnt many left then ignore it
            else if (gameManager.collectedSpeedcubes > speedCubeLimit && speedCubePositions.Length > 4)
            {
                _state = State.GetSpeed;
            }
            //if player has more than x$ money variable we will collect coins, if not many left then ignore
            else if (gameManager.money > moneyLimit && coinPositions.Length > 5)
            {
                _state = State.GetCoin;
            }

            if (distanceToTarget < 3)
            {
                arrayIndex++;
                if (arrayIndex >= waypoints.Length)
                {
                    arrayIndex = 0; // loop back to the first waypoint
                }
                currentTarget = waypoints[arrayIndex];

            }

            yield return null;
            //wait one frame


        }
        Debug.Log("Exit Waypoint");
        NextState();

    }

    private IEnumerator GetCoinState()
    {

        Debug.Log("Coin Search");
        arrayIndex = 0;
        currentTarget = coinPositions[arrayIndex];
        while (_state == State.GetCoin)
        {
            if (distanceToPlayer < 40f)
            {
                _state = State.Attack;
            }

            if (coinPositions.Length < 3)
            {
                _state = State.Waypoint;
            }

            if (distanceToTarget < 3)
            {
                arrayIndex++;
                if (arrayIndex >= coinPositions.Length)
                {
                    arrayIndex = 0; // loop back to the first waypoint
                }
                currentTarget = coinPositions[arrayIndex];

            }

            yield return null;
        }
        Debug.Log("Exit CoinSearch");
        NextState();

    }

    private IEnumerator GetSpeedState()
    {
        arrayIndex = 0;
        currentTarget = speedCubePositions[arrayIndex];
        Debug.Log("Speed Search");
        while (_state == State.GetSpeed)
        {
            if (distanceToPlayer < 40f)
            {
                _state = State.Attack;
            }


            if (distanceToTarget < 3)
            {
                arrayIndex++;
                if (arrayIndex >= speedCubePositions.Length)
                {
                    arrayIndex = 0; // loop back to the first waypoint
                }
                currentTarget = speedCubePositions[arrayIndex];

            }

            yield return null;
        }
        Debug.Log("Exit Speed State");
        NextState();

    }
    private IEnumerator AttackState()
    {
        currentTarget = player;
        Debug.Log("Hunt Player");

        while (_state == State.Attack)
        {
            if (distanceToPlayer < 3)
            {
                _state = State.Attacking;
            }
            //if player goes to far away then return to waypoint
            if (distanceToPlayer > 50f)
            {
                _state = State.Waypoint;
            }

            yield return null;
        }

        Debug.Log("Exit AttackState");
        NextState();
    }

    private IEnumerator AttackingState()
    {
        animator.SetBool("Attacking", true);
        while (_state == State.Attacking)
        {
            if (distanceToPlayer > 5)
            {
                _state = State.Attack;
            }
            //if player goes to far away then return to waypoint
            if (distanceToPlayer > 50f)
            {
                _state = State.Waypoint;
            }

            yield return null;
        }

        Debug.Log("Exit AttackState");
        animator.SetBool("Attacking", false);
        NextState();

    }


}
