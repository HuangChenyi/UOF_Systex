using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Training.PO;
using Training.Data;

namespace Training.UCO
{
    public  class DemoUCO
    {
        DemoPO m_DemoPO = new DemoPO();


        public DataTable GetUserData(string groupId)
        {
            return m_DemoPO.GetUserData(groupId);
        }

        public void InsertTaskData(DataRow dr)
        {
            m_DemoPO.InsertTaskData(dr);
        }


        public List<ExcelData> ParseExcel(string fileName)
        {
            List<ExcelData> list = new List<ExcelData>();

            OfficeOpenXml.ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            using (OfficeOpenXml.ExcelPackage package = 
                new OfficeOpenXml.ExcelPackage(new System.IO.FileInfo(fileName)))
            {
                var sheet = package.Workbook.Worksheets[0];
                var start = sheet.Dimension.Start;
                var end = sheet.Dimension.End;
                for (int i = 2; i <= end.Row; i++)
                {
                    ExcelData data = new ExcelData();
                    data.品項 = sheet.Cells[i, 1].Text;
                    data.數量 = double.Parse(sheet.Cells[i, 2].Text);
                    data.金額 = double.Parse(sheet.Cells[i, 3].Text);
                    data.小計 = double.Parse(sheet.Cells[i, 4].Text);

                    list.Add(data);
                }
            }

            return list;
        }
      

        public DataTable GetUserData()
        {
            return m_DemoPO.GetUserData();
        }

        public void InsertWsEndFormData(DataRow dr)
        {
            m_DemoPO.InsertWsEndFormData(dr);
        }


        public void InsertDDLStartFormData(DemoDataSet.TB_DEMO_DLL_FORMRow dr)
        {
            m_DemoPO.InsertDDLStartFormData(dr);
        }

        public void UpdateFormStatus(string docNbr, string signStatus)
        {
            m_DemoPO.UpdateFormStatus(docNbr, signStatus);
        }

        public void UpdateFormResult(string docNbr, string formResult)
        {
            m_DemoPO.UpdateFormResult(docNbr, formResult);
        }

    }


    public class ExcelData
    {
        public string 品項 { get; set; }
        public double 數量 { get; set; }
        public double 金額 { get; set; }
        public double 小計 { get; set; }
    }
}
