namespace ParquetMapper.Model;

public class PropertyValue
{
    public string Name { get; set; }
    public string Value { get; set; }
    public string DataType { get; set; }
    public string Locale { get; set; }
    public int OverrideGroupId { get; set; }
    public long BulletSortOrder { get; set; }
    public long MdmId { get; set; }
}