using Microsoft.AspNetCore.Mvc;

namespace WebHookClient.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WebHookClientController : ControllerBase
    {       
        private readonly ILogger<WebHookClientController> _logger;

        public WebHookClientController(ILogger<WebHookClientController> logger)
        {
            _logger = logger;
        }

        [HttpPost("/item/new")]
        public void CreateNewItem(object payload, ILogger<WebHookClientController> logger)
        {
            logger.LogInformation("Payload recebido - {payload}", payload);
        }
    }
}
