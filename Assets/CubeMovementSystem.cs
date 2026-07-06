using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.NetCode;
using Unity.Physics;

[UpdateInGroup(typeof(PredictedSimulationSystemGroup))]
[BurstCompile]
public partial struct CubeMovementSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        const float speed = 4.0F;
        foreach (var (input, velocity) in SystemAPI.Query<RefRO<CubeInput>, RefRW<PhysicsVelocity>>().WithAll<Simulate>())
        {
            var moveInput = new float2(input.ValueRO.Horizontal, input.ValueRO.Vertical);
            moveInput = math.normalizesafe(moveInput) * speed;
            velocity.ValueRW.Linear = new float3(moveInput.x, velocity.ValueRO.Linear.y, moveInput.y);
        }
    }
}
