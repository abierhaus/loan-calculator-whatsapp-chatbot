# Chatbot for a loan payment calculator based on the .NET Twilio WhatsApp API
Example how to create a loan chatbot calculator in .NET/ C# for WhatsApp/ SMS using the Twilio API.

The approach is to build a lightweight loan calculator bot without using to much dependencies that can be deployed and used within minutes. 
Calculation and input checking is done at a simple level. Thus, edge cases and complex inputs are not considered.

The chatbot takes the WhatsApp message and implements a basic routing service for providing a multi step conversation.

![image](https://user-images.githubusercontent.com/18400458/108873862-db372f00-75fb-11eb-92e7-62c95b8f3caf.png)

### Tests
The solution comes with some tests in order to ensure consistency of the process.

### Remarks
Follow the tutorial on https://alexbierhaus.medium.com/twilio-api-whatsapp-net-blazor-example-f7d226da5367 to learn more about using the Twilio API in .NET and how to setup your Twilio WhatsApp account