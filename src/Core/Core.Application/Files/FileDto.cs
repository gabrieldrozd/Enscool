namespace Core.Application.Files;

public sealed class FileDto
{
    public string FileName { get; private set; }
    public string ContentType { get; private set; }
    public byte[] Content { get; private set; }

    public FileDto(string fileName, string contentType, byte[] content)
    {
        FileName = fileName;
        ContentType = contentType;
        Content = content;
    }

    public static FileDto CreateSpreadsheet(string fileName, byte[] content)
        => new(fileName, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", content);
}