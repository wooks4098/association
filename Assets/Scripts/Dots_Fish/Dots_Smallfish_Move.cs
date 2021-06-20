using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Collections;
public class Dots_Smallfish_Move : SystemBase
{
    private Creat CreatValue;

    int index = 0;
    protected override void OnUpdate()
    {
        if (!CreatValue)
        {
            CreatValue = Creat.instance;

        }

        EntityManager entitymanager = World.EntityManager;

        float3 goalPos = (float3)Small_Fish_Manager.goalPos;
        float deltaTime = Time.DeltaTime;

        EntityQuery boidQuery = GetEntityQuery(ComponentType.ReadOnly<Dots_Smallfish_base>(), ComponentType.ReadOnly<LocalToWorld>());
        NativeArray<float4x4> newPositions = new NativeArray<float4x4>(boidQuery.CalculateEntityCount(), Allocator.Temp);


        index = 0;
        Entities.WithAll<Dots_Smallfish_base>().ForEach((Entity entity, ref LocalToWorld localToWorld) =>
        {
            float3 MyPos = localToWorld.Position; //����ġ

            float3 center = float3.zero;
            float3 vavoid = float3.zero;
            int groupSize = 0;


                //NativeArray<Entity> entityArry = entitymanager.GetAllEntities(Allocator.Temp);
                //for(int i = 0; i<entityArry.Length; i++)
                //{
                //    if (entity != entityArry[i])
                //    {//�ٸ� entity��
                //        float distance = math.length(MyPos - entityArry[i];
                //        if (distance < CreatValue.neighbourDistance)
                //        {//����� �Ÿ��� �ִ� entity�� ����

                //            center += otherlocalToWorld.Position;
                //            if (distance < 1.3f)
                //            {//ȸ��
                //                vavoid += (MyPos - otherlocalToWorld.Position);
                //            }
                //            groupSize++;
                //        }


                //    }
                //}
                //Entities.WithAll<Dots_Smallfish_base>().ForEach((Entity otherentity, ref LocalToWorld otherlocalToWorld) =>
                //{//�ٸ� entity�� ��ġ�� ���ؼ� �� ��ġ ���ϱ�

                //    if (entity != otherentity)
                //    {//�ٸ� entity��
                //        float distance = math.length(MyPos - otherlocalToWorld.Position);
                //        if (distance < CreatValue.neighbourDistance)
                //        {//����� �Ÿ��� �ִ� entity�� ����

                //            center += otherlocalToWorld.Position;
                //            if (distance < 1.3f)
                //            {//ȸ��
                //                vavoid += (MyPos - otherlocalToWorld.Position);
                //            }
                //            groupSize++;
                //        }


                //    }
                //}).WithoutBurst().Run();

                float3 force = float3.zero;

            if (groupSize > 0)
            {
                center = center / groupSize + (goalPos - MyPos); //�߽� ���

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


        }).WithoutBurst().Run();


        index = 0;
        Entities.WithAll<Dots_Smallfish_base>().ForEach((Entity entity, ref LocalToWorld localToWorld) =>
        {
                //�̵��ڵ�
                localToWorld.Value = newPositions[index++];

        }).WithoutBurst().Run();

    }

    //public void Execute(Translation trans, Rotation rot, ref MoveData move, ref InputFloat2Data input)
    //{
    //    direction = math.normalizesafe(new float3(input.Results.x, 0, input.Results.y));
    //    trans.Value += math.mul(rot.Value, direction) * move.moveSpeed * deltaTime;
    //}

}

