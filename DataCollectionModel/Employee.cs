using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataCollectionModel
{
    [Serializable]
    public class Employee
    {
        private List<Reward> _rewards = new List<Reward>(); //коллекция наград

        public int Id { get; set; } //порядковый номер

        public string FIO { get; set; } //ФИО

        public string Address { get; set; } //Адрес

        public string Tel { get; set; } //номер телефона

        public Department Department { get; set; } //подразделение

        public Gender Gender { get; set; } //пол

        public DateTime Birthday { get; set; }

        public List<Reward> Rewards
        {
            get
            {
                return _rewards;
            }
        }

        public Employee()
        {

        }

        public Employee(string fio)
        {
            FIO = fio;
        }

        public override string ToString()
        {
            return FIO;
        }
    }

    public enum Gender
    {
        Male,
        Female
    }
}
