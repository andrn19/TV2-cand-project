namespace TV2.Backend.Services.VideoAnalyser.Client.Auth
{
    using Azure.Identity;
    using Azure.Core;
    using System.Net.Http.Headers;
    using System.Text.Json;
    
    public static class AccountTokenProvider
    {
        
        public static async Task<string> GetArmAccessTokenAsync(CancellationToken ct = default)
        {
            var tokenRequestContext = new TokenRequestContext(new[] { $"{Consts.AzureResourceManager}/.default" });
            var tokenRequestResult = await new DefaultAzureCredential().GetTokenAsync(tokenRequestContext, ct);
            return tokenRequestResult.Token;
        }

        public static async Task<string> GetAccountAccessTokenAsync(string armAccessToken, HttpClient client, ArmAccessTokenPermission permission = ArmAccessTokenPermission.Contributor, ArmAccessTokenScope scope = ArmAccessTokenScope.Account, CancellationToken ct = default)
        {
            var accessTokenRequest = new AccessTokenRequest
            {
                PermissionType = permission,
                Scope = scope
            };

            try
            {
                var jsonRequestBody = JsonSerializer.Serialize(accessTokenRequest);
                var httpContent = new StringContent(jsonRequestBody, System.Text.Encoding.UTF8, "application/json");

                // Set request uri
                var requestUri = $"{Consts.AzureResourceManager}/subscriptions/{Consts.SubscriptionId}/resourcegroups/{Consts.ResourceGroup}/providers/Microsoft.VideoIndexer/accounts/{Consts.ViAccountName}/generateAccessToken?api-version={Consts.ApiVersion}";
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", armAccessToken);

                var result = await client.PostAsync(requestUri, httpContent, ct);
                result.EnsureSuccessStatusCode();
                var jsonResponseBody = await result.Content.ReadAsStringAsync(ct);
                return JsonSerializer.Deserialize<GenerateAccessTokenResponse>(jsonResponseBody)?.AccessToken!;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }

    }
}