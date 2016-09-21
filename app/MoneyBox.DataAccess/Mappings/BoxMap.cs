using System.Data.Entity.ModelConfiguration;
using MoneyBox.Domain;

namespace MoneyBox.DataAccess.Mappings
{
    public class BoxMap : EntityTypeConfiguration<Box>
    {
        public BoxMap()
        {
            ToTable("boxes");
        }
    }
}