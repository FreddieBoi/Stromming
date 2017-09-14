using Stromming.Streamers;
using System;
using System.Collections.Generic;

namespace Stromming {

    public class Program {

        private static IList<IStreamer> streamers = new List<IStreamer> {
            new ViaplayStreamer(),
            new SvtPlayStreamer(),
            new SfAnytimeStreamer()
        };

        public static void Main(string[] args) {
            Utils.WriteLine(ConsoleColor.White, $"{Utils.applicationName} {Utils.applicationVersionVerboseName}");
            var term = args != null && args?.Length > 0 ? args[0] : null;
#if DEBUG
            if (term == null) {
                Utils.Write(ConsoleColor.Cyan, "Search: ");
                term = Console.ReadLine();
            }
#endif
            if (term == null) {
                Utils.WriteLine(ConsoleColor.Red, "No search term!");
            } else {
                Utils.WriteLine(ConsoleColor.Gray, $"Searching for \"{term}\"...");
                long count = 0;
                foreach (var streamer in streamers) {
                    try {
                        streamer.Search(term);
                        count += streamer.Count;
                    } catch (Exception exception) {
                        Utils.WriteLine(ConsoleColor.DarkRed, $"Failed searching {streamer.Name}: {exception}");
                    }
                }
                if (count <= 0) {
                    Utils.WriteLine(ConsoleColor.Red, $"0... :(");
                } else {
                    Utils.WriteLine(ConsoleColor.Green, $"{count}! :D");
                    foreach (var worker in streamers) {
                        Utils.WriteLine(ConsoleColor.DarkGreen, $"{worker.Name}: {worker.Count}");
                    }

                }
            }
#if DEBUG
            Utils.Write(ConsoleColor.Cyan, "Press any key to quit: ");
            Console.Read();
#endif
        }

    }

}
