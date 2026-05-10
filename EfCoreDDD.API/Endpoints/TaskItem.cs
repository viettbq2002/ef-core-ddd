namespace EfCoreDDD.API.Endpoints;

public class TaskItem : IEndpointGroup
{
    public static void Map(RouteGroupBuilder groupBuilder)
    {
        groupBuilder.MapGet("/", () => "Test");
    }
    
}
