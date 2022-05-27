using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;

namespace DataBindings
{
    public class DB_Person
    {
        private Person _person;
        public int b_id { get { return _person.ID; } }
        public string b_sex { get { return _person.sex; } }
        public int b_age { get { return _person.age; } }
        public string b_location { get { return _person.location; } }
        public string b_profession { get { return _person.profession; } }
        public double b_salary { get { return _person.salary; } }

        public DB_Person(Person _p)
        {
            _person = _p;
        }    
    }
    public class DB_PersonsListRaw
    {
        private List<DB_Person> _personsList = new List<DB_Person>();
        public List<DB_Person> b_personsList { get { return _personsList; } }
        public DB_PersonsListRaw(List<Person> _pList)
        {
            foreach (var p in _pList)
            {
                _personsList.Add(new DB_Person(p));
            }
        }
    }
    public class DB_DataSpecs
    {
        private DataSpecs b_dataSpecs;
        public int b_idsAmount { get { return b_dataSpecs.idsAmount; } }
        public int b_specsAmount { get { return b_dataSpecs.specsAmount; } }
        public double b_avgAge { get { return b_dataSpecs.avgAge; } }
        public double b_avgSalary { get { return b_dataSpecs.avgSalary; } }
        public int b_maxAge { get { return b_dataSpecs.maxAge; } }
        public int b_minAge { get { return b_dataSpecs.minAge; } }
        public double b_maxSalary { get { return b_dataSpecs.maxSalary; } }
        public double b_minSalary { get { return b_dataSpecs.minSalary; } }
        public int b_zerosAmount { get { return b_dataSpecs.zerosAmount; } }

        public DB_DataSpecs(DataSpecs ds)
        { 
            b_dataSpecs = ds;
        }
    }
}
