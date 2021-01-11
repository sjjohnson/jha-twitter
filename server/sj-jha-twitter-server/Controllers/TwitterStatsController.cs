using System;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using sj_jha_twitter_server.Services;
using sj_jha_twitter_server.Twitter;

namespace sj_jha_twitter_server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TwitterStatsController : ControllerBase
    {
        private readonly ITwitterStatsService _statsService;
        private readonly ILogger<TwitterStatsController> _logger;

        public TwitterStatsController(ITwitterStatsService statsService, ILoggerFactory loggerFactory)
        {
            _statsService = statsService ?? throw new ArgumentNullException(nameof(statsService));
            _ = loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory));

            _logger = loggerFactory.CreateLogger<TwitterStatsController>();
            _logger.LogTrace($"Exit {nameof(TwitterStatsService)}.{MethodBase.GetCurrentMethod().Name}");
        }

        [HttpGet]
        public IActionResult Get()
        {
            _logger.LogTrace($"Enter {nameof(TwitterStatsService)}.{MethodBase.GetCurrentMethod().Name}");

            TwitterStats stats;
            try
            {
                stats = _statsService.GetStats();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{GetType().Name}.{MethodBase.GetCurrentMethod().Name} failed: ");
                return new StatusCodeResult(500);
            }

            _logger.LogTrace($"Exit {nameof(TwitterStatsService)}.{MethodBase.GetCurrentMethod().Name}");

            return Ok(stats);
        }
    }
}
