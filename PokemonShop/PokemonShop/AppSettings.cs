namespace PokemonShop
{
    public class AppSettings
    {
        public SmtpSettings SmtpSettings { get; set; }
        
        public SmtpTemplate SmtpTemplate { get; set; }
    }

    public class SmtpSettings
    {
        public string FromEmail { get; set; }

        public string Password { get; set; }

        public string Host { get; set; }

        public int Port { get; set; }

        public bool UseSsl { get; set; }
    }

    public class SmtpTemplate
    {
        public string Name { get; set; }

        public string Subject { get; set; }

        public string Message { get; set; }
    }
}