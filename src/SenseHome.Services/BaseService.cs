using AutoMapper;

namespace SenseHome.Services
{
    public class BaseService
    {
        public IMapper mapper;
        public BaseService(IMapper mapper)
        {
            this.mapper = mapper;
        }
    }
}
