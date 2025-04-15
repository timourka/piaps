﻿using Models.Interfaces;
using System.Text.Json.Serialization;

namespace Models
{
    public class Worker : IModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public JobTitle jobTitle { get; set; }
        public List<Vacation> vacations { get; set; }
    }
}
