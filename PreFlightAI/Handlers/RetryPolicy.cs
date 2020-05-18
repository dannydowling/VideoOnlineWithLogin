using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace PreFlight.AI.Server.Http.Services
{    
        public class RetryPolicy : DelegatingHandler
        {
            private readonly int _maximumAmountOfRetries = 3;
            private readonly TimeSpan _timeOut = TimeSpan.FromSeconds(100);

        public RetryPolicy(int maximumAmountOfRetries, TimeSpan timeOut)
                : base()
            {
                _maximumAmountOfRetries = maximumAmountOfRetries;
                 _timeOut = timeOut;
        }

            public RetryPolicy(HttpMessageHandler innerHandler,
              int maximumAmountOfRetries, TimeSpan timeOut)
          : base(innerHandler)
            {
                _maximumAmountOfRetries = maximumAmountOfRetries;
            _timeOut = timeOut;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
                CancellationToken cancellationToken)
            {
            using (var linkedCancellationTokenSource =
             CancellationTokenSource.CreateLinkedTokenSource(cancellationToken))
            {
                linkedCancellationTokenSource.CancelAfter(_timeOut);
                HttpResponseMessage response = null;
                try
                {
                    for (int i = 0; i < _maximumAmountOfRetries; i++)
                    {
                        response = await base.SendAsync(request, cancellationToken);

                        if (response.IsSuccessStatusCode)
                        {
                            return response;
                        }
                    }
                }

                catch (OperationCanceledException ex)
                {
                    if (!cancellationToken.IsCancellationRequested)
                    {
                        throw new TimeoutException("The request timed out.", ex);
                    }
                    
                }
                return response;
            }        
        }
    }
}
