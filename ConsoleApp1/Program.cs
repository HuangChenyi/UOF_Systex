using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string key = "PFJTQUtleVZhbHVlPjxNb2R1bHVzPnFCNENRVlFSS1JiQncxb3ZWa2xrVW95K1JLU2UwQUZ1WWxtNGlwaVFFc1dwWUcrYm1ocUFJRkN4UnI5S2syRk16aklvRmc1ZHYwWWdzU2Y1V3FGOXViWW5qTDJuNGtyalRNZXBiVWlycGErNUtLVkJhZUNKTXkxRlJjZDUxd2NCU0szSzlqTVRkQUM4ZHBDT0l3Z3lEb1NjYjd5eTUrYzdka1pQT1EzWlFGMD08L01vZHVsdXM+PEV4cG9uZW50PkFRQUI8L0V4cG9uZW50PjwvUlNBS2V5VmFsdWU+";
            Auth.Authentication auth = new Auth.Authentication();
            auth.Url = "http://localhost/UOF_Systex"+"/PublicAPI/System/Authentication.asmx";
          string token=  auth.GetToken("ERP",
                RSAEncrypt(key,"admin"),
                RSAEncrypt(key,"123456"));


            WKF.Wkf wkf = new WKF.Wkf();
            wkf.Url = "http://localhost/UOF_Systex"+"/PublicAPI/WKF/Wkf.asmx";

            string result = "";
            //  result = wkf.GetFormList(token);
            //result = wkf.GetFormStructure(token, "0418e15d-67ee-4583-968d-b55f0bcae18b");

            LabForm form = new LabForm("6fd8fa9c-ecc2-41b3-8315-7c77d9e9e676",
                 UrgentLevel.Normal,
                 "Tony", "c496e32b-0968-4de5-95fc-acf7e5a561c0", "Tony");

            form.Fields.Field_type.FieldValue = "A";
            form.Fields.Field_NO.FieldValue=DateTime.Now.ToString("yyyyMMddHHmmss");
            form.Fields.Field_item.FieldValue = "item";
            form.Fields.Field_amount.FieldValue = "100";
            Console.WriteLine(form.ConvertToFormInfoXml());

           result = wkf.SendForm(token, form.ConvertToFormInfoXml());
            Console.WriteLine(result);
        }

        /// <summary>
        /// RSA 加密
        /// </summary>
        /// <param name="privateKey"></param>
        /// <param name="crTexturlparam>
        /// <returns></returns>
        private static string RSAEncrypt(string publicKey, string crText)
        {

            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();

            byte[] base64PublicKey = Convert.FromBase64String(publicKey);
            rsa.FromXmlString(System.Text.Encoding.UTF8.GetString(base64PublicKey));


            byte[] ctTextArray = Encoding.UTF8.GetBytes(crText);


            byte[] decodeBs = rsa.Encrypt(ctTextArray, false);

            return Convert.ToBase64String(decodeBs);
        }
    }
}
