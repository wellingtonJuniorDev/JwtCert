
namespace JwtCert.Core
{
    public class JwtConfiguration
    {
        public string CertificatePath { get; set; }
        public string CertificatePassword { get; set; }
        public int MinutesExpiration { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}
