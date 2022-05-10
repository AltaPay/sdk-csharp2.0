﻿using AltaPay.Service;
using AltaPay.Service.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Examples
{
    public class CaptureExamples
    {
        private readonly IMerchantApi _api;

        public CaptureExamples()
        {
            //This is the URL to connect to your gateway instance. If you are in doubt contact support.
			//For test, use: testgateway.altapaysecure.com
            string gatewayUrl = "https://testgateway.altapaysecure.com/merchant/API/";

            //username to be authenticated on the gateway
            string username = "shop api";

            //provided password for user authentication
            string password = "testpassword";

            //Instatiation of the API helper class which provide all necessary merchant api methods
            //This class requires gateway URL, username and password params are forwarded in the contructor
            _api = new MerchantApi(gatewayUrl, username, password);
        }

        /// <summary>
        /// Example for performing simple capture request.
        /// </summary>
        public void SimpleCapture()
        {
            //Reserving amount in order to capture method example could be successful
            PaymentResult paymentResult = ReserveAmount(Amount.Get(2190.00, Currency.EUR), AuthType.payment);
            //Transaction ID is returned from the gateway when payment request was successful
            string transactionId = paymentResult.Payment.TransactionId;

            //initialize capture request class, this class is used for forwarding all the data needed for capture request
            //for simple capture request only transaction ID is required
            CaptureRequest captureRequest = new CaptureRequest
            {
                PaymentId = transactionId
            };

            //call capture method
            CaptureResult captureResult = _api.Capture(captureRequest);

            //Result property contains information if the request was successful or not
            if (captureResult.Result == Result.Success)
            {
                //capture was successful
                Transaction transaction = captureResult.Payment;
            }
            else
            {
                //capture unsuccessful
                //error messages contain information about what went wrong
                string errorMerchantMessage = captureResult.ResultMerchantMessage;
                string errorMessage = captureResult.ResultMessage;
            }
        }
        
        /// <summary>
        /// Example for performing charge subscription request with Agreement.
        /// </summary>
        public void ChargeSubscriptionWithAgreement()
        {
            //Set up an agreement / setupSubscription / Reserving amount in order to charge it later
            PaymentResult createPaymentResult = ReserveAmount(Amount.Get(7777.00, Currency.EUR), AuthType.subscription, "IT_AGREEMENTS_UI_");
            //Transaction ID is returned from the gateway when payment request was successful
            string transactionId = createPaymentResult.Payment.TransactionId;
            //WaitForDataToFlowIntoReporting
            Thread.Sleep(3000);
            //initialise charge subscription request class, this class is used for forwarding all the data needed for charging an agreement / charge subscription request
            var chargeSubscriptionRequest = new ChargeSubscriptionRequest()
            {
                AgreementId = transactionId,
                Amount = Amount.Get(7777, Currency.XXX),
                AgreementUnscheduledType = AgreementUnscheduledType.incremental,
            };

            //call capture method
            SubscriptionResult subscriptionResult = _api.ChargeSubscription(chargeSubscriptionRequest);

            //Result property contains information if the request was successful or not
            if ((subscriptionResult.Result == Result.Success) && (createPaymentResult.Payment.TransactionId == subscriptionResult.Payment.TransactionId) && (subscriptionResult.Payment.TransactionStatus == "recurring_confirmed") && (subscriptionResult.RecurringPayment.TransactionStatus == "captured"))
            {
                //subscription charge was successful
                Transaction transaction = subscriptionResult.Payment;
            }            
            else
            {
                //capture unsuccessful
                //error messages contain information about what went wrong
                string errorMerchantMessage = subscriptionResult.ResultMerchantMessage;
                string errorMessage = subscriptionResult.ResultMessage;
            }
        }

        /// <summary>
        /// Example for performing simple partial capture request. 
        /// Amount sent to be partialy captured has to be less than total amount of the payment
        /// </summary>
        public void SimplePartialCapture()
        {
            //Reserving amount in order to capture method example could be successful
            PaymentResult paymentResult = ReserveAmount(Amount.Get(1200.00, Currency.EUR), AuthType.payment);
            //Transaction ID is returned from the gateway when payment request was successful
            string transactionId = paymentResult.Payment.TransactionId;

            //initialize capture request class, this class is used for forwarding all the data needed for capture request
            //for simple partial capture amount property should be set with amount less than total amount
            CaptureRequest captureRequest = new CaptureRequest
            {
                PaymentId = transactionId,
                Amount = Amount.Get(600.00, Currency.EUR)
            };

            //call capture method
            CaptureResult captureResult = _api.Capture(captureRequest);

            //Result property contains information if the request was successful or not
            if (captureResult.Result == Result.Success)
            {
                //capture was successful
                Transaction transaction = captureResult.Payment;
            }
            else
            {
                //capture unsuccessful
                //error messages contain information about what went wrong
                string errorMerchantMessage = captureResult.ResultMerchantMessage;
                string errorMessage = captureResult.ResultMessage;
            }
        }

        /// <summary>
        /// Example for performing complex capture request. 
        /// In complex capture request orderlines are sent to the gateway
        /// </summary>
        public void ComplexCapture()
        {
            //Reserving amount in order to capture method example could be successful
            PaymentResult paymentResult = ReserveAmount(Amount.Get(1200.00, Currency.EUR), AuthType.payment);
            //Transaction ID is returned from the gateway when payment request was successful
            string transactionId = paymentResult.Payment.TransactionId;

            //inizialize orderlines
            List<PaymentOrderLine> orderLines = new List<PaymentOrderLine>
            {
                new PaymentOrderLine()
                {
                    Description = "The Item Desc",
                    ItemId = "itemId1",
                    Quantity = 10,
                    TaxPercent = 0,
                    UnitCode = "unitCode",
                    UnitPrice = 120,
                    Discount = 0.00,
                    GoodsType = GoodsType.Item,
                    ProductUrl = "product/path/product.html",
                }
            };
            //initialize capture request class, this class is used for forwarding all the data needed for capture request
            //for simple partial capture Amount property should be set with amount less than total amount
            CaptureRequest captureRequest = new CaptureRequest
            {
                PaymentId = transactionId,
                Amount = Amount.Get(1200.00, Currency.EUR),
                OrderLines = orderLines
            };

            //call capture method
            CaptureResult captureResult = _api.Capture(captureRequest);

            //Result property contains information if the request was successful or not
            if (captureResult.Result == Result.Success)
            {
                //capture was successful
                Transaction transaction = captureResult.Payment;
            }
            else
            {
                //capture unsuccessful
                //error messages contain information about what went wrong
                string errorMerchantMessage = captureResult.ResultMerchantMessage;
                string errorMessage = captureResult.ResultMessage;
            }
        }

        #region helpers

        /// <summary>
        /// Helper method needed for reserving amount in order to capture examples could work
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private PaymentResult ReserveAmount(Amount amount, AuthType type, string prefix = "")
        {
            var request = new ReserveRequest
            {
                ShopOrderId = prefix + "csharpexample" + Guid.NewGuid().ToString(),
                Terminal = "AltaPay Dev Terminal",
                Amount = amount,
                PaymentType = type,
                Pan = "4111000011110000",
                ExpiryMonth = 1,
                ExpiryYear = 2025,
                Cvc = "123",
            };

            if (prefix != "")
            {
                var agreementConfig = new AgreementConfig();
                agreementConfig.AgreementType = AgreementType.unscheduled;
                agreementConfig.AgreementUnscheduledType = AgreementUnscheduledType.incremental;
                request.AgreementConfig = agreementConfig;
            }

            PaymentResult result = _api.ReserveAmount(request);

            if (result.Result != Result.Success)
            {
                throw new Exception("The result was: " + result.Result + ", message: " + result.ResultMerchantMessage);
            }

            return result;
        }

        #endregion helpers
    }
}
