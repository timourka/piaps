using System;

namespace Models.Models
{
    public class Приём : Model
    {
        private DateOnly время;
        private Должность[] необходимый_персонал;
        private Рабочий[] персонал;
        private Отделение отделение;
        public int id;

    }

}
