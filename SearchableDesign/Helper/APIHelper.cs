using Microsoft.AspNetCore.DataProtection.KeyManagement;
using SearchableDesign.UI.Models;
using System.Net.Http.Headers;

namespace SearchableDesign.UI.Helper
{
    public static class APIHelper
    {
        public static async Task<JsonOutput> ApiRespond(string apiurl)
        {
            JsonOutput jsonOutput = new JsonOutput();
            using (HttpClient httpClient = new HttpClient())
            {
                //httpClient.DefaultRequestHeaders.Add("APIKEY", apikey);
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    HttpResponseMessage result = await httpClient.GetAsync(apiurl);
                    if (result.IsSuccessStatusCode)
                    {
                        string contents = await result.Content.ReadAsStringAsync();
                        jsonOutput.Result = contents;
                        jsonOutput.Success = true;
                    }
                    else
                    {
                        jsonOutput.Success = false;
                        jsonOutput.Message = "Employees not found!";
                    }
                }
                catch (Exception ex)
                {
                    jsonOutput.Message = ex.Message;
                }
            }

            return jsonOutput;
        }
    }
}
