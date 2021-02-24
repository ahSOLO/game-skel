using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Movement
    [SerializeField] private float speed = 5f;
    private bool isFacingRight;
    private bool isWalking;

    // Components
    private Rigidbody2D rb;
    private Animator anim;
    private GameObject cf;

    // FSM
    private enum State { idle, walking }
    private State state = State.idle;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        cf = GameObject.FindWithTag("CursorFollower");
        Debug.Log(cf);
        
        isFacingRight = true;
        isWalking = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFacingRight)
        {
            transform.localScale = new Vector2(1, 1);
        }
        else
        {
            transform.localScale = new Vector2(-1, 1);
        }

        if (Input.GetButton("Fire1"))
        {
            Vector3 target = cf.transform.position;
            StopAllCoroutines();
            StartCoroutine(WalkToTarget(target));
        }
    }

    IEnumerator WalkToTarget (Vector3 target)
    {
        while (Vector3.Distance(transform.position, target) > 0.1f)
        {
            anim.SetBool("isWalking", true);
            if (transform.position.x > target.x)
            {
                isFacingRight = false;
            }
            else
            {
                isFacingRight = true;
            }
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            yield return null;
        }

        anim.SetBool("isWalking", false);
    }
}
