using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public float speed;
    float rotationSpeed = 4.0f;
    Vector3 averageHeading;
    Vector3 averagePosition;
    float neighbourDistance = 5.0f; //무리를 짓기 위한 거리

    bool turning = false;

    void Start()
    {
        speed = Random.Range(1f,2);
    }

    void Update()
    {
        TruningCheck();
        if (turning)
        {
            Vector3 direction = Vector3.zero - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);

            speed = Random.RandomRange(1, 2);
        }
        else
        {
            if(Random.Range(0,5)<1)
                ApplyRules();
        }
        transform.Translate(0, 0, Time.deltaTime * speed);
    }

    void TruningCheck()
    {
        if (Vector3.Distance(transform.position, Vector3.zero) >= GlobalFlock.tankSize)
            turning = true;
        else
            turning = false;
    }
    
    void ApplyRules()
    {
        GameObject[] gos;
        gos = GlobalFlock.AllPrefab; //모든 오브젝트 받기

        Vector3 vcentre = Vector3.zero; //중심 벡터
        Vector3 vavoid = Vector3.zero;  //회피 벡터
        float gSpeed = 0.1f;

        Vector3 goalPos = GlobalFlock.goalPos;

        float dist;

        int groupSize = 0;
        foreach(GameObject go in gos)
        {
            if(go != this.gameObject)
            {
                dist = Vector3.Distance(go.transform.position, this.transform.position);
                if(dist <= neighbourDistance)
                {//가까운 거리에 있는 오브젝트만
                    vcentre += go.transform.position;
                    groupSize++;
                    if(dist <1.3f)
                    {//회피
                        vavoid = vavoid + (this.transform.position - go.transform.position);
                    }
                    Flock anotherFlock = go.GetComponent<Flock>();
                    gSpeed = gSpeed + anotherFlock.speed;
                }
            }
        }

        if (groupSize>0) //그룹을 만들었다면
        {
            vcentre = vcentre / groupSize + (goalPos - this.transform.position); //중심 계산
            speed = gSpeed / groupSize; //평균속도 적용

            Vector3 direction = (vcentre + vavoid) - transform.position;
            if (direction != Vector3.zero)
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
        }
    }

    
}
