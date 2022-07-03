using System.Net.Http;
using TodoREST.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(HttpCHService))]
namespace TodoREST.Droid
{
    public class HttpCHService : IHttpCHService
    {
        public HttpClientHandler GetInsecureHandler()
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback =
                (message, cert, chain, errors) =>
            {
                if (cert.Issuer.Equals("CN=localhost"))
                    return true;
                return errors == System.Net.Security.SslPolicyErrors.None;
            };
            return handler;
        }
    }
}