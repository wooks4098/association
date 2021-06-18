using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
public class Dots_Smallfish_Move : SystemBase
{
    float3 goalPos;
    protected override void OnUpdate()
    {
       
       float deltaTime = Time.DeltaTime;

        Entities.ForEach((ref Translation translation, ref Rotation rot, ref Dots_Smallfish_base moveData) => {


            float3 Dir = translation.Value - goalPos;
            quaternion targetRotation = quaternion.LookRotationSafe(Dir, math.up());
            rot.Value = math.slerp(rot.Value, targetRotation, moveData.Speed);

            //float3 normalizedDir = math.normalizesafe(moveData.direction);
            //translation.Value += normalizedDir * moveData.Speed * deltaTime;

            //quaternion targetRotation = quaternion.LookRotationSafe(moveData.direction, math.up());
            //rot.Value = math.slerp(rot.Value, targetRotation, moveData.Speed);

        }).ScheduleParallel();
      
    }

    //public void Execute(Translation trans, Rotation rot, ref MoveData move, ref InputFloat2Data input)
    //{
    //    direction = math.normalizesafe(new float3(input.Results.x, 0, input.Results.y));
    //    trans.Value += math.mul(rot.Value, direction) * move.moveSpeed * deltaTime;
    //}
}

