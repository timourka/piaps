using System;

namespace Api {
	public class Ведение_праздников : ControllerBase  {
		public Models.Праздник[] Read(ref string sid) {
			throw new System.NotImplementedException("Not implemented");
		}
		public Models.Праздник Read(ref string sid, ref object id_int) {
			throw new System.NotImplementedException("Not implemented");
		}
		public void Create(ref string sid, ref Models.Праздник праздник) {
			throw new System.NotImplementedException("Not implemented");
		}
		public void Update(ref string sid, ref int id, ref Models.Праздник праздник) {
			throw new System.NotImplementedException("Not implemented");
		}
		public void Delete(ref string sid, ref int id) {
			throw new System.NotImplementedException("Not implemented");
		}

		private repositories.ПраздникRepository праздникRepository;
		private Логер логер;

	}

}
