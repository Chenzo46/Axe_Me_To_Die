using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class levelEnd : MonoBehaviour
{
    private CinemachineVirtualCamera cam;

    private void Awake()
    {
        cam = GameObject.FindGameObjectWithTag("cmCam").GetComponent<CinemachineVirtualCamera>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            StartCoroutine(waitToEnd());
            GetComponent<Animator>().SetTrigger("end");
            cam.Follow = transform;
        }
    }

    private IEnumerator waitToEnd()
    {
        yield return new WaitForSeconds(1f);
        levelManager.instance.nextLevel();
    }

}
