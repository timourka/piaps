using System;

namespace repositories {
	public class ПраздникRepository : Repository  {
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

		private Models.Праздник праздник;
		private Логер логер;

		private Api.Ведение_праздников ведение_праздников;

	}

}
