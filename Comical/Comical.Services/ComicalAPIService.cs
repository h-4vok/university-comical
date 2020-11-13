﻿using Comical.Models.Common;
using Comical.Models.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Comical.Services
{
    public class ComicalAPIService
    {
        #region Singleton

        private ComicalAPIService() { }
        static ComicalAPIService() { }

        public static ComicalAPIService obj { get; } = new ComicalAPIService();

        #endregion

        public async Task<bool> RecalculateVerifiers()
        {
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ComicalConfiguration.ComicalAPIUri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    using (var response =
                        await client.PostAsJsonAsync("api/checksum",
                        new ChecksumRecalculationRequest { UserName = "admin", Password = "admin" })
                        .ConfigureAwait(false))
                    using(var content = response.Content)
                    {
                        var jsonResponse = await content.ReadAsStringAsync().ConfigureAwait(false);
                        var apiResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(jsonResponse);
                        var serializer = new XmlSerializer(typeof(ChecksumRecalculationResponse), new XmlRootAttribute("ChecksumRecalculationResponse"));
                        ChecksumRecalculationResponse responseObject;

                        using(var reader = new StringReader(apiResponse))
                        {
                            responseObject = serializer.Deserialize(reader) as ChecksumRecalculationResponse;
                        }
                        return responseObject.Success;
                    }
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}
