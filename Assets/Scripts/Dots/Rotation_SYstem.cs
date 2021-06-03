using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

public class Rotation_SYstem : SystemBase
{
    protected override void OnUpdate()
    {
        float deltaTime = Time.DeltaTime;

        Entities.ForEach((ref Rotation rotation, in Cube_Rotation cube_Rotation) => {

            rotation.Value = math.mul(rotation.Value, quaternion.RotateY(cube_Rotation.rotationSpeed * deltaTime));


        }).Schedule();
    }
}
