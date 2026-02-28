using FinTracker.Domain.Transaction.Model;
using Microsoft.EntityFrameworkCore;

namespace FinTracker.Persistence.Context;

public static class TransactionContext
{
    public static ModelBuilder BuildTransactionScheme(this ModelBuilder modelBuilder)
    {
        string Scheme = "transaction";

        return modelBuilder
            .Entity<Category>(entity =>
            {
                entity.ToTable("Categories", Scheme);
                entity.HasKey(e => e.Id).HasName("PK_Transaction_Categories");
                entity.Property(e => e.Id).HasConversion(id => id.Value, value => new(value));
                entity.Property(e => e.UserId).HasConversion(id => id.Value, value => UserId.From(value)).IsRequired();
            })
            .Entity<Transaction>(entity =>
            {
                entity.ToTable("Transactions", Scheme);
                entity.HasKey(e => e.Id).HasName("PK_Transaction_Transactions");
                entity.Property(e => e.Id).HasConversion(id => id.Value, value => new(value));

                entity.Property(e => e.CategoryId).IsRequired();
                entity.Property(e => e.CategoryId).HasConversion(id => id.Value, value => new(value));
                entity.HasOne<Category>().WithMany().HasForeignKey(c => c.CategoryId);

                entity.OwnsOne(e => e.Money, money =>
                {
                    money.Property(m => m.Amount).HasColumnName("MoneyAmount").IsRequired();
                    money.Property(m => m.Currency).HasConversion(c => c.Value, v => Currency.FromString(v)).HasColumnName("MoneyCurrency").IsRequired();
                });

                entity.Property(e => e.Type).HasConversion(v => v.Value, v => TransactionType.FromString(v)).IsRequired();
            });
    }
}