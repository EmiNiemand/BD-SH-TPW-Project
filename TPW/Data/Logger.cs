using System;
using System.Collections.Concurrent;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Data
{
    internal class Logger : ILogger
    {
        private readonly string logPath;

        private Task? loggingTask;

        private readonly ConcurrentQueue<JObject> ballQueue = new ConcurrentQueue<JObject>();

        private readonly Mutex queueMutex = new Mutex();
        private readonly JArray dataArray;

        public Logger()
        {
            string tempPath = Path.GetTempPath();
            logPath = tempPath + "z.json";

            if (File.Exists(logPath))
            {
                string input = File.ReadAllText(logPath);
                dataArray = JArray.Parse(input);
                return;
            }

            dataArray = new JArray();
            File.Create(logPath);
        }

        public void AddToLogQueue(IBall ball)
        {
            queueMutex.WaitOne();
            try
            {
                JObject itemToAdd = JObject.FromObject(ball);
                itemToAdd["Time"] = DateTime.Now.ToString("HH:mm:ss");
                ballQueue.Enqueue(itemToAdd);

                if (loggingTask == null || loggingTask.IsCompleted)
                {
                    loggingTask = Task.Factory.StartNew(this.LogToFile);
                }
            }
            finally
            {
                queueMutex.ReleaseMutex();
            }
        }

        private Mutex fileMutex = new Mutex();

        private async Task LogToFile()
        {
            while (ballQueue.TryDequeue(out JObject ball))
            {
                dataArray.Add(ball);
            }

            string output = JsonConvert.SerializeObject(dataArray);

            fileMutex.WaitOne();
            try
            {
                File.WriteAllText(logPath, output);
            }
            finally
            {
                fileMutex.ReleaseMutex();
            }
        }

        ~Logger()
        {
            fileMutex.WaitOne();
            fileMutex.ReleaseMutex();
        }
    }
}