using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PDFGenerator.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PDFGeneratorController : ControllerBase
    {
        private readonly ILogger<PDFGeneratorController> _logger;
        private readonly IConfiguration _config;

        public PDFGeneratorController(ILogger<PDFGeneratorController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        [HttpPost(Name = "GeneratePDF")]
        public async Task<IActionResult> Post()
        {
            if (Authorized(this.Request.Headers["api_key"]))
            {
                PDFClass pdf = new PDFClass();
                try
                {
                    using (var reader = new StreamReader(Request.Body))
                    {
                        var body = await reader.ReadToEndAsync();
                        JsonConvert.PopulateObject(body, pdf);
                        if (pdf.CreateAndSend(data))
                            return Ok("Done");
                        else
                            return BadRequest("Error completing task.");
                    }
                }
                catch
                {
                    return BadRequest("Something didn't work here.");
                }
            }
            return Ok("ok");  
        }
        private bool Authorized(Microsoft.Extensions.Primitives.StringValues apikey)
        {
            string error = "";
            AccomonUser user = AccomonUser.Instance;
            user.Login("|~APICALL~|", apikey, Request.Host.Host, _config.GetConnectionString("WebUser"), out error);
            if (user.IsAuthenticated)
                return true;
            else
                return false;
        }
    }
}
