using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Walks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string[]> GetAllStudents()
        {
            string[] students = new string[] { "John", "Doe", "Mary", "Dave", "Emily", "Wally" };

            return BadRequest(students);
        }
    }
}
