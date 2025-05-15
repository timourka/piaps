using System;

namespace repositories {
	public class ПриёмRepository : Repository  {
		public void Delete(ref Models.Model object_) {
			throw new System.NotImplementedException("Not implemented");
		}
		public void Update(ref Models.Model objectBefore, ref Models.Model objectAfter) {
			throw new System.NotImplementedException("Not implemented");
		}
		public Models.Model[] Read() {
			throw new System.NotImplementedException("Not implemented");
		}
		public void Create(ref Models.Model object_) {
			throw new System.NotImplementedException("Not implemented");
		}

		private Models.Приём приём;
		private Логер логер;

		private Api.Ведение_приёмов ведение_приёмов;

	}

}
