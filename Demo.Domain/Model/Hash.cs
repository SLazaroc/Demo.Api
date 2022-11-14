namespace Demo.Domain.Model
{
    public class Hash
    {
        public int Id { get; set; }
        public string? SHA512 { get; set; }
        public long ElapsedTime { get; set; }
        public bool IsEncrypted { get; set; }
    }
}
