using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JuManage
{
    class PrinterHelper
    {
        String fileName;
        Microsoft.Office.Interop.Word.Application wordApp;
        Microsoft.Office.Interop.Word.Document printDoc;
        String oringinFileName = "C:\\Users\\u10168\\Downloads\\貨運單\\大榮test.docx";
        public PrinterHelper()
        {
            this.fileName = oringinFileName;
            wordApp = new Microsoft.Office.Interop.Word.Application { Visible = true };
            printDoc = wordApp.Documents.Open(fileName, ReadOnly: false, Visible: true);
            printDoc.Activate();
        }
        public PrinterHelper(String fileName) 
        {
            this.fileName = fileName;
            wordApp = new Microsoft.Office.Interop.Word.Application { Visible = true };
            printDoc = wordApp.Documents.Open(fileName, ReadOnly: false, Visible: true);
            printDoc.Activate();
        }


        //大榮格式
        public void Print(string recipient_name, string recipient_phone,string recipient_company, string recipient_address, string sender_name, string sender_phone,string sender_company, string sender_address)
        {
            try 
            {
                printDoc.Activate();
                FindAndReplace("Recipient_name", recipient_name);
                FindAndReplace("Recipient_phone", recipient_phone);
                FindAndReplace("Recipient_company", recipient_company);
                FindAndReplace("Recipient_address", recipient_address);

                FindAndReplace("Sender_name", sender_name);
                FindAndReplace("Sender_phone", sender_phone);
                FindAndReplace("Sender_company", sender_company);
                FindAndReplace("Sender_address", sender_address);
                printDoc.PrintOut();
                Thread.Sleep(1000);
                wordApp.Quit(false);
            }
            catch (Exception e)
            {
                e.ToString();
            }
        }
        public void FindAndReplace(object findText, object replaceWithText)
        {
            //options
            object matchCase = false;
            object matchWholeWord = true;
            object matchWildCards = false;
            object matchSoundsLike = false;
            object matchAllWordForms = false;
            object forward = true;
            object format = false;
            object matchKashida = false;
            object matchDiacritics = false;
            object matchAlefHamza = false;
            object matchControl = false;
            object read_only = false;
            object visible = true;
            object replace = 2;
            object wrap = 1;
            //execute find and replace           
            wordApp.Selection.Find.Execute(ref findText, ref matchCase, ref matchWholeWord,
                ref matchWildCards, ref matchSoundsLike, ref matchAllWordForms, ref forward, ref wrap, ref format, ref replaceWithText, ref replace,
                ref matchKashida, ref matchDiacritics, ref matchAlefHamza, ref matchControl);
        }
    }
}
