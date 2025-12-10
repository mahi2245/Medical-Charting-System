using Library.Assignment1.DTO;
using Library.Assignment1.Data;
using Microsoft.AspNetCore.Mvc;
using API.Assignment1.Enterprise;

namespace API.Assignment1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly ILogger<PatientController> _logger;

        public PatientController(ILogger<PatientController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<PatientDTO> Get()
        {
            return new PatientEC().GetPatients();
        }

        [HttpGet("{id}")]
        public PatientDTO? GetById(int id)
        {
            return new PatientEC().GetById(id);
        }

        [HttpDelete("{id}")]
        public PatientDTO? Delete(int id)
        {
            return new PatientEC().Delete(id);
        }

        [HttpPost]
        public PatientDTO? AddOrUpdate([FromBody] PatientDTO patient)
        {
            return new PatientEC().AddOrUpdate(patient);
        }

        [HttpPost("Search")]
        public IEnumerable<PatientDTO?> Search([FromBody] QueryRequest query)
        {
            return new PatientEC().Search(query.Content);
        }
    }
}
