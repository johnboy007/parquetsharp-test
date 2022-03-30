using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ParquetMapper;

[Collection("ParquetMapperTests")]
public class ParquetMapperTests
{
    private const string ParquetsTestFullSnappyParquet = "full-00000.snappy.parquet";


    [Fact]
    public async Task ParquetMapper_WithDelta_Returns41_PartialDtos()
    {
        const int expected = 41;
        await using Stream fileStream = File.OpenRead(ParquetsTestFullSnappyParquet);
        var parquetMapper = new ParquetMapper();

        var mapProductAndCatalogs = parquetMapper.MapPartialIds(fileStream);

        Assert.Equal(expected, mapProductAndCatalogs.Count());
    }

    [Fact]
    public async Task ParquetMapper_WithFull_Returns41_PartialDtos()
    {
        const int expected = 41;
        await using Stream fileStream = File.OpenRead(ParquetsTestFullSnappyParquet);
        var parquetMapper = new ParquetMapper();
        var mapProductAndCatalogs = parquetMapper.MapPartialIds(fileStream);

        var firstDto = mapProductAndCatalogs.First();

        Assert.Equal(expected, mapProductAndCatalogs.Count());
        Assert.Equal(13376128, firstDto.ProductId);
        Assert.Equal(new List<long> { 488, 507 }, firstDto.Catalogs);
    }

    [Fact]
    public async Task ParquetMapper_WithParquet_MapsSimpleFields()
    {
        await using Stream fileStream = File.OpenRead(ParquetsTestFullSnappyParquet);
        var parquetMapper = new ParquetMapper();

        var mapProductAndCatalogs = parquetMapper.Map(fileStream);

        var firstDto = mapProductAndCatalogs.First();

        Assert.Equal(13376128, firstDto.ProductId);
        Assert.Equal(0, firstDto.BaseproductId);
        Assert.Equal(1, firstDto.AmdmStatusCode);
        Assert.Equal("10000000", firstDto.StyleId);
        Assert.Equal("AUTMER", firstDto.Dcs);
        Assert.Equal(42.390000000000001, firstDto.Cost);
        Assert.Equal(30437, firstDto.MerchClassId);
        Assert.Equal(new List<long> { 488, 507 }, firstDto.Catalogs);
        Assert.Equal(new List<long> { 7017 }, firstDto.Categories);
        Assert.Equal(new List<string> { null }, firstDto.ProductGroups);
        Assert.Equal(new List<string> { "No Size" }, firstDto.Sizes);
    }

    [Fact]
    public async Task ParquetMapper_WithParquet_MapsPropertyValues()
    {
        await using Stream fileStream = File.OpenRead(ParquetsTestFullSnappyParquet);
        var parquetMapper = new ParquetMapper();

        var mapProductAndCatalogs = parquetMapper.Map(fileStream);

        var firstDto = mapProductAndCatalogs.First().PropertyValues.First();

        Assert.Equal(1, firstDto.OverrideGroupId);
        Assert.Equal("1", firstDto.Value);
        Assert.Equal("Boolean", firstDto.DataType);
        Assert.Equal("AMDMIsDropShip", firstDto.Name);
        Assert.Equal("en-US", firstDto.Locale);
        Assert.Equal(0, firstDto.MdmId);
        Assert.Equal(0, firstDto.BulletSortOrder);
    }


    [Fact]
    public async Task ParquetMapper_WithParquet_MapsOverriddenPropertyValues()
    {
        await using Stream fileStream = File.OpenRead(ParquetsTestFullSnappyParquet);
        var parquetMapper = new ParquetMapper();

        var mapProductAndCatalogs = parquetMapper.Map(fileStream);

        var firstDto = mapProductAndCatalogs.First().OverriddenPropertyValues.First();

        Assert.Equal(1, firstDto.OverrideGroupId);
        Assert.Equal("ss2/p-13376128_u-6xgijbgj95ilkyynrgqb_v-8a662e558493455795f4034d0399cc4d.jpg", firstDto.Value);
        Assert.Equal("String", firstDto.DataType);
        Assert.Equal("ImageURL", firstDto.Name);
        Assert.Equal("en-US", firstDto.Locale);
        Assert.Equal(0, firstDto.MdmId);
        Assert.Equal(0, firstDto.BulletSortOrder);
    }

    [Fact]
    public async Task ParquetMapper_WithParquet_MapsItems()
    {
        await using Stream fileStream = File.OpenRead(ParquetsTestFullSnappyParquet);
        var parquetMapper = new ParquetMapper();

        var mapProductAndCatalogs = parquetMapper.Map(fileStream);

        var firstDto = mapProductAndCatalogs.ElementAt(2).Items.ElementAt(0);
        Assert.Equal(37975481, firstDto.ItemId);
        Assert.Equal(0, firstDto.BaseItemId);
        Assert.Equal(12065638, firstDto.ProductId);
        Assert.Equal(1, firstDto.AmdmStatusCode);
        Assert.Equal(22.32, firstDto.Cost);
        Assert.Equal(50, firstDto.Height);
        Assert.Equal(0.47, firstDto.Weight);
        Assert.Equal(280, firstDto.Width);
        Assert.Equal(300, firstDto.Length);
        Assert.Equal(0, firstDto.MaxQuantityPerOrder);
        Assert.Equal(50, firstDto.SortOrder);

        // Bool
        Assert.False(firstDto.IsDeleted);
        Assert.True(firstDto.IsDiscountable);
        Assert.True(firstDto.IsSellable);
        Assert.False(firstDto.IsSmartExclusion);
        Assert.True(firstDto.IsTaxable);

        //String
        Assert.Equal("Yth XL", firstDto.ItemSize);
        Assert.Null(firstDto.DtgItemSku);
        Assert.Null(firstDto.Ean);
        Assert.Equal("", firstDto.FulfillmentSystemSku);
        Assert.Null(firstDto.JcpItemSku);
        Assert.Null(firstDto.JcpPricePointLot);
        Assert.Equal("195237538843", firstDto.Upc);
        Assert.Equal("72437901", firstDto.KitbagBarcode);
        Assert.Null(firstDto.PromiseShipDate);
    }

    [Fact]
    public async Task ParquetMapper_WithParquet_MapsProductGroups_Returns47()
    {
        const int expected = 47;
        await using Stream fileStream = File.OpenRead(ParquetsTestFullSnappyParquet);
        var parquetMapper = new ParquetMapper();

        var mapProductAndCatalogs = parquetMapper.Map(fileStream);

        var firstDto = mapProductAndCatalogs.ElementAt(1);

        Assert.Equal(expected, firstDto.ProductGroups.Count);

        mapProductAndCatalogs = parquetMapper.Map(fileStream);

        Assert.Equal(expected, mapProductAndCatalogs.ElementAt(1).ProductGroups.Count);
    }
}