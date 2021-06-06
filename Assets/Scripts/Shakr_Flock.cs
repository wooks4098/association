using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shakr_Flock : MonoBehaviour
{
    public static Vector3 goalPos = Vector3.zero;
    public int tankSize = 13;


    private void Update()
    {
        if (Random.Range(0, 10000) < 10)
        {
            goalPos = new Vector3(Random.Range(-tankSize, tankSize),
                                      Random.Range(-tankSize, tankSize),
                                      Random.Range(-tankSize, tankSize));
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(goalPos), 3 * Time.deltaTime);

        }

    }
}
