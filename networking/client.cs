using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class client {

    static NetworkStream stream;

    static void Main(string[] args)
    {
        String server_ip = "10.0.0.166";
        Int32 server_port = 55555;
        runClient(server_ip, server_port);
    }

    static void listen()
        {
            Byte[] message = new Byte[1024];
            while (true)
            {
                String recv_message = String.Empty;
                Int32 bytes = stream.Read(message, 0, message.Length);
                recv_message = System.Text.Encoding.ASCII.GetString(message, 0, bytes);
            if(!recv_message.Equals("")) 
            {
                Console.WriteLine(recv_message);
            }
        }
    }

    static void runClient(String server, Int32 port)
    {
        try
            {
                TcpClient client = new TcpClient(server, port);

                stream = client.GetStream();

                Console.WriteLine("Socket Connected to: ", server);

                Thread listener = new Thread(listen);
                listener.start();

                while (true)
                {
                    Console.WriteLine(">");
                    String input = Console.ReadLine();
                    Byte[] message = System.Text.Encoding.ASCII.GetBytes(input);
                    stream.Write(message, 0, message.Length);
                }
            }
        catch (ArgumentNullException e)
        {
            Console.WriteLine("ArgumentNullException: {0}",e);
        }
    }
}