using FinTracker.Domain.Presentation.Model;
using Microsoft.EntityFrameworkCore;

namespace FinTracker.Persistence.Context;

public static class PresentationContext
{
    public static ModelBuilder BuildPresentationScheme(this ModelBuilder modelBuilder)
    {
        string Scheme = "presentation";

        return modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("Categories", Scheme);
            entity.HasKey(e => e.Id).HasName("PK_Presentation_Categories");;
            entity.Property(e => e.Id).HasConversion(id => id.Value, value => EntityId.From(value));
            entity.Property(e => e.UserId).HasConversion(id => id.Value, value => EntityId.From(value));
            
            entity.OwnsOne(e => e.Information, info =>
            {
                info.Property(i => i.Title).HasColumnName("Title").IsRequired();
                info.Property(i => i.Description).HasColumnName("Description");
            });

            entity.OwnsOne(e => e.Appearance, appearance =>
                appearance.Property(a => a.Color).HasColumnName("Color").HasConversion(color => color.Value, value => HexColor.From(value))
            );
        });
    }
}