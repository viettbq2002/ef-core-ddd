namespace EfCoreDDD.API
{
    public interface IEndpointGroup
    {
        static virtual string? RoutePrefix => null;

        static abstract void Map(RouteGroupBuilder groupBuilder);
    }
}
