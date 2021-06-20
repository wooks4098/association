using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Collections;
using Unity.Rendering;

public class Creat : MonoBehaviour
{
    public static Creat instance;

    public int entities_Num;
    [SerializeField] private Mesh mesh;
    [SerializeField] private Material material;

    public float neighbourDistance;
    public float Speed;


    float Range = Small_Fish_Manager.Range;
    void Start()
    {
        EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;//entitymanager ����
        //entityManager.CreateEntity(typeof(Dots_Smallfish_base));//entity ����

        EntityArchetype entityArchetype = entityManager.CreateArchetype(
            typeof(Dots_Smallfish_base),
           // typeof(Translation),
            typeof(Rotation),
            typeof(RenderMesh),
            typeof(RenderBounds),
            typeof(LocalToWorld)
            );//entity �Ӽ� ����

        NativeArray<Entity> entityArry = new NativeArray<Entity>(entities_Num, Allocator.Temp);
        entityManager.CreateEntity(entityArchetype, entityArry);
        for (int i = 0; i < entities_Num; i++)
        {
            Entity entity = entityArry[i];

            entityManager.AddComponentData(entity, new Dots_Smallfish_base
            {
                Speed = Random.Range(1, 2),
                trunSpeed = 3f,
            });

        //    entityManager.AddComponentData(entity, new Translation
        //    {
        //        Value = new Vector3(Random.Range(-Range, Range),
        //                              Random.Range(-Range, Range),
        //                              Random.Range(-Range, Range)),
        //});
            entityManager.SetSharedComponentData(entity, new RenderMesh
            {
                mesh = mesh,
                material = material,
                castShadows = UnityEngine.Rendering.ShadowCastingMode.On
            });
        }


    }
}
