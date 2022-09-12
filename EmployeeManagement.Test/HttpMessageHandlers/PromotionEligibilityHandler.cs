using EmployeeManagement.Business;
using System.Text;
using System.Text.Json;

namespace EmployeeManagement.Test.HttpMessageHandlers
{
    public class PromotionEligibilityHandler : HttpMessageHandler
    {
        private readonly bool _isEligibleForPromotion;
        public PromotionEligibilityHandler(bool isEligibleForPromotion) => 
            _isEligibleForPromotion = isEligibleForPromotion;

        /// <summary>
        /// Generate the manually-created response and return it.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var promotion = new PromotionEligibility { EligibleForPromotion = _isEligibleForPromotion };

            var response = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
            {
                Content = new StringContent(
                    JsonSerializer.Serialize(promotion,
                    new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    }),
                Encoding.ASCII,
                "application/json")
             };

            return Task.FromResult(response);
        }
    }
}
