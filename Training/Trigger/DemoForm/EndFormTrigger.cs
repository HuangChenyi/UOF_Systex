using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Ede.Uof.Utility.Log;
using Ede.Uof.WKF.ExternalUtility;
using Training.UCO;

namespace Training.Trigger.DemoForm
{
    public class EndFormTrigger : ICallbackTriggerPlugin
    {
        public void Finally()
        {
            //  throw new NotImplementedException();
        }

        public string GetFormResult(ApplyTask applyTask)
        {
            // throw new NotImplementedException();

            //<Form formVersionId="30d33f52-802f-49b3-933e-f93a9c5d61cb">
            //  <FormFieldValue>
            //    <FieldItem fieldId="NO" fieldValue="" realValue="" />
            //    <FieldItem fieldId="A01" fieldValue="xxx" realValue="" fillerName="黃建龍" fillerUserGuid="07a00c72-270e-403e-b9df-20b530ba45e8" fillerAccount="Howard_Huang" fillSiteId="" />
            //    <FieldItem fieldId="A02" fieldValue="3" realValue="" fillerName="黃建龍" fillerUserGuid="07a00c72-270e-403e-b9df-20b530ba45e8" fillerAccount="Howard_Huang" fillSiteId="" />
            //    <FieldItem fieldId="A03" fieldValue="4" realValue="" fillerName="黃建龍" fillerUserGuid="07a00c72-270e-403e-b9df-20b530ba45e8" fillerAccount="Howard_Huang" fillSiteId="" />
            //    <FieldItem fieldId="A04" fieldValue="222" realValue="" fillerName="黃建龍" fillerUserGuid="07a00c72-270e-403e-b9df-20b530ba45e8" fillerAccount="Howard_Huang" fillSiteId="" />
            //  </FormFieldValue>
            //</Form>

            DemoUCO uco = new DemoUCO();
            string docNbr = applyTask.FormNumber;
            string signStatus = applyTask.FormResult.ToString();

            uco.UpdateFormResult(docNbr, signStatus);
         
            if(applyTask.FormResult == Ede.Uof.WKF.Engine.ApplyResult.Adopt)
            {
                //todo
                //<Form formVersionId="4dffe3b4-7a4d-400c-9968-bfdd5eac9dee">
                //  <FormFieldValue>
                //    <FieldItem fieldId="NO" fieldValue="" realValue="" enableSearch="True" />
                //    <FieldItem fieldId="A01" ConditionValue="" realValue="" fillerName="Tony" fillerUserGuid="c496e32b-0968-4de5-95fc-acf7e5a561c0" fillerAccount="Tony" fillSiteId="">
                //      <FormChooseInfo taskGuid="66d203b9-fe6d-4745-afb4-a4babd727f92" />
                //    </FieldItem>
                //    <FieldItem fieldId="type" fieldValue="A" realValue="" enableSearch="True" customValue="@null" fillerName="Tony" fillerUserGuid="c496e32b-0968-4de5-95fc-acf7e5a561c0" fillerAccount="Tony" fillSiteId="" />
                //    <FieldItem fieldId="item" fieldValue="s" realValue="" enableSearch="True" fillerName="Tony" fillerUserGuid="c496e32b-0968-4de5-95fc-acf7e5a561c0" fillerAccount="Tony" fillSiteId="" />
                //    <FieldItem fieldId="amount" fieldValue="2" realValue="" enableSearch="True" fillerName="Tony" fillerUserGuid="c496e32b-0968-4de5-95fc-acf7e5a561c0" fillerAccount="Tony" fillSiteId="" />
                //  </FormFieldValue>
                //</Form>
                var applyInfo = applyTask.Task.Applicant;
                LabForm form = new LabForm("4dffe3b4-7a4d-400c-9968-bfdd5eac9dee",
                     UrgentLevel.Normal,
                     applyInfo.Account,applyInfo.UserGUID,
                     applyInfo.UserName);


                form.Fields.Field_type.FieldValue = "A";
                form.Fields.Field_NO.IsNeedAutoNbr = true.ToString();
                form.Fields.Field_item.FieldValue = "BB";
                form.Fields.Field_amount.FieldValue = "100";
                form.Fields.Field_NO.FieldValue = DateTime.Now.ToString("yyyyMMddHHmmss");
                XElement a01Value = new XElement("FormChooseInfo",
                    new XAttribute("taskGuid",applyTask.TaskId));
                form.Fields.Field_A01.FieldValue = a01Value.ToString();

                string result = "";

                Ede.Uof.WKF.Utility.TaskUtilityUCO taskUCO = new Ede.Uof.WKF.Utility.TaskUtilityUCO();
                result =taskUCO.WebService_CreateTask(form.ConvertToFormInfoXml());


                XElement resultXE = XElement.Parse(result);
                string msg = "";
                if (resultXE.Element("Status").Value == "1")
                {
                    string formNBR = resultXE.Element("FormNumber").Value;
                    msg = $"起單成功{formNBR}";

                    Logger.Write("SystexInfo", msg);
                }
                else
                {
                    string type = resultXE.Element("Exception").Element("Type").Value;
                    msg = $"起單失敗\r\n，失敗原因{type}，表單XML:{form.ConvertToFormInfoXml()}";

                    Logger.Write("SystexInfo",msg);
                    throw new Exception(msg);
                
                }


            }
            
            return "";
        }

        public void OnError(Exception errorException)
        {
            //  throw new NotImplementedException();
        }
    }
}
