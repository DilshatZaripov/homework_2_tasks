namespace homework_2_tasks
{
    internal static class FileGenerator
    {
        private static string _outputDir = Path.Combine(AppContext.BaseDirectory, "TestFiles");
        public static void GenerateFiles(int count = 100)
        {
            Directory.CreateDirectory(_outputDir);

            File.WriteAllText(Path.Combine(_outputDir, "File1.txt"), new string(' ', 11) + "\n11_пробелов");
            File.WriteAllText(Path.Combine(_outputDir, "File2.txt"), new string(' ', 100) + "\n100_пробелов");
            File.WriteAllText(Path.Combine(_outputDir, "File3.txt"), new string(' ', 289) + "\n289_пробелов");

            var rand = new Random();

            for (int i = 0; i < count; i++)
            {
                string filePath = Path.Combine(_outputDir, $"test_file_{i}.txt");
                int spaceCount = rand.Next(5000, 10001);
                string content = new string(' ', spaceCount) + $"\n{spaceCount}_пробелов";
                File.WriteAllText(filePath, content);
            }
        }
    }
}
