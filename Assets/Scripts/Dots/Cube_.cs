using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube_ : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 20f, 0) * Time.deltaTime);
    }
}
