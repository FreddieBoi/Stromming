using System;
using System.Net.Http;
using System.Reflection;
using System.Text;

namespace Stromming {

    public static class Utils {

        public const string applicationName = "Stromming";
        public static readonly Version applicationVersion = Assembly.GetExecutingAssembly().GetName().Version;
        public static readonly string applicationVersionVerboseName = string.Format("v{0}.{1} Patch {2} Build {3}", applicationVersion.Major, applicationVersion.Minor, applicationVersion.Build, applicationVersion.Revision);

        public static void Write(ConsoleColor color, string text) {
            Console.ForegroundColor = color;
            Console.Write($"[{DateTime.Now}] {text}");
            Console.ResetColor();
        }

        public static void WriteLine(ConsoleColor color, string line) {
            Console.ForegroundColor = color;
            Console.WriteLine($"[{DateTime.Now}] {line}");
            Console.ResetColor();
        }

        public static string GetContent(string uri) {
            var client = new HttpClient();
            var response = client.GetAsync(uri).Result;
            var content = response?.Content?.ReadAsStringAsync().Result;
            client.Dispose();
            return content;
        }

        public static string PostContent(string uri, string data) {
            var client = new HttpClient();
            var formUrlEncodedContent = new StringContent(data, Encoding.UTF8, "application/x-www-form-urlencoded");
            var response = client.PostAsync(uri, formUrlEncodedContent).Result;
            var content = response?.Content?.ReadAsStringAsync().Result;
            client.Dispose();
            return content;
        }

    }

}
