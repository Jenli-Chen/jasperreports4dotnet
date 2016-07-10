jasperreports for .net framework 是使用ikvm.net工具將jasperreports6 相依jar編譯成dll

使.net環境下可以使用C# or VB.net 執行 jasperreports 所編譯後的jasper檔執行報表製作

可相容於 .net framework 2.0以上環境

元件版本
ikvmbin-8.1.5717.0、jasperreports6.3 

相關ikvm.net 可參考
http://weblog.ikvm.net/

相關jasperreports可參考
http://community.jaspersoft.com/project/jasperreports-library/releases

目前測試過可支援報表格式:PDF、DOCX、XLSX、PPTX、RTF、HTML

報表設計工具
TIBCO Jaspersoft Studio - Visual Designer for JasperReports. 
http://community.jaspersoft.com/project/jaspersoft-studio  

iReport Designer - Visual Designer for JasperReports. 
http://community.jaspersoft.com/project/ireport-designer/releases

已知問題，無法使用下列API
JasperCompileManager.compileReportToFile(fileName)
JasperCompileManager.compileReport(fileName)
JExcelApiExporter
JRXlsExporter
