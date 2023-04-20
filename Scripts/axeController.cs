using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Animations;
public class axeController : MonoBehaviour
{
    public static axeController instance;

    //[SerializeField] private float offset;
    [SerializeField] private Transform pivot;
    [SerializeField] private Transform pivot2;
    [SerializeField] private float pivotRadius = 0.05f;
    [SerializeField] private LayerMask mask;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float throwIntensity = 1f;
    [SerializeField] private float spinSpeed = 5f;
    [SerializeField] private float Cooldown = 0.2f;
    [SerializeField] private float maxHoldInt = 5f;
    [SerializeField] private float holdTime = 2f;
    [SerializeField] private Slider holdSlider;

    [SerializeField] private AudioClip jump;
    [SerializeField] private AudioClip cling;

    private ParentConstraint PC;

    private CapsuleCollider2D col;

    private AudioSource ad;

    private bool canThrow = true;

    private float gScale;

    public bool isHittingEdge = false;

    private bool holding = false;

    private float multiplier = 1;

    public bool onBreakable = false;

    //Reference varaibles
    float intRef;

    private void Awake()
    {
        instance = this;

        col = GetComponent<CapsuleCollider2D>();

        ad = GetComponent<AudioSource>();

        PC = GetComponent<ParentConstraint>();

       

        gScale = rb.gravityScale;

        holdSlider.maxValue = maxHoldInt;
        holdSlider.minValue = multiplier;
    }


    // Update is called once per frame
    void Update()
    {
        //lookAtMouse();

        holdSlider.value = multiplier;

        if (holding)
        {
            multiplier = Mathf.SmoothDamp(multiplier, maxHoldInt, ref intRef, holdTime);
        }

        if (isHittingEdge )
        {
            if (Input.GetMouseButtonDown(0))
            {
                holding = true;
                multiplier = 1;
            }
            else if (Input.GetMouseButtonUp(0) && canThrow)
            {

                holding = false;
                col.isTrigger = false;

                if (isThrowingAtWall() && onBreakable)
                {
                    rb.AddForce(getMouseDir() * (throwIntensity * multiplier), ForceMode2D.Impulse);
                }
                else if (isThrowingAtWall() && !onBreakable)
                {
                    //TODO
                }
                else
                {
                    rb.AddForce(getMouseDir() * (throwIntensity * multiplier), ForceMode2D.Impulse);
                }

                

                ad.PlayOneShot(jump);

                canThrow = false;
                StartCoroutine(throwCooldown());
            }
        }
        else
        {
            continueMovement();
        }

        if (grounded())
        {
            rb.angularVelocity = 0;
        }
        
    }

    public float SpinDirection()
    {
        Vector2 MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        float diff = MousePos.x - transform.position.x;

        if(diff >= 0)
        {
            return 1;
        }
        else
        {
            return -1;
        }
    }

    private Vector2 getMouseDir()
    {
        Vector2 MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        return (MousePos - (Vector2)transform.position).normalized;
    }

    private bool grounded()
    {
        return (Physics2D.OverlapCircle(pivot.position, pivotRadius, mask) != null) || (Physics2D.OverlapCircle(pivot2.position, pivotRadius, mask) != null);
    }
    
    private void spin()
    {
        rb.angularVelocity = (spinSpeed * maxHoldInt);
    }

    private IEnumerator throwCooldown()
    {
        yield return new WaitForSeconds(Cooldown);
        canThrow = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(pivot.position, pivotRadius);
        Gizmos.DrawWireSphere(pivot2.position, pivotRadius);

        Gizmos.DrawRay(transform.position, getMouseDir());
    }

    public void stopMovement()
    {

        ad.PlayOneShot(cling);

        col.isTrigger = true;

        rb.gravityScale = 0f;
        rb.angularVelocity = 0f;
        rb.velocity = Vector2.zero;
        //Debug.Log("Hit surface");
    }
    public void continueMovement()
    {
        spin();
        rb.gravityScale = gScale;
    }

    private bool isThrowingAtWall()
    {
        return Physics2D.Raycast(transform.position, getMouseDir(), 1f, mask);
    }

    public void die()
    {
        levelManager.instance.incDeath();
        StartCoroutine(restart());
    }

    public void dieImmediate()
    {
        levelManager.instance.incDeath();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private IEnumerator restart()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void setAnchor(Transform anch)
    {
        if(anch == pivot)
        {
            pivot2.gameObject.SetActive(false);
        }
        else if (anch == pivot2)
        {
            pivot.gameObject.SetActive(false);
        }
    }

    public void setAnchorBack(Transform anch)
    {
        if (anch == pivot)
        {
            pivot2.gameObject.SetActive(true);
        }
        else if (anch == pivot2)
        {
            pivot.gameObject.SetActive(true);
        }
    }


}
