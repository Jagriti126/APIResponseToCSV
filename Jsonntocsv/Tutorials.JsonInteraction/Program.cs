using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace Tutorials.JsonInteraction
{
    class Program
    {
        static void Main(string[] args)
        {

            using (var webclient = new System.Net.WebClient())
            {
                String jsonString = webclient.DownloadString("https://api.publicapis.org/entries");
                
                Root root = JsonSerializer.Deserialize<Root>(jsonString);

                Console.WriteLine($"count: {root?.count}");

                int k = 1;

                for(int i=0;i< root?.count;i++,k++)
                {
                    Console.WriteLine("Entry " + k);

                    Console.WriteLine($"entries: {root?.entries[i].API}");

                    Console.WriteLine($"entries: {root?.entries[i].Description}");

                    Console.WriteLine($"entries: {root?.entries[i].HTTPS}");

                    Console.WriteLine($"entries: {root?.entries[i].Cors}");

                    Console.WriteLine($"entries: {root?.entries[i].Link}");
                        
                    Console.WriteLine($"entries: {root?.entries[i].Category}");

                    Console.WriteLine("---------------------------------------------------");
                }

                StringBuilder output = new StringBuilder();
                String file = @"C:\Users\Success\Desktop\Jagrity Sharmaa\Jsonntocsv\Tutorials.JsonInteraction\output.csv";

                for (int i = 0; i < root?.count; i++)
                {
                    String[] dataEntries = { root?.entries[i].API, root?.entries[i].Description, root?.entries[i].HTTPS.ToString(), root?.entries[i].Cors, root?.entries[i].Link, root?.entries[i].Category};
                    output.AppendLine(string.Join(",", dataEntries));
                }
                try
                {
                    File.AppendAllText(file, output.ToString());
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Data could not be written to the CSV file.");
                    return;
                }


            }

   

        }
        public class Entry
        {
            public string API { get; set; }
            public string Description { get; set; }
            public string Auth { get; set; }
            public bool HTTPS { get; set; }
            public string Cors { get; set; }
            public string Link { get; set; }
            public string Category { get; set; }
        }

        public class Root
        {
            public int count { get; set; }
            public List<Entry> entries { get; set; }
        }
        

    }
}
