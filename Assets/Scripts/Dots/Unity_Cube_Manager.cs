using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unity_Cube_Manager : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    public int num;
    public GameObject[] cube;

    private void Start()
    {
        cube = new GameObject[num];
        for (int i = 0; i < num; i++)
        {
            Vector3 pos = new Vector3(Random.Range(-5, 5),
                                     Random.Range(-5, 5),
                                     Random.Range(-5, 5));
            cube[i] = Instantiate(prefab, pos, Quaternion.identity);
        }
    }
}
