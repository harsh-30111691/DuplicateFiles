
using System;
using System.Collections.Generic;
using System.IO;

namespace DuplicateFileDetector
{
    class Program
    {
        static void Main(string[] args)
        {
            string directoryPath = @"/Users/30111691/Desktop/df"; 

            if (Directory.Exists(directoryPath))
            {
                FindAndDeleteDuplicates(directoryPath);
                Console.WriteLine("Duplicate files have been deleted.");
            }
            else
            {
                Console.WriteLine("Directory does not exist.");
            }
        }

        static void FindAndDeleteDuplicates(string directoryPath)
        {
            var fileContents = new Dictionary<string, List<string>>();

            foreach (var filePath in Directory.GetFiles(directoryPath, "*", SearchOption.AllDirectories))
            {
                var content = File.ReadAllBytes(filePath);
                var contentAsString = Convert.ToBase64String(content);

                if (fileContents.ContainsKey(contentAsString))
                {
                    fileContents[contentAsString].Add(filePath);
                }
                else
                {
                    fileContents[contentAsString] = new List<string> { filePath };
                }
            }

            foreach (var content in fileContents.Values)
            {
                if (content.Count > 1)
                {
                    Console.WriteLine($"Found {content.Count} files with identical content:");

                    for (int i = 1; i < content.Count; i++)
                    {
                        Console.WriteLine($"Deleting: {content[i]}");
                        File.Delete(content[i]);
                    }
                }
            }
        }
    }
}


