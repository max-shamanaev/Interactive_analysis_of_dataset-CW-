using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Data
{
    public class Person
    {
        public int ID { get; set; }
        public string sex { get; set; }
        public int age { get; set; }
        public string location { get; set; }
        public string profession { get; set; }
        public double salary { get; set; }

        public Person() { }

        public override string ToString()
        {
            return ID.ToString() + ", " + sex + ", " + age.ToString()
                + ", " + location + ", " + profession + salary.ToString() + "\n";
        }
    }
    public class RawData
    {
        public static List<string> professions = new List<string>
        {
            "database administrator",
            "software developer",
            "cybersecurity specialist",
            "IT analyst",
            "IT leadership",
            "cloud computing engineer"
        };
        public static List<string> countries = new List<string>
        {
            "Russian Federation",
            "France",
            "Germany",
            "UK",
            "USA",
            "Australia",
            "China",
            "Japan",
            "India"
        };
 
        private char divider = '*';

        public void CreateRandomData(String filepath, int lines = 100)
        {
            Random rnd = new Random(DateTime.Now.Millisecond);

            StreamWriter sw  = new StreamWriter(filepath);

            String line;
            Person p;

            for (int i = 1; i <= lines; i++)
            {
                line = "";
                p = new Person();

                p.ID = i;

                if (rnd.Next(1, 3) == 1)
                    p.sex = "male";
                else
                    p.sex = "female";

                p.age = rnd.Next(18, 51);
                p.location = countries[rnd.Next(0, countries.Count)];
                p.profession = professions[rnd.Next(0, professions.Count)];
                p.salary = rnd.Next(1000, 10001) + rnd.NextDouble();

                line += p.ID + divider.ToString() + p.sex + divider + p.age + divider +
                    p.location + divider + p.profession + divider +
                     Math.Round(p.salary, 2) + "\n";

                sw.Write(line);
            }

            sw.Close();
        }

        public List<Person> CreateDataFromFile(String filepath)
        {
            StreamReader sr;
            if (File.Exists(filepath))
                sr = new StreamReader(filepath);
            else
                throw new Exception("File was not found!");

            String line;
            Person p;
            string[] items;
            List<Person> personsList = new List<Person>();

            try
            {
                while ((line = sr.ReadLine()) != null)
                {
                    items = line.Split(divider);
                    p = new Person();

                    p.ID = Convert.ToInt32(items[0].Trim());
                    p.sex = items[1].Trim();
                    p.age = Convert.ToInt32(items[2].Trim());
                    p.location = items[3].Trim();
                    p.profession = items[4].Trim();
                    p.salary = Convert.ToDouble(items[5].Trim());

                    personsList.Add(p);
                }
                sr.Close();
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return personsList;
        }

        public static double[] GetSalaries(List<Person> pList)
        {
            double[] tmp = new double[pList.Count];
            for(int i = 0; i < pList.Count; i++)
                tmp[i] = pList[i].salary;

            return tmp;
        }

        public static int[] GetAges(List<Person> pList)
        {
            int[] tmp = new int[pList.Count];
            for (int i = 0; i < pList.Count; i++)
                tmp[i] = pList[i].age;

            return tmp;
        }
    }
}
