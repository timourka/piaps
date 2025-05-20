using Microsoft.AspNetCore.Mvc;
using System;

namespace Api
{
    [ApiController]
    [Route("api/pri")]
    public class Ведение_приёмов : ControllerBase
    {
        [HttpGet("get-all")]
        public Models.Models.Приём[] Read() {
			throw new System.NotImplementedException("Not implemented");
        }
        [HttpPost("add")]
        public void Create([FromBody] Models.Models.Приём приём) {
			throw new System.NotImplementedException("Not implemented");
        }
        [HttpPut("update/{id}")]
        public void Update(int id, [FromBody] Models.Models.Приём приём) {
			throw new System.NotImplementedException("Not implemented");
        }
        [HttpGet("get/{id}")]
        public Models.Models.Приём Read(int id) {
			throw new System.NotImplementedException("Not implemented");
        }
        [HttpDelete("delete/{id}")]
        public void Delete(int id) {
			throw new System.NotImplementedException("Not implemented");
		}

		private repositories.ПриёмRepository приёмRepository;
		private Логер логер;

	}

}
