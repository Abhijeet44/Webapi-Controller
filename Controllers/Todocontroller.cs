using Microsoft.AspNetCore.Mvc;
using System.Text;
using Newtonsoft.Json;
using Serilog.Extensions.Logging;



namespace Crud_Api;

[ApiController]
[Route("todo")]
public class TodoController : ControllerBase
{ 

  private ICorelationIdGenerator _correlationIdGenerator;
  private readonly ILogger<TodoController> _logger;
  public TodoController(ILogger<TodoController> logger, ICorelationIdGenerator correlationIdGenerator){
    _logger = logger;
    _correlationIdGenerator = correlationIdGenerator;
  }
  
  [HttpGet]
  public async Task<HttpResponseMessage> CallApi()
  {     

    _logger.LogInformation("called get method in Todoconrtoller {correlationId}", _correlationIdGenerator.Get());
    var httpclient = new HttpClient();
    var response = await httpclient.GetAsync("https://jsonplaceholder.typicode.com/todos/1");
    return response;   

  }

  [HttpPost]
  public async Task<IActionResult> CreateApi(){

    _logger.LogInformation("Post method in TodoController");
    using (var client = new HttpClient()){

      client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/");
      var values = new 
      {
        title = "foo",
        body = "bar",
        userId = 1
      };
      
      var content = new StringContent(JsonConvert.SerializeObject(values), Encoding.UTF8, "application/json");
      
      using (HttpResponseMessage response = await client.PostAsync("posts", content)){
        var responseContent = response.Content.ReadAsStringAsync().Result;
        response.EnsureSuccessStatusCode();
        return Ok(responseContent);
      }
    }
  }   

  [HttpDelete]
  public async Task<IActionResult> DeleteApi(){

    _logger.LogInformation("Delete Method in TodoController");
    using(HttpClient client = new HttpClient()){
      client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/");

      using (HttpResponseMessage response = await client.DeleteAsync("posts/1")){
        var responseContent = response.Content.ReadAsStringAsync().Result;
        response.EnsureSuccessStatusCode();

        return Ok(responseContent);
        
      }
    }
  }

  [HttpPut]
  public async Task<OkObjectResult> Updating()
  {

    _logger.LogInformation("Put method in TodoController");
    using (var client = new HttpClient()){

      client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/");
      var values = new 
      {
        title = "foo",
        body = "bar",
        userId = 1
      };

      HttpContent content = new StringContent(JsonConvert.SerializeObject(values), Encoding.UTF8, "application/json");

      using (HttpResponseMessage response = await client.PutAsync("https://jsonplaceholder.typicode.com/posts/1", content)){
        var responseContent = response.Content.ReadAsStringAsync().Result;
        response.EnsureSuccessStatusCode();
        return Ok(responseContent);
      }
    }
  } 
}



