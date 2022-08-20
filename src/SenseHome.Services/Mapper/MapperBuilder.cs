namespace SenseHome.Services.Mapper
{
    public class MapperBuilder : AutoMapper.MapperConfiguration
    {
        public MapperBuilder() : base(config =>
        {
            config.AddProfile<MapperProfile>();
        })
        { }
    }
}
