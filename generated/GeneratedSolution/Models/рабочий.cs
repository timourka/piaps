using System;

namespace Models.Models
{
    public class Рабочий : Model
    {
        private Должность должность;
        private Отделение отделение;
        private Приём[] прдстоящие_приёмы;
        private string фИО;
        private Отпуск[] отпуски;
        private string пароль;
        private string login;
        public int id;

        private Приём приём;
        private Отпуск отпуск;

        private repositories.РабочийRepository рабочийRepository;

    }

}
