﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Examples
{
    class Program
    {
        public static void Main(string[] args)
        {
            //createPaymentRequest examples
            Console.WriteLine("Executing createPayment examples...");
            CreatePaymentRequestExamples cprExamples = new CreatePaymentRequestExamples();
            cprExamples.CreateSimplePaymentRequest();
            cprExamples.CreateComplexPaymentRequest();
            cprExamples.CreateSimplePaymentRequestWithAgreement();            

            //capture examples
            Console.WriteLine("Executing capture examples...");
            CaptureExamples captureExamples = new CaptureExamples();
            captureExamples.SimpleCapture();
            captureExamples.SimplePartialCapture();
            captureExamples.ComplexCapture();
            captureExamples.ChargeSubscriptionWithAgreement();

            //refund examples
            Console.WriteLine("Executing refund examples...");
            RefundExamples refundExamples = new RefundExamples();
            refundExamples.SimpleRefund();
            refundExamples.SimplePartialRefund();
            refundExamples.ComplexRefund();

            //release examples
            Console.WriteLine("Executing release examples...");
            ReleaseExamples releaseExamples = new ReleaseExamples();
            releaseExamples.Release();
            //getTerminals examples
            Console.WriteLine("Executing Get Terminals example");
            GetTerminalsExamples getTerminalsExamples = new GetTerminalsExamples();
            getTerminalsExamples.GetTerminals();
            //calculateSurcharge examples
            Console.WriteLine ("Executing Calculate Surcharge example");
            CalculateSurchargeExamples calculateSurchargeExamples = new CalculateSurchargeExamples ();
            calculateSurchargeExamples.CalculateSurcharge ();

            System.Console.ReadLine();
        }
    }
}
