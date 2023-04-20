using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pivotManager : MonoBehaviour
{
    [SerializeField] private GameObject blockExplode;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("ground"))
        {
            axeController.instance.stopMovement();
            axeController.instance.isHittingEdge = true;
            //axeController.instance.gameObject.transform.parent = collision.transform;
            //axeController.instance.setAnchor(gameObject.transform);
        }
        else if (collision.tag.Equals("moving"))
        {
            axeController.instance.stopMovement();
            axeController.instance.isHittingEdge = true;
            axeController.instance.gameObject.transform.parent = collision.transform;
            //axeController.instance.setAnchor(gameObject.transform);
        }
        else if (collision.tag.Equals("breakable"))
        {
            axeController.instance.stopMovement();
            axeController.instance.isHittingEdge = true;
            axeController.instance.onBreakable = true;
            //axeController.instance.gameObject.transform.parent = collision.transform;
            //axeController.instance.setAnchor(gameObject.transform);
        }
    }



    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("ground"))
        {
            axeController.instance.continueMovement();
            axeController.instance.isHittingEdge = false;
            //axeController.instance.gameObject.transform.parent = null;
            axeController.instance.setAnchorBack(gameObject.transform);
        }
        else if (collision.tag.Equals("moving"))
        {
            axeController.instance.continueMovement();
            axeController.instance.isHittingEdge = false;
            axeController.instance.gameObject.transform.parent = null;
            //axeController.instance.setAnchorBack(gameObject.transform);
        }
        else if (collision.tag.Equals("breakable"))
        {
            axeController.instance.isHittingEdge = false;

            axeController.instance.onBreakable = false ;
            //axeController.instance.gameObject.transform.parent = null;

            Instantiate(blockExplode, collision.transform.position, blockExplode.transform.rotation);
            Destroy(collision.gameObject);

            axeController.instance.continueMovement();

            //axeController.instance.setAnchorBack(gameObject.transform);

        }
    }

}
