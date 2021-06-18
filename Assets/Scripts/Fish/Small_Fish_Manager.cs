using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Small_Fish_Manager : MonoBehaviour
{
    public GameObject Prfab;
    public GameObject Goal_Object;
    static int num = 30;
    public static GameObject[] AllPrefab = new GameObject[num];

    public static int Range = 7;

    public static Vector3 goalPos = Vector3.zero;

    private void Start()
    {
        //for (int i = 0; i < num; i++)
        //{
        //    Vector3 pos = new Vector3(Random.Range(-Range, Range),
        //                              Random.Range(-Range, Range),
        //                              Random.Range(-Range, Range));
        //    AllPrefab[i] = Instantiate(Prfab, pos, Quaternion.identity);
        //}
    }

    private void Update()
    {
        if (Random.Range(0, 10000) < 50)
        {
            goalPos = new Vector3(Random.Range(-Range, Range),
                                      Random.Range(-Range, Range),
                                      Random.Range(-Range, Range));
            Goal_Object.transform.position = goalPos;
        }
    }
}
