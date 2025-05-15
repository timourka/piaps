using System;

namespace Models.Models
{
    public class График_работы_на_день : Model
    {
        private time начало_работы;
        private time конец_работы;
        private bool работает;
        public int id;

        private Отделение отделение;

    }

}
