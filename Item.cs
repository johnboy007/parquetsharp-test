namespace ParquetMapper.Model;

public class Item
{
    public long ItemId { get; set; }
    public long ProductId { get; set; }
    public long BaseItemId { get; set; }
    public int AmdmStatusCode { get; set; }
    public string ItemSize { get; set; }
    public string FulfillmentSystemSku { get; set; }
    public bool IsDiscountable { get; set; }
    public bool IsTaxable { get; set; }
    public bool IsDeleted { get; set; }
    public bool IsSmartExclusion { get; set; }
    public bool IsSellable { get; set; }
    public long SortOrder { get; set; }
    public long MaxQuantityPerOrder { get; set; }
    public double Cost { get; set; }
    public string Upc { get; set; }
    public string Ean { get; set; }
    public string KitbagBarcode { get; set; }
    public string JcpItemSku { get; set; }
    public string JcpPricePointLot { get; set; }
    public string DtgItemSku { get; set; }
    public double Width { get; set; }
    public double Weight { get; set; }
    public double Height { get; set; }
    public double Length { get; set; }
    public string PromiseShipDate { get; set; }
}