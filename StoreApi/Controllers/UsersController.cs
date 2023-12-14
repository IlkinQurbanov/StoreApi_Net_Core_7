using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreApi.Filters;
using StoreApi.Models;
using StoreApi.Sevcies;
using System.Security.Cryptography.X509Certificates;

namespace StoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private static List<UserDto> listUsers = new List<UserDto>() {
          new UserDto()
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                Phone = "123-456-7890",
                Address = "123 Main St"
            },
          new UserDto()
            {
                FirstName = "Jane",
                LastName = "Smith",
                Email = "jane.smith@example.com",
                Phone = "987-654-3210",
                Address = "456 Oak St"
            }

        };

        private readonly IConfiguration configuration;
        private readonly TimeService timeService;
       



        [HttpGet("info")]
        [DebugFilter]
        public IActionResult GetInfo(int? id, string? name, int? page,[FromServices] IConfiguration configuration,
            [FromServices] TimeService timeService)
        {
            if (id != null || name != null || page != null)
            {
                var response = new
                {
                    Id = id, Name = name, Page = page,
                    ErrorMessage = "The search functionality is not supported yet"

                };
                return Ok(response);
            }



            List<string> listInfo = new List<string>();
            listInfo.Add("AppName=" + configuration["AppName"]);
            listInfo.Add("Language=" + configuration["Language"]);
            listInfo.Add("Country=" + configuration["Country"]);
            listInfo.Add("Log=" + configuration["Logging:LogLevel:Default"]);
            listInfo.Add("Date=" + timeService.GetDate());
            listInfo.Add("Time=" + timeService.GetTime());

            return Ok(listInfo);
        }

        [HttpGet]

        public IActionResult GetUsers()
        {
            if (listUsers.Count > 0)
            {
                return Ok(listUsers);
            }
            return NotFound();

        }

        [HttpGet("{id:int}")]
        public IActionResult GetUsers(int id)
        {
            if (id >= 0 && id < listUsers.Count)
            {
                return Ok(listUsers[id]);
            }
            return NotFound();
        }


        [HttpGet("{name}")]
        public IActionResult GetUser(string name)
        {

            var user = listUsers.FirstOrDefault(u =>u.FirstName.Contains(name) ||  u.LastName.Contains(name));
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }



        [HttpPost]
        public IActionResult AddUser(UserDto  user)
        {

            //check that the email address is not authorized
            if(user.Email.Equals("user@example.com"))
            {
                ModelState.AddModelError("Email", "This Email Address is not authorized");
                return BadRequest(ModelState);
            }

            listUsers.Add(user);
            return Ok();
        }



        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, UserDto user)
        {

            //check that the email address is not authorized
            if (user.Email.Equals("user@example.com"))
            {
                ModelState.AddModelError("Email", "This Email Address is not authorized");
                return BadRequest(ModelState);
            }


            if (id >= 0 &&  id < listUsers.Count)
            {
                listUsers[id] = user;

            }
            return Ok();

           
            }



        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            if (id >= 0 && id < listUsers.Count)
            {
                listUsers.RemoveAt(id);
            }

            return NoContent();
        }

    }
    }

