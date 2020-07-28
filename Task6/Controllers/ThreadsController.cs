using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.Extensions.Logging;

namespace Task6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThreadsController : ControllerBase
    {
        private readonly ILogger<ThreadsController> _logger;

        string filePath = @"C:\Epam\TestFiles\File.jpeg";
        string uri = "https://media.gettyimages.com/photos/peacock-picture-id180699358?s=612x612";

        public ThreadsController(ILogger<ThreadsController> logger)
        {
            _logger = logger;
        }

        // api/Threads/download
        [Route("download")]
        public void DownloadFile()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();

            for (int i = 0; i < 5; i++)
            {
                string FileName = filePath + i.ToString();

                DownloadFile(uri, FileName);
            }

            watch.Stop();

            var elapsedTimeInMilliseconds = watch.ElapsedMilliseconds;

            _logger.LogInformation("{0} - time spent on the sync method", elapsedTimeInMilliseconds.ToString());
        }


        [Route("downloadAsync")]
        public async Task DownloadFileAsync()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();

            for (int i = 0; i < 5; i++)
            {
                string FileName = filePath + i.ToString();

                await Task.Run(() => DownloadFile(uri, FileName));
            }

            watch.Stop();

            var elapsedTimeInMilliseconds = watch.ElapsedMilliseconds;

            _logger.LogInformation("{0} - time spent on the async method", elapsedTimeInMilliseconds.ToString());
        }


        [Route("downloadParallel")]
        public async Task DownloadFileParallel()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();

            List<Task> tasks = new List<Task>();

            for (int i = 0; i < 5; i++)
            {
                string FileName = filePath + i.ToString();

                tasks.Add(Task.Run(() => DownloadFile(uri, FileName))); 
            }

            await Task.WhenAll(tasks);

            watch.Stop();

            var elapsedTimeInMilliseconds = watch.ElapsedMilliseconds;

            _logger.LogInformation("{0} - time spent on the parallel method", elapsedTimeInMilliseconds.ToString());
        }

        private void DownloadFile(string uri, string path)
        {
            using (var client = new WebClient())
            {
                client.DownloadFile(uri, path);
            }
        }
    }
}
