using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBlock : MonoBehaviour
{
    [SerializeField] private bool isVertical = false;
    [SerializeField] private float travelRange = 3f;

    [SerializeField] private bool startPositive = true;

    [SerializeField] private float moveSpeed = 1f;

    [SerializeField] private float waitTime = 0.5f;

    private LineRenderer LR;

    private Vector2 p1;
    private Vector2 p2;

    private bool p1Reached;
    private bool p2Reached;

    [SerializeField] private Vector2 destination;

    private bool isAwake = false;

    //Reference Variables
    Vector2 move;

    private void Awake()
    {
        LR = GetComponent<LineRenderer>();

        isAwake = true;

        if (!isVertical)
        {
            p1 = new Vector2(transform.position.x + travelRange, transform.position.y);
            p2 = new Vector2(transform.position.x - travelRange, transform.position.y);
        }
        else
        {
            p1 = new Vector2(transform.position.x , transform.position.y + travelRange);
            p2 = new Vector2(transform.position.x , transform.position.y - travelRange);
        }

        LR.SetPosition(0, p1);
        LR.SetPosition(1,p2);

        if (startPositive)
        {
            destination = p1;
            p2Reached = true;
            //transform.position = p2;
        }
        else
        {
            destination = p2;
            p1Reached = true;
            //transform.position = p1;
        }
        

    }

    void Update()
    {

        if(Vector2.Distance(transform.position, destination) <= 0.3f && !(p1Reached && p2Reached))
        {
            StartCoroutine(reached());
        }
        else
        {

            transform.position = (Vector2)transform.position + (getDirection() * moveSpeed * Time.deltaTime);
        }

    }

    private Vector2 getDirection()
    {
        if (startPositive && isVertical)
        {
            return Vector2.up;
        }
        else if (!startPositive && isVertical)
        {
            return Vector2.down;
        }
        else if(startPositive && !isVertical)
        {
            return Vector2.right;
        }
        else if(!startPositive && !isVertical)
        {
            return Vector2.left;
        }
        else
        {
            return new Vector2();
        }
        
    }

    IEnumerator reached()
    {
        if (p1Reached)
        {
            p2Reached = true;
        }
        else if(p2Reached)
        {
            p1Reached = true;
        }

        startPositive = !startPositive;

        yield return new WaitForSeconds(waitTime);

        if (destination == p2)
        {
            destination = p1;
            p1Reached = false;
        }
        else if (destination == p1)
        {
            destination = p2;
            p2Reached = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (isAwake)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(p1, 0.2f);
            Gizmos.DrawWireSphere(p2, 0.2f);
        }
    }
}
