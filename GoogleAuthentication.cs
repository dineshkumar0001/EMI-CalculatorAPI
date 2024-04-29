using Google.Apis.Auth.OAuth2;

namespace EmiCalculator
{
    public static class GoogleAuthentication
    {
        public static UserCredential login(string googleClientId, string googleClientSecret, string[] scopes)
        {
            ClientSecrets secrets = new ClientSecrets()
            {
                ClientId = googleClientId,
                ClientSecret = googleClientSecret
            };
            return GoogleWebAuthorizationBroker.AuthorizeAsync(secrets, scopes, "user", CancellationToken.None).Result;
         }

        public  static async Task<UserCredential> loginAsync(string googleClientId, string googleClientSecret, string[] scopes)
        {
            ClientSecrets secrets = new ClientSecrets()
            {
                ClientId = googleClientId,
                ClientSecret = googleClientSecret
            };
            return await GoogleWebAuthorizationBroker.AuthorizeAsync(secrets, scopes, "user", CancellationToken.None);
        }
    }
}
