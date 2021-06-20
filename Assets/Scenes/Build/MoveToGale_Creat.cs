using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Collections;
using Unity.Rendering;
//using Unity.Mathematics;

public class MoveToGale_Creat : MonoBehaviour
{
    public static MoveToGale_Creat instance;

    public int entities_Num;
    [SerializeField] private Mesh mesh;
    [SerializeField] private Material material;

    public float neighbourDistance;
    public float Speed;


    float Range = Small_Fish_Manager.Range;
    void Start()
    {
        instance = this;
        EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;//entitymanager 积己

        EntityArchetype entityArchetype = entityManager.CreateArchetype(
            typeof(Dots_Smallfish_base),
            typeof(Rotation),
            typeof(RenderMesh),
            typeof(RenderBounds),
            typeof(LocalToWorld),
            typeof(Translation)
            );//entity 加己 瘤沥

        NativeArray<Entity> entityArry = new NativeArray<Entity>(entities_Num, Allocator.Temp);
        entityManager.CreateEntity(entityArchetype, entityArry);
        for (int i = 0; i < entities_Num; i++)
        {
            Entity entity = entityArry[i];

            entityManager.AddComponentData(entity, new Dots_Smallfish_base
            {
                Speed = UnityEngine.Random.Range(1, 2),
                trunSpeed = 3f,
            });
            //entityManager.SetComponentData(entity, new LocalToWorld
            //{
            //    Value = float4x4.TRS(new float3(UnityEngine.Random.Range(-Range / 2f, Range / 2f),
            //                                    UnityEngine.Random.Range(-Range / 2f, Range / 2f),
            //                                    UnityEngine.Random.Range(-Range / 2f, Range / 2f)),
            //                           quaternion.Euler(UnityEngine.Random.Range(-360f, 360f),
            //                                            UnityEngine.Random.Range(-360f, 360f),
            //                                            UnityEngine.Random.Range(-360f, 360f)), new float3(1f))
            //});
            entityManager.AddComponentData(entity, new Translation
            {
                Value = new Vector3(Random.Range(-Range, Range),
                                      Random.Range(-Range, Range),
                                      Random.Range(-Range, Range)),
            });
            entityManager.SetSharedComponentData(entity, new RenderMesh
            {
                mesh = mesh,
                material = material,
                castShadows = UnityEngine.Rendering.ShadowCastingMode.On
            });
        }


    }
}
