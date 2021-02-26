using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    // Movement
    [SerializeField] private GameObject cursorFollower;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float stoppingDistance = 1.5f;

    // Scaling
    private int xDirection = 1;
    [SerializeField] private float horizonPos = 10f;
    [SerializeField] private float closestSize = 1.5f;

    // Components
    private Animator anim;
    private NavMeshAgent agent;

    // FSM
    private enum State { idle, walking }
    private State state = State.idle;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            //Vector3 target = cursorFollower.transform.position;
            agent.destination = cursorFollower.transform.position;
        }

        if (agent.velocity.sqrMagnitude > 0.1f)
        {
            anim.SetBool("isWalking", true);
            if (transform.position.x < agent.destination.x)
            {
                xDirection = 1;
            }
            else
            {
                xDirection = -1;
            }
        }
        else
        {
            anim.SetBool("isWalking", false);
        }

        transform.localScale = new Vector2(xDirection * closestSize * (horizonPos - transform.position.y) / horizonPos, closestSize * (horizonPos - transform.position.y) / horizonPos);

        agent.speed = speed * 2 * ((horizonPos - transform.position.y) * (horizonPos - transform.position.y)) / (horizonPos* horizonPos);
        agent.stoppingDistance = stoppingDistance * (horizonPos - transform.position.y) / horizonPos;
    }
}