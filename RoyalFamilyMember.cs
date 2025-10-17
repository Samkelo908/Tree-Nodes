using System;

namespace RoyalFamilyTree
{
    /// <summary>
    /// Represents a member of the Royal Family
    /// </summary>
    public class RoyalFamilyMember
    {
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool IsAlive { get; set; }
        public string Title { get; set; }

        public RoyalFamilyMember(string name, DateTime dateOfBirth, bool isAlive, string title = "")
        {
            Name = name;
            DateOfBirth = dateOfBirth;
            IsAlive = isAlive;
            Title = title;
        }

        public int Age
        {
            get
            {
                DateTime endDate = IsAlive ? DateTime.Now : DateTime.Now;
                int age = endDate.Year - DateOfBirth.Year;
                if (endDate < DateOfBirth.AddYears(age))
                    age--;
                return age;
            }
        }

        public override string ToString()
        {
            string status = IsAlive ? "Alive" : "Deceased";
            return $"{Name} {(string.IsNullOrEmpty(Title) ? "" : $"({Title})")}, Born: {DateOfBirth:yyyy-MM-dd}, {status}";
        }
    }
}