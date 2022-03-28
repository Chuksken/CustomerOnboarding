
using NLog;
using System;
using System.IO;

namespace CustomerOnb.Shared.Helpers
{

    public interface ILogService
    {
        void LogInfo(string message);
        void LogWarn(string message);
        void LogDebug(string message);
        void LogError(string message);

    }
    public class LogService : ILogService
    {
        private readonly ILogger _logger;
       
        public LogService()
        {
            _logger = LogManager.GetCurrentClassLogger();
            

        }
        public void LogDebug(string message)
        {
            var theMessage = string.Format("Time: {0} | LoggedMessage : {1}", DateTime.Now, message);
            _logger.Debug(theMessage);
            LogToFile(theMessage);
        }

        public void LogError(string message)
        {
            var theMessage = string.Format("Time: {0} | LoggedMessage : {1}", DateTime.Now, message);
            _logger.Error(theMessage);
            LogToFile(theMessage);
        }

        public void LogInfo(string message)
        {
            var theMessage = string.Format("Time: {0} | LoggedMessage : {1}", DateTime.Now, message);
            _logger.Info(theMessage);
            LogToFile(theMessage);
        }

        public void LogWarn(string message)
        {
            var theMessage = string.Format("Time: {0} | LoggedMessage : {1}", DateTime.Now, message);
            _logger.Warn(theMessage);
            LogToFile(theMessage);
        }
        private void LogToFile(string theMessage)
        {
            var folderPath = Directory.GetCurrentDirectory() + "\\Logs";
            var fileName = string.Format("log_{0}-{1}-{2}.txt", DateTime.Today.Day, DateTime.Today.Month, DateTime.Today.Year);
            var filePath = string.Format(folderPath + "\\" + fileName);
            StreamWriter writer;
            try
            {
                CreateFolder(folderPath);
                if (!File.Exists(filePath))
                {
                    writer = new StreamWriter(filePath);
                }
                else
                {
                    writer = File.AppendText(filePath);
                }
                //var theMessage = string.Format("Time: {0} | LogMessage : {1}", DateTime.Now, message);

                writer.WriteLine(theMessage);
                writer.WriteLine();
                writer.Close();

            }
            catch (Exception ex)
            {

            }
        }
        private void CreateFolder(string pathString)
        {
            if (!Directory.Exists(pathString))
            {
                Directory.CreateDirectory(pathString);
            }
        }

    }
}
