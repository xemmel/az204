- Create new Resource group (sdulogdemo-init)

- Create log ana workspace

- Create app insight (point at log ana work)

- Create function app
    - .NET
    - 6 (In-process)

  Monitoring
    choose your app insight

  Create


Function App Overview

   Create function
   (Http) -> Calculator

            string numberString = req.Query["number"];

            log.LogInformation($"The number entered is {numberString}");
            int number = int.Parse(numberString);
            string responseMessage = $"The number {number} squared is {number*number}";
            return new OkObjectResult(responseMessage);


Postman check both