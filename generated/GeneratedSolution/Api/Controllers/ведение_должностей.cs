using Microsoft.AspNetCore.Mvc;
using System;

namespace Api
{
    [ApiController]
    [Route("api/dol")]
    public class Ведение_должностей : ControllerBase  {

        [HttpGet("get-all")]
        public Models.Models.Должность[] Read() {
			throw new System.NotImplementedException("Not implemented");
        }
        [HttpGet("get/{id}")]
        public Models.Models.Должность Read(int id) {
			throw new System.NotImplementedException("Not implemented");
        }
        [HttpPost("add")]
        public void Create([FromBody] Models.Models.Должность должность) {
			throw new System.NotImplementedException("Not implemented");
        }
        [HttpPut("update/{id}")]
        public void Update(int id, [FromBody] Models.Models.Должность должность) {
			throw new System.NotImplementedException("Not implemented");
        }
        [HttpDelete("delete/{id}")]
        public void Delete(int id) {
			throw new System.NotImplementedException("Not implemented");
		}

		private repositories.ДолжностьRepository должностьRepository;
		private Логер логер;

	}

}
