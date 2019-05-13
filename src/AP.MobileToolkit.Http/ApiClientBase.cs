﻿using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using AP.CrossPlatform.Auth;
using AP.CrossPlatform.Http;
using Prism.Logging;

namespace AP.MobileToolkit.Http
{
    public abstract class ApiClientBase : IApiClient
    {
        protected IAuthenticationHandler AuthenticationHandler { get; }

        protected IApiClientOptions Options { get; }

        protected ILogger Logger { get; }

        protected string InstallId { get; }

        private HttpClient _client;

        private HttpClient Client
        {
            get
            {
                if (_client is null)
                {
                    _client = CreateClient();
                }

                return _client;
            }
        }

        public ApiClientBase(IApiClientOptions options, IAuthenticationHandler authenticationHandler, ILogger logger)
        {
            InstallId = options.InstallId;
            AuthenticationHandler = authenticationHandler;
            Options = options;
            Logger = logger;
        }

        public virtual Task<HttpResponseMessage> GetAsync(string requestUri, CancellationToken cancellationToken = default)
        {
            return Client.GetAsync(requestUri, cancellationToken);
        }

        public virtual Task<HttpResponseMessage> DeleteAsync(string requestUri, object content = null, CancellationToken cancellationToken = default)
        {
            return Client.DeleteAsync(requestUri, content, cancellationToken);
        }

        public virtual Task<HttpResponseMessage> PatchAsync(string requestUri, object content = null, CancellationToken cancellationToken = default)
        {
            return Client.PatchAsync(requestUri, content, cancellationToken);
        }

        public virtual Task<HttpResponseMessage> PostAsync(string requestUri, object content = null, CancellationToken cancellationToken = default)
        {
            return Client.PostJsonObjectAsync(requestUri, content, cancellationToken);
        }

        public virtual Task<HttpResponseMessage> PutAsync(string requestUri, object content = null, CancellationToken cancellationToken = default)
        {
            return Client.PutAsync(requestUri, content, cancellationToken);
        }

        public virtual async Task<IUser> GetUserAsync()
        {
            try
            {
                var token = await AuthenticationHandler.GetTokenAsync().ConfigureAwait(false);
                return new JwtUser(token);
            }
            catch (Exception ex)
            {
                Logger.Report(ex);
                return null;
            }
        }

        protected virtual void SetDefaultHeaders(HttpClient client)
        {
        }

        protected virtual HttpClient CreateClient()
        {
            var authenticationMessageHandler = new AuthenticationMessageHandler(AuthenticationHandler)
            {
                InnerHandler = new RetryRequestDelegateHandler()
            };
            var client = new HttpClient(authenticationMessageHandler)
            {
                BaseAddress = new Uri(Options.BaseUri)
            };

            SetDefaultHeaders(client);

            return client;
        }

        public void Dispose()
        {
            _client?.Dispose();
            _client = null;
        }
    }
}
