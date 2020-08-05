using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carCollide : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Physics.IgnoreLayerCollision(9, 9);

    }
    public bool checkpoint = false;
    public bool finish = false;
    public bool track = false;
    public bool fence = false;
    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider collision)
    {

        
      
            if (collision.gameObject.tag == "fence")
            {
                // SetReward(2f);
                Debug.Log("fence (trigger)");
                fence = true;
            }
            if (collision.gameObject.tag == "checkpoint")
            {
                // SetReward(2f);
                Debug.Log("Checkpoint (trigger)");
                checkpoint = true;
            }
            if (collision.gameObject.tag == "finish")
            {
                //SetReward(3f);
                //Debug.Log("Finnish (trigger)");
            //timeTxt.text = time.ToString();
                finish = true;
                //Done();
            }
            if (collision.gameObject.tag == "track")
            {
                track = true;
                //SetReward(-1f);
                //Debug.Log("hit track (trigger)");
                // Done();
            }
        
    }
    private void OnCollisionEnter(Collision collision)
    {



        if (collision.gameObject.tag == "fence")
            {
                // SetReward(2f);
                Debug.Log("fence collision");
                fence = true;
            }

        if (collision.gameObject.tag == "checkpoint")
        {
            // SetReward(2f);
            Debug.Log("Checkpoint (collision)");
           // checkpoint = true;
        }
        if (collision.gameObject.tag == "finish")
        {
            //SetReward(3f);
            //Debug.Log("Finnish (trigger)");
            //timeTxt.text = time.ToString();
            finish = true;
            //Done();
        }
        if (collision.gameObject.tag == "track")
        {
            track = true;
            //SetReward(-1f);
            Debug.Log("hit track (trigger)");
            // Done();
        }

    }
}
