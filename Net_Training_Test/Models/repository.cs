using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Linq;

namespace Net_Training_Test.Models
{
    //The class for working with list data models Person.
    public class Repository
    {
        string Xmlurl;
        private XmlDocument xDoc;
        XDocument xdoc;
        private XmlElement xRoot;
        private List<Person> Result;

        public Repository(string xmlurl)
        {
            Xmlurl = xmlurl;
          // xDoc = new XmlDocument();
            //xDoc.Load(Xmlurl);
            xdoc = XDocument.Load(Xmlurl);
           // xRoot = xDoc.DocumentElement;
            Result = new List<Person>();
        }

        //Return List of persons at page 
        public List<Person> getList()
        {

            foreach (XElement personElement in xdoc.Element("People").Elements("Person"))
            {
                Person temp = new Person();
                XAttribute nameAttribute = personElement.Attribute("id");
                XElement personSurnameElem = new XElement("Surname", personElement.Element("Surname"));
                XElement personNameElem = new XElement("Name", personElement.Element("Name"));
                XElement personYearBornElem = new XElement("YearBorn", personElement.Element("YearBorn"));
                XElement personPhoneElem = new XElement("Phone", personElement.Element("Phone"));

                if (nameAttribute != null && personSurnameElem != null && personNameElem != null)
                {
                    temp.Id = Convert.ToInt32(nameAttribute.Value);
                    temp.Surname = personSurnameElem.Value;
                    temp.Name = personNameElem.Value;
                    temp.YearBorn = Convert.ToInt32(personYearBornElem.Value);
                    temp.Phone = personPhoneElem.Value;
                }
                Result.Add(temp);
            }
            return Result;
        }

        public string addPerson(Person person)
        {          
            // создаем первый элемент
            XElement Person = new XElement("Person");
            // создаем атрибут
            XAttribute personNameAttr = new XAttribute("id", "2");
            XElement personSurnameElem = new XElement("Surname", person.Surname);
            XElement personNameElem = new XElement("Name", person.Name);
            XElement personYearBornElem = new XElement("YearBorn", person.YearBorn);
            XElement personPhoneElem = new XElement("Phone", person.Phone);
            // добавляем атрибут и элементы в первый элемент
            Person.Add(personNameAttr);
            Person.Add(personSurnameElem);
            Person.Add(personNameElem);
            Person.Add(personYearBornElem);
            Person.Add(personPhoneElem);
            xdoc.Element("People").Add(Person);
            xdoc.Save(Xmlurl);
            return null;
        }

        public string updatePerson(Person person)
        {
     

            return null;
        }

        public string delPerson(Person person)
        {
            return null;
        }

    }
}