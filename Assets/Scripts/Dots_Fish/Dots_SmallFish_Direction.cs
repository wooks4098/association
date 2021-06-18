using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

public class Dots_SmallFish_Direction : SystemBase
{
    protected override void OnUpdate()
    {
        float deltaTime = Time.DeltaTime;

        Entities.ForEach((ref Translation pos, ref Dots_Smallfish_base Move_Base) => {

            //Move_Base.direction.x = 1;
            //Move_Base.direction.y = 0;
            //Move_Base.direction.z = 1;
            //translation.Value.y += 3 * deltaTime;
        }).ScheduleParallel();
    }
}
