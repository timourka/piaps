using System;

namespace Api {
	public class Ведение_отделений {
		public Models.Отделение[] Read(ref string sid) {
			throw new System.NotImplementedException("Not implemented");
		}
		public Models.Отделение Read(ref string sid, ref int id) {
			throw new System.NotImplementedException("Not implemented");
		}
		public void Create(ref string sid, ref Models.Отделение отделение) {
			throw new System.NotImplementedException("Not implemented");
		}
		public void Update(ref string sid, ref int id, ref Models.Отделение отделение) {
			throw new System.NotImplementedException("Not implemented");
		}
		public void Delete(ref string sid, ref int id) {
			throw new System.NotImplementedException("Not implemented");
		}
		public void СоздатьРасписаниеАвтоматически(ref string sid, ref int id) {
			throw new System.NotImplementedException("Not implemented");
		}
		public void ПолучитьРасписаниеЗаПериод(ref string sid, ref int id, ref date начало, ref date конец) {
			throw new System.NotImplementedException("Not implemented");
		}

		private repositories.ОтделениеRepository отделениеRepository;
		private Логер логер;

	}

}
