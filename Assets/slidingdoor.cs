using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slidingdoor : MonoBehaviour
{
    public GameObject door1;
    public GameObject door2;

    public float waitTime = 2.0f;

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                door1.SetActive(false);
                door2.SetActive(false);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //if (other.tag == "Player")
        //{
        //    door1.SetActive(true);
        //    door2.SetActive(true);
        //}
        StartCoroutine(countdown());
    }

    IEnumerator countdown()
    { 
        yield return new WaitForSeconds(waitTime);
        door1.SetActive(true);
        door2.SetActive(true);
    }
}
