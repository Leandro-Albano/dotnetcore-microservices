
using System;
using System.Threading.Tasks;

namespace Dynasoft.Security.Application.Auth.Consumer
{
    public class Program
    {
        public static async Task Main()
        {
            var client = new Client.AuthClient("https://localhost:5001/");

            var result = await client.AuthenticateAsync("eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCIsImtpZCI6Ik5UVXpSVVUxUmtZNVJUSTNNamN6TUVaRk4wRkJRME0zUkRNeFJFUXdRVFZGUlVZd1JEazVSUSJ9.eyJpc3MiOiJodHRwczovL2F1dGguY2xpY2tpcS5jby51ay8iLCJzdWIiOiJhdXRoMHw1ZGNiZDkyOWI3NjI1MjBlNDgwYzcxMmYiLCJhdWQiOlsiaHR0cHM6Ly9jbHVzdGVyLnN0YWdlLmNsaWNraXEuaW8iLCJodHRwczovL2NsaWNraXEuZXUuYXV0aDAuY29tL3VzZXJpbmZvIl0sImlhdCI6MTYwNDE2Nzg3NywiZXhwIjoxNjA0MTc1MDc3LCJhenAiOiJxVmtJdXVXWGpFNkMxTTRFd1RwYlpxVTRVM2pqZmRIMiIsInNjb3BlIjoib3BlbmlkIHByb2ZpbGUgZW1haWwifQ.r3xOOHPoilYV9MAYtQhZ-HfHv7uAlMcPYktdsiR1UXINKALZFMWOGyMSDgYy-zwkKjepPCV2U5UfdHOoea5EjqTc24m_PDs47Bo7KSDHJ7igJPhfvfvwHTg-kmzeWWXhj8K-ywB_dfkCeZEfS3dy0zg_aJprJOh8LJtmA5kpa_qbBNcBme9QN6II05PzSbBDaBegG6uBDo08LoodcHrIX5DjTVruadLwj_xr0hfiLEcRFhNGBe714QGg9qTu97mavYIp0s_V0p1EmMK2UKHtFnCWpv3zvecV-SDPOKw9U692lROHKSgeNjmOrTgFuN7xQXT8Fq2Ma3rouixVW85vJQ");

            Console.WriteLine(result.Name);
            Console.ReadKey();
        }
    }
}
