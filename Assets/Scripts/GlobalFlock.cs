using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalFlock : MonoBehaviour
{
    public GameObject Prfab;
    public GameObject goalPrefab;
    static int num = 30;
    public static GameObject[] AllPrefab = new GameObject[num];

    public static int tankSize = 10;

    public static Vector3 goalPos = Vector3.zero;

    private void Start()
    {
        for(int i = 0; i<num; i++)
        {
            Vector3 pos = new Vector3(Random.Range(-tankSize, tankSize), 
                                      Random.Range(-tankSize, tankSize), 
                                      Random.Range(-tankSize, tankSize));
            AllPrefab[i] = Instantiate(Prfab, pos, Quaternion.identity);
        }
    }

    private void Update()
    {
        if(Random.Range(0,10000)<50)
        {
            goalPos = new Vector3(Random.Range(-tankSize, tankSize),
                                      Random.Range(-tankSize, tankSize),
                                      Random.Range(-tankSize, tankSize));
            goalPrefab.transform.position = goalPos;
        }
    }

}
