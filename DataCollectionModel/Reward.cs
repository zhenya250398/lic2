using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataCollectionModel
{
    [Serializable]
    public class Reward //награда
    {
        public int Id { get; set; } //порядковый номер

        public string Name { get; set; } //название награды

        public string Description { get; set; } //описание

        public int Year { get; set; } //год получения награды

        public Employee Employee { get; set; }
    }
}
