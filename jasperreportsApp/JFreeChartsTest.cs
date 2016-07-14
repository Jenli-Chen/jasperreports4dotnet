using System;
using java.io;
using java.sql;
using java.util;
using net.sf.jasperreports.engine;
using net.sf.jasperreports.engine.export;
using net.sf.jasperreports.engine.util;
using net.sf.jasperreports.engine.export.ooxml;
using java.awt.image;
using java.awt;
using org.jCharts.chartData;
using org.jCharts.axisChart;
using org.jCharts.types;
using org.jCharts.properties;
using javax.imageio;
using net.sf.jasperreports.renderers;
using org.jfree.data.general;
using org.jfree.chart;
using org.jfree.chart.plot;
using org.jfree.util;

namespace jasperreportsApp
{
    public class JFreeChartsTest : DSTest
    {
        private string fileName = "JFreeChartReport.jasper";

        override public void ExpReort(string taskName)
        {

            try
            {
                string reports_dir = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "reports");
                fileName = System.IO.Path.Combine(reports_dir, fileName);
                DateTime start = DateTime.Now;/////  DateTime.Now.Millisecond;  
                JREmptyDataSource ds = new JREmptyDataSource(1);
                java.util.Map parms = new java.util.HashMap();
                BufferedImage bufferedImage = getJFreeChart();
                parms.put("pjChart", bufferedImage);

                if (TASK_FILL.Equals(taskName))
                {
                    JasperFillManager.fillReportToFile(fileName, parms, ds);
                    System.Console.WriteLine("TASK_FILL time : " + (DateTime.Now.Subtract(start)));
                }
                else if (TASK_PRINT.Equals(taskName))
                {
                    JasperPrintManager.printReport(fileName, true);
                    System.Console.WriteLine("TASK_FILL time : " + (DateTime.Now.Subtract(start)));
                }
                else if (TASK_TEXT.Equals(taskName))
                {
                    JRTextExporter exporter = new JRTextExporter();
                    File sourceFile = new File(fileName);
                    net.sf.jasperreports.engine.JasperReport jasperPrint = (net.sf.jasperreports.engine.JasperReport)JRLoader.loadObject(sourceFile);
                    File destFile = new File(sourceFile.getParent(), jasperPrint.getName() + ".txt");
                    exporter.setParameter(JRExporterParameter.OUTPUT_FILE, jasperPrint);
                    exporter.setParameter(JRExporterParameter.OUTPUT_FILE_NAME, destFile.toString());
                    exporter.setParameter(JRTextExporterParameter.CHARACTER_WIDTH, new java.lang.Integer(10));
                    exporter.setParameter(JRTextExporterParameter.CHARACTER_HEIGHT, new java.lang.Integer(10));
                    exporter.exportReport();

                    System.Console.WriteLine("TASK_TEXT creation time : " + (DateTime.Now.Subtract(start)));
                }
                else if (TASK_PDF.Equals(taskName))
                {
                    File sourceFile = new File(fileName);
                    JasperPrint jasperPrint = JasperFillManager.fillReport(fileName, parms, ds);
                    JasperExportManager.exportReportToPdfFile(jasperPrint, fileName + ".pdf");

                    System.Console.WriteLine("TASK_PDF creation time : " + (DateTime.Now.Subtract(start)));
                }
                else if (TASK_RTF.Equals(taskName))
                {
                    File sourceFile = new File(fileName);

                    ////net.sf.jasperreports.engine.JasperReport jasperPrint = (net.sf.jasperreports.engine.JasperReport)JRLoader.loadObject(sourceFile);
                    JasperPrint jasperPrint = JasperFillManager.fillReport(fileName, parms, ds);
                    File destFile = new File(sourceFile.getParent(), jasperPrint.getName() + ".rtf");

                    JRRtfExporter exporter = new JRRtfExporter();

                    exporter.setParameter(JRExporterParameter.JASPER_PRINT, jasperPrint);
                    exporter.setParameter(JRExporterParameter.OUTPUT_FILE_NAME, destFile.toString());

                    exporter.exportReport();

                    System.Console.WriteLine("TASK_RTF creation time : " + (DateTime.Now.Subtract(start)));
                }
                else if (TASK_DOCX.Equals(taskName))
                {
                    File sourceFile = new File(fileName);

                    JasperPrint jasperPrint = JasperFillManager.fillReport(fileName, parms, ds);

                    File destFile = new File(sourceFile.getParent(), jasperPrint.getName() + ".docx");
                    JRDocxExporter exporter = new JRDocxExporter();

                    exporter.setParameter(JRExporterParameter.JASPER_PRINT, jasperPrint);
                    exporter.setParameter(JRExporterParameter.OUTPUT_FILE_NAME, destFile.toString());

                    exporter.exportReport();
                    TimeSpan prot = DateTime.Now.Subtract(start);
                    System.Console.WriteLine("TASK_DOCX creation time : " + prot.Seconds);
                }
                else if (TASK_PPTX.Equals(taskName))
                {
                    File sourceFile = new File(fileName);
                    JasperPrint jasperPrint = JasperFillManager.fillReport(fileName, parms, ds);
                    File destFile = new File(sourceFile.getParent(), jasperPrint.getName() + ".pptx");
                    JRPptxExporter exporter = new JRPptxExporter();
                    exporter.setParameter(JRExporterParameter.JASPER_PRINT, jasperPrint);
                    exporter.setParameter(JRExporterParameter.OUTPUT_FILE_NAME, destFile.toString());

                    exporter.exportReport();
                    TimeSpan prot = DateTime.Now.Subtract(start);
                    System.Console.WriteLine("TASK_PPTX creation time : " + prot.Seconds);
                }
                else if (TASK_XLS.Equals(taskName))
                {
                    File sourceFile = new File(fileName);
                    Map dateFormats = new HashMap();
                    dateFormats.put("EEE, MMM d, yyyy", "ddd, mmm d, yyyy");

                    parms.put(JRXlsExporterParameter.IS_DETECT_CELL_TYPE, java.lang.Boolean.FALSE);
                    parms.put(JRXlsExporterParameter.IS_FONT_SIZE_FIX_ENABLED, java.lang.Boolean.TRUE);
                    parms.put(JRXlsExporterParameter.IS_WHITE_PAGE_BACKGROUND, java.lang.Boolean.FALSE);
                    parms.put(JRXlsExporterParameter.IS_REMOVE_EMPTY_SPACE_BETWEEN_ROWS, java.lang.Boolean.TRUE);
                    JasperPrint jasperPrint = JasperFillManager.fillReport(fileName, parms, ds);
                    /////JasperPrint jasperPrint = (JasperPrint)JRLoader.loadObject(sourceFile);
                    //JExcelApiExporter, JROdsExporter, JRXlsAbstractMetadataExporter, JRXlsExporter, JRXlsxExporter
                    File destFile = new File(sourceFile.getParent(), jasperPrint.getName() + ".xls");
                    JRXlsExporter exporter = new JRXlsExporter();
                    exporter.setParameter(JRExporterParameter.JASPER_PRINT, jasperPrint);
                    exporter.setParameter(JRExporterParameter.OUTPUT_FILE_NAME, destFile.toString());

                    exporter.exportReport();
                    System.Console.WriteLine("TASK_XLS creation time : " + (DateTime.Now.Subtract(start)));
                }
                else if (TASK_XLSX.Equals(taskName))
                {
                    File sourceFile = new File(fileName);
                    Map dateFormats = new HashMap();
                    dateFormats.put("EEE, MMM d, yyyy", "ddd, mmm d, yyyy");

                    JasperPrint jasperPrint = JasperFillManager.fillReport(fileName, parms, ds);
                    /////JasperPrint jasperPrint = (JasperPrint)JRLoader.loadObject(sourceFile);
                    File destFile = new File(sourceFile.getParent(), jasperPrint.getName() + ".xlsx");
                    JRXlsxExporter exporter = new JRXlsxExporter();
                    exporter.setParameter(JRExporterParameter.JASPER_PRINT, jasperPrint);
                    exporter.setParameter(JRExporterParameter.OUTPUT_FILE_NAME, destFile.toString());
                    exporter.exportReport();
                    System.Console.WriteLine("TASK_XLSX creation time : " + (DateTime.Now.Subtract(start)));
                }
                else if (TASK_HTML.Equals(taskName))
                {
                    File sourceFile = new File(fileName);
                    Map dateFormats = new HashMap();
                    dateFormats.put("EEE, MMM d, yyyy", "ddd, mmm d, yyyy");

                    JasperPrint jasperPrint = JasperFillManager.fillReport(fileName, parms, ds);
                    /////JasperPrint jasperPrint = (JasperPrint)JRLoader.loadObject(sourceFile);
                    File destFile = new File(sourceFile.getParent(), jasperPrint.getName() + ".html");
                    JRHtmlExporter exporter = new JRHtmlExporter();
                    exporter.setParameter(JRExporterParameter.JASPER_PRINT, jasperPrint);
                    exporter.setParameter(JRExporterParameter.OUTPUT_FILE_NAME, destFile.toString());
                    exporter.exportReport();
                    System.Console.WriteLine("TASK_HTML creation time : " + (DateTime.Now.Subtract(start)));
                }
                //else if (TASK_JXL.Equals(taskName))
                //{
                //    File sourceFile = new File(fileName); 
                //    JasperPrint jasperPrint = JasperFillManager.fillReport(fileName, parms, ds);
                //    File destFile = new File(sourceFile.getParent(), jasperPrint.getName() + ".jxl.xls"); 
                //    JExcelApiExporter exporter = new JExcelApiExporter(); 
                //    exporter.setParameter(JRExporterParameter.JASPER_PRINT, jasperPrint);
                //    exporter.setParameter(JRExporterParameter.OUTPUT_FILE_NAME, destFile.toString());
                //    exporter.setParameter(JRXlsExporterParameter.IS_ONE_PAGE_PER_SHEET, java.lang.Boolean.TRUE);

                //    exporter.exportReport();

                //    System.Console.WriteLine("TASK_JXL XLS creation time : " + (DateTime.Now.Subtract(start)));
                //}
                else if (TASK_CSV.Equals(taskName))
                {
                    File sourceFile = new File(fileName);

                    JasperPrint jasperPrint = JasperFillManager.fillReport(fileName, parms, ds);
                    ////JasperPrint jasperPrint = (JasperPrint)JRLoader.loadObject(sourceFile);

                    File destFile = new File(sourceFile.getParent(), jasperPrint.getName() + ".csv");

                    JRCsvExporter exporter = new JRCsvExporter();

                    exporter.setParameter(JRExporterParameter.JASPER_PRINT, jasperPrint);
                    exporter.setParameter(JRExporterParameter.OUTPUT_FILE_NAME, destFile.toString());

                    exporter.exportReport();

                    System.Console.WriteLine("TASK_CSV creation time : " + (DateTime.Now.Subtract(start)));
                }
                else
                {
                    //// usage();
                }

            }
            catch (Exception e)
            {
                string str = e.StackTrace;
                System.Console.WriteLine(e.StackTrace);
                throw e;
                ///e.printStackTrace();
            }
            finally
            {

            }//end try
        }


        public static BufferedImage getJFreeChart()
        {
            BufferedImage bufferedImage = null;
            try
            {
                //string reports_dir = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "reports");
                //string fileName = System.IO.Path.Combine(reports_dir, "AreaChart.jpg");
                //bufferedImage = ImageIO.read(new File(fileName));
                DefaultPieDataset dataset = new DefaultPieDataset();
                dataset.setValue("Java", 43.2);
                dataset.setValue("Visual Basic", 10.0);
                dataset.setValue("C/C++", 17.5);
                dataset.setValue("PHP", 32.5);
                dataset.setValue("Perl", 1.0);

                JFreeChart chart = ChartFactory.createPieChart3D(
                        "Pie Chart 3D Demo 1",
                        dataset,
                        true,
                        true,
                        false
                        );

                PiePlot3D plot = (PiePlot3D)chart.getPlot();
                plot.setStartAngle(290);
                plot.setDirection(Rotation.CLOCKWISE);
                plot.setForegroundAlpha(0.5f);
                plot.setNoDataMessage("No data to display");

                /*   */
                bufferedImage = chart.createBufferedImage(515, 350);
            }
            catch (Exception ex) 
            {
                throw ex;
            }
            return bufferedImage;
        }
    }
}