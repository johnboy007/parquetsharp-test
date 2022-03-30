using System.Collections.Generic;

namespace ParquetMapper.Model;

public class AmdiMasterSetPartialDto
{
    public long ProductId { get; set; }
    public int AmdmStatusCode { get; set; }
    public List<long> Catalogs { get; set; }
}

public class AmdiMasterSetDto
{
    public long ProductId { get; set; }
    public long BaseproductId { get; set; }
    public int AmdmStatusCode { get; set; }
    public string StyleId { get; set; }
    public string Dcs { get; set; }
    public List<long> Catalogs { get; set; }
    public List<long> Categories { get; set; }
    public List<Item> Items { get; set; }
    public List<PropertyValue> PropertyValues { get; set; }
    public List<PropertyValue> OverriddenPropertyValues { get; set; }
    public List<string> ProductGroups { get; set; }
    public long MerchClassId { get; set; }
    public double Cost { get; set; }
    public List<string> Sizes { get; set; }
}