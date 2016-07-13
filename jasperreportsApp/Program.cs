using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jasperreportsApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ChartsDSTest charts_test = new ChartsDSTest();
            charts_test.ExpReort(DSTest.TASK_PDF, ChartsDSTest.ChartType.AreaChartReport);
            charts_test.ExpReort(DSTest.TASK_PDF, ChartsDSTest.ChartType.Bar3DChartReport);
            charts_test.ExpReort(DSTest.TASK_PDF, ChartsDSTest.ChartType.BubbleChartReport);
            charts_test.ExpReort(DSTest.TASK_PDF, ChartsDSTest.ChartType.CandlestickChartReport);
            charts_test.ExpReort(DSTest.TASK_PDF, ChartsDSTest.ChartType.HighLowChartReport);
            charts_test.ExpReort(DSTest.TASK_PDF, ChartsDSTest.ChartType.LineChartReport);
            charts_test.ExpReort(DSTest.TASK_PDF, ChartsDSTest.ChartType.MeterChartReport);
            charts_test.ExpReort(DSTest.TASK_PDF, ChartsDSTest.ChartType.MultipleAxisChartReport);
            charts_test.ExpReort(DSTest.TASK_PDF, ChartsDSTest.ChartType.Pie3DChartReport);
            charts_test.ExpReort(DSTest.TASK_PDF, ChartsDSTest.ChartType.PieChartReport);
            charts_test.ExpReort(DSTest.TASK_PDF, ChartsDSTest.ChartType.Pie3DChartReport);
            charts_test.ExpReort(DSTest.TASK_PDF, ChartsDSTest.ChartType.ScatterChartReport);
            charts_test.ExpReort(DSTest.TASK_PDF, ChartsDSTest.ChartType.StackedAreaChartReport);
            charts_test.ExpReort(DSTest.TASK_PDF, ChartsDSTest.ChartType.StackedBar3DChartReport);
            charts_test.ExpReort(DSTest.TASK_PDF, ChartsDSTest.ChartType.StackedBarChartReport);
            charts_test.ExpReort(DSTest.TASK_PDF, ChartsDSTest.ChartType.ThermometerChartReport);
            charts_test.ExpReort(DSTest.TASK_PDF, ChartsDSTest.ChartType.TimeSeriesChartReport);
            charts_test.ExpReort(DSTest.TASK_PDF, ChartsDSTest.ChartType.XYAreaChartReport);
            charts_test.ExpReort(DSTest.TASK_PDF, ChartsDSTest.ChartType.XYBarChartReport);
            charts_test.ExpReort(DSTest.TASK_PDF, ChartsDSTest.ChartType.XYBarChartTimePeriodReport);
            charts_test.ExpReort(DSTest.TASK_PDF, ChartsDSTest.ChartType.XYBarChartTimeSeriesReport);
            charts_test.ExpReort(DSTest.TASK_PDF, ChartsDSTest.ChartType.XYLineChartReport); 

            //DSTest sqlconn_test = new SqlConnectionDSTest();
            //sqlconn_test.ExpReort(DSTest.TASK_PDF);
            //sqlconn_test.ExpReort(DSTest.TASK_DOCX);
            //sqlconn_test.ExpReort(DSTest.TASK_XLS);
            //sqlconn_test.ExpReort(DSTest.TASK_XLSX);
            //sqlconn_test.ExpReort(DSTest.TASK_PPTX);
            //sqlconn_test.ExpReort(DSTest.TASK_RTF);
            //sqlconn_test.ExpReort(DSTest.TASK_HTML);

            //DSTest jrtable_test = new JRTableModelDSTest(); 
            //jrtable_test.ExpReort(DSTest.TASK_PDF);
            //jrtable_test.ExpReort(DSTest.TASK_DOCX);
            //jrtable_test.ExpReort(DSTest.TASK_XLS);
            //jrtable_test.ExpReort(DSTest.TASK_XLSX);
            //jrtable_test.ExpReort(DSTest.TASK_PPTX);
            //jrtable_test.ExpReort(DSTest.TASK_RTF);
            //jrtable_test.ExpReort(DSTest.TASK_HTML);

        }
    }
}
