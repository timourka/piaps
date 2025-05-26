using Microsoft.AspNetCore.Mvc;
using System;

namespace Api
{
    [ApiController]
    [Route("api/otd")]
    public class Ведение_отделений : ControllerBase
    {
        [HttpGet("get-all")]
        public Models.Models.Отделение[] Read() {
			throw new System.NotImplementedException("Not implemented");
        }
        [HttpGet("get/{id}")]
        public Models.Models.Отделение Read(int id) {
			throw new System.NotImplementedException("Not implemented");
        }
        [HttpPost("add")]
        public void Create([FromBody] Models.Models.Отделение отделение) {
			throw new System.NotImplementedException("Not implemented");
        }
        [HttpPut("update/{id}")]
        public void Update(int id, [FromBody] Models.Models.Отделение отделение) {
			throw new System.NotImplementedException("Not implemented");
        }
        [HttpDelete("delete/{id}")]
        public void Delete(int id) {
			throw new System.NotImplementedException("Not implemented");
        }
        [HttpGet("createrasp/{id}")]
        public void СоздатьРасписаниеАвтоматически(int id) {
			throw new System.NotImplementedException("Not implemented");
        }
        [HttpGet("rasp/{id}")]
        public void ПолучитьРасписаниеЗаПериод(int id, DateOnly начало, DateOnly конец) {
			throw new System.NotImplementedException("Not implemented");
		}

		private repositories.ОтделениеRepository отделениеRepository;
		private Логер логер;

	}

}
