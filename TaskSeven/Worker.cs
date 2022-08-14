using System;

namespace TaskSeven
{
    struct Worker
    {
        public int Id { get; set; }
        public string FullName{ get; set; }
        public DateTime DateOfCreation { get; set; }
        public byte Age { get; set; }
        public byte Height { get; set; }
        public DateTime DayOfBirth { get; set; }
        public string PlaceOfBirth { get; set; }

        public Worker (int Id, DateTime DateOfCreation, string FullName,  byte Age, byte Height, DateTime DayOfBirth, string PlaceOfBirth)
        {
            this.Id = Id;
            this.FullName = FullName;
            this.DateOfCreation = DateOfCreation;
            this.Age = Age;
            this.Height = Height;
            this.DayOfBirth = DayOfBirth;
            this.PlaceOfBirth = PlaceOfBirth;
        }
    }
}
