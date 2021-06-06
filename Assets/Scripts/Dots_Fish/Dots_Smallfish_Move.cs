using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
public class Dots_Smallfish_Move : SystemBase
{
    protected override void OnUpdate()
    {
        float deltaTime = Time.DeltaTime;

        Entities.ForEach((ref Translation translation, ref Dots_Smallfish_base Move_Base) => {

            //translation.Value =  localToWorld.Position + localToWorld.Forward * Move_Base.Speed * deltaTime;

        }).ScheduleParallel(); 
    }

    //public void Execute(Translation trans, Rotation rot, ref MoveData move, ref InputFloat2Data input)
    //{
    //    direction = math.normalizesafe(new float3(input.Results.x, 0, input.Results.y));
    //    trans.Value += math.mul(rot.Value, direction) * move.moveSpeed * deltaTime;
    //}
}
