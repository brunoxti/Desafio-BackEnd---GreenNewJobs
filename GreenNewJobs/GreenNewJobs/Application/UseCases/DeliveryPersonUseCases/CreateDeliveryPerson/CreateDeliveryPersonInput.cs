namespace GreenNewJobs.Application.UseCases.DeliveryPersonUseCases.CreateDeliveryPerson
{
    public class CreateDeliveryPersonInput
    {
        public string Name { get; set; }
        public string CNPJ { get; set; }
        public DateTime BirthDate { get; set; }
        public string CNHNumber { get; set; }
        public string CNHType { get; set; }

        public bool IsValid(out string validationMessage)
        {
            validationMessage = string.Empty;

            var validCnhTypes = new HashSet<string> { "A", "B", "A+B" };
            if (!validCnhTypes.Contains(CNHType))
            {
                validationMessage = "Invalid CNH type. Valid types are A, B, or A+B.";
                return false;
            }

            return true;
        }
    }
}
