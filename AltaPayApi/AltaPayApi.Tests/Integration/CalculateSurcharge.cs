using NUnit.Framework;
using AltaPay.Api.Tests;

namespace AltaPay.Service.Tests.Integration
{
	[TestFixture]
	class CalculateSurcharge : BaseTest
	{
		IMerchantApi _api;

		[SetUp]
		public void Setup ()
		{
			_api = new MerchantApi (GatewayConstants.gatewayUrl, GatewayConstants.username, GatewayConstants.password);
		}

		[Test]
		public void CallingMerchantApiReturnsSuccessfulResult ()
		{
			CalculateSurchargeResult result = _api.CalculateSurcharge();

			Assert.AreEqual (Result.Success, result.Result);
		}
	}
}
