using Microsoft.AspNetCore.Mvc;
using System;

namespace Api
{
    [ApiController]
    [Route("api/dol")]
    public class �������_���������� : ControllerBase  {

        [HttpGet("get-all")]
        public Models.Models.���������[] Read() {
			throw new System.NotImplementedException("Not implemented");
        }
        [HttpGet("get/{id}")]
        public Models.Models.��������� Read(int id) {
			throw new System.NotImplementedException("Not implemented");
        }
        [HttpPost("add")]
        public void Create([FromBody] Models.Models.��������� ���������) {
			throw new System.NotImplementedException("Not implemented");
        }
        [HttpPut("update/{id}")]
        public void Update(int id, [FromBody] Models.Models.��������� ���������) {
			throw new System.NotImplementedException("Not implemented");
        }
        [HttpDelete("delete/{id}")]
        public void Delete(int id) {
			throw new System.NotImplementedException("Not implemented");
		}

		private repositories.���������Repository ���������Repository;
		private ����� �����;

	}

}
