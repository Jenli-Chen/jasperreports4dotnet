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
using net.sf.jasperreports.engine.data;

namespace jasperreportsApp
{
    public class SpiderChartTest : DSTest
    { 
        private string fileName = "SpiderChart.jasper";
 
        override public void ExpReort(string taskName)
        {
             
            try
            {
                string reports_dir = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "reports");
                fileName = System.IO.Path.Combine(reports_dir, fileName); 
                DateTime start = DateTime.Now;/////  DateTime.Now.Millisecond;  
                JRCsvDataSource ds = null;
                try
                {
                    string csvPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data/spiderDatasource.csv");
                    String[] columnNames = new String[] { "value", "series", "category" };
                    ds = new JRCsvDataSource(JRLoader.getLocationInputStream(csvPath), "UTF-8");
                    ds.setRecordDelimiter("\n");
                    ds.setUseFirstRowAsHeader(false);
                    ds.setColumnNames(columnNames);
                }
                catch (UnsupportedEncodingException e)
                {
                    throw new JRException(e);
                }

                java.util.Map parms = new java.util.HashMap(); 
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


        public static BufferedImage getJChart()
        {
            BufferedImage bufferedImage = null;
            try
            {
                //string reports_dir = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "reports");
                //string fileName = System.IO.Path.Combine(reports_dir, "AreaChart.jpg");
                //bufferedImage = ImageIO.read(new File(fileName));
                AreaChartProperties areaChartProperties = new AreaChartProperties();
                double[][] data = new double[][] {
                    new double[]{ 10, 15, 30, 53 },
                    new double[]{ 6, 30, 10, 21 },
                    new double[]{ 20, 25, 20, 8 } };
                Paint[] paints = { new Color(0, 255, 0, 100), new Color(255, 0, 0, 100), new Color(0, 0, 255, 100) };
                String[] legendLabels = { "Games", "Events", "Players" };
                AxisChartDataSet axisChartDataSet = new AxisChartDataSet(data, legendLabels, paints, ChartType.AREA, areaChartProperties);

                String[] axisLabels = { "January", "March", "May", "June" };
                DataSeries dataSeries = new DataSeries(axisLabels, "Months", "People", "Popular Events");
                dataSeries.addIAxisPlotDataSet(axisChartDataSet);

                ChartProperties chartProperties = new ChartProperties();
                AxisProperties axisProperties = new AxisProperties();
                axisProperties.setYAxisRoundValuesToNearest(0);
                LegendProperties legendProperties = new LegendProperties();

                AxisChart axisChart = new AxisChart(dataSeries, chartProperties, axisProperties, legendProperties, 500, 350);

                bufferedImage = new BufferedImage(500, 350, BufferedImage.TYPE_INT_RGB);

                axisChart.setGraphics2D(bufferedImage.createGraphics());
                axisChart.render();

            }
            catch (ChartDataException chartDataException)

            {
                throw new JRScriptletException(chartDataException);
            }
            return bufferedImage;
        }
    }
}