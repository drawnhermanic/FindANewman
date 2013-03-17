using FindANewman.Domain.Entities;
using FluentNHibernate.Mapping;

namespace FindANewman.Data.Mappings
{
    public class UserMapping : ClassMap<User>
    {
        public UserMapping()
        {
            Schema("dbo");
            Table("[User]");

            Id(entity => entity.Id).Column("UserId").GeneratedBy.HiLo("1000", pb => pb.AddParam("TableName", "User"));

            Map(entity => entity.EmailAddress);

        }
    }
}
