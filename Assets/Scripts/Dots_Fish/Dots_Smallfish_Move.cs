using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Collections;
public class Dots_Smallfish_Move : ComponentSystem
{
    private Creat CreatValue;
    int index = 0;
    protected override void OnUpdate()
    {
        if (!CreatValue)
            CreatValue = Creat.instance;
        if (!CreatValue)
            return;
        EntityManager entitymanager = World.EntityManager;

        float3 goalPos = (float3)Small_Fish_Manager.goalPos;
        float deltaTime = Time.DeltaTime;

        EntityQuery boidQuery = GetEntityQuery(ComponentType.ReadOnly<Dots_Smallfish_base>(), ComponentType.ReadOnly<LocalToWorld>());
        NativeArray<float4x4> newPositions = new NativeArray<float4x4>(boidQuery.CalculateEntityCount(), Allocator.Temp);


        index = 0;
        Entities.WithAll<Dots_Smallfish_base>().ForEach((Entity entity, ref LocalToWorld localToWorld) =>
        {
            float3 MyPos = localToWorld.Position; //내위치

            float3 center = float3.zero;
            float3 vavoid = float3.zero;
            int groupSize = 0;

            Entities.WithAll<Dots_Smallfish_base>().ForEach((Entity otherentity, ref LocalToWorld otherlocalToWorld) =>
            {//다른 entity와 위치를 비교해서 내 위치 정하기

                if (entity != otherentity)
                {//다른 entity만
                    float distance = math.length(MyPos - otherlocalToWorld.Position);
                    if (distance < CreatValue.neighbourDistance)
                    {//가까운 거리에 있는 entity만 적용

                        center += otherlocalToWorld.Position;
                        if (distance < 1.3f)
                        {//회피
                                 vavoid += (MyPos - otherlocalToWorld.Position);
                        }
                        groupSize++;
                    }


                }
            });


            float3 force = float3.zero;

            if (groupSize > 0)
            {
                center = center / groupSize + (goalPos - MyPos); //중심 계산

                force = (center + vavoid) - MyPos;

            }
            float3 velocity = localToWorld.Forward * CreatValue.Speed;
            velocity += force * Time.DeltaTime;
            velocity = math.normalize(velocity) * CreatValue.Speed;

            newPositions[index] = float4x4.TRS(
                localToWorld.Position + velocity * Time.DeltaTime,
                quaternion.LookRotationSafe(velocity, localToWorld.Up),
                new float3(1f)
            );


        });


        index = 0;
        Entities.WithAll<Dots_Smallfish_base>().ForEach((Entity entity, ref LocalToWorld localToWorld) =>
        {
            //이동코드
            localToWorld.Value = newPositions[index++];
        });

    }

   

}

// math.normalizesafe