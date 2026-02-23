using FinTracker.Domain.Transaction.Model;
using Microsoft.EntityFrameworkCore;

namespace FinTracker.Persistence.Context;

public static class TransactionContext
{
    public static ModelBuilder BuildTransactionScheme(this ModelBuilder modelBuilder)
    {
        string TransactionScheme = "transaction";

        return modelBuilder
            .HasDefaultSchema(TransactionScheme).Entity<Category>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasConversion(id => id.Value, value => new(value));
                entity.Property(e => e.UserId).IsRequired();
            })
            .HasDefaultSchema(TransactionScheme).Entity<Transaction>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasConversion(id => id.Value, value => new(value));

                entity.Property(e => e.CategoryId).IsRequired();
                entity.Property(e => e.CategoryId).HasConversion(id => id.Value, value => new(value));
                entity.HasOne<Category>().WithMany().HasForeignKey(c => c.CategoryId);

                entity.Property(e => e.Money).HasConversion(
                        m => $"{m.Amount} {m.Currency.Value}",
                        v => Money.New(double.Parse(v.Split(" ")[0]), Currency.FromString(v.Split(" ")[1])))
                    .IsRequired();
                entity.Property(e => e.Type).HasConversion(v => v.Value, v => TransactionType.FromString(v)).IsRequired();
            });
    }
}