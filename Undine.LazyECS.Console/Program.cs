// See https://aka.ms/new-console-template for more information
using Undine.Core;
using Undine.LazyECS;

Console.WriteLine("Hello, World!");
var container = new LazyEcsContainer();
container.AddSystem(new SpeedSystem());
container.AddSystem(new AccelerationSystem());

var positionLogger = container.GetSystem(new PositionLogger());
container.Init();
var entity = container.CreateNewEntity();
entity.AddComponent(new PositionComponent());
entity.AddComponent(new VelocityComponent()
{
    x = 0,
    y = 10,
});
entity.AddComponent(new AccelerationComponent()
{
    x = 0,
    y = -1
});

for (int i = 0; i < 20; i++)
{
    container.Run();
    //Console.WriteLine("Y =" + entity.GetComponent<PositionComponent>().y);
    positionLogger.ProcessAll();
}

public class PositionLogger : UnifiedSystem<PositionComponent>
{
    public override void ProcessSingleEntity(int entityId, ref PositionComponent a)
    {
        Console.WriteLine("Y =" + a.y);
    }
}

public class SpeedSystem : UnifiedSystem<PositionComponent, VelocityComponent>
{
    public override void ProcessSingleEntity(int entityId, ref PositionComponent a, ref VelocityComponent b)
    {
        a.x += b.x;
        a.y += b.y;
    }
}

public class AccelerationSystem : UnifiedSystem<VelocityComponent, AccelerationComponent>
{
    public override void ProcessSingleEntity(int entityId, ref VelocityComponent a, ref AccelerationComponent b)
    {
        a.x += b.x;
        a.y += b.y;
    }
}

public struct PositionComponent
{
    public float x;
    public float y;

    public override string ToString()
    {
        return y.ToString();
    }
}

public struct VelocityComponent
{
    public float x;
    public float y;
}

public struct AccelerationComponent
{
    public float x;
    public float y;
}