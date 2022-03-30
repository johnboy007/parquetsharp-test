using System.Collections.Generic;
using System.IO;
using ParquetMapper.Model;

namespace ParquetMapper;

public interface IParquetMapper
{
    List<AmdiMasterSetDto> Map(Stream stream);

    IEnumerable<AmdiMasterSetPartialDto> MapPartialIds(Stream stream);
}