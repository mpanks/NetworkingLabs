using System.Data;
using System.Diagnostics;
using System.Net.Sockets;

namespace Networking_Lab1
{

    internal class Program
    {
        static Dictionary<string, User> DataBase = new Dictionary<string, User>
        {
            { "cssbct",
                new User{UserID="cssbct",
                    SurName = "Panks",
                    FirstName="Matthew",
                    Title="Mr",
                    Position="Student",
                    Phone="07936129687",
                    Email="sadfas",
                Location = "In The Lab"}
                }
        };
        static bool debug = true;
        static void Main(string[] args)
        {
            try
            {

                if (args.Length == 0)
                {
                    Console.WriteLine("Starting Server");
                    RunServer();
                }
                else
                {
                    for (int i = 0; i < args.Length; i++)
                    {
                        ProcessCommand(args[i]);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }
        static void ProcessCommand(string command)
        {
            Console.WriteLine($"Command: {command}");

            String[] slice = command.Split(new char[] { '?' }, 2);
            string ID = slice[0];
            string operation = null;
            string update = null;
            string field = null;
            if (slice.Length == 2)
            {
                operation = slice[1];
                String[] pieces = operation.Split(new char[] { '=' }, 2);
                field = pieces[0];
                if (pieces.Length == 2) { update = pieces[1]; }
            }

            Console.WriteLine($"Operation on ID '{ID}'");
            if (operation == null)
            {
                Dump(ID);
            }
            else if (update == null)
            {
                Lookup(ID, field);
            }
            else
            {
                Update(ID, field, update);
            }
        }
        static void Dump(string ID)
        {
            if (debug)
            {

                Console.WriteLine($"UserID={DataBase[ID].UserID}");
                Console.WriteLine($"Surname={DataBase[ID].SurName}");
                Console.WriteLine($"Fornames={DataBase[ID].FirstName}");
                Console.WriteLine($"Title={DataBase[ID].Title}");
                Console.WriteLine($"Position={DataBase[ID].Position}");
                Console.WriteLine($"Phone={DataBase[ID].Phone}");
                Console.WriteLine($"Email={DataBase[ID].Email}");
                Console.WriteLine($"location={DataBase[ID].Location}");
            }
        }
        static void Lookup(string ID, string field)
        {
            if (debug)
                Console.WriteLine($" lookup field '{field}'");
            String result = null;
            switch (field)
            {
                case "location":
                    result = DataBase[ID].Location;
                    break;
                case "UserID":
                    result = DataBase[ID].UserID;
                    break;
                case "Surname":
                    result = DataBase[ID].SurName;
                    break;
                case "Fornames":
                    result = DataBase[ID].FirstName;
                    break;
                case "Title":
                    result = DataBase[ID].Title;
                    break;
                case "Phone":
                    result = DataBase[ID].Phone;
                    break;
                case "Position":
                    result = DataBase[ID].Position;
                    break;
                case "Email":
                    result = DataBase[ID].Email;
                    break;
            }
            Console.WriteLine(result);
        }
        static void Update(string ID, string field, string update)
        {
            if (debug)
                Console.WriteLine($" update field '{field}' to '{update}'");
            switch (field)
            {
                case "location": DataBase[ID].Location = update; break;
                case "UserID": DataBase[ID].UserID = update; break;
                case "Surname": DataBase[ID].SurName = update; break;
                case "Fornames": DataBase[ID].FirstName = update; break;
                case "Title": DataBase[ID].Title = update; break;
                case "Phone": DataBase[ID].Phone = update; break;
                case "Position": DataBase[ID].Position = update; break;
                case "Email": DataBase[ID].Email = update; break;
            }
            Console.WriteLine("OK");
        }
        static void RunServer()
        {
            //no server yet :'(
            TcpListener listener;
            Socket connection;
            NetworkStream socketStream;
            try
            {
                listener = new TcpListener(43);
                while(true)
                {
                    if (debug) Console.WriteLine("Server Waiting Connection...");
                    connection = listener.AcceptSocket();
                    socketStream = new NetworkStream(connection);
                    //do the request here
                    doRequest(socketStream);
                    socketStream.Close();
                    connection.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            if (debug) Console.WriteLine("Terminating Server");
        }
        static void doRequest(NetworkStream socketStream)
        {
            StreamWriter sw = new StreamWriter(socketStream);
            StreamReader sr = new StreamReader(socketStream);

            if (debug) Console.WriteLine("Waiting for client input");
            String line  = sr.ReadLine();
            Console.WriteLine($"Received Network Command: '{line}");
            sw.WriteLine(line); // needs removing after testing
            sw.Flush(); // needs removing after testing
        }
    }
}