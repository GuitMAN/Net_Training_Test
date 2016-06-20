using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Net_Training_Test.Models
{
    //The class for working with list data models Person.
    public class Repository
    {
        //filename xml
        string Xmlurl;
        private XDocument xDoc;
        private XElement  xRoot;
        //List of Person
        private List<Person> Result;
        
        //Constructor of class Repository
        public Repository(string xmlurl)
        {
            Xmlurl = xmlurl;
            //Load file
            xDoc = XDocument.Load(Xmlurl);
            //Root of data
            xRoot = xDoc.Element("People");
            //List of Person
            Result = new List<Person>();
        }

        //Return List of persons from file
        //if param sort = 0 then no sort
        //sort = 1 - sorting by Surname
        //sort = 2 - sorting by YearBorn
        public List<Person> getList(int sort = 0)
        {
            foreach (XElement personElement in xRoot.Elements("Person"))
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
            if (sort == 1)
            {
                return Result.Select(p =>p).OrderBy(s => s.Surname).ToList();
            }
            if (sort == 2)
            {
                return Result.Select(p => p).OrderBy(s => s.YearBorn).ToList();
            }
            return Result;
        }

        //Add new element person to file
        public string addPerson(Person person)
        {          
            // Create new element Person
            XElement Person = new XElement("Person");

            //Determine the maximum id
            int id = 0;
            foreach (XElement xe in xRoot.Elements("Person").ToList())
            {
                
                if (Convert.ToInt32(xe.Attribute("id").Value) > id)
                {
                    id = Convert.ToInt32(xe.Attribute("id").Value);
                }
            }
            //Create attribute and elements of Person
            //++id - The increment of id 
            XAttribute personNameAttr = new XAttribute("id", ++id);
            XElement personSurnameElem = new XElement("Surname", person.Surname);
            XElement personNameElem = new XElement("Name", person.Name);
            XElement personYearBornElem = new XElement("YearBorn", person.YearBorn);
            XElement personPhoneElem = new XElement("Phone", person.Phone);
            // Add attr and elements
            Person.Add(personNameAttr);
            Person.Add(personSurnameElem);
            Person.Add(personNameElem);
            Person.Add(personYearBornElem);
            Person.Add(personPhoneElem);
            //Add all to parent element
            xRoot.Add(Person);
            ///Save xml file
            xDoc.Save(Xmlurl);
            return null;
        }

        //Update item Person
        public string updatePerson(Person person)
        {
            foreach (XElement xe in xRoot.Elements("Person").ToList())
            {
                // The search item id
                if (xe.Attribute("id").Value.Equals(Convert.ToInt32(person.Id)))
                {
                    //Save item with new values
                    xe.Element("Surname").Value = person.Surname;
                    xe.Element("Name").Value = person.Name;
                    xe.Element("YearBorn").Value = person.YearBorn.ToString();
                    xe.Element("Phone").Value = person.Phone;
                }
            }
            xDoc.Save(Xmlurl);
            return null;
        }


        //Get person for id
        public Person getPerson(string id)
        {
            Person person = new Person();

            foreach (XElement xe in xRoot.Elements("Person").ToList())
            {
                // The search item id
                if (xe.Attribute("id").Value.Equals(id))
                {
                    //get person item
                    person.Surname=xe.Element("Surname").Value;
                    person.Name = xe.Element("Name").Value;
                    person.YearBorn = Convert.ToInt32(xe.Element("YearBorn").Value);
                    person.Phone = xe.Element("Phone").Value;
                }
            }
            return person;
        }


        //Get person for id
        public List<Person> searchPerson(string Surname=null, string Name = null, string Phone = null)
        {
            foreach (XElement xe in xRoot.Elements("Person").ToList())
            {
                Person person = new Person();
                bool isfound  = false;
                // The search item id
                if (Surname != null)
                {
                   // xe.Element("Surname").Value
                    if (xe.Element("Surname").Value.ToUpper().Contains(Surname.ToUpper()))
                    {
                        //get person item
                        person.Id = Convert.ToInt32(xe.Attribute("id").Value);
                        person.Surname = xe.Element("Surname").Value;
                        person.Name = xe.Element("Name").Value;
                        person.YearBorn = Convert.ToInt32(xe.Element("YearBorn").Value);
                        person.Phone = xe.Element("Phone").Value;
                        isfound = true;
                    }
                }
                if (Name != null)
                {
                    if (xe.Element("Name").Value.ToUpper().Contains(Name.ToUpper()))
                    {
                        //get person item
                        person.Id = Convert.ToInt32(xe.Attribute("id").Value);
                        person.Surname = xe.Element("Surname").Value;
                        person.Name = xe.Element("Name").Value;
                        person.YearBorn = Convert.ToInt32(xe.Element("YearBorn").Value);
                        person.Phone = xe.Element("Phone").Value;
                        isfound = true;
                    }
                }
                if (Phone != null)
                {
                    if (xe.Element("Phone").Value.ToUpper().Contains(Phone.ToUpper()))
                    {
                        //get person item
                        person.Id = Convert.ToInt32(xe.Attribute("id").Value);
                        person.Surname = xe.Element("Surname").Value;
                        person.Name = xe.Element("Name").Value;
                        person.YearBorn = Convert.ToInt32(xe.Element("YearBorn").Value);
                        person.Phone = xe.Element("Phone").Value;
                        isfound = true;
                    }
                }
                if (isfound) Result.Add(person);               
            }
            return Result;
        }


        //Delete item person
        public string delPerson(int id)
        {
            foreach (XElement xe in xRoot.Elements("Person").ToList())
            {
                // The search item id
                if (xe.Attribute("id").Value == id.ToString())
                {
                    //Remove item
                    xe.Remove();
                }
            }
            //Save to file
            xDoc.Save(Xmlurl);
            return null;
        }

    }
}