namespace homework_2_tasks
{
    internal static class FileSpaceCounter
    {
        public static string TestFilesDir = Path.Combine(AppContext.BaseDirectory, "TestFiles");

        public static async Task Task1_ConcurrentTask()
        {
            var files = GetFiles();

            Console.WriteLine("Запуск параллельных задач");
            Task<int>[] tasks = new[] {
                CountSpacesTask(files[0]),
                CountSpacesTask(files[1]),
                CountSpacesTask(files[2])
            };
            Console.WriteLine("Ожидание выполнения параллельных задач");
            var results = await Task.WhenAll(tasks);
            Console.WriteLine($"Завершение параллельных задач. Результат: {results.Sum()} пробелов");
        }

        public static async Task Task2_ManyFilesTask(string dirPath)
        {
            var files = Directory.GetFiles(dirPath);
            
            Console.WriteLine("Запуск параллельных задачи");
            var tasks = files.Select(f => CountSpacesTask(f));
            Console.WriteLine("Ожидание выполнения параллельных задачи");
            var results = await Task.WhenAll(tasks);
            Console.WriteLine($"Завершение параллельных задач. Результат: {results.Sum()} пробелов");
        }

        private static async Task<int> CountSpacesAsync(string filePath)
        {
            //await Task.Delay(1000);
            var content = await File.ReadAllTextAsync(filePath);
            return content.Count(c => c == ' ');
        }

        private static Task<int> CountSpacesTask(string filePath)
        {
            return Task.Run(() =>
            {
                //Thread.Sleep(1000);
                var content = File.ReadAllText(filePath);
                return content.Count(c => c == ' ');
            });
        }

        private static List<string> GetFiles()
        {
            return new List<string>()
            {
                Path.Combine(TestFilesDir, "File1.txt"),
                Path.Combine(TestFilesDir, "File2.txt"),
                Path.Combine(TestFilesDir, "File3.txt")
            };
        }

        #region Experimental Part
        public static async Task ManyFilesAsync(string dirPath)
        {
            var files = Directory.GetFiles(dirPath);

            Console.WriteLine("Запуск асинхронных задачи");
            var tasks = files.Select(f => CountSpacesAsync(f));
            Console.WriteLine("Ожидание выполнения асинхронных задач");
            var results = await Task.WhenAll(tasks);
            Console.WriteLine($"Завершение асинхронных задач. Результат: {results.Sum()} пробелов");
        }

        public static async Task ConcurrentAwait()
        {
            List<string> files = GetFiles();

            Console.WriteLine("Запуск асинхронных задач");
            Task<int>[] tasks = new[] {
                CountSpacesAsync(files[0]),
                CountSpacesAsync(files[1]),
                CountSpacesAsync(files[2])
            };
            Console.WriteLine("Ожидание выполнения асинхронных задач");
            var results = await Task.WhenAll(tasks);
            Console.WriteLine($"Завершение асинхронных задач. Результат: {results.Sum()} пробелов");
        }

        public static async Task SequentialAwait()
        {
            var files = GetFiles();

            Console.WriteLine("Последовательный запуск асинхронных задач");
            var r1 = await CountSpacesAsync(files[0]);
            var r2 = await CountSpacesAsync(files[1]);
            var r3 = await CountSpacesAsync(files[2]);
            Console.WriteLine($"Завершение последовательных асинхронных задач. Результат: {r1 + r2 + r3} пробелов");
        }

        public static async Task SequentialTask()
        {
            var files = GetFiles();

            Console.WriteLine("Последовательный запуск параллельных задач");
            var r1 = await CountSpacesTask(files[0]);
            var r2 = await CountSpacesTask(files[1]);
            var r3 = await CountSpacesTask(files[2]);
            Console.WriteLine($"Завершение последовательных параллельных задач. Результат: {r1 + r2 + r3} пробелов");
        }
        #endregion
    }
}
