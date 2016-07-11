JasperReports 是由Java 所發展開發 open source 報表工具，可以支援多種形式產生動態報表，並支援多種報表格式產出。
功能類似 crystal reports 的 open source 報表工具。

唯官方目前尚無支援直接使用 .net framework 的版本。
因此有本專案產生，目的是要讓開發者能簡易使用功能強大JasperReports open source 報表工具。

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

使用方法
步驟1.
使用報表設計工具設計報表樣版(.jrxml)，使用報表設計工具設計編輯報表樣版(.jasper)
步驟2.
參考範例程式
/jasperreportsApp/jasperreportsApp/JRTableModelDSTest.cs
/jasperreportsApp/jasperreportsApp/SqlConnectionDSTest.cs

已知問題，無法使用下列API
JasperCompileManager.compileReportToFile(fileName)
JasperCompileManager.compileReport(fileName)
JExcelApiExporter
JRXlsExporter
