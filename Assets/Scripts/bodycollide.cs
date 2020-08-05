using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bodycollide : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {



    }
    public bool hitFloor = false;
    public bool checkpoint = false;
    public bool backstop = false;
    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
      if(collision.gameObject.tag == "backstop") {

        backstop = true;
        //Debug.Log("hit backstop");
      }

      if(collision.gameObject.tag == "checkpoint") {

        checkpoint = true;
        Debug.Log("hit checkpoint");

      }

    }
    private void OnCollisionEnter2D(Collision2D collision)
  {
    if(collision.gameObject.tag == "floor") {

      hitFloor = true;

    }





  }

}
