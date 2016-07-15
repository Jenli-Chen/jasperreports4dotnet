/*
 * JasperReports - Free Java Reporting Library.
 * Copyright (C) 2001 - 2016 TIBCO Software Inc. All rights reserved.
 * http://www.jaspersoft.com
 *
 * Unless you have purchased a commercial license agreement from Jaspersoft,
 * the following license terms apply:
 *
 * This program is part of JasperReports.
 *
 * JasperReports is free software: you can redistribute it and/or modify
 * it under the terms of the GNU Lesser General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * JasperReports is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
 * GNU Lesser General Public License for more details.
 *
 * You should have received a copy of the GNU Lesser General Public License
 * along with JasperReports. If not, see <http://www.gnu.org/licenses/>.
 */


using net.sf.jasperreports.engine;
using java.awt;
using java.awt.image;
using org.jCharts.axisChart;
using org.jCharts.chartData;
using org.jCharts.properties;
using org.jCharts.types; 


/**
 * @author Teodor Danciu (teodord@users.sourceforge.net)
 */
public class JChartsScriptlet : JRDefaultScriptlet 
{


	/**
	 *
	 */
	override public void afterReportInit() 
	{
		try 
		{
			AreaChartProperties areaChartProperties = new AreaChartProperties();

			double[][] data = new double[][] { new double[] { 10, 15, 30, 53},
                new double[]{6, 30, 10, 21}, new double[]{20, 25, 20, 8}};
			Paint[] paints = {new Color( 0, 255, 0, 100 ), new Color( 255, 0, 0, 100 ), new Color( 0, 0, 255, 100 )};
			string[] legendLabels = { "Games", "Events", "Players" };
			AxisChartDataSet axisChartDataSet = new AxisChartDataSet(data, legendLabels, paints, ChartType.AREA, areaChartProperties);

            string[] axisLabels = {"January", "March", "May", "June"};
			DataSeries dataSeries = new DataSeries(axisLabels, "Months", "People", "Popular Events");
			dataSeries.addIAxisPlotDataSet(axisChartDataSet);

			ChartProperties chartProperties = new ChartProperties();
			AxisProperties axisProperties = new AxisProperties();
			axisProperties.setYAxisRoundValuesToNearest(0);
			LegendProperties legendProperties = new LegendProperties();

			AxisChart axisChart = new AxisChart(dataSeries, chartProperties, axisProperties, legendProperties, 500, 350);

			BufferedImage bufferedImage = new BufferedImage(500, 350, BufferedImage.TYPE_INT_RGB);

			axisChart.setGraphics2D(bufferedImage.createGraphics());
			axisChart.render();

			base.setVariableValue("ChartImage", bufferedImage);
		}
		catch(ChartDataException chartDataException) 
		{
			throw new JRScriptletException(chartDataException);
		}
	}


}
