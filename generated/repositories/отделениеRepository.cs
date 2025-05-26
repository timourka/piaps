using System;

namespace repositories {
	public class ОтделениеRepository : Repository  {
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

		private Models.Отделение отделение;
		private Логер логер;

		private Api.Ведение_отделений ведение_отделений;

	}

}
