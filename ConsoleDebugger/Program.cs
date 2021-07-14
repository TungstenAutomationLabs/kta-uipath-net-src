using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UiPathClassLib;

namespace ConsoleDebugger
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Setting properties");
            var client = new UiPathClient();
            //client.Url = "https://cloud.uipath.com/kofaxclirezd/";
            //client.ExtAppId = "bc843cb1-e394-4086-a133-8da25e11dcdf";
            //client.ExtAppSecret = "AKrjJsTfX#QX1s^j";
            //client.Username = "adam.sawyers@kofax.com";
            //client.Password = "As2021%%";

            Console.WriteLine("Ext App token");
            string extAuthToken = client.GetExtAppAuthToken("bc843cb1-e394-4086-a133-8da25e11dcdf", "AKrjJsTfX#QX1s^j");
            Console.WriteLine(extAuthToken);

            //Console.WriteLine("Regular token");
            //client.UserKey = "LkGV1jx1m5NgUwfcKi6wRf6-BX6xsC9QTdGdHjrKLgN7F";
            //client.ClientId = "8DEv1AMNXczW3y4U15LL3jYf62jK93n5";
            //string authToken = client.GetAuthToken();
            //Console.WriteLine(authToken);

            Console.ReadLine();
        }
    }
}
