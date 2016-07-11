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
            DSTest sqlconn_test = new SqlConnectionDSTest();
            sqlconn_test.ExpReort(DSTest.TASK_PDF);
            sqlconn_test.ExpReort(DSTest.TASK_DOCX);
            sqlconn_test.ExpReort(DSTest.TASK_XLS);
            sqlconn_test.ExpReort(DSTest.TASK_XLSX);
            sqlconn_test.ExpReort(DSTest.TASK_PPTX);
            sqlconn_test.ExpReort(DSTest.TASK_RTF);
            sqlconn_test.ExpReort(DSTest.TASK_HTML);

            DSTest jrtable_test = new JRTableModelDSTest(); 
            jrtable_test.ExpReort(DSTest.TASK_PDF);
            jrtable_test.ExpReort(DSTest.TASK_DOCX);
            jrtable_test.ExpReort(DSTest.TASK_XLS);
            jrtable_test.ExpReort(DSTest.TASK_XLSX);
            jrtable_test.ExpReort(DSTest.TASK_PPTX);
            jrtable_test.ExpReort(DSTest.TASK_RTF);
            jrtable_test.ExpReort(DSTest.TASK_HTML);

        }
    }
}
