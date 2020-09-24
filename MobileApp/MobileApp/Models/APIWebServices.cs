using System.Net.Http;

namespace MobileApp.Models
{
    class APIWebServices
    {
        HttpClient Client { get; set; }

        public APIWebServices()
        {
#if DEBUG
            HttpClientHandler insecureHandler = GetInsecureHandler();
            Client = new HttpClient(insecureHandler);
#else
            HttpClient Client = new HttpClient();
#endif
        }

        public HttpClientHandler GetInsecureHandler()
        {
            HttpClientHandler handler = new HttpClientHandler();

            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
            {
                if (cert.Issuer.Equals("CN=localhost"))
                    return true;

                return errors == System.Net.Security.SslPolicyErrors.None;
            };

            return handler;
        }
    }
}
