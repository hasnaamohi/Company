namespace Company.PL.Services
{
    public class SingletonService:ISingeltonService
    {
        public SingletonService()
        {
            Guid = Guid.NewGuid();

        }
        public Guid Guid { get; set; }

        public string GetGuid()
        {
            return Guid.ToString();
        }
    }
}
