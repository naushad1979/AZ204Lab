using AZ204Lab.QueueStorage.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace AZ204Lab.QueueStorage.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QueueController : ControllerBase
    {
        private readonly ILogger<QueueController> _logger;
        private readonly IQueueStorageService _queueStorageService;

        public QueueController(ILogger<QueueController> logger, IQueueStorageService queueStorageService)
        {
            _logger = logger;
            _queueStorageService = queueStorageService;
        }

        [HttpPost("SendMessageToQueue")]
        public async Task<IActionResult> SendMessageToQueueAsync(JObject request)
        {
            string queueMessage = request["queueMessage"].ToString();

            await _queueStorageService.SendMessageToQueueAsync("az204lab-queue","az204labstorage ", queueMessage);
            return Ok();
        }

        [HttpGet("PeekMessageFromQueue")]
        public async Task<IActionResult> PeekMessageFromQueueAsync()
        {
            await _queueStorageService.PeekMessagesAsync("az204lab-queue");
            return Ok();
        }

        [HttpGet("GetMessageFromQueue")]
        public async Task<IActionResult> GetMessageFromQueueAsync()
        {
            var messages = await _queueStorageService.GetQueueMessagesAsync("az204lab-queue");
            return Ok(messages);
        }
    }
}
