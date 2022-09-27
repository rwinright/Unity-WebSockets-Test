using System;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace WSServer
{
    public class MessageService : WebSocketBehavior
    {
        //https://www.dotnetperls.com/random-string
        public string Get8CharacterRandomString()
        {
            string path = Path.GetRandomFileName();
            path = path.Replace(".", ""); // Remove period.
            return path.Substring(0, 8);  // Return 8 character string
        }
        protected override void OnMessage(MessageEventArgs e)
        {
            var msg = System.Text.Encoding.UTF8.GetString(e.RawData);
            Console.WriteLine(msg);
            var response = System.Text.Encoding.UTF8.GetBytes($"Welcome Back! {Get8CharacterRandomString()}");
            Send(response);
        }
    }


    public class Program
    {
        public static void Main(string[] args)
        {
            int port = 9000;
            var ws_server = new WebSocketServer(port); //Create server on port 9000
            ws_server.AddWebSocketService<MessageService>("/msgs"); //Set up to use the messaging service behavior.
            ws_server.Start(); //Start the server

            if (ws_server.IsListening)
            {
                Console.WriteLine($"Server is listening on port: {port}");
            }

            Console.ReadKey(true); //Wait until key is pressed and terminate
            ws_server.Stop();
        }
    }    
}