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
            //Iterate through the entire list
            foreach (XElement xe in xRoot.Elements("Person").ToList())
            {
                 Person person = new Person(); 
                
                //get person item to temporary variable person
                person.Id = Convert.ToInt32(xe.Attribute("id").Value);
                person.Surname = xe.Element("Surname").Value;
                person.Name = xe.Element("Name").Value;
                person.YearBorn = Convert.ToInt32(xe.Element("YearBorn").Value);
                person.Phone = xe.Element("Phone").Value;
                Result.Add(person);
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
            //Iterate through the entire list
            foreach (XElement xe in xRoot.Elements("Person").ToList())
            {
                //Looking for the highest id value
                if (Convert.ToInt32(xe.Attribute("id").Value) > id)
                {
                    id = Convert.ToInt32(xe.Attribute("id").Value);
                }
            }
            //Create attribute and elements of Person
            //++id - The increment of id 
            // Add attr and elements
            Person.Add(new XAttribute("id", ++id));
            Person.Add(new XElement("Surname", person.Surname));
            Person.Add(new XElement("Name", person.Name));
            Person.Add(new XElement("YearBorn", person.YearBorn));
            Person.Add(new XElement("Phone", person.Phone));
            //Add all to parent element
            xRoot.Add(Person);
            //Save xml file
            xDoc.Save(Xmlurl);
            return null;
        }

        //Update item Person
        public string updatePerson(Person person)
        {
            //Iterate through the entire list
            foreach (XElement xe in xRoot.Elements("Person").ToList())
            {
                // The search item id
                if (xe.Attribute("id").Value.Equals(person.Id.ToString()))
                {
                    //Save item with new values
                    xe.Element("Surname").Value = person.Surname;
                    xe.Element("Name").Value = person.Name;
                    xe.Element("YearBorn").Value = person.YearBorn.ToString();
                    xe.Element("Phone").Value = person.Phone;
                    //Save xml file after update
                    xDoc.Save(Xmlurl);
                    return null;
                }
            }
            return "Not Found";
        }


        //Get person for id
        public Person getPerson(string id)
        {
            Person person = new Person();
            //Iterate through the entire list
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


        //Search People by Surname, Name and Phone
        public List<Person> searchPerson(string Surname="", string Name = "", string Phone = "")
        {
            //Iterate through the entire list
            foreach (XElement xe in xRoot.Elements("Person").ToList())
            {
                //Create a temporary variable of Person
                Person person = new Person();
                //Flag is found element, false default
                bool isfound  = false;

                // The search item by Surname
                if (Surname != "")
                {
                    //True if the value xe.Element contains the substring 'Surname'
                    if (xe.Element("Surname").Value.ToUpper().Contains(Surname.ToUpper()))
                    {
                        //get person item to temporary variable person
                        person.Id = Convert.ToInt32(xe.Attribute("id").Value);
                        person.Surname = xe.Element("Surname").Value;
                        person.Name = xe.Element("Name").Value;
                        person.YearBorn = Convert.ToInt32(xe.Element("YearBorn").Value);
                        person.Phone = xe.Element("Phone").Value;
                        //flag is true
                        isfound = true;
                    }
                }

                // The search item by Name
                if (Name != "")
                {
                    //True if the value xe.Element contains the substring 'Name'
                    if (xe.Element("Name").Value.ToUpper().Contains(Name.ToUpper()))
                    {
                        //get person item to temporary variable person
                        person.Id = Convert.ToInt32(xe.Attribute("id").Value);
                        person.Surname = xe.Element("Surname").Value;
                        person.Name = xe.Element("Name").Value;
                        person.YearBorn = Convert.ToInt32(xe.Element("YearBorn").Value);
                        person.Phone = xe.Element("Phone").Value;
                        isfound = true;
                    }
                }

                // The search item by Phone
                if (Phone != "")
                {
                    //True if the value xe.Element contains the substring 'YearBorn'
                    if (xe.Element("Phone").Value.ToUpper().Contains(Phone.ToUpper()))
                    {
                        //get person item to temporary variable person
                        person.Id = Convert.ToInt32(xe.Attribute("id").Value);
                        person.Surname = xe.Element("Surname").Value;
                        person.Name = xe.Element("Name").Value;
                        person.YearBorn = Convert.ToInt32(xe.Element("YearBorn").Value);
                        person.Phone = xe.Element("Phone").Value;
                        isfound = true;
                    }
                }
                //If the flag is true then add person found to List
                if (isfound) Result.Add(person);               
            }
            //return the list of found person
            return Result;
        }


        //Delete item person
        public string delPerson(string id)
        {
            if (id != "")
            {
                foreach (XElement xe in xRoot.Elements("Person").ToList())
                {
                    // The search item id
                    if (xe.Attribute("id").Value == id)
                    {
                        //Remove item
                        xe.Remove();
                        //Save to file
                        xDoc.Save(Xmlurl);
                        return null;
                    }
                }
            }
            return "Not found";
        }
    }
}