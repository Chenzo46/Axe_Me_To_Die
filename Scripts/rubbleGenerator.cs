using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rubbleGenerator : MonoBehaviour
{
    [SerializeField] private Transform pointOne;
    [SerializeField] private Transform pointTwo;
    [SerializeField] private GameObject rock;
    [SerializeField] private float cool = 1f;

    private bool canSpawn = true;

    void Update()
    {
        if (canSpawn)
        {
            float point = Random.Range(pointOne.position.x, pointTwo.position.x);

            GameObject g = Instantiate(rock, new Vector2(point, transform.position.y), rock.transform.rotation);
            g.GetComponent<Rigidbody2D>().angularVelocity = 500;

            StartCoroutine(cooldown());
        }
    }

    private IEnumerator cooldown()
    {
        canSpawn = false;
        yield return new WaitForSeconds(cool);
        canSpawn = true;
    }
}
