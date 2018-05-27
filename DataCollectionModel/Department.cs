using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataCollectionModel
{
    [Serializable]
    public class Department
    {
        public int Id { get; set; } //порядковый номер

        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
