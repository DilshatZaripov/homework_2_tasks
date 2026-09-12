using homework_2_tasks;
using System.Diagnostics;

static async Task UseStopwatch(string title, Func<Task> task)
{
    var prevForegroundColor = Console.ForegroundColor;

    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine($"=== {title} ===");
    Console.ForegroundColor = prevForegroundColor;

    var sw = Stopwatch.StartNew();
    await task();
    sw.Stop();

    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine($"{title} - выполнено за {sw.ElapsedMilliseconds} мс");
    Console.ForegroundColor = prevForegroundColor;
}

FileGenerator.GenerateFiles(100);
await UseStopwatch("Прогрев потоков", FileSpaceCounter.Task1_ConcurrentTask);
//Console.Clear();

//await UseStopwatch("Эксперимент: одновременный await", FileSpaceCounter.ConcurrentAwait);
//await UseStopwatch("Эксперимент: последовательный await", FileSpaceCounter.SequentialAwait);
await UseStopwatch("Задача 1: три файла параллельно", FileSpaceCounter.Task1_ConcurrentTask);
//await UseStopwatch("Эксперимент: последовательный Task.Run()", FileSpaceCounter.SequentialTaskRun);

//await UseStopwatch("Эксперимент: множественные файлы async/await", () => FileSpaceCounter.ManyFilesAsync(FileSpaceCounter.TestFilesDir));
await UseStopwatch("Задача 2: все файлы в папке параллельно", () => FileSpaceCounter.Task2_ManyFilesTask(FileSpaceCounter.TestFilesDir));
