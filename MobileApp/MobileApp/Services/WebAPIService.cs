using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace MobileApp.Services
{
    class WebAPIService
    {
        public HttpClient HttpClient { get; }

        public WebAPIService()
        {
            HttpClient = new HttpClient();
        }

        public async void RefreshUserInfo()
        {
            var uri = new Uri($"{App.ApiUrl}/");
        }
    }
}
