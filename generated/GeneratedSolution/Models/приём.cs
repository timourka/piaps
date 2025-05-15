using System;

namespace Models.Models
{
    public class Приём : Model
    {
        private date время;
        private олжность[] необходимый_персонал;
        private Рабочий[] персонал;
        private Отделение отделение;
        public int id;

        private Рабочий рабочий;
        private repositories.ПриёмRepository приёмRepository;

    }

}
