using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace TCPClient
{
    class Program
    {

        static void Main(string[] args)
        {
            StartListener();
        }

        private static void StartListener()
        {
            try
            {
                Thread clientThread = new Thread(Connect);
                clientThread.Start();
            }
            catch (Exception e)
            {
                Console.Clear();
                for (int i = 5; i > 0; i--)
                {
                    Console.WriteLine($"Kan ikke oprette forbindelse til serveren. Prøver igen om {i} sekunder.");
                    Thread.Sleep(1000);
                    Console.Clear();
                }
                StartListener();
            }
        }

        private static void Connect()
        {
            Console.WriteLine("Opretter forbindelse til server ...");
            TcpClient client = new TcpClient("127.0.0.1", 7000);
            Listen(client);
        }

        private static void Listen(TcpClient client)
        {
            NetworkStream nwStream = client.GetStream();
            StreamReader stReader = new StreamReader(nwStream);
            StreamWriter stWriter = new StreamWriter(nwStream) { AutoFlush = true };

            Console.WriteLine("Klient forbundet til server!");
            while (true)
            {
                try
                {
                    Console.WriteLine(stReader.ReadLine());
                    stWriter.WriteLine(Console.ReadLine());
                    Console.Clear();
                }
                catch (Exception e)
                {
                    Environment.Exit(0);
                }
            }
        }
    }
}
