using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjectADP.Factory;
using ProjectADP.Models;
using ProjectADP.Utils;

namespace ProjectADP.Controllers
{ 
    [Route("api/[controller]")]
    [ApiController]
    public class AdpController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdpController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet("GetandPostTaskAdp")]
        public async Task<ActionResult<AdpModel>> GetTaskandPost()
        {
            try
            {
                AdpModel? adpModel = null;            
                var HttpClient = _httpClientFactory.CreateClient();

                var getTask = await HttpClient.GetAsync(AdpTaskConstants.AdpGetTask);
                var dataCalculation = await getTask.Content.ReadAsStringAsync();
                adpModel = JsonConvert.DeserializeObject<AdpModel>(dataCalculation);
               
                var createOperation = new OperationFactory();

                adpModel.Result = createOperation.CreateOperations(
                    adpModel.Operation, adpModel.Left, adpModel.Right);

                // Call Post request with Calculation Result
                await PostCalculationResult(adpModel);

                return StatusCode(StatusCodes.Status200OK, adpModel); ;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, 
                                  "Something Went Wrong with Your Request");
            }

        }

        [HttpPost("PostTaskAdp")]
        public async Task<ActionResult<AdpModel>> PostCalculationResult(AdpModel adpmodel)
        {
            var erroMessage = new ErrorFactory();
            var HttpClient = _httpClientFactory.CreateClient();
            var postTask = await HttpClient.PostAsJsonAsync(AdpTaskConstants.AdpPostTask, adpmodel);

            if (postTask.IsSuccessStatusCode)
            {
                Console.WriteLine($"We just send the result of " +
                           $"{adpmodel.Result} for the calculation");
                
                return StatusCode(StatusCodes.Status200OK, adpmodel);
            }
            else 
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                       erroMessage.MessageError((int)postTask.StatusCode));   
            }

        }
    }

   
}
