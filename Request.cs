using Microsoft.AspNet.SignalR.Client;
using Microsoft.AspNet.SignalR.Client.Transports;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreezeTest
{
    class Request
    {
        
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public static List<Task> tasks = new List<Task>();
        public static async Task Task1(Datas datas, object locker )
        {
            
            HubConnection connection = new HubConnection(datas.Url);
            var proxy = connection.CreateHubProxy("c");
            proxy.On<string, string>("start",
                     (name, message) => Console.WriteLine($"{name}:{message}")); 
            try
            {
                await connection.Start();
                 proxy.Subscribe("start");
            }
            catch (Exception exception)
            {
                try
                {
                    logger.Error(exception + " " + datas.PartnerName + "  first attempt crahed, connection staus is {0} ", connection.State);
                   await  connection.Start(new WebSocketTransport());
                }
                catch (Exception exception1)
                {
                    try
                    {
                        logger.Error(exception1 + " " + datas.PartnerName + "  second attempt crahed, connection staus is {0} ", connection.State);
                       await connection.Start(new WebSocketTransport());
                    }
                    catch (Exception exception2)
                    {
                        lock (locker)
                        {
                            Report.SendEmail(datas.PartnerName + "  is crashed-- " + exception);
                            logger.Error(exception2 + "  " + datas.PartnerName + "  is crashed connection staus is {0} ", connection.State);
                            Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine(exception2);
                            Console.WriteLine(datas.PartnerName + "  is crashed connection staus is {0} ", connection.State+" "+DateTime.Now);
                            Console.ResetColor();
                        }
                    }
                        
                }
            }

            if (connection.State == ConnectionState.Connected)
            {
                lock (locker)
                {
                   //Report.SendEmail("ok");
                    logger.Info("connection ID is: " + connection.ConnectionId + " " + datas + "  SignalR connection is {0}", connection.State);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(datas.PartnerName + "  SignalR connection is {0}", connection.State+ " " + DateTime.Now);
                    Console.ResetColor();
                    Console.WriteLine(connection.ConnectionId);
                    connection.Stop();
                    if (connection.State == ConnectionState.Disconnected)
                    {
                        Console.WriteLine(datas.PartnerName + " " + connection.State+ " " + DateTime.Now);
                    }

                }

            }

        }

        public static async Task Send1Async(object locker)
        {


            foreach (var datas in List.Datas)
            {

                tasks.Add(Task1(datas,locker));

            }

            Task.WaitAll(tasks.Cast<Task>().ToArray());
            tasks.Clear();
        }

    }
}
