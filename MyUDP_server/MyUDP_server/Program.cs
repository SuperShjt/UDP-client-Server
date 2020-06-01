using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;
namespace MyUDP_server
{
    class Program
    {
        static void Main(string[] args)
        {
            UdpClient myClient = new UdpClient();
            int count = 1;
            myClient.Client.SendTimeout = 1000;
            myClient.Client.ReceiveTimeout = 1000;
           DateTime t1, t2;
            //While loop iterate as long as there is a connection and less than 10 msg were sent
            while (true && count <=10)
            {
                String msg = "ping";
                Console.WriteLine("Client msg: {0} ", msg);
                //convert Client msg to Bytes
                Byte[] msgdata = Encoding.ASCII.GetBytes(msg);
                IPEndPoint ipserver = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 7777);
                //send msg to server
                myClient.Send(msgdata, msgdata.Length, ipserver);
                t1 = DateTime.Now;
                //try and Catch block was used to catch the timeout Exception
                try
                {
                    //receive msg from server
                    Byte[] recieveData = myClient.Receive(ref ipserver);
                    t2 = DateTime.Now;
                    //convert Server msg from Byte to string
                    string servermsg = Encoding.ASCII.GetString(recieveData);
                    Console.WriteLine("From server: {0} {1}", servermsg,count);
                    Console.WriteLine("RTT : {0}", (t2 - t1).TotalMilliseconds);
                    Console.WriteLine("-----------------------");
                }
                catch(Exception e)
                {
                    t2 = DateTime.Now;
                    Console.WriteLine("Request Timeout");           
                    Console.WriteLine("-----------------------");
                }  
                count++;
            }
        }
    }
}
