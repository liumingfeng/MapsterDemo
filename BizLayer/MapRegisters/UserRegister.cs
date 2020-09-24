using System;
using System.Collections.Generic;
using System.Text;
using DataLayer;
using Mapster;

namespace BizLayer.MapRegisters
{
    public class UserRegister : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<UserInfo, User>()
                .Map(dest => dest.UserAge, src => src.Age)
                .NameMatchingStrategy(NameMatchingStrategy.IgnoreCase);
            //.IgnoreNullValues(true)//忽略空值映射
            //.Ignore(dest => dest.UserAge)//忽略指定字段
            //.IgnoreAttribute(typeof(DataMemberAttribute))//忽略指定特性的字段
            //.NameMatchingStrategy(NameMatchingStrategy.IgnoreCase)//忽略字段名称的大小写
            //.IgnoreNonMapped(true);//忽略除以上配置的所有字段
            //.IgnoreMember((member, side) => !member.Type.Namespace.StartsWith("System"));//实现更细致的忽略规则
        }
    }
}
