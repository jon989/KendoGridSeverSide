public class SalesOrderDetail
{
    public string SalesOrderID { get; set; }
    public string SalesOrderDetailID { get; set; }
    public string CarrierTrackingNumber { get; set; }
    public string OrderQty { get; set; }
    public string ProductID { get; set; }
    public string SpecialOfferID { get; set; }
    public string UnitPrice { get; set; }
    public string UnitPriceDiscount { get; set; }
    public double LineTotal { get; set; }
    public string rowguid { get; set; }
    public string ModifiedDate { get; set; }
    public double Sum { get; set; }
}