using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.NetCode;
using Unity.Physics;
using Unity.Transforms;

[UpdateInGroup(typeof(PredictedSimulationSystemGroup))]
[BurstCompile]
public partial struct CubeMovementSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        const float speed = 1.0F;
        
        foreach (var (input, velocity) in SystemAPI.Query<RefRO<CubeInput>, RefRW<PhysicsVelocity>>().WithAll<Simulate>())
        {
            var moveInput = new float2(input.ValueRO.Horizontal, input.ValueRO.Vertical);
            moveInput = math.normalizesafe(moveInput) * speed;
            velocity.ValueRW.Linear += new float3(moveInput.x, 0.0F, moveInput.y);
        }
    }
}

[BurstCompile]
[WorldSystemFilter(WorldSystemFilterFlags.ServerSimulation)] 
public partial struct CubeServerMovementSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<ScoreBufferElement>();
    }
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        foreach (var (velocity, trans, owner) in SystemAPI.Query<RefRW<PhysicsVelocity>, RefRW<LocalTransform>, RefRO<GhostOwner>>().WithAll<Simulate>())
        {
            if(trans.ValueRO.Position.y < -1.0F)
            {
                velocity.ValueRW.Linear = new float3(0.0F, 0.0F, 0.0F);
                velocity.ValueRW.Angular = new float3(0.0F, 0.0F, 0.0F);
                trans.ValueRW.Rotation = new float4(0.0F, 0.0F, 0.0F, 1.0F);
                trans.ValueRW.Position = new float3(0.0F, 0.5F, 0.0F);
                var score = SystemAPI.GetSingletonBuffer<ScoreBufferElement>();
                ScoreBufferElement lastElementValue = score[score.Length - 1];
                if(owner.ValueRO.NetworkId > 1)
                {
                    ++lastElementValue.redScore;
                }
                else
                {
                    ++lastElementValue.blueScore;
                }
                score.Add(lastElementValue);
            }
        }
    }
}