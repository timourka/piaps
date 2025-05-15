using System;

namespace Api {
	public class Ведение_персонала {
		public Models.Рабочий[] Read(ref string sid) {
			throw new System.NotImplementedException("Not implemented");
		}
		public void Create(ref string sid, ref Models.Рабочий рабочий) {
			throw new System.NotImplementedException("Not implemented");
		}
		public void Update(ref string sid, ref int id, ref Models.Рабочий рабочий) {
			throw new System.NotImplementedException("Not implemented");
		}
		public Models.Рабочий Read(ref string sid, ref object id_int) {
			throw new System.NotImplementedException("Not implemented");
		}
		public void Delete(ref string sid, ref object id_int) {
			throw new System.NotImplementedException("Not implemented");
		}
		public string Аутентификация(ref string пароль, ref string login) {
			throw new System.NotImplementedException("Not implemented");
		}

		private repositories.РабочийRepository рабочийRepository;
		private Логер логер;

	}

}
