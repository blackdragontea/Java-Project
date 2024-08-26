using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using System.Data;
using System.Data.SqlClient;
using app = Microsoft.Office.Interop.Excel.Application;
using Excel = Microsoft.Office.Interop.Excel;
using System.Windows.Forms;
using Application = System.Windows.Forms.Application;
using System.Diagnostics;
using System.Threading;

namespace QuanLyBanHang
{
    class XuatExecl
    {
        public static string duongdanex = Application.StartupPath;

        // Hàm hỗ trợ chuyển số cột thành chữ cái (1 -> A, 2 -> B, ...)
        private static string GetExcelColumnName(int columnNumber)
        {
            int dividend = columnNumber;
            string columnName = String.Empty;
            int modulo;

            while (dividend > 0)
            {
                modulo = (dividend - 1) % 26;
                columnName = Convert.ToChar(65 + modulo) + columnName;
                dividend = (dividend - modulo) / 26;
            }

            return columnName;
        }

        private static void OpenExcelFileInPrintPreview(string filePath)
        {
            app excelApp = new app();
            Excel.Workbook workbook = excelApp.Workbooks.Open(filePath);
            Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Sheets[1];
            // Chuyển sang chế độ giấy ngang
            worksheet.PageSetup.Orientation = Excel.XlPageOrientation.xlLandscape;
            // Thiết lập Excel ở chế độ toàn màn hình
            excelApp.Visible = true;
            excelApp.WindowState = Excel.XlWindowState.xlMaximized;
            worksheet.PrintPreview();

            // Đóng workbook và ứng dụng Excel sau khi xem trước khi in
            workbook.Close(false);
            excelApp.Quit();
        }

        public static void exportecxel(DataGridView g, string duongdan, string tenfile)
        {
            duongdan = duongdan + @"\Excel\Accounts\"; // Cập nhật đường dẫn
            if (!System.IO.Directory.Exists(duongdan))
            {
                System.IO.Directory.CreateDirectory(duongdan);
            }

            app obj = new app();
            Excel.Workbook workbook = obj.Application.Workbooks.Add(Type.Missing);
            Excel.Worksheet worksheet = (Excel.Worksheet)workbook.ActiveSheet;

            
            Excel.Range titleRange = worksheet.Range["A1", GetExcelColumnName(g.Columns.Count) + "1"];
            titleRange.Merge();
            titleRange.Value = "Danh Sách Tài Khoản";
            titleRange.Font.Size = 18;
            titleRange.Font.Bold = true;
            titleRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

            // Thêm dòng ngày giờ - Cửa Hàng Duy Đức căn phải
            string ngayGio = DateTime.Now.ToString();
            Excel.Range infoRange = worksheet.Range["A2", GetExcelColumnName(g.Columns.Count) + "2"];
            infoRange.Merge();
            infoRange.Value = ngayGio + " - Cửa Hàng Duy Đức";
            infoRange.Font.Size = 12;
            infoRange.Font.Italic = true;
            infoRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;

            obj.Columns.ColumnWidth = 25;

            // Định dạng header và ngắt dòng văn bản
            for (int i = 1; i < g.Columns.Count + 1; i++)
            {
                Excel.Range headerCell = worksheet.Cells[3, i];
                headerCell.Value = g.Columns[i - 1].HeaderText;
                headerCell.WrapText = true; // Ngắt dòng văn bản
            }

            Excel.Range headerRange = worksheet.get_Range("A3", GetExcelColumnName(g.Columns.Count) + "3");
            headerRange.Font.Bold = true;
            headerRange.Font.Name = "Times New Roman";
            headerRange.Font.Size = 12;
            headerRange.Interior.Color = Excel.XlRgbColor.rgbLightGray;
            headerRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

            // Định dạng border cho bảng dữ liệu và ngắt dòng văn bản
            for (int i = 0; i < g.Rows.Count; i++)
            {
                for (int j = 0; j < g.Columns.Count; j++)
                {
                    if (g.Rows[i].Cells[j].Value != null)
                    {
                        Excel.Range dataCell = worksheet.Cells[i + 4, j + 1];
                        dataCell.Value = g.Rows[i].Cells[j].Value;
                        dataCell.WrapText = true; // Ngắt dòng văn bản
                    }
                }
            }

            // Lấy phạm vi dữ liệu
            Excel.Range dataRange = worksheet.get_Range("A4", GetExcelColumnName(g.Columns.Count) + (g.Rows.Count + 2).ToString());
            dataRange.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
            dataRange.Borders.Weight = Excel.XlBorderWeight.xlThin;

            // Định dạng font và căn chỉnh cho toàn bộ bảng
            dataRange.Font.Name = "Times New Roman";
            dataRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;

            // Lưu file
            string fullPath = duongdan + tenfile + ".xlsx";
            workbook.SaveCopyAs(fullPath);
            workbook.Saved = true;

            // Mở file Excel để xem trước
            OpenExcelFileInPrintPreview(fullPath);

            // Đóng ứng dụng Excel
            workbook.Close(false);
            obj.Quit();
        }


        public static void exportecxelchitietdonhang(DataGridView g, string duongdan, string tenfile)
        {
            duongdan = duongdan + @"\Excel\ChiTietDonHang\";
            if (!System.IO.Directory.Exists(duongdan))
            {
                System.IO.Directory.CreateDirectory(duongdan);
            }

            app obj = new app();
            Excel.Workbook workbook = obj.Application.Workbooks.Add(Type.Missing);
            Excel.Worksheet worksheet = (Excel.Worksheet)workbook.ActiveSheet;
            worksheet.Name = "ChiTietDonHang";

            // Thêm dòng "HOÁ ĐƠN BÁN HÀNG" căn giữa
            Excel.Range titleRange = worksheet.Range["A1", GetExcelColumnName(g.Columns.Count) + "1"];
            titleRange.Merge();
            titleRange.Value = "HOÁ ĐƠN BÁN HÀNG";
            titleRange.Font.Size = 18;
            titleRange.Font.Bold = true;
            titleRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

            // Thêm dòng ngày giờ - Cửa Hàng Duy Đức căn phải
            string ngayGio = DateTime.Now.ToString();
            Excel.Range infoRange = worksheet.Range["A2", GetExcelColumnName(g.Columns.Count) + "2"];
            infoRange.Merge();
            infoRange.Value = ngayGio + " - Cửa Hàng Duy Đức";
            infoRange.Font.Size = 12;
            infoRange.Font.Italic = true;
            infoRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;

            obj.Columns.ColumnWidth = 25;

            // Định dạng header và ngắt dòng văn bản
            for (int i = 1; i < g.Columns.Count + 1; i++)
            {
                Excel.Range headerCell = worksheet.Cells[3, i];
                headerCell.Value = g.Columns[i - 1].HeaderText;
                headerCell.WrapText = true; // Ngắt dòng văn bản
            }

            Excel.Range headerRange = worksheet.get_Range("A3", GetExcelColumnName(g.Columns.Count) + "3");
            headerRange.Font.Bold = true;
            headerRange.Font.Name = "Times New Roman";
            headerRange.Font.Size = 12;
            headerRange.Interior.Color = Excel.XlRgbColor.rgbLightGray;
            headerRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

            // Định dạng border cho bảng dữ liệu và ngắt dòng văn bản
            for (int i = 0; i < g.Rows.Count; i++)
            {
                for (int j = 0; j < g.Columns.Count; j++)
                {
                    if (g.Rows[i].Cells[j].Value != null)
                    {
                        Excel.Range dataCell = worksheet.Cells[i + 4, j + 1];
                        dataCell.Value = g.Rows[i].Cells[j].Value;
                        dataCell.WrapText = true; // Ngắt dòng văn bản
                    }
                }
            }

            // Lấy phạm vi dữ liệu
            Excel.Range dataRange = worksheet.get_Range("A4", GetExcelColumnName(g.Columns.Count) + (g.Rows.Count + 2).ToString());
            dataRange.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
            dataRange.Borders.Weight = Excel.XlBorderWeight.xlThin;

            // Định dạng font và căn chỉnh cho toàn bộ bảng
            dataRange.Font.Name = "Times New Roman";
            dataRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;

            // Lưu file
            string fullPath = duongdan + tenfile + ".xlsx";
            workbook.SaveCopyAs(fullPath);
            workbook.Saved = true;

            // Mở file Excel và chuyển đến Print Preview
            OpenExcelFileInPrintPreview(fullPath);

            workbook.Close(false);
            obj.Quit();
        }

        public static void export_phieu(DataGridView g, string duongdan, string tenfile, string solg)
        {
            duongdan = duongdan + @"\Excel\ThongTinPhieu\"; // Cập nhật đường dẫn
            if (!System.IO.Directory.Exists(duongdan))
            {
                System.IO.Directory.CreateDirectory(duongdan);
            }

            app obj = new app();
            Excel.Workbook workbook = obj.Application.Workbooks.Add(Type.Missing);
            Excel.Worksheet worksheet = (Excel.Worksheet)workbook.ActiveSheet;

            Excel.Range titleRange = worksheet.Range["A1", GetExcelColumnName(g.Columns.Count) + "1"];
            titleRange.Merge();
            titleRange.Value = "THÔNG TIN PHIẾU";
            titleRange.Font.Size = 18;
            titleRange.Font.Bold = true;
            titleRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

            // Thêm dòng ngày giờ - Cửa Hàng Duy Đức căn phải
            string ngayGio = DateTime.Now.ToString();
            Excel.Range infoRange = worksheet.Range["A2", GetExcelColumnName(g.Columns.Count) + "2"];
            infoRange.Merge();
            infoRange.Value = ngayGio + " - Cửa Hàng Duy Đức";
            infoRange.Font.Size = 12;
            infoRange.Font.Italic = true;
            infoRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;

            obj.Columns.ColumnWidth = 25;

            // Định dạng header và ngắt dòng văn bản
            for (int i = 1; i < g.Columns.Count + 1; i++)
            {
                Excel.Range headerCell = worksheet.Cells[3, i];
                headerCell.Value = g.Columns[i - 1].HeaderText;
                headerCell.WrapText = true; // Ngắt dòng văn bản
            }

            Excel.Range headerRange = worksheet.get_Range("A3", GetExcelColumnName(g.Columns.Count) + "3");
            headerRange.Font.Bold = true;
            headerRange.Font.Name = "Times New Roman";
            headerRange.Font.Size = 12;
            headerRange.Interior.Color = Excel.XlRgbColor.rgbLightGray;
            headerRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

            // Định dạng border cho bảng dữ liệu và ngắt dòng văn bản
            for (int i = 0; i < g.Rows.Count; i++)
            {
                for (int j = 0; j < g.Columns.Count; j++)
                {
                    if (g.Rows[i].Cells[j].Value != null)
                    {
                        Excel.Range dataCell = worksheet.Cells[i + 4, j + 1];
                        dataCell.Value = g.Rows[i].Cells[j].Value;
                        dataCell.WrapText = true; // Ngắt dòng văn bản
                    }
                }
            }

            // Lấy phạm vi dữ liệu
            Excel.Range dataRange = worksheet.get_Range("A4", GetExcelColumnName(g.Columns.Count) + (g.Rows.Count + 2).ToString());
            dataRange.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
            dataRange.Borders.Weight = Excel.XlBorderWeight.xlThin;

        // Ghi số lượng vào ô cuối cùng
        int lastRow = g.Rows.Count + 2;
            obj.Cells[lastRow, g.Columns.Count].Value = "Số lượng :";
            obj.Cells[lastRow, g.Columns.Count + 1].Value = solg;

            // Định dạng font và căn chỉnh
            obj.Range["A1", GetExcelColumnName(g.Columns.Count) + lastRow.ToString()].Font.Name = "Times New Roman";
            obj.Range["A1", GetExcelColumnName(g.Columns.Count) + lastRow.ToString()].HorizontalAlignment = 3;

            // Lưu file
            string fullPath = duongdan + tenfile + ".xlsx";
            workbook.SaveCopyAs(fullPath);
            workbook.Saved = true;

            // Mở file Excel để xem trước
            OpenExcelFileInPrintPreview(fullPath);

            // Đóng ứng dụng Excel
            workbook.Close(false);
            obj.Quit();
        }

        public static void nhapnhieu(DataGridView g, string duongdan, string tenfile, string s, string tile, string chietkhau)
        {
            duongdan = duongdan + @"\Excel\NhapNhieu\"; // Cập nhật đường dẫn
            if (!System.IO.Directory.Exists(duongdan))
            {
                System.IO.Directory.CreateDirectory(duongdan);
            }

            app obj = new app();
            Excel.Workbook workbook = obj.Application.Workbooks.Add(Type.Missing);
            Excel.Worksheet worksheet = (Excel.Worksheet)workbook.ActiveSheet;

            Excel.Range titleRange = worksheet.Range["A1", GetExcelColumnName(g.Columns.Count) + "1"];
            titleRange.Merge();
            titleRange.Value = "HOÁ ĐƠN BÁN HÀNG";
            titleRange.Font.Size = 18;
            titleRange.Font.Bold = true;
            titleRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

            // Thêm dòng ngày giờ - Cửa Hàng Duy Đức căn phải
            string ngayGio = DateTime.Now.ToString();
            Excel.Range infoRange = worksheet.Range["A2", GetExcelColumnName(g.Columns.Count) + "2"];
            infoRange.Merge();
            infoRange.Value = ngayGio + " - Cửa Hàng Duy Đức";
            infoRange.Font.Size = 12;
            infoRange.Font.Italic = true;
            infoRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;

            obj.Columns.ColumnWidth = 25;

            // Định dạng header và ngắt dòng văn bản
            for (int i = 1; i < g.Columns.Count + 1; i++)
            {
                Excel.Range headerCell = worksheet.Cells[3, i];
                headerCell.Value = g.Columns[i - 1].HeaderText;
                headerCell.WrapText = true; // Ngắt dòng văn bản
            }

            Excel.Range headerRange = worksheet.get_Range("A3", GetExcelColumnName(g.Columns.Count) + "3");
            headerRange.Font.Bold = true;
            headerRange.Font.Name = "Times New Roman";
            headerRange.Font.Size = 12;
            headerRange.Interior.Color = Excel.XlRgbColor.rgbLightGray;
            headerRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

            // Định dạng border cho bảng dữ liệu và ngắt dòng văn bản
            for (int i = 0; i < g.Rows.Count; i++)
            {
                for (int j = 0; j < g.Columns.Count; j++)
                {
                    if (g.Rows[i].Cells[j].Value != null)
                    {
                        Excel.Range dataCell = worksheet.Cells[i + 4, j + 1];
                        dataCell.Value = g.Rows[i].Cells[j].Value;
                        dataCell.WrapText = true; // Ngắt dòng văn bản
                    }
                }
            }

            // Lấy phạm vi dữ liệu
            Excel.Range dataRange = worksheet.get_Range("A4", GetExcelColumnName(g.Columns.Count) + (g.Rows.Count + 2).ToString());
            dataRange.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
            dataRange.Borders.Weight = Excel.XlBorderWeight.xlThin;

        // Ghi chiết khấu và tổng tiền vào cuối cùng
        int lastRow = g.Rows.Count + 3;
            obj.Cells[lastRow, g.Columns.Count - 1].Value = "Chiết khấu :";
            obj.Cells[lastRow, g.Columns.Count].Value = " " + chietkhau + " %";

            obj.Cells[lastRow + 1, g.Columns.Count - 1].Value = "Tổng Tiền :";
            obj.Cells[lastRow + 1, g.Columns.Count].Value = " " + s;

            // Định dạng font và căn chỉnh
            obj.Range["A1", GetExcelColumnName(g.Columns.Count) + (lastRow + 1).ToString()].Font.Name = "Times New Roman";
            obj.Range["A1", GetExcelColumnName(g.Columns.Count) + (lastRow + 1).ToString()].HorizontalAlignment = 3;

            // Lưu file
            string fullPath = duongdan + tenfile + ".xlsx";
            workbook.SaveCopyAs(fullPath);
            workbook.Saved = true;

            // Mở file Excel để xem trước
            OpenExcelFileInPrintPreview(fullPath);

            // Đóng ứng dụng Excel
            workbook.Close(false);
            obj.Quit();
        }
    }
}