using System;
using java.io;
using java.util;
using net.sf.jasperreports.engine;
using net.sf.jasperreports.engine.export;
using net.sf.jasperreports.engine.util;
using net.sf.jasperreports.engine.export.ooxml;
using java.awt.image;
using org.krysalis.barcode4j;
using org.krysalis.barcode4j.output.bitmap;
using org.krysalis.barcode4j.impl.code39;
using org.krysalis.barcode4j.tools;
using com.google.zxing.qrcode;
using com.google.zxing.common;
using com.google.zxing;
using java.lang;
using java.awt;
using org.krysalis.barcode4j.impl;
using org.krysalis.barcode4j.impl.code128;
using org.krysalis.barcode4j.impl.codabar;
using org.krysalis.barcode4j.impl.datamatrix;
using org.krysalis.barcode4j.impl.fourstate;
using org.krysalis.barcode4j.impl.int2of5;
using org.krysalis.barcode4j.impl.upcean;
using org.krysalis.barcode4j.impl.postnet;
using org.krysalis.barcode4j.impl.pdf417;

namespace jasperreportsApp
{
    public class BarcodeTest : DSTest
    {
        private string fileName = "Barcode4J2Report.jasper";

        override public void ExpReort(string taskName)
        {

            try
            {
                string reports_dir = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "reports");
                fileName = System.IO.Path.Combine(reports_dir, fileName);
                DateTime start = DateTime.Now;/////  DateTime.Now.Millisecond;  
                JREmptyDataSource ds = new JREmptyDataSource();

                java.util.Map parms = new java.util.HashMap();
                GetBarcode(parms);
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
            catch (System.Exception e)
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


        private void GetBarcode(java.util.Map parms)
        {
             
            try
            {
                int dpi = 150;
                BitmapCanvasProvider canvasCode128 = new BitmapCanvasProvider(dpi, BufferedImage.TYPE_BYTE_BINARY, false, 0);
                //<parameter name = "Code128" class="java.awt.Image" isForPrompting="false"> 
                AbstractBarcodeBean beanCode128 = new Code128Bean();
                //Configure the barcode generator
                beanCode128.setModuleWidth(UnitConv.in2mm(1.0f / dpi)); //makes the narrow bar  //width exactly one pixel  //bean.setWideFactor(3);
                beanCode128.doQuietZone(false);
                //Set up the canvas provider for monochrome PNG output  
                //Generate the barcode
                beanCode128.generateBarcode(canvasCode128, "ABC123456123456");
                //Signal end of generation
                canvasCode128.finish();
                parms.put("Code128", canvasCode128.getBufferedImage());

                //<parameter name = "Codabar" class="java.awt.Image" isForPrompting="false">  
                BitmapCanvasProvider canvasCodabar = new BitmapCanvasProvider(dpi, BufferedImage.TYPE_BYTE_BINARY, false, 0);
                AbstractBarcodeBean beanCodabar = new CodabarBean();
                //Configure the barcode generator
                beanCodabar.setModuleWidth(UnitConv.in2mm(1.0f / dpi)); //makes the narrow bar  //width exactly one pixel  //bean.setWideFactor(3);
                beanCodabar.doQuietZone(false);
                //Set up the canvas provider for monochrome PNG output  
                //Generate the barcode
                beanCodabar.generateBarcode(canvasCodabar, "01234567890");
                //Signal end of generation
                canvasCodabar.finish();
                parms.put("Codabar", canvasCodabar.getBufferedImage());

                int dpiDataMatrix = 300;
                //<parameter name = "DataMatrix" class="java.awt.Image" isForPrompting="false">
                BitmapCanvasProvider canvasDataMatrix = new BitmapCanvasProvider(dpiDataMatrix, BufferedImage.TYPE_BYTE_BINARY, false, 0);
                AbstractBarcodeBean beanDataMatrix = new DataMatrixBean();
                //Configure the barcode generator
                beanDataMatrix.setModuleWidth(UnitConv.in2mm(1.0f / dpiDataMatrix)); //makes the narrow bar  //width exactly one pixel  //bean.setWideFactor(3);
                beanDataMatrix.doQuietZone(false);
                //Set up the canvas provider for monochrome PNG output  
                //Generate the barcode
                beanDataMatrix.generateBarcode(canvasDataMatrix, "JasperReportsABC123456123456");
                //Signal end of generation
                canvasDataMatrix.finish();
                parms.put("DataMatrix", canvasDataMatrix.getBufferedImage());


                //<parameter name = "EAN128" class="java.awt.Image" isForPrompting="false"> 
                BitmapCanvasProvider canvasEAN128 = new BitmapCanvasProvider(dpi, BufferedImage.TYPE_BYTE_BINARY, false, 0);
                AbstractBarcodeBean beanDataEAN128 = new EAN128Bean();
                //Configure the barcode generator
                beanDataEAN128.setModuleWidth(UnitConv.in2mm(1.0f / dpi)); //makes the narrow bar  //width exactly one pixel  //bean.setWideFactor(3);
                beanDataEAN128.doQuietZone(false);
                //Set up the canvas provider for monochrome PNG output  
                //Generate the barcode
                beanDataEAN128.generateBarcode(canvasEAN128, "0101234567890128");
                //Signal end of generation
                canvasEAN128.finish();
                parms.put("EAN128", canvasEAN128.getBufferedImage());

                //<parameter name = "Code39" class="java.awt.Image" isForPrompting="false"> 
                //Create the barcode bean
                BitmapCanvasProvider canvasCode39 = new BitmapCanvasProvider(dpi, BufferedImage.TYPE_BYTE_BINARY, false, 0);
                AbstractBarcodeBean beanCode39 = new Code39Bean();
                //Configure the barcode generator
                beanCode39.setModuleWidth(UnitConv.in2mm(1.0f / dpi)); //makes the narrow bar  //width exactly one pixel  //bean.setWideFactor(3);
                beanCode39.doQuietZone(false);
                //Set up the canvas provider for monochrome PNG output 
                //BitmapCanvasProvider canvas = new BitmapCanvasProvider(outs, "image/x-png", dpi, BufferedImage.TYPE_BYTE_BINARY, false, 0);
                //Generate the barcode
                beanCode39.generateBarcode(canvasCode39, "0123456789");
                //Signal end of generation
                canvasCode39.finish();
                /////BufferedImage barcodeImage = canvas.getBufferedImage(); 
                parms.put("Code39", canvasCode39.getBufferedImage());

                //<parameter name = "USPSIntelligentMail" class="java.awt.Image" isForPrompting="false"> 
                BitmapCanvasProvider canvasUSPSIntelligentMail = new BitmapCanvasProvider(dpi, BufferedImage.TYPE_BYTE_BINARY, false, 0);
                AbstractBarcodeBean beanUSPSIntelligentMail = new USPSIntelligentMailBean();
                //Configure the barcode generator
                beanUSPSIntelligentMail.setModuleWidth(UnitConv.in2mm(1.0f / dpi)); //makes the narrow bar  //width exactly one pixel  //bean.setWideFactor(3);
                beanUSPSIntelligentMail.doQuietZone(false);
                //Set up the canvas provider for monochrome PNG output 
                //BitmapCanvasProvider canvas = new BitmapCanvasProvider(outs, "image/x-png", dpi, BufferedImage.TYPE_BYTE_BINARY, false, 0);
                //Generate the barcode
                beanUSPSIntelligentMail.generateBarcode(canvasUSPSIntelligentMail, "00040123456200800001987654321");
                //Signal end of generation
                canvasUSPSIntelligentMail.finish();
                /////BufferedImage barcodeImage = canvas.getBufferedImage(); 
                parms.put("USPSIntelligentMail", canvasUSPSIntelligentMail.getBufferedImage());

                //<parameter name = "RoyalMailCustomer" class="java.awt.Image" isForPrompting="false">  
                //BitmapCanvasProvider canvasRoyalMailCustomer = new BitmapCanvasProvider(dpi, BufferedImage.TYPE_BYTE_BINARY, false, 0);
                //AbstractBarcodeBean beanRoyalMailCustomer = new RoyalMailCustomerBean();
                ////Configure the barcode generator
                //beanUSPSIntelligentMail.setModuleWidth(UnitConv.in2mm(1.0f / dpi)); //makes the narrow bar  //width exactly one pixel  //bean.setWideFactor(3);
                //beanUSPSIntelligentMail.doQuietZone(false);
                ////Set up the canvas provider for monochrome PNG output 
                ////BitmapCanvasProvider canvas = new BitmapCanvasProvider(outs, "image/x-png", dpi, BufferedImage.TYPE_BYTE_BINARY, false, 0);
                ////Generate the barcode
                //beanUSPSIntelligentMail.generateBarcode(canvasUSPSIntelligentMail, "ABC123456123456");
                ////Signal end of generation
                //canvasUSPSIntelligentMail.finish();
                ///////BufferedImage barcodeImage = canvas.getBufferedImage(); 
                //parms.put("USPSIntelligentMail", canvasUSPSIntelligentMail.getBufferedImage());

                //<parameter name = "Interleaved2Of5" class="java.awt.Image" isForPrompting="false"> 
                BitmapCanvasProvider canvasInterleaved2Of5 = new BitmapCanvasProvider(dpi, BufferedImage.TYPE_BYTE_BINARY, false, 0);
                AbstractBarcodeBean beanInterleaved2Of5 = new Interleaved2Of5Bean();
                //Configure the barcode generator
                beanInterleaved2Of5.setModuleWidth(UnitConv.in2mm(1.0f / dpi)); //makes the narrow bar  //width exactly one pixel  //bean.setWideFactor(3);
                beanInterleaved2Of5.doQuietZone(false);
                //Set up the canvas provider for monochrome PNG output 
                //BitmapCanvasProvider canvas = new BitmapCanvasProvider(outs, "image/x-png", dpi, BufferedImage.TYPE_BYTE_BINARY, false, 0);
                //Generate the barcode
                beanInterleaved2Of5.generateBarcode(canvasInterleaved2Of5, "0123456789");
                //Signal end of generation
                canvasInterleaved2Of5.finish();
                /////BufferedImage barcodeImage = canvas.getBufferedImage(); 
                parms.put("Interleaved2Of5", canvasInterleaved2Of5.getBufferedImage());

                //<parameter name = "UPCA" class="java.awt.Image" isForPrompting="false">
                BitmapCanvasProvider canvasUPCA = new BitmapCanvasProvider(dpi, BufferedImage.TYPE_BYTE_BINARY, false, 0);
                AbstractBarcodeBean beanUPCA = new UPCABean();
                //Configure the barcode generator
                beanUPCA.setModuleWidth(UnitConv.in2mm(1.0f / dpi)); //makes the narrow bar  //width exactly one pixel  //bean.setWideFactor(3);
                beanUPCA.doQuietZone(false);
                //Set up the canvas provider for monochrome PNG output 
                //BitmapCanvasProvider canvas = new BitmapCanvasProvider(outs, "image/x-png", dpi, BufferedImage.TYPE_BYTE_BINARY, false, 0);
                //Generate the barcode
                beanUPCA.generateBarcode(canvasUPCA, "01234567890");
                //Signal end of generation
                canvasUPCA.finish();
                /////BufferedImage barcodeImage = canvas.getBufferedImage(); 
                parms.put("UPCA", canvasUPCA.getBufferedImage());

                //<parameter name = "UPCE" class="java.awt.Image" isForPrompting="false"> 
                BitmapCanvasProvider canvasUPCE = new BitmapCanvasProvider(dpi, BufferedImage.TYPE_BYTE_BINARY, false, 0);
                AbstractBarcodeBean beanUPCE = new UPCABean();
                //Configure the barcode generator
                beanUPCE.setModuleWidth(UnitConv.in2mm(1.0f / dpi)); //makes the narrow bar  //width exactly one pixel  //bean.setWideFactor(3);
                beanUPCE.doQuietZone(false);
                //Set up the canvas provider for monochrome PNG output 
                //BitmapCanvasProvider canvas = new BitmapCanvasProvider(outs, "image/x-png", dpi, BufferedImage.TYPE_BYTE_BINARY, false, 0);
                //Generate the barcode
                beanUPCE.generateBarcode(canvasUPCE, "01234567890");
                //Signal end of generation
                canvasUPCE.finish();
                /////BufferedImage barcodeImage = canvas.getBufferedImage(); 
                parms.put("UPCE", canvasUPCE.getBufferedImage());

                //<parameter name = "EAN13" class="java.awt.Image" isForPrompting="false"> 
                BitmapCanvasProvider canvasEAN13 = new BitmapCanvasProvider(dpi, BufferedImage.TYPE_BYTE_BINARY, false, 0);
                AbstractBarcodeBean beanEAN13 = new EAN13Bean();
                //Configure the barcode generator
                beanEAN13.setModuleWidth(UnitConv.in2mm(1.0f / dpi)); //makes the narrow bar  //width exactly one pixel  //bean.setWideFactor(3);
                beanEAN13.doQuietZone(false);
                //Set up the canvas provider for monochrome PNG output 
                //BitmapCanvasProvider canvas = new BitmapCanvasProvider(outs, "image/x-png", dpi, BufferedImage.TYPE_BYTE_BINARY, false, 0);
                //Generate the barcode
                beanEAN13.generateBarcode(canvasEAN13, "012345678901");
                //Signal end of generation
                canvasEAN13.finish();
                /////BufferedImage barcodeImage = canvas.getBufferedImage(); 
                parms.put("EAN13", canvasEAN13.getBufferedImage());

                //<parameter name = "EAN8" class="java.awt.Image" isForPrompting="false"> 
                BitmapCanvasProvider canvasEAN8 = new BitmapCanvasProvider(dpi, BufferedImage.TYPE_BYTE_BINARY, false, 0);
                AbstractBarcodeBean beanEAN8 = new EAN8Bean();
                //Configure the barcode generator
                beanEAN8.setModuleWidth(UnitConv.in2mm(1.0f / dpi)); //makes the narrow bar  //width exactly one pixel  //bean.setWideFactor(3);
                beanEAN8.doQuietZone(false);
                //Set up the canvas provider for monochrome PNG output 
                //BitmapCanvasProvider canvas = new BitmapCanvasProvider(outs, "image/x-png", dpi, BufferedImage.TYPE_BYTE_BINARY, false, 0);
                //Generate the barcode
                beanEAN8.generateBarcode(canvasEAN8, "01234565");
                //Signal end of generation
                canvasEAN8.finish();
                /////BufferedImage barcodeImage = canvas.getBufferedImage(); 
                parms.put("EAN8", canvasEAN8.getBufferedImage());

                //<parameter name = "POSTNET" class="java.awt.Image" isForPrompting="false"> 
                BitmapCanvasProvider canvasPOSTNET = new BitmapCanvasProvider(dpi, BufferedImage.TYPE_BYTE_BINARY, false, 0);
                AbstractBarcodeBean beanPOSTNET = new POSTNETBean();
                //Configure the barcode generator
                beanPOSTNET.setModuleWidth(UnitConv.in2mm(1.0f / dpi)); //makes the narrow bar  //width exactly one pixel  //bean.setWideFactor(3);
                beanPOSTNET.doQuietZone(false);
                //Set up the canvas provider for monochrome PNG output 
                //BitmapCanvasProvider canvas = new BitmapCanvasProvider(outs, "image/x-png", dpi, BufferedImage.TYPE_BYTE_BINARY, false, 0);
                //Generate the barcode
                beanPOSTNET.generateBarcode(canvasPOSTNET, "01234");
                //Signal end of generation
                canvasPOSTNET.finish();
                /////BufferedImage barcodeImage = canvas.getBufferedImage(); 
                parms.put("POSTNET", canvasPOSTNET.getBufferedImage());

                //<parameter name = "PDF417" class="java.awt.Image" isForPrompting="false"> 
                BitmapCanvasProvider canvasPDF417 = new BitmapCanvasProvider(dpi, BufferedImage.TYPE_BYTE_BINARY, false, 0);
                AbstractBarcodeBean beanPDF417 = new PDF417Bean();
                //Configure the barcode generator
                beanPDF417.setModuleWidth(UnitConv.in2mm(1.0f / dpi)); //makes the narrow bar  //width exactly one pixel  //bean.setWideFactor(3);
                beanPDF417.doQuietZone(false);
                //Set up the canvas provider for monochrome PNG output 
                //BitmapCanvasProvider canvas = new BitmapCanvasProvider(outs, "image/x-png", dpi, BufferedImage.TYPE_BYTE_BINARY, false, 0);
                //Generate the barcode
                beanPDF417.generateBarcode(canvasPDF417, "ABC123456123456");
                //Signal end of generation
                canvasPDF417.finish();
                /////BufferedImage barcodeImage = canvas.getBufferedImage(); 
                parms.put("PDF417", canvasPDF417.getBufferedImage());

                //<parameter name = "QRCode" class="java.awt.Image" isForPrompting="false"> 
                QRCodeWriter writer = new QRCodeWriter();
                BitMatrix matrix = writer.encode("https://github.com/Jenli-Chen/jasperreports4dotnet", BarcodeFormat.QR_CODE, 100, 100);
                parms.put("QRCode", toBufferedImage(matrix));
            }
            catch (WriterException e)
            {
                e.printStackTrace();
            }
            finally
            { 
            }


        }

        private BufferedImage toBufferedImage(BitMatrix bitMatrix)
        {

            if (bitMatrix == null)
            {
                throw new IllegalArgumentException("BitMatrix cannot be null");
            }
            int width = bitMatrix.getWidth();
            int height = bitMatrix.getHeight();
            BufferedImage image = new BufferedImage(width, height, BufferedImage.TYPE_BYTE_INDEXED);
            image.createGraphics();
            Graphics2D graphics = (Graphics2D)image.getGraphics();
            graphics.setColor(Color.WHITE);
            graphics.fillRect(0, 0, width, height);
            graphics.setColor(Color.BLACK);
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (bitMatrix.get(i, j))
                    {
                        graphics.fillRect(i, j, 1, 1);
                    }
                }
            }
            return image;
        }
    }
}