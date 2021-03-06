﻿using ArcTMDb.Models;
using ArcTMDb.Helpers;
using ModernHttpClient;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ArcTMDb.Service
{
    public class ArcTMDbApiService : IArcTMDbApiService
    {
        public async Task<MovieResults> GetUpcomingMoviesAsync(int page = 1)
        {
            var httpClient = new HttpClient(new NativeMessageHandler());

            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var url = $"{Constants.BaseUrl}/movie/upcoming?{Constants.ApiKey}&page={page}";

            try
            {
                var response = await httpClient.GetAsync(new Uri(url)).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    using (var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                    {
                        return JsonConvert.DeserializeObject<MovieResults>(
                            await new StreamReader(responseStream)
                                .ReadToEndAsync().ConfigureAwait(false));
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }

            return null;
        }

        public async Task<MovieResults> GetMoviesByTitleAsync(string title, int page = 1)
        {
            var httpClient = new HttpClient(new NativeMessageHandler());

            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var url = $"{Constants.BaseUrl}/search/movie?{Constants.ApiKey}&query={title}&page={page}";

            try
            {
                var response = await httpClient.GetAsync(new Uri(url)).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    using (var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                    {
                        return JsonConvert.DeserializeObject<MovieResults>(
                            await new StreamReader(responseStream)
                                .ReadToEndAsync().ConfigureAwait(false));
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }

            return null;
        }

        public async Task<GenreResults> GetGenresAsync()
        {
            var httpClient = new HttpClient(new NativeMessageHandler());

            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var url = ($"{Constants.BaseUrl}/genre/movie/list?{Constants.ApiKey}");

            try
            {
                var response = await httpClient.GetAsync(new Uri(url)).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content;
                    using (var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                    {
                        return JsonConvert.DeserializeObject<GenreResults>(
                            await new StreamReader(responseStream)
                                .ReadToEndAsync().ConfigureAwait(false));
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }

            return null;
        }
    }
}
