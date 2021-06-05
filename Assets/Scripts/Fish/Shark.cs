using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Shark : MonoBehaviour
{
    public UnityEvent Near;
    bool turning = false;
    public float speed;

    private void Update()
    {
        TruningCheck();
        if (turning)
        {
            Vector3 direction = Vector3.zero - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 3 * Time.deltaTime);

            //speed = Random.RandomRange(1, 2);
        }
        else
        {
            if (Random.Range(0, 8) < 1)
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Shakr_Flock.goalPos), 3 * Time.deltaTime);
        }


        transform.Translate(0, 0, Time.deltaTime * 3);
    }

    void TruningCheck()
    {
        if (Vector3.Distance(transform.position, Vector3.zero) >= GlobalFlock.tankSize)
            turning = true;
        else
            turning = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Fish")
        { 
            var fish = other.transform.GetComponent<Small_Fish>();
            fish.Run(transform.position);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "fish")
        {
            var fish = collision.transform.GetComponent<Small_Fish>();
            fish.Run(transform.position);
        }

    }
}
