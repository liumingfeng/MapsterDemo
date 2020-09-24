using BizLayer;
using DataLayer;
using Mapster;

namespace ConsoleApp15.MapRegisters
{
    public class UserViewRegister : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<User, UserView>()
                .Map(dest => dest.UserLike, src => src.Like)
                .Map(dest => dest.UserName, src => src.Name)
                .NameMatchingStrategy(NameMatchingStrategy.IgnoreCase);
        }
    }
}
