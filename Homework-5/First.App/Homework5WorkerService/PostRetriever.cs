using First.App.Business.Abstract;
using First.App.Business.Concretes;
using First.App.DataAccess.EntityFramework;
using First.App.DataAccess.EntityFramework.Repository.Concretes;
using First.App.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Homework5WorkerService
{
    public class PostRetriever : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private IQueue<Post> _postQueue;
        private readonly ILogger<PostRetriever> _logger;
        private HttpClient _httpClient;
        public PostRetriever(ILogger<PostRetriever> logger, IServiceScopeFactory serviceScopeFactory, IQueue<Post> postQueue)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
            _postQueue = postQueue;
        }
        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _httpClient = new HttpClient();
            return base.StartAsync(cancellationToken);
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                
                var request = await _httpClient.GetAsync("https://jsonplaceholder.typicode.com/posts");
                
                if (request.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<List<Post>>(request.Content.ReadAsStringAsync().Result);
                    result.ForEach(x => _postQueue.Enqueue(x));
                    _logger.LogInformation("Posts successfully Enqueue");
                }
                else
                {
                    _logger.LogCritical(request.StatusCode.ToString());
                }
                await Task.Delay(10000, stoppingToken);
            }
        }
        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _httpClient.Dispose();
            return base.StopAsync(cancellationToken);
        }
        public override void Dispose()
        {
            base.Dispose();
        }
    }
}
