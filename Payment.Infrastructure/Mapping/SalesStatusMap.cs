using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payment.Domain.Models;
using Payment.Domain.Utils.Constants;

namespace Payment.Infrastructure.Mapping;

public class SalesStatusMap : IEntityTypeConfiguration<SalesStatus>
{
    public void Configure(EntityTypeBuilder<SalesStatus> builder)
    {
        builder.ToTable("SalesStatus");

        builder.HasKey(ss => ss.Id);

        builder.Property(ss => ss.Name)
            .HasMaxLength(30)
            .IsRequired();

        builder.HasData(
            new SalesStatus { Id = SaleStatusEnum.WaitingForPayment, Name = "Waiting for payment" },
            new SalesStatus { Id = SaleStatusEnum.PaymentApproved, Name = "Payment approved" },
            new SalesStatus { Id = SaleStatusEnum.SentToShippingCompany, Name = "Sent to shipping company" },
            new SalesStatus { Id = SaleStatusEnum.Delivered, Name = "Delivered" },
            new SalesStatus { Id = SaleStatusEnum.Canceled, Name = "Canceled" }
            );
    }
}