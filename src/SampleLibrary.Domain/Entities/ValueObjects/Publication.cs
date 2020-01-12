namespace SampleLibrary.Domain.Tests.Entities.Validators.Entities.ValueObjects
{
    public class Publication
    {
        public Publication(int edition, int year)
        {
            Edition = edition;
            Year = year;
        }

        public int Edition { get; private set; }
        public int Year { get; private set; }
    }
}
