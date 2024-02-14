using CsvHelper;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using OfficeOpenXml.Table;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Formats.Asn1;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LicenseContext = OfficeOpenXml.LicenseContext;

namespace SearchableDesign.Repository
{
    public static class Utility
    {
        public static byte[] ExportToExcel(DataTable dt)
        {
            try
            {
                var memoryStream = new MemoryStream();
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (var excelPackage = new ExcelPackage(memoryStream))
                {
                    var worksheet = excelPackage.Workbook.Worksheets.Add("Sheet1");
                    worksheet.Cells["A1"].LoadFromDataTable(dt, true, TableStyles.None);
                    worksheet.Cells[1, 1, 1, dt.Columns.Count].Style.Font.Bold = true;
                    string lastAddress = worksheet.Dimension.Address.Last().ToString();

                    System.Drawing.Color fontColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
                    worksheet.Cells[1, 1, 1, dt.Columns.Count].Style.Font.Color.SetColor(fontColor);
                    System.Drawing.Color backGroundColor = System.Drawing.ColorTranslator.FromHtml("#008738");
                    worksheet.Cells[1, 1, 1, dt.Columns.Count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    worksheet.Cells[1, 1, 1, dt.Columns.Count].Style.Fill.BackgroundColor.SetColor(backGroundColor);


                    worksheet.DefaultRowHeight = 18;


                    worksheet.Column(2).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    worksheet.Column(6).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Column(7).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.DefaultColWidth = 20;
                    worksheet.Column(2).AutoFit();
                    return excelPackage.GetAsByteArray();
                }

            }
            catch (Exception)
            {
                throw;
            }

        }
        public static DataTable ToDataTable<T>(this IQueryable items)
        {
            Type type = typeof(T);

            var props = TypeDescriptor.GetProperties(type)
                                      .Cast<PropertyDescriptor>()
                                      .Where(propertyInfo => propertyInfo.PropertyType.Namespace.Equals("System"))
                                      .Where(propertyInfo => propertyInfo.IsReadOnly == false)
                                      .ToArray();

            var table = new DataTable();

            foreach (var propertyInfo in props)
            {
                table.Columns.Add(propertyInfo.Name, Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType);
            }

            foreach (var item in items)
            {
                table.Rows.Add(props.Select(property => property.GetValue(item)).ToArray());
            }

            return table;
        }
        public static (DataTable, List<int>) ReadExcelFile(string filePath)
        {
            var dt = new DataTable();
            dt.Columns.Add("SN", typeof(string));
            dt.Columns.Add("FullName", typeof(string));
            dt.Columns.Add("DOB", typeof(DateTime));
            dt.Columns.Add("Gender", typeof(string));
            dt.Columns.Add("Salary", typeof(decimal));
            dt.Columns.Add("Designation", typeof(string));

            
            FileInfo existingFile = new FileInfo(filePath);
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            string strSno = "";
            int sno = 0;
            List<int> SkippedRow = new List<int>();

            using (ExcelPackage package = new ExcelPackage(existingFile))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                int colCount = worksheet.Dimension.End.Column;  //get Column Count
                int rowCount = worksheet.Dimension.End.Row;     //get row count                
                for (int row = 2; row <= rowCount; row++)
                {
                     strSno = (worksheet.Cells[row, 1].Value ?? string.Empty).ToString();
                    int.TryParse(strSno, out sno);
                    if (sno>0)
                    {
                        DataRow newRow = dt.NewRow();
                        newRow["SN"] = row-1;
                        newRow["FullName"] = (worksheet.Cells[row, 2].Value ??string.Empty).ToString();
                        newRow["DOB"] =Convert.ToDateTime((worksheet.Cells[row, 3].Value ?? string.Empty).ToString());
                        newRow["Gender"] = (worksheet.Cells[row, 4].Value ?? string.Empty).ToString();
                        newRow["Salary"] =Convert.ToDecimal( (worksheet.Cells[row, 5].Value ?? string.Empty).ToString());
                        newRow["Designation"] = (worksheet.Cells[row, 6].Value ?? string.Empty).ToString();

                        dt.Rows.Add(newRow);
                    }
                    else if (strSno=="")
                    {
                        SkippedRow.Add(row);
                    }
                };
            }
            return (dt, SkippedRow);
        }
        public static DataTable ReadCSVFile(string filePath)
        {
            DataTable dt = new DataTable();
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                using (var dr = new CsvDataReader(csv))
                {
                    dt.Columns.Add("SN", typeof(string));
                    dt.Columns.Add("FullName", typeof(string));
                    dt.Columns.Add("DOB", typeof(DateTime));
                    dt.Columns.Add("Gender", typeof(string));
                    dt.Columns.Add("Salary", typeof(decimal));
                    dt.Columns.Add("Designation", typeof(string));
                    dt.Load(dr);
                }
            }
            return dt;
        }
    }
}
