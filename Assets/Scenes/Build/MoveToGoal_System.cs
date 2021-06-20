using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Collections;

public class MoveToGoal_System : SystemBase
{
    protected override void OnUpdate()
    {

        float3 goalPos = (float3)Small_Fish_Manager.goalPos;
        float deltaTime = Time.DeltaTime;

        Entities.ForEach((ref Translation translation, ref Rotation rot, ref Dots_Smallfish_base moveData) => {

            float3 Dir = translation.Value - goalPos;
            float3 mypos = translation.Value;
            translation.Value = mypos - math.normalizesafe(Dir) * moveData.Speed * deltaTime;

        }).ScheduleParallel();
    }
}
