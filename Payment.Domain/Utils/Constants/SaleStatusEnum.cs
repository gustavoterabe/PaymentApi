namespace Payment.Domain.Utils.Constants;

public enum SaleStatusEnum
{
    WaitingForPayment,
    PaymentApproved,
    SentToShippingCompany,
    Delivered,
    Canceled
}