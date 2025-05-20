using System;

namespace Api {
	public class Ведение_приёмов : ControllerBase  {
		public Models.Приём[] Read(ref string sid) {
			throw new System.NotImplementedException("Not implemented");
		}
		public void Create(ref string sid, ref Models.Приём приём) {
			throw new System.NotImplementedException("Not implemented");
		}
		public void Update(ref string sid, ref int id, ref Models.Приём приём) {
			throw new System.NotImplementedException("Not implemented");
		}
		public Models.Приём Read(ref string sid, ref int id) {
			throw new System.NotImplementedException("Not implemented");
		}
		public void Delete(ref string sid, ref int id) {
			throw new System.NotImplementedException("Not implemented");
		}

		private repositories.ПриёмRepository приёмRepository;
		private Логер логер;

	}

}
