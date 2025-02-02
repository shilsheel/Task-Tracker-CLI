using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TaskTrackerCLI.Models
{
    public class TaskStorage
    {
        private static readonly string filePath = "tasks.json";
        public static void EnsureJsonFileExists()
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    File.WriteAllText(filePath, "[]");
                    Console.WriteLine("Created tasks.json file.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating file: {ex.Message}");
            }
        }

        // Load tasks from the JSON file
        public static List<Task> LoadTasks()
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    EnsureJsonFileExists();
                    return new List<Task>();
                }

                string json = File.ReadAllText(filePath);
                return JsonSerializer.Deserialize<List<Task>>(json) ?? new List<Task>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading tasks: {ex.Message}");
                return new List<Task>();
            }
        }

        // Save tasks to the JSON file
        public static void SaveTasks(List<Task> tasks)
        {
            try
            {
                string json = JsonSerializer.Serialize(tasks, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(filePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving tasks: {ex.Message}");
            }
        }
    }
}
