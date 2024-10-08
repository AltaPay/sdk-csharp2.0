# Changelog
All notable changes to this project will be documented in this file.

## [1.1.11]

- Add new response property `AppUrl` for `createPaymentRequest`.
- Add new method `CreditCardWalletInitiateAppPayment` to initiate credit card wallet payment in APP for MobilePay & Vipps.

## [1.1.10]

- Add support for endpoint `calculateSurcharge`.

## [1.0.9]

- Add option to set new callback parameter `callback_mobile_app_redirect` for `createPaymentRequest`.

## [1.0.8]

- Add support for endpoint `getTerminals`.
## [1.0.7]

- Add `digital`, `discount`, `gift_card`, `physical` and `sales_tax` as new values for goodsType.

## [1.0.6]

- Replace the deprecated 'transactions' endpoint with 'payments', for 'GetPayment' & 'GetPayments' methods

## [1.0.5]

- Supports API changes from 20230412
- Enforce the right HTTP methods on all API endpoints.

## [1.0.4]

- Add support for redirect response in reservation, chargeSubscription & reserveSubscriptionCharge

## [1.0.3]

- Add support for subscription via MobilePay

## [1.0.2]

- Fix 'Agreements Engine' parameter 'unscheduled_type'
- Add support for Apple Pay

## [1.0.1]

- Add support for new 'Agreements Engine' parameters

## [1.0.0]

- Supports API changes from 20210324
