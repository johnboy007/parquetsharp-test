using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ParquetMapper.Model;
using ParquetSharp;
using ParquetSharp.IO;

namespace ParquetMapper;

public class ParquetMapper : IParquetMapper
{
    public List<AmdiMasterSetDto> Map(Stream stream)
    {
        var amdiMasterSetDtos = new List<AmdiMasterSetDto>();
        var input = new ManagedRandomAccessFile(stream, true);
        using var fileReader = new ParquetFileReader(input);
        try
        {
            for (var rowGroup = 0; rowGroup < fileReader.FileMetaData.NumRowGroups; ++rowGroup)
            {
                using var rowGroupReader = fileReader.RowGroup(rowGroup);
                var groupNumRows = checked((int)rowGroupReader.MetaData.NumRows);

                var productIds = rowGroupReader.Column((int)AmdiColumn.ProductId).LogicalReader<long?>()
                    .ReadAll(groupNumRows);
                var baseProductIds = rowGroupReader.Column((int)AmdiColumn.BaseProductId).LogicalReader<long?>()
                    .ReadAll(groupNumRows);
                var amdmStatusCode = rowGroupReader.Column((int)AmdiColumn.AmdmStatusCode).LogicalReader<int?>()
                    .ReadAll(groupNumRows);
                var styleId = rowGroupReader.Column((int)AmdiColumn.StyleId).LogicalReader<string>()
                    .ReadAll(groupNumRows);
                var dcs = rowGroupReader.Column((int)AmdiColumn.Dcs).LogicalReader<string>().ReadAll(groupNumRows);
                var catalogs = rowGroupReader.Column((int)AmdiColumn.Catalogs).LogicalReader<long?[]>()
                    .ReadAll(groupNumRows);
                var categories = rowGroupReader.Column((int)AmdiColumn.Categories).LogicalReader<long?[]>()
                    .ReadAll(groupNumRows);

                //Items
                var itemId = rowGroupReader.Column((int)AmdiColumn.ItemId).LogicalReader<long?[]>()
                    .ReadAll(groupNumRows);
                var itemProductId = rowGroupReader.Column((int)AmdiColumn.ItemProductId).LogicalReader<long?[]>()
                    .ReadAll(groupNumRows);
                var itemAmdmStatusCode = rowGroupReader.Column((int)AmdiColumn.ItemAmdmStatusCode)
                    .LogicalReader<int?[]>().ReadAll(groupNumRows);
                var itemBaseItemId = rowGroupReader.Column((int)AmdiColumn.ItemBaseItemId).LogicalReader<long?[]>()
                    .ReadAll(groupNumRows);
                var itemSize = rowGroupReader.Column((int)AmdiColumn.ItemSize).LogicalReader<string[]>()
                    .ReadAll(groupNumRows);
                var itemFulfillmentSystemSku =
                    rowGroupReader.Column((int)AmdiColumn.ItemFSS).LogicalReader<string[]>().ReadAll(groupNumRows);
                var itemIsDiscountable = rowGroupReader.Column((int)AmdiColumn.IsDiscountable).LogicalReader<bool?[]>()
                    .ReadAll(groupNumRows);
                var itemIsTaxable = rowGroupReader.Column((int)AmdiColumn.IsTaxable).LogicalReader<bool?[]>()
                    .ReadAll(groupNumRows);
                var itemIsDeleted = rowGroupReader.Column((int)AmdiColumn.IsDeleted).LogicalReader<bool?[]>()
                    .ReadAll(groupNumRows);
                var itemIsSmartExclusion = rowGroupReader.Column((int)AmdiColumn.IsSmartExclusion)
                    .LogicalReader<bool?[]>().ReadAll(groupNumRows);
                var itemDaxVendorItemIds = rowGroupReader.Column((int)AmdiColumn.ItemDaxVendorItemIds)
                    .LogicalReader<string[][]>().ReadAll(groupNumRows);
                var itemIsSellable = rowGroupReader.Column((int)AmdiColumn.IsSellable).LogicalReader<bool?[]>()
                    .ReadAll(groupNumRows);
                var itemSortOrder = rowGroupReader.Column((int)AmdiColumn.ItemSortOrder).LogicalReader<long?[]>()
                    .ReadAll(groupNumRows);
                var itemMaxQuantityPerOrder = rowGroupReader.Column((int)AmdiColumn.ItemMaxQPO).LogicalReader<long?[]>()
                    .ReadAll(groupNumRows);
                var itemCost = rowGroupReader.Column((int)AmdiColumn.ItemCost).LogicalReader<double?[]>()
                    .ReadAll(groupNumRows);
                var itemUpc = rowGroupReader.Column((int)AmdiColumn.ItemUpc).LogicalReader<string[]>()
                    .ReadAll(groupNumRows);
                var itemEan = rowGroupReader.Column((int)AmdiColumn.ItemEan).LogicalReader<string[]>()
                    .ReadAll(groupNumRows);
                var itemKitbagBarcode = rowGroupReader.Column((int)AmdiColumn.ItemKbc).LogicalReader<string[]>()
                    .ReadAll(groupNumRows);
                var itemJcpItemSku = rowGroupReader.Column((int)AmdiColumn.ItemJcpSku).LogicalReader<string[]>()
                    .ReadAll(groupNumRows);
                var itemJcpPricePointLot = rowGroupReader.Column((int)AmdiColumn.ItemJcpPPL).LogicalReader<string[]>()
                    .ReadAll(groupNumRows);
                var itemDtgItemSku = rowGroupReader.Column((int)AmdiColumn.ItemDtgSku).LogicalReader<string[]>()
                    .ReadAll(groupNumRows);
                var itemWidth = rowGroupReader.Column((int)AmdiColumn.ItemWidth).LogicalReader<double?[]>()
                    .ReadAll(groupNumRows);
                var itemHeight = rowGroupReader.Column((int)AmdiColumn.ItemHeight).LogicalReader<double?[]>()
                    .ReadAll(groupNumRows);
                var itemWeight = rowGroupReader.Column((int)AmdiColumn.ItemWeight).LogicalReader<double?[]>()
                    .ReadAll(groupNumRows);
                var itemLength = rowGroupReader.Column((int)AmdiColumn.ItemLength).LogicalReader<double?[]>()
                    .ReadAll(groupNumRows);
                var itemPromiseShipDate = rowGroupReader.Column((int)AmdiColumn.ItemPsd).LogicalReader<string[]>()
                    .ReadAll(groupNumRows);

                //PropertyValues
                var pvName = rowGroupReader.Column((int)AmdiColumn.PvName).LogicalReader<string[]>()
                    .ReadAll(groupNumRows);
                var pvValue = rowGroupReader.Column((int)AmdiColumn.PvValue).LogicalReader<string[]>()
                    .ReadAll(groupNumRows);
                var pvDataType = rowGroupReader.Column((int)AmdiColumn.PvDataType).LogicalReader<string[]>()
                    .ReadAll(groupNumRows);
                var pvLocale = rowGroupReader.Column((int)AmdiColumn.PvLocale).LogicalReader<string[]>()
                    .ReadAll(groupNumRows);
                var pvOverrideGroupId = rowGroupReader.Column((int)AmdiColumn.PvORGId).LogicalReader<int?[]>()
                    .ReadAll(groupNumRows);
                var pvBulletSortOrder = rowGroupReader.Column((int)AmdiColumn.PvBSO).LogicalReader<long?[]>()
                    .ReadAll(groupNumRows);
                var pvMmdId = rowGroupReader.Column((int)AmdiColumn.PvMdmId).LogicalReader<long?[]>()
                    .ReadAll(groupNumRows);

                //OverriddenPropertyValues
                var ovpvOverrideGroupId = rowGroupReader.Column((int)AmdiColumn.OPvORGId).LogicalReader<int?[]>()
                    .ReadAll(groupNumRows);
                var orpvName = rowGroupReader.Column((int)AmdiColumn.OPvName).LogicalReader<string[]>()
                    .ReadAll(groupNumRows);
                var orpvValue = rowGroupReader.Column((int)AmdiColumn.OPvValue).LogicalReader<string[]>()
                    .ReadAll(groupNumRows);
                var orpvDataType = rowGroupReader.Column((int)AmdiColumn.OPvDataType).LogicalReader<string[]>()
                    .ReadAll(groupNumRows);
                var orpvLocale = rowGroupReader.Column((int)AmdiColumn.OPvLocale).LogicalReader<string[]>()
                    .ReadAll(groupNumRows);
                var orpvMmdId = rowGroupReader.Column((int)AmdiColumn.OPvMdmId).LogicalReader<long?[]>()
                    .ReadAll(groupNumRows);


                var productGroups = rowGroupReader.Column((int)AmdiColumn.ProductGroups).LogicalReader<string[]>()
                    .ReadAll(groupNumRows);
                var merchClass = rowGroupReader.Column((int)AmdiColumn.MerchClass).LogicalReader<long?>()
                    .ReadAll(groupNumRows);
                var cost = rowGroupReader.Column((int)AmdiColumn.Cost).LogicalReader<double?>().ReadAll(groupNumRows);
                var sizes = rowGroupReader.Column((int)AmdiColumn.Sizes).LogicalReader<string[]>()
                    .ReadAll(groupNumRows);

                for (var row = 0; row < rowGroupReader.MetaData.NumRows; row++)
                {
                    var amdiMasterSetDto = new AmdiMasterSetDto();

                    amdiMasterSetDto.ProductId = productIds[row].HasValue ? productIds[row].Value : 0;
                    amdiMasterSetDto.BaseproductId = baseProductIds[row].HasValue ? baseProductIds[row].Value : 0;
                    amdiMasterSetDto.AmdmStatusCode = amdmStatusCode[row].HasValue ? amdmStatusCode[row].Value : 0;
                    amdiMasterSetDto.StyleId = styleId[row];
                    amdiMasterSetDto.Dcs = dcs[row];
                    amdiMasterSetDto.Catalogs = BuildListLong(catalogs[row]);
                    amdiMasterSetDto.Categories = BuildListLong(categories[row]);
                    amdiMasterSetDto.ProductGroups = GetStringList(productGroups[row]);
                    amdiMasterSetDto.MerchClassId = merchClass[row].HasValue ? merchClass[row].Value : 0;
                    amdiMasterSetDto.Cost = cost[row].HasValue ? cost[row].Value : 0;
                    amdiMasterSetDto.Sizes = GetStringList(sizes[row]);

                    amdiMasterSetDto.PropertyValues = BuildPropertyValues(pvOverrideGroupId[row], pvName[row],
                        pvValue[row], pvDataType[row], pvLocale[row], pvMmdId[row], pvBulletSortOrder[row]);

                    amdiMasterSetDto.OverriddenPropertyValues = BuildPropertyValues(ovpvOverrideGroupId[row],
                        orpvName[row], orpvValue[row], orpvDataType[row], orpvLocale[row], orpvMmdId[row]);

                    amdiMasterSetDto.Items = BuildItems(itemId[row], itemProductId[row], itemAmdmStatusCode[row],
                        itemBaseItemId[row], itemSize[row], itemFulfillmentSystemSku[row], itemIsDiscountable[row],
                        itemIsTaxable[row], itemIsDeleted[row], itemIsSmartExclusion[row], itemDaxVendorItemIds[row],
                        itemIsSellable[row], itemSortOrder[row], itemMaxQuantityPerOrder[row], itemCost[row],
                        itemUpc[row], itemEan[row], itemKitbagBarcode[row], itemJcpItemSku[row],
                        itemJcpPricePointLot[row], itemDtgItemSku[row], itemWidth[row], itemHeight[row],
                        itemWeight[row], itemLength[row], itemPromiseShipDate[row]);

                    amdiMasterSetDtos.Add(amdiMasterSetDto);
                }
            }

            return amdiMasterSetDtos;
        }
        catch (Exception e)
        {
            throw;
        }
        finally
        {
            fileReader.Close();
        }
    }

    

    public IEnumerable<AmdiMasterSetPartialDto> MapPartialIds(Stream stream)
    {
        var amdiMasterSetDtos = new List<AmdiMasterSetPartialDto>();
        var input = new ManagedRandomAccessFile(stream, true);
        using var file = new ParquetFileReader(input);
        for (var rowGroup = 0; rowGroup < file.FileMetaData.NumRowGroups; ++rowGroup)
        {
            using var rowGroupReader = file.RowGroup(rowGroup);
            var groupNumRows = checked((int)rowGroupReader.MetaData.NumRows);

            var productIds = rowGroupReader.Column((int)AmdiColumn.ProductId).LogicalReader<long?>().ReadAll(groupNumRows);
            var catalogs = rowGroupReader.Column((int)AmdiColumn.Catalogs).LogicalReader<long?[]>().ReadAll(groupNumRows);
            amdiMasterSetDtos.AddRange(MapPartialIds(productIds, catalogs, rowGroupReader.MetaData.NumRows));
        }

        file.Close();
        return amdiMasterSetDtos;
    }

    private List<Item> BuildItems(IReadOnlyList<long?> itemId, IReadOnlyList<long?> productId,
        IReadOnlyList<int?> amdmStatusCode, IReadOnlyList<long?> baseItemId,
        IReadOnlyList<string> size, IReadOnlyList<string> fulfilmentSystemSku, IReadOnlyList<bool?> isDiscountable,
        IReadOnlyList<bool?> isTaxable, IReadOnlyList<bool?> isDeleted,
        IReadOnlyList<bool?> isSmartExclusion, IReadOnlyList<string[]> daxVendorItemId, IReadOnlyList<bool?> isSellable,
        IReadOnlyList<long?> sortOrder,
        IReadOnlyList<long?> maxQuantityPerOrder, IReadOnlyList<double?> cost, IReadOnlyList<string> upc,
        IReadOnlyList<string> ean, IReadOnlyList<string> kitBagBarcode,
        IReadOnlyList<string> jcpIemSku, IReadOnlyList<string> jcpPricePointLot, IReadOnlyList<string> dtgItemSku,
        IReadOnlyList<double?> width, IReadOnlyList<double?> height,
        IReadOnlyList<double?> weight, IReadOnlyList<double?> length, IReadOnlyList<string> promiseShipDate)
    {
        var items = new List<Item>();
        var nbEntries = itemId.Count;
        for (var i = 0; i < nbEntries; i++)
        {
            var item = new Item();
            if (itemId != null)
            {
                item.ItemId = itemId[i].HasValue ? itemId[i].Value : 0;
            }

            if (productId != null)
            {
                item.ProductId = productId[i].HasValue ? productId[i].Value : 0;
            }

            if (amdmStatusCode != null)
            {
                item.AmdmStatusCode = amdmStatusCode[i].HasValue ? amdmStatusCode[i].Value : 0;
            }

            if (baseItemId != null)
            {
                item.BaseItemId = baseItemId[i].HasValue ? baseItemId[i].Value : 0;
            }

            if (sortOrder != null)
            {
                item.SortOrder = sortOrder[i].HasValue ? sortOrder[i].Value : 0;
            }

            if (maxQuantityPerOrder != null)
            {
                item.MaxQuantityPerOrder = maxQuantityPerOrder[i].HasValue ? maxQuantityPerOrder[i].Value : 0;
            }

            if (cost != null)
            {
                item.Cost = cost[i].HasValue ? cost[i].Value : 0;
            }

            if (width != null)
            {
                item.Width = width[i].HasValue ? width[i].Value : 0;
            }

            if (weight != null)
            {
                item.Weight = weight[i].HasValue ? weight[i].Value : 0;
            }

            if (length != null)
            {
                item.Length = length[i].HasValue ? length[i].Value : 0;
            }

            if (height != null)
            {
                item.Height = height[i].HasValue ? height[i].Value : 0;
            }

            item.ItemSize = size[i];
            item.FulfillmentSystemSku = fulfilmentSystemSku[i];
            item.PromiseShipDate = promiseShipDate[i];
            item.FulfillmentSystemSku = fulfilmentSystemSku[i];
            item.DtgItemSku = dtgItemSku[i];
            item.Ean = ean[i];
            item.KitbagBarcode = kitBagBarcode[i];
            item.JcpItemSku = jcpIemSku[i];
            item.JcpPricePointLot = jcpPricePointLot[i];
            item.Upc = upc[i];

            if (isDiscountable != null)
            {
                item.IsDiscountable = isDiscountable[i].Value;
            }

            if (isDeleted != null)
            {
                item.IsDeleted = isDeleted[i].Value;
            }

            if (isSellable != null)
            {
                item.IsSellable = isSellable[i].Value;
            }

            if (isSmartExclusion != null)
            {
                item.IsSmartExclusion = isSmartExclusion[i].Value;
            }

            if (isTaxable != null)
            {
                item.IsTaxable = isTaxable[i].Value;
            }
            
            items.Add(item);
        }

        return items;
    }

    private List<PropertyValue> BuildPropertyValues(IReadOnlyList<int?> groupId, IReadOnlyList<string> name,
        IReadOnlyList<string> value, IReadOnlyList<string> dataType,
        IReadOnlyList<string> locale, IReadOnlyList<long?> mdmId, IReadOnlyList<long?> bulletSortOrder = null)
    {
        var nbEntries = groupId.Count;
        var pvList = new List<PropertyValue>();
        for (var i = 0; i < nbEntries; i++)
        {
            var propertyValue = new PropertyValue();
            if (groupId != null)
            {
                propertyValue.OverrideGroupId = groupId[i].HasValue ? groupId[i].Value : 0;
            }

            propertyValue.Name = name[i];
            propertyValue.Value = value[i];
            propertyValue.DataType = dataType[i];
            propertyValue.Locale = locale[i];
            if (mdmId != null)
            {
                propertyValue.MdmId = mdmId[i].HasValue ? mdmId[i].Value : 0;
            }

            if (bulletSortOrder != null)
            {
                propertyValue.BulletSortOrder = bulletSortOrder[i].HasValue ? bulletSortOrder[i].Value : 0;
            }

            pvList.Add(propertyValue);
        }

        return pvList;
    }

    private List<string> GetStringList(string[] value) => value == null ? new List<string>() : value.ToList();

    private IEnumerable<AmdiMasterSetPartialDto> MapPartialIds(IReadOnlyList<long?> productIds,
        IReadOnlyList<long?[]> catalogs, long numRows)
    {
        var amdiMasterSetDtos = new List<AmdiMasterSetPartialDto>();
        for (var row = 0; row < numRows; row++)
        {
            if (productIds[row].HasValue)
            {
                amdiMasterSetDtos.Add(new AmdiMasterSetPartialDto
                {
                    ProductId = productIds[row].Value,
                    Catalogs = BuildListLong(catalogs[row])
                });
            }
        }

        return amdiMasterSetDtos;
    }

    private List<long> BuildListLong(IEnumerable<long?> value) => value == null
        ? new List<long>()
        : (from l1 in value where l1.HasValue select l1.Value).ToList();
}