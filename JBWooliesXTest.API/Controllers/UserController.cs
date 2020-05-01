using JBWooliesXTest.Core.Model;
using JBWooliesXTest.Core.Model.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace JBWooliesXTest.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly string _token;
        
        public UserController(IConfiguration configuration)
        {
            //_token = configuration["ResourceServiceHttpHttpClient:Token"];
            _token = configuration.GetSection("ResourceServiceHttpHttpClient:Token").Value;
        }
        [HttpGet]
        public UserResponse Get()
        {
            return new UserResponse() {Name = "Johann Brink", Token = _token};
        }
    }
}
