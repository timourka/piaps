using System;

namespace Models {
	public class Отделение : Model  {
		private Рабочий[] персонал;
		private График_работы_на_день[] график_работы;
		public int id;

		private График_работы_на_день график_работы_на_день;

		private Рабочий рабочий;
		private Приём приём;
		private repositories.ОтделениеRepository отделениеRepository;

	}

}
