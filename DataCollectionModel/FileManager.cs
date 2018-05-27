using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace DataCollectionModel
{
    public class FileManager
    {
        string _filename;

        public FileManager(string filename)
        {
            _filename = filename;
        }

        public void Save(List<Employee> employes)
        {
            XmlSerializer SerializerObj = new XmlSerializer(typeof(List<Employee>));

            using (StreamWriter WriteFileStream = new StreamWriter(_filename))
            {
                SerializerObj.Serialize(WriteFileStream, employes);
            }
        }

        public void Save(List<Department> departments)
        {
            XmlSerializer SerializerObj = new XmlSerializer(typeof(List<Department>));

            string path = Directory.GetParent(_filename).FullName + "\\departments.xml";

            using (StreamWriter WriteFileStream = new StreamWriter(path))
            {
                SerializerObj.Serialize(WriteFileStream, departments);
            }
        }

        public List<Employee> LoadEmployees()
        {
            List<Employee> list = new List<Employee>();

            XmlSerializer SerializerObj = new XmlSerializer(typeof(List<Employee>));

            using (StreamReader fileStream = new StreamReader(_filename))
            {
                list = SerializerObj.Deserialize(fileStream) as List<Employee>;
            }

            return list;
        }

        public List<Department> LoadDepartments()
        {
            List<Department> list = new List<Department>();

            XmlSerializer SerializerObj = new XmlSerializer(typeof(List<Department>));

            string path = Directory.GetParent(_filename).FullName + "\\departments.xml";

            using (StreamReader fileStream = new StreamReader(path))
            {
                list = SerializerObj.Deserialize(fileStream) as List<Department>;
            }

            return list;
        }
    }
}
