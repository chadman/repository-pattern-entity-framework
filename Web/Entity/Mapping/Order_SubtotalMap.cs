using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Web.Entity.Mapping
{
    public class Order_SubtotalMap : EntityTypeConfiguration<Order_Subtotal>
    {
        public Order_SubtotalMap()
        {
            // Primary Key
            this.HasKey(t => t.OrderID);

            // Properties
            this.Property(t => t.OrderID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("Order Subtotals");
            this.Property(t => t.OrderID).HasColumnName("OrderID");
            this.Property(t => t.Subtotal).HasColumnName("Subtotal");
        }
    }
}
