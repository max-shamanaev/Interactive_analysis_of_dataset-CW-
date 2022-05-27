using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class DataSpecs
    {
        //ID не учитывается
        public int specsAmount = 5;

        public int idsAmount;
        public double avgAge;
        public double avgSalary;
        public int maxAge;
        public int minAge;
        public double maxSalary;
        public double minSalary;
        public int zerosAmount;

        public DataSpecs() { }
        public DataSpecs(List<Person> pList)
        {
            int tempAgeSum = 0;
            double tempSalarySum = 0;

            maxAge = int.MinValue;
            maxSalary = double.MinValue;
            minAge = int.MaxValue;
            minSalary = double.MaxValue;
            zerosAmount = 0;

            idsAmount = pList.Count;

            foreach(var p in pList)
            {
                tempAgeSum += p.age;
                tempSalarySum += p.salary;

                if (maxAge < p.age)
                    maxAge = p.age;

                if (minAge > p.age)
                    minAge = p.age;

                if (maxSalary < p.salary)
                    maxSalary = p.salary;

                if (minSalary > p.salary)
                    minSalary = p.salary;

                if (p.ToString().Contains("0"))
                {
                    foreach(var ch in 
                        (p.ID.ToString() + p.age.ToString() + p.salary.ToString()))
                    {
                        if (ch == '0')
                            ++zerosAmount;
                    }
                }
            }

            avgAge = Math.Round((double)tempAgeSum / pList.Count, 2);
            avgSalary = Math.Round((double)tempSalarySum / pList.Count, 2);
        }

        public static Dictionary<string, double> AvgSalaryPerLocation(List<Person> pList)
        {
            Dictionary<string, int> countDict = new Dictionary<string, int>();
            Dictionary<string, double> dict = new Dictionary<string, double>();
            Dictionary<string, double> temp = new Dictionary<string, double>();

            foreach (var p in pList)
            {
                if (!temp.ContainsKey(p.location))
                {
                    temp.Add(p.location, p.salary);
                    countDict.Add(p.location, 1);
                }
                else
                {
                    temp[p.location] += p.salary;
                    ++countDict[p.location];
                }
            }
            foreach (var d in temp)
                dict.Add(d.Key, d.Value / countDict[d.Key]); 

            return dict;
        }
        public static Dictionary<string, double> AvgSalaryPerProfession(List<Person> pList)
        {
            Dictionary<string, int> countDict = new Dictionary<string, int>();
            Dictionary<string, double> dict = new Dictionary<string, double>();
            Dictionary<string, double> temp = new Dictionary<string, double>();

            foreach (var p in pList)
            {
                if (!temp.ContainsKey(p.profession))
                {
                    temp.Add(p.profession, p.salary);
                    countDict.Add(p.profession, 1);
                }
                else
                {
                    temp[p.profession] += p.salary;
                    ++countDict[p.profession];
                }
            }
            foreach (var d in temp)
                dict.Add(d.Key, d.Value / countDict[d.Key]);

            return dict;
        }
        public static Dictionary<string, double> AvgSalaryPerGender(List<Person> pList)
        {
            Dictionary<string, int> countDict = new Dictionary<string, int>();
            Dictionary<string, double> dict = new Dictionary<string, double>();
            Dictionary<string, double> temp = new Dictionary<string, double>();

            foreach (var p in pList)
            {
                if (!temp.ContainsKey(p.sex))
                {
                    temp.Add(p.sex, p.salary);
                    countDict.Add(p.sex, 1);
                }
                else
                {
                    temp[p.sex] += p.salary;
                    ++countDict[p.sex];
                }
            }
            foreach (var d in temp)
                dict.Add(d.Key, d.Value / countDict[d.Key]);

            return dict;
        }
        public static Dictionary<string, double> AvgAgePerProfession(List<Person> pList)
        {
            Dictionary<string, int> countDict = new Dictionary<string, int>();
            Dictionary<string, double> dict = new Dictionary<string, double>();
            Dictionary<string, double> temp = new Dictionary<string, double>();

            foreach (var p in pList)
            {
                if (!temp.ContainsKey(p.profession))
                {
                    temp.Add(p.profession, p.age);
                    countDict.Add(p.profession, 1);
                }
                else
                {
                    temp[p.profession] += p.age;
                    ++countDict[p.profession];
                }
            }
            foreach (var d in temp)
                dict[d.Key] = d.Value / countDict[d.Key];

            return dict;
        }
    }
}
