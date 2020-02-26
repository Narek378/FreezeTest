using Microsoft.AspNet.SignalR.Client;
using Microsoft.AspNet.SignalR.Client.Hubs;
using Microsoft.AspNet.SignalR.Client.Transports;
using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace FreezeTest
{


    class Program
    {

        private static Logger logger = LogManager.GetCurrentClassLogger();
        
        public static async Task Main(string[] args)
        {
            object locker = new object();

            
            while (true)
            {

                List.InitialiseData();
                await Request.Send1Async(locker);
                Console.WriteLine(new string('-', 50));
                Thread.Sleep(TimeSpan.FromMinutes(1));
                List.Datas.Clear();
                
            }

            Console.Read();


        }


    }
}

