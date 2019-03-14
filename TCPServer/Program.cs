using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using Registreringsafgift;

namespace TCPServer
{
    class Program
    {
        private static readonly TcpListener TcpListener = new TcpListener(IPAddress.Any, 7000);
        private static readonly Afgift Afgift = new Afgift();
        static void Main(string[] args)
        {
            StartListener();
        }

        private static void StartListener()
        {
            TcpListener.Start();
            Console.WriteLine("TCP Listener startet og lytter efter klienter ...");
            TcpClient client = TcpListener.AcceptTcpClient();
            Console.WriteLine("Klient forbundet!");
            Listen(client);
        }

        private static void Listen(TcpClient client)
        {
            NetworkStream nwStream = client.GetStream();
            StreamReader stReader = new StreamReader(nwStream);
            StreamWriter stWriter = new StreamWriter(nwStream) { AutoFlush = true };

            while (true)
            {
                stWriter.WriteLine("Vil du vælge personbil eller elbil? 'Stop' for at lukke programmet!");
                string msg = stReader.ReadLine();
                if (msg != null && !msg.All(char.IsDigit) && msg.Length < int.MaxValue)
                {
                    if (msg.ToLower().Contains("person"))
                    {
                        Console.WriteLine("Personbil valgt");
                        stWriter.WriteLine("Angiv prisen for personbilen:");
                        string pris = stReader.ReadLine();
                        Console.WriteLine("Pris: " + pris);
                        stWriter.WriteLine("Afgift for personbil: " + Afgift.BilAfgift(Convert.ToInt32(pris)) + " - Tryk enter for at udregne ny pris.");
                        Console.WriteLine("Afgift: " + Afgift.BilAfgift(Convert.ToInt32(pris)));
                        stReader.ReadLine();
                    }

                    if (msg.ToLower().Contains("el"))
                    {
                        Console.WriteLine("Elbil valgt");
                        stWriter.WriteLine("Angiv prisen for elbilen:");
                        string pris = stReader.ReadLine();
                        Console.WriteLine("Pris: " + pris);
                        stWriter.WriteLine("Afgift for elbil: " + Afgift.ElBilAfgift(Convert.ToInt32(pris)) + " - Tryk enter for at udregne ny pris.");
                        Console.WriteLine("Afgift: " + Afgift.ElBilAfgift(Convert.ToInt32(pris)));
                        stReader.ReadLine();
                    }

                    if (msg.ToLower().Contains("stop"))
                    {
                        Environment.Exit(0);
                    }
                }
            }
        }
    }
}
