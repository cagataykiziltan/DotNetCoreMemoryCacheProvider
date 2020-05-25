using System.Collections.Generic;


namespace CacheProvider.Api
{
    public class StudentApi
    {
        public List<Student> GetStudents()
        {
            var myList = new List<Student> { new Student { Name="Cagatay",Surname="Kiziltan", IdentityNumber="12345678910", Gender="Male"  },
                                             new Student {  Name="Doguhan",Surname="Ciftci", IdentityNumber="12345678910", Gender= "Male" },
                                             new Student {  Name="Ömer",Surname="Korkmaz", IdentityNumber="12345678910", Gender="Male"  },
                                             new Student { Name="Kuthay",Surname="Gümüş", IdentityNumber="12345678910", Gender= "Male" },
                                             new Student {  Name="Nevşin",Surname="Avşar", IdentityNumber="12345678910", Gender= "Female" },
                                             new Student {  Name="Canan",Surname="Tan", IdentityNumber="12345678910", Gender= "Female" },

            };

            return myList;
        }
    }

    public class Student
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string IdentityNumber { get; set; }
        public string Gender { get; set; }
    }
}
