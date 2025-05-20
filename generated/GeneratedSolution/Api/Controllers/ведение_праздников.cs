using Microsoft.AspNetCore.Mvc;
using System;

namespace Api
{
    [ApiController]
    [Route("api/pra")]
    public class Ведение_праздников : ControllerBase
    {
        [HttpGet("get-all")]
        public Models.Models.Праздник[] Read() {
			throw new System.NotImplementedException("Not implemented");
        }
        [HttpGet("get/{id}")]
        public Models.Models.Праздник Read(  int id) {
			throw new System.NotImplementedException("Not implemented");
        }
        [HttpPost("add")]
        public void Create([FromBody] Models.Models.Праздник праздник) {
			throw new System.NotImplementedException("Not implemented");
        }
        [HttpPut("update/{id}")]
        public void Update( int id, [FromBody] Models.Models.Праздник праздник) {
			throw new System.NotImplementedException("Not implemented");
        }
        [HttpDelete("delete/{id}")]
        public void Delete( int id) {
			throw new System.NotImplementedException("Not implemented");
		}

		private repositories.ПраздникRepository праздникRepository;
		private Логер логер;

	}

}
