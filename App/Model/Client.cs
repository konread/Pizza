namespace Model
{
    public class Client
    {
        public int Id_Customer { get; set; }
        public string First_Name { get; set; }
        public string Surname { get; set; }
        public string City_Name { get; set; }
        public string Street_Name { get; set; }
        public string House_Number { get; set; }
        public string Postal_code { get; set; }

        public Client(string first_Name, string surname, string city_Name, string street_Name, string house_Number, string postal_code)
        {
            First_Name = first_Name;
            Surname = surname;
            City_Name = city_Name;
            Street_Name = street_Name;
            House_Number = house_Number;
            Postal_code = postal_code;
        }

        public override string ToString()
        {
            return First_Name + " " + Surname + ", " + Street_Name + " " + House_Number + ", " + Postal_code + " " + City_Name;
        }
    }
}