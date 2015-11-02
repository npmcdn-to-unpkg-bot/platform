namespace Website.Export
{
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Web.Mvc;
    using Allors.Domain;

    using OfficeOpenXml;
    using OfficeOpenXml.Style;

    [Authorize]
    public class ExportController : CustomController
    {
        private const string XlsxContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        private const string XlsmContentType = "application/vnd.ms-excel.sheet.macroEnabled.12";

        [HttpGet]
        public ActionResult Users(Model model)
        {
            var fromYear = model.FromYear;
            var toYear = model.ToYear;

            using (var package = new ExcelPackage())
            {
                this.UsersToExcel(package, fromYear, toYear);

                var stream = new MemoryStream();
                package.SaveAs(stream);

                var fileName = "variables.xlsx";

                stream.Position = 0;
                return File(stream, XlsxContentType, fileName);
            }
        }

        private void UsersToExcel(ExcelPackage package, int? fromYear, int? toYear)
        {
            var worksheet = package.Workbook.Worksheets.Add("Users");
           
            var users = new Persons(this.AllorsSession).Extent();

            var data = users.Select(x => new []
            {
                x.UserName, 
                x.FirstName, 
                x.LastName, 
                x.UserEmail
            }).ToArray();

            using (var headerRange = worksheet.Cells["A1:E1"])
            {
                headerRange.Style.Font.Bold = true;
                headerRange.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                headerRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                headerRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
                headerRange.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(224, 224, 224));
                headerRange.Style.Font.Size = 12;
            }
            
            // worksheet.Column(1).Style.Numberformat.Format = @"0.00";
            
            if (data.Length != 0)
            {
                worksheet.Cells["A1"].Value = "UserName";
                worksheet.Cells["B1"].Value = "FirstName";
                worksheet.Cells["C1"].Value = "LastName";
                worksheet.Cells["D1"].Value = "UserEmail";
                worksheet.Cells["A2"].LoadFromArrays(data);
            }

            worksheet.Cells.AutoFitColumns();
        }
    }
}