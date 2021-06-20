using UnityEngine;
using Unity.Entities;
using Unity.Rendering;
using Unity.Transforms;
using Unity.Collections;
using Unity.Mathematics;

public class BoidController : MonoBehaviour
{

    public static BoidController Instance;

    [SerializeField] private int boidAmount;
    [SerializeField] private Mesh sharedMesh;
    [SerializeField] private Material sharedMaterial;

    public float boidSpeed; //이동속도
    public float boidPerceptionRadius; //이웃거리
    public float cageSize; //상자크기

    public float separationWeight; //분리 
    public float cohesionWeight; //응집력
    public float alignmentWeight; //조정

    public float avoidWallsWeight; //일정거리
    public float avoidWallsTurnDist; //회전

    private void Awake()
    {

        Instance = this;

        EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

        EntityArchetype boidArchetype = entityManager.CreateArchetype(
            typeof(BoidECS),
            typeof(RenderMesh),
            typeof(RenderBounds),
            typeof(LocalToWorld)
        );

        NativeArray<Entity> boidArray = new NativeArray<Entity>(boidAmount, Allocator.Temp);
        entityManager.CreateEntity(boidArchetype, boidArray);

        for (int i = 0; i < boidArray.Length; i++)
        {
            Unity.Mathematics.Random rand = new Unity.Mathematics.Random((uint)i + 1);
            entityManager.SetComponentData(boidArray[i], new LocalToWorld
            {
                Value = float4x4.TRS(
                    RandomPosition(),
                    RandomRotation(),
                    new float3(1f))
            });
            entityManager.SetSharedComponentData(boidArray[i], new RenderMesh
            {
                mesh = sharedMesh,
                material = sharedMaterial,
            });
        }

        boidArray.Dispose();
    }

    private float3 RandomPosition()
    {
        return new float3(
            UnityEngine.Random.Range(-cageSize / 2f, cageSize / 2f),
            UnityEngine.Random.Range(-cageSize / 2f, cageSize / 2f),
            UnityEngine.Random.Range(-cageSize / 2f, cageSize / 2f)
        );
    }
    private quaternion RandomRotation()
    {
        return quaternion.Euler(
            UnityEngine.Random.Range(-360f, 360f),
            UnityEngine.Random.Range(-360f, 360f),
            UnityEngine.Random.Range(-360f, 360f)
        );
    }

    private void OnDrawGizmos()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(
            Vector3.zero,
            new Vector3(
                cageSize,
                cageSize,
                cageSize
            )
        );
    }
}
