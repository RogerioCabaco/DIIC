using IoTakers.Models;
using Microsoft.Ajax.Utilities;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.UI;

namespace IoTakers.Controllers
{
    public class HomeController : Controller
    {
        RequestsViewModel viewModel = new RequestsViewModel();
        StreamReader streamReader, streamReader2, streamReader3;

        public void readFileHistoricRequests()
        {
            string filename = AppDomain.CurrentDomain.BaseDirectory + "txt/historicRequests.txt";

            streamReader = new StreamReader(filename);
            using (streamReader)
            {
                int lineNumber = 0;
                List<string> medicamentosNames = new List<string>();
                List<int> quantidades = new List<int>();
                string line = streamReader.ReadLine();
                string actualWord = "";
                int index = 0;
                while (line != null)
                {
                    lineNumber++;
                    actualWord = "";
                    index = 0;

                    /*Ler as variaveis*/
                    while (index < line.Length)
                    {
                        if (line[index].ToString() == " ")
                        {
                            // print("nickname: " + actualWord + "\n");
                            medicamentosNames.Add(actualWord);
                            actualWord = "";
                        }
                        actualWord += line[index];
                        index++;
                    }
                    quantidades.Add(int.Parse(actualWord));
                    line = streamReader.ReadLine();
                }
                viewModel.historialRequests = new List<HistorialRequests>();
                for (int i = 0; i < medicamentosNames.Count; i++)
                {
                    viewModel.historialRequests.Add(new HistorialRequests(medicamentosNames[i], quantidades[i]));
                }

            }
        }

        public void readFilePillsRegistered()
        {
            string filename = AppDomain.CurrentDomain.BaseDirectory + "txt/pillsRegistered.txt";
            streamReader = new StreamReader(filename);
            using (streamReader)
            {
                int lineNumber = 0;
                List<string> medicamentosRegistered = new List<string>();
                string line = streamReader.ReadLine();
                string actualWord = "";
                int index = 0;
                while (line != null)
                {
                    lineNumber++;
                    actualWord = "";
                    index = 0;

                    /*Ler as variaveis*/
                    while (index < line.Length)
                    {
                        actualWord += line[index];
                        index++;
                    }
                    medicamentosRegistered.Add(actualWord);
                    //quantidades.Add(int.Parse(actualWord));
                    line = streamReader.ReadLine();
                }
                viewModel.pillsRegistered = new List<Pill>();
                for (int i = 0; i < medicamentosRegistered.Count; i++)
                {
                    viewModel.pillsRegistered.Add(new Pill(medicamentosRegistered[i]));
                }
            }
        }

        public void readFileRenovationRequests()
        {
            string filename =  AppDomain.CurrentDomain.BaseDirectory + "txt/renovationRequests.txt";
            streamReader = new StreamReader(filename);
            using (streamReader)
            {
                int lineNumber = 0;
                List<string> medicamentosNames = new List<string>();
                List<int> quantidades = new List<int>();
                string line = streamReader.ReadLine();
                string actualWord = "";
                int index = 0;
                while (line != null)
                {
                    lineNumber++;
                    actualWord = "";
                    index = 0;

                    /*Ler as variaveis*/
                    while (index < line.Length)
                    {
                        if (line[index].ToString() == " ")
                        {
                            // print("nickname: " + actualWord + "\n");
                            medicamentosNames.Add(actualWord);
                            actualWord = "";
                        }
                        actualWord += line[index];
                        index++;
                    }
                    quantidades.Add(int.Parse(actualWord));
                    line = streamReader.ReadLine();
                }
                viewModel.renovationRequests = new List<RenovationStockRequest>();
                for (int i = 0; i < medicamentosNames.Count; i++)
                {
                    viewModel.renovationRequests.Add(new RenovationStockRequest(new Pill(medicamentosNames[i]), quantidades[i]));
                }

            }
        }

        //public void WriteFile()
        //{
        //    System.IO.File.Create(filename).Close(); // Para apagar o que lá está
        //    List<string> aux = new List<string>();
        //    //print(s);s

        //    //for (int i = 0; i < nicknames.Count; i++)
        //    //{
        //    //    // print("AQUI: " + nicknames.ToArray()[i].ToString());
        //    //    aux.Add(nicknames.ToArray()[i].ToString() + " " + scores.ToArray()[i].ToString());
        //    //}
        //    ////string[] lines = { "First line", "Second line", "Third line" };

        //    System.IO.File.WriteAllLines(filename, aux.ToArray());
        //}


        public ActionResult Index() {
            readFileHistoricRequests();
            readFilePillsRegistered();
            readFileRenovationRequests();


            return View(viewModel);
        }

        public ActionResult Pills() {
            readFileHistoricRequests();
            readFilePillsRegistered();
            readFileRenovationRequests();

            return View(viewModel);
        }

        public ActionResult Patients()
        {
            readFileHistoricRequests();
            readFilePillsRegistered();
            readFileRenovationRequests();

            return View(viewModel);
        }

        public ActionResult Account() {
            readFileHistoricRequests();
            readFilePillsRegistered();
            readFileRenovationRequests();

            return View(viewModel);
        }

        public ActionResult RenovationOrders()
        {
            ViewData["Message"] = "Your RenovationOrders page.";

            return View();
        }

        public ActionResult HistorialOrders()
        {
            ViewData["Message"] = "Your HistorialOrders page.";
            readFileHistoricRequests();
            return View();
        }

        public ActionResult RegisteredPills()
        {
            return View();
        }

        [HttpPost]
        public JsonResult RemovePill(string index) {

            string filename = AppDomain.CurrentDomain.BaseDirectory + "txt/pillsRegistered.txt";
            readFilePillsRegistered();

            System.IO.File.Create(filename).Close(); // Para apagar o que lá está
            List<string> aux = new List<string>();

            viewModel.pillsRegistered.Remove(viewModel.pillsRegistered[Int32.Parse(index)]); // Remove bulldog

            for (int i = 0; i < viewModel.pillsRegistered.Count; i++)
            {
                aux.Add(viewModel.pillsRegistered[i].Name);
            }

            System.IO.File.WriteAllLines(filename, aux.ToArray());
            return Json("200: OK");
        }

        [HttpPost]
        public JsonResult AddPill(string name)
        {


            string filename = AppDomain.CurrentDomain.BaseDirectory + "txt/pillsRegistered.txt";
            readFilePillsRegistered();

            System.IO.File.Create(filename).Close(); // Para apagar o que lá está
            List<string> aux = new List<string>();

            for (int i = 0; i < viewModel.pillsRegistered.Count; i++)
            {
                aux.Add(viewModel.pillsRegistered[i].Name);
            }

            aux.Add(name);

            System.IO.File.WriteAllLines(filename, aux.ToArray());
            return Json("200: OK");
        }

        [HttpPost]
        public JsonResult AddHistoricRequest(string name, int quantidade, int index)
        {

            string filename = AppDomain.CurrentDomain.BaseDirectory + "txt/historicRequests.txt";
            readFileHistoricRequests();

            System.IO.File.Create(filename).Close(); // Para apagar o que lá está
            List<string> aux = new List<string>();


            for (int i = 0; i < viewModel.historialRequests.Count; i++)
            {
                aux.Add(viewModel.historialRequests[i].medicamento + " " + viewModel.historialRequests[i].quantidade);
            }
            aux.Add(name + " " + quantidade);
            System.IO.File.WriteAllLines(filename, aux.ToArray());

            //Second part
            filename = AppDomain.CurrentDomain.BaseDirectory + "txt/renovationRequests.txt";
            aux = new List<string>();
            readFileRenovationRequests();

            viewModel.renovationRequests.Remove(viewModel.renovationRequests[index]);

            for (int i = 0; i < viewModel.renovationRequests.Count; i++)
            {
                aux.Add(viewModel.renovationRequests[i].pill.Name + " " + viewModel.renovationRequests[i].stock_days);
            }

            System.IO.File.WriteAllLines(filename, aux.ToArray());

            return Json("200:OK");
        }

        public void AddStockRequest(List<Request> requests)
        {

            string filename = AppDomain.CurrentDomain.BaseDirectory + "txt/renovationRequests.txt";
            readFileRenovationRequests();

            System.IO.File.Create(filename).Close(); // Para apagar o que lá está
            List<string> aux = new List<string>();

            for (int i = 0; i < viewModel.renovationRequests.Count; i++)
            {
                aux.Add(viewModel.renovationRequests[i].pill.Name + " " + viewModel.renovationRequests[i].stock_days);
            }

            for (int i = 0; i < requests.Count; i++)
            {
                aux.Add(requests[i].pillName + " " + requests[i].stock_days);
            }
            

            System.IO.File.WriteAllLines(filename, aux.ToArray());
  
        }

        //public class hist {
        //    public string hist3;
        //}

        [HttpGet]
        public JsonResult SendDataFrontEnd() {
            readFileHistoricRequests();
            readFilePillsRegistered();
            readFileRenovationRequests();

            return Json(viewModel,JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void CommunicationArduino(List<Request> requests) {
            readFileHistoricRequests();
            readFilePillsRegistered();
            readFileRenovationRequests();

            string filename = AppDomain.CurrentDomain.BaseDirectory + "txt/renovationRequests.txt";

            RenovationStockRequest request = new RenovationStockRequest(new Pill(requests[0].pillName), requests[0].stock_days);

            System.IO.File.Create(filename).Close(); // Para apagar o que lá está
            List<string> aux = new List<string>();
            //print(s);s
            viewModel.renovationRequests.Add(request);
            for (int i = 0; i < viewModel.renovationRequests.Count; i++)
            {
                aux.Add(viewModel.renovationRequests[i].pill.Name + " " + viewModel.renovationRequests[i].stock_days);
            }
            ////string[] lines = { "First line", "Second line", "Third line" };
            
            System.IO.File.WriteAllLines(filename, aux.ToArray());
        }
        //public void WriteFile()
        //{

        //}


        [HttpPost]
        public ActionResult FlagFalse() {
            WriteRequestFlag(false);
            return Index();
        }
        [HttpPost]
        public string ReceiveArduinoMessage()
        {
            string x = "sadfsad";
            return x;
        }

        public class Request
        {
            public String pillName { get; set; }
            public int stock_days { get; set; }
        }
        [HttpPost]
        public ActionResult StockRequestFromArduino(List<Request> requests)
        {
            AddStockRequest(requests);
            readFileHistoricRequests();
            readFilePillsRegistered();
            readFileRenovationRequests();
            WriteRequestFlag(true);
            return Index();
        }

        public void WriteRequestFlag(bool flag)
        {
            string filename = AppDomain.CurrentDomain.BaseDirectory + "txt/requestionFlag.txt";

            System.IO.File.Create(filename).Close(); // Para apagar o que lá está
            List<string> aux = new List<string>();
            aux.Add("" + flag + "");

            System.IO.File.WriteAllLines(filename, aux.ToArray());

        }

        public void readRequestFlag()
        {
            string filename = AppDomain.CurrentDomain.BaseDirectory + "txt/requestionFlag.txt";
            streamReader = new StreamReader(filename);
            using (streamReader)
            {
                int lineNumber = 0;
                List<string> medicamentosNames = new List<string>();
                string line = streamReader.ReadLine();
                string actualWord = "";
                int index = 0;
                while (line != null)
                {
                    lineNumber++;
                    actualWord = "";
                    index = 0;

                    /*Ler as variaveis*/
                    while (index < line.Length)
                    {
                        if (line[index].ToString() == " ")
                        {
                            // print("nickname: " + actualWord + "\n");
                            medicamentosNames.Add(actualWord);
                            actualWord = "";
                        }
                        actualWord += line[index];
                        index++;
                    }
                    line = streamReader.ReadLine();
                }
                viewModel.arduinoRequest = new bool();
                if (actualWord == "True") viewModel.arduinoRequest = true;
                else viewModel.arduinoRequest = false;
            }
        }
    }
}