namespace PaymentApi.Utils.Constants;

public enum SaleStatusEnum
{
    WaitingForPayment,
    PaymentApproved,
    SentToShippingCompany,
    Delivered,
    Canceled
}