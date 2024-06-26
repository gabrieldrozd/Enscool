using System.Data;
using ClosedXML.Excel;
using Common.Utilities.Extensions;
using Common.Utilities.Primitives.Results;
using Core.Application.Communication.Internal.Commands;
using Core.Application.Extensions;
using Core.Application.Files;
using Microsoft.EntityFrameworkCore;
using Modules.Management.Application.Abstractions;
using Modules.Management.Domain.Users;

namespace Modules.Management.Application.Features.InstitutionUsers.Commands.Export;

/// <summary>
/// Creates xlsx file with <see cref="InstitutionUser"/>s data.
/// </summary>
public sealed record ExportInstitutionUsersCommand : ITransactionCommand<FileDto>
{
    public Guid InstitutionId { get; init; }
    public List<Guid> UserIds { get; init; } = [];
    public List<string> Columns { get; init; } = [];

    internal sealed class Handler : ICommandHandler<ExportInstitutionUsersCommand, FileDto>
    {
        private readonly IManagementDbContext _context;

        public Handler(IManagementDbContext context)
        {
            _context = context;
        }

        public async Task<Result<FileDto>> Handle(ExportInstitutionUsersCommand request, CancellationToken cancellationToken)
        {
            var users = await _context.Users
                .AsNoTracking()
                .OfType<InstitutionUser>()
                .Where(x => x.InstitutionId! == request.InstitutionId && request.UserIds.Contains(x.Id))
                .ToListAsync(cancellationToken);

            if (users.Count == 0)
                return Result.Failure.NotFound<FileDto>("Users not found");

            var institutionName = await _context.Institutions
                .AsNoTracking()
                .Where(x => x.Id == request.InstitutionId)
                .Select(x => x.ShortName)
                .FirstOrDefaultAsync(cancellationToken);

            var workbook = CreateUsersWorkbook(request.Columns, institutionName, users);
            var fileDto = FileDto.CreateSpreadsheet($"{institutionName ?? "Institution"}_Users.xlsx", workbook.ToByteArray());
            return Result.Success.Ok(fileDto);
        }

        private static XLWorkbook CreateUsersWorkbook(List<string> columns, string? institutionName, List<InstitutionUser> users)
        {
            const int headingRowStart = 1;
            const int headingColumnStart = 1;
            const int headingRowEnd = 2;
            const int dataStartRow = 3;
            const int dataStartColumn = 1;

            var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Users Table");
            var formattedColumnNames = columns.Select(x => x.ToTitleCase()).ToList();

            if (formattedColumnNames.Count == 0)
                return workbook;

            var headingText = $"{institutionName ?? "Institution"} Users";
            worksheet.Cell(headingRowStart, headingColumnStart).Value = headingText;
            worksheet.Range(headingRowStart, headingColumnStart, headingRowEnd, formattedColumnNames.Count + 1).Merge().Style
                .Font.SetBold()
                .Font.SetFontSize(16)
                .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center)
                .Alignment.SetVertical(XLAlignmentVerticalValues.Center)
                .Fill.SetBackgroundColor(XLColor.LightGray)
                .Border.SetOutsideBorder(XLBorderStyleValues.Thin);

            var dataTable = new DataTable("Users");
            dataTable.Columns.Add("#", typeof(int));
            formattedColumnNames.ForEach(column => dataTable.Columns.Add(column, typeof(string)));

            for (var i = 0; i < users.Count; i++)
            {
                var user = users[i];
                var row = dataTable.NewRow();
                row["#"] = i + 1;
                formattedColumnNames.ForEach(column =>
                {
                    var cellValue = GetCellValue(column, user);
                    row[column] = cellValue;
                });
                dataTable.Rows.Add(row);
            }

            var usersTable = worksheet.Cell(dataStartRow, dataStartColumn).InsertTable(dataTable, "Users", true);
            usersTable.Theme = XLTableTheme.TableStyleLight1;
            usersTable.ShowAutoFilter = true;

            worksheet.Columns().AdjustToContents(20.0, 100.0);
            worksheet.Column(1).Width = 5.0;
            worksheet.Columns().Style
                .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center)
                .Alignment.SetVertical(XLAlignmentVerticalValues.Center);

            return workbook;
        }

        private static string? GetCellValue(string column, InstitutionUser user)
        {
            return column switch
            {
                nameof(InstitutionUser.State) => user.State.ToString(),
                nameof(InstitutionUser.Role) => user.Role.ToString(),
                nameof(InstitutionUser.FirstName) => user.FirstName,
                nameof(InstitutionUser.MiddleName) => string.IsNullOrWhiteSpace(user.MiddleName) ? "-" : user.MiddleName,
                nameof(InstitutionUser.LastName) => user.LastName,
                nameof(InstitutionUser.Email) => user.Email.Value,
                nameof(InstitutionUser.Phone) => user.Phone.Value,
                nameof(InstitutionUser.BirthDate) => user.BirthDate?.ToFormattedString() ?? "-",
                nameof(InstitutionUser.LanguageLevel) => user.LanguageLevel?.Value ?? "-",
                nameof(InstitutionUser.Address) => user.Address?.ToString() ?? "-",
                _ => null
            };
        }
    }
}