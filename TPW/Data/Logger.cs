using System;
using System.Collections.Concurrent;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Data
{
    internal class Logger
    {
        private readonly string logPath;
        private readonly ConcurrentQueue<JObject> ballQueue = new ConcurrentQueue<JObject>();
        private Mutex fileMutex = new Mutex();
        private readonly Mutex queueMutex = new Mutex();
        private readonly JArray dataArray;

        public Logger()
        {
            string tempPath = Path.GetTempPath();
            logPath = tempPath + "zLoggingBalls.json";
            try
            {
                if (File.Exists(logPath))
                {
                    File.Delete(logPath);
                }
            }
            catch (JsonReaderException) { }

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
            }
            finally
            {
                queueMutex.ReleaseMutex();
            }
        }

        public async void LogToFile()
        {
            while(true)
            {
                while (ballQueue.TryDequeue(out JObject ballFromQueue))
                {
                    dataArray.Add(ballFromQueue);
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
                await Task.Delay(1000);
            }
        }

        ~Logger()
        {
            fileMutex.WaitOne();
            fileMutex.ReleaseMutex();
        }
    }
}