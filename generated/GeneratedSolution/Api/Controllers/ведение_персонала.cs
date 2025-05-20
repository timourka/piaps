using Microsoft.AspNetCore.Mvc;
using System;

namespace Api
{
    [ApiController]
    [Route("api/per")]
    public class Ведение_персонала : ControllerBase
    {
        [HttpGet("get-all")]
        public Models.Models.Рабочий[] Read() {
			throw new System.NotImplementedException("Not implemented");
        }
        [HttpPost("add")]
        public void Create([FromBody] Models.Models.Рабочий рабочий) {
			throw new System.NotImplementedException("Not implemented");
        }
        [HttpPut("update/{id}")]
        public void Update(int id, [FromBody] Models.Models.Рабочий рабочий) {
			throw new System.NotImplementedException("Not implemented");
        }
        [HttpGet("get/{id}")]
        public Models.Models.Рабочий Read( int id) {
			throw new System.NotImplementedException("Not implemented");
        }
        [HttpDelete("delete/{id}")]
        public void Delete( int id) {
			throw new System.NotImplementedException("Not implemented");
		}
		public string Аутентификация( string пароль,  string login) {
			throw new System.NotImplementedException("Not implemented");
		}

		private repositories.РабочийRepository рабочийRepository;
		private Логер логер;

	}

}
