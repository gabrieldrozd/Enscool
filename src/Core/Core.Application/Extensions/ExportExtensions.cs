using ClosedXML.Excel;

namespace Core.Application.Extensions;

public static class ExportExtensions
{
    public static byte[] ToByteArray(this XLWorkbook workbook)
    {
        using var memoryStream = new MemoryStream();
        workbook.SaveAs(memoryStream);
        return memoryStream.ToArray();
    }
}