using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.IO.Ports;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

using System.Linq;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using OfficeOpenXml;
using System.Text.RegularExpressions;

namespace ConsoleApp1
{
    class a
    {
        static SerialPort _serialPort;
        static string _result;
        private static readonly HttpClient client = new HttpClient();

        static string ConvertToIndex(string option)
        {
            string conversion = "";
            if (option == "Strongly Disagree") conversion = "1";
            else if (option == "Disagree") conversion = "2";
            else if (option == "Somewhat Disagree") conversion = "3";
            else if (option == "Neutral") conversion = "4";
            else if (option == "Somewhat Agree") conversion = "5";
            else if (option == "Agree") conversion = "6";
            else if (option == "Strongly Agree") conversion = "7";

            return conversion;
        }

        public static void Main()
        {
            //Receiving data from arduino
            //_serialPort = new SerialPort();
            //_serialPort.PortName = "COM4"; //Set your board COM
            //_serialPort.BaudRate = 9600;
            //_serialPort.Open();
            //while (true)
            //{
            //    _result = ReadMsg();
            //    Console.WriteLine(_result);
            //    Thread.Sleep(200);
            //}

            //Sending data to WebApp
            List<Request> requestList = new List<Request>();
            Request r1 = new Request { pillName = "Ben-u-Ron", stock_days = 3 };
            requestList.Add(r1);
            PostToWebApp(requestList);

        }

        static string ReadMsg()
        {
            string message = ""; bool tag = true;

            while (tag)
            {
                string a = _serialPort.ReadExisting();
                if ((a.IndexOf('&') > -1) && (a != "") && (a.IndexOf('%') > -1))
                {
                    //Console.WriteLine("tou no if: " + a);
                    message = ConvertArduinoMsg(a);
                    tag = false;
                }
                Thread.Sleep(200);
            }
            return message;
        }

        static string ConvertArduinoMsg(string a)
        {
            string x = ""; bool flag = false, fim = false, begin = false;

            if (a != "")
            {
                for (int c = 0; c < a.Length; c++)
                {
                    if (a[c] == '&')
                    {
                        flag = true;
                        begin = true;
                    }
                    if (a[c] == '%')
                    {
                        flag = false;
                        if (begin) fim = true;
                    }

                    if (flag && a[c] != '&')
                    {
                        x += a[c];
                    }
                    if (begin && fim)
                    {
                        string cv = "";
                        for (int i = 0; i < x.Length; i++)
                        {
                            if (x[i] == ':')
                            {
                                for (int _count = (i + 1); _count < x.Length; _count++)
                                {
                                    cv += "" + x[_count] + "";
                                }
                            }
                        }
                        Console.WriteLine(cv);

                    }
                    return x;
                }
            }

            return x;
        }
        public class Request
        {
            public String pillName { get; set; }
            public int stock_days { get; set; }
        }
        public static void PostToWebApp(List<Request> requests)
        {
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri("http://localhost:53450/Home/CommunicationArduino");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = client.PostAsJsonAsync("http://localhost:53450/Home/CommunicationArduino", requests).Result;
            if (response.IsSuccessStatusCode)
            {
                Console.Write("Success");
            }
            else
                Console.Write("Error");

        }

    }
}