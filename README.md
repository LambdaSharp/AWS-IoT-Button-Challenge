# Build a Serverless .NET Core app for the AWS IoT Button

In this challenge, we're going to learn how to write a serverless .NET Core backend for an IoT device. We're going to use the [AWS IoT Button](https://www.amazon.com/dp/B01C7WE5WM/), which is a nifty little devices that is very simple to setup and send events to the [AWS IoT](https://aws.amazon.com/iot/) service. We're then going to define new AWS IoT rules to store events in [DynamoDB](https://aws.amazon.com/dynamodb/) and send them to a [AWS Lambda](https://aws.amazon.com/lambda/) function for additional processing.

## Prerequisites
1. AWS Tools
    1. Sign-up for an [AWS account](https://aws.amazon.com)
    2. Install [AWS CLI](https://aws.amazon.com/cli/)
2. .NET Core
    1. Install [.NET Core 1.0](https://www.microsoft.com/net/core)
    2. Install [Visual Studio Code](https://code.visualstudio.com/)
    3. Install [C# Extension for VS Code](https://code.visualstudio.com/Docs/languages/csharp)
3. AWS C# Lambda Tools
    1. Install [Nodejs](https://nodejs.org/en/)
    2. Install [Yeoman](http://yeoman.io/codelab/setup.html)
    3. Install AWS C# Lambda generator: `npm install -g yo generator-aws-lambda-dotnet`

## Level 1: Sending an email from the AWS IoT Button

To begin, let's register the AWS IoT Button and confirm it can communicate with the AWS IoT backend. The followig instructions should get you there.

1. Get an [AWS IoT Button](https://www.amazon.com/dp/B01C7WE5WM/) (if not provided)
2. Install and launch [AWS IoT Button mobile app](https://aws.amazon.com/iotbutton/getting-started/) on your mobile device
3. Register the AWS IoT Button
4. Select `Send Email (nodejs)` as action

**ACCEPTANCE TEST:** Press the AWS IoT Button and confirm that you receive an email!

**NOTE:** if you get the red blinking LED of doom, see the [AWS IoT Button FAQ](https://aws.amazon.com/iotbutton/faq/) to determine what it means.


## Level 2: Store AWS IoT Button events in DynamoDB

For the second part, we want to enhance the [AWS IoT](https://aws.amazon.com/iot/) rule that was created in the previous step so that each received event is stored in a [DynamoDB](https://aws.amazon.com/dynamodb/) table. Make sure to split the message into multiple columns when it is stored. Also, beware that DynamoDB records are uniquely identified by their primary and sort key. Check the [AWS IoT SQL](http://docs.aws.amazon.com/iot/latest/developerguide/iot-sql-reference.html) syntax for some ideas.

**ACCEPTANCE TEST:** The AWS IoT button can send three different kind of events: single click, double click, and long click (3 seconds). Each event should be recorded as a new row, giving you a history of all clicks on your AWS IoT Button.


## Level 3: Invoke C# AWS Lambda function

For the third part, we want to invoke a C# [AWS Lambda](https://aws.amazon.com/lambda/) function when a long press occurs. When this function is invoked, it should query the [DynamoDB](https://docs.aws.amazon.com/sdk-for-net/v3/developer-guide/dynamodb-intro.html) table for all records belonging to the button, tally the results and print them on the console (console output is visible in the [CloudWatch Logs](http://docs.aws.amazon.com/AmazonCloudWatch/latest/logs/WhatIsCloudWatchLogs.html)).

There is a skeleton C# function in `src/AWSIoTButton` that is ready to be deployed. Assuming you have already stored your [default AWS credentials](http://docs.aws.amazon.com/sdk-for-java/v1/developer-guide/credentials.html) under `.aws` in your user folder, all you need to do is run:
```
dotnet lambda deploy-function -fn my-iot-button-function
```

**ACCEPTANCE TEST:** Do a sequence of single and double clicks on the AWS IoT Button. Confirm all events are recorded in DynamoDB then do a long click. Confirm in the CloudWatch Logs


## Boss Level: Show results in a Console app

For the final part, we want the results of the C# Lambda function to be displayed on a console screen. Details of the implementation are left as an exercise to the reader.

**ACCEPTANCE TEST:** Do a sequence of single and double clicks on the AWS IoT Button. Do a long click. Confirm the result is shown to everyone and that all the records are gone from the DynamoDB table.
