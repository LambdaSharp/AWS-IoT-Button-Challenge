# AWS IoT Button with .NET Core

## Prerequisites
1. Sign-up for an [AWS account](https://aws.amazon.com)
2. Install [.NET Core 1.0](https://www.microsoft.com/net/core)
3. Install [Nodejs](https://nodejs.org/en/)
4. Install [Yeoman](http://yeoman.io/codelab/setup.html)
5. Install AWS C# Lambda generator: `npm install -g yo generator-aws-lambda-dotnet`
6. Install [AWS CLI](https://aws.amazon.com/cli/)
7. Install [Visual Studio Code](https://code.visualstudio.com/)

## Getting Started with AWS IoT Button

1. Get an [AWS IoT Button](https://www.amazon.com/dp/B01C7WE5WM/) (if not provided)
2. Install and launch [AWS IoT Button mobile app]((https://aws.amazon.com/iotbutton/getting-started/)) on your mobile device
3. Sign-in with your AWS credentials
4. Click `Setup AWS IoT Button`
5. Click `Agree & Get Started`
6. Scan AWS IoT Button barcode on the box with mobile app
7. Customize the name of the AWS IoT Button to make it easy to identify
8. Click `Register Button`
9. Hold AWS IoT Button for 6 seconds to enable its Wi-Fi hotspot
10. Make note of the AWS IoT Button name (used to connect later)
11. Click `Copy password to clipboard`
12. Go to `Wi-Fi Settings` on mobile device
13. Connect to AWS IoT Button Wi-Fi network
14. Go back to AWS IoT Button mobile app
15. Pick local Wi-Fi network for AWS IoT Button
16. Provide password for local Wi-Fi network
17. Click `Confirm`
18. Select `Send Email (nodejs)` under "Choose what to do when your button is pressed"
19. Provide your email address
20. Click `Set Action`
21. Press the AWS IoT Button and confirm that you receive an email!

## blah

1. Open the [AWS Web Console](https://console.aws.amazon.com)
2. Expand `All Services` tab
3. Click on the `AWS IoT` link
4. Click on the `Rules` link in the left navigation
5. Click on the `iobutton_...` rule (if you don't see it, you are not connected to the correct region!)
6. Click `Add Action`
7. Click `Split message into multiple columns of a database table`
8. Click `Configure Action`
9. Click `Create a new resource` (a new browser tab will open)
7. Click `Create Table`
8. Enter a custom table name
9. Enter `Id` as primary key name
10. Click `Create`
11. Close the DynamoDB browser tab
12. Click the refresh icon
13. Select the newly created dynamodb table
14. Click `Create a new role`
15. Enter a custom role name
16. Click `Create a new role`
17. Select newly created role
18. Click `Update role`
19. Click `Add action`





3. Create an AWS SQS queue for this challenge
4. Configure Scorekeeper "device" application's scorekeper.js with the following values
    - The paths to the private signing key and certificate generated for this device
    - The AWS Account ID (You can get this from any ARN)
    - The AWS region you are using for this challenge
    - An AWS access key and secret that can read from your AWS SQS queue
    - The AWS SQS queue url
    - The AWS Iot Button device serial number (DSN). The DSN will be the MQTT topic that is published and subscribed to for button click messages.
6. Create a DynamoDB table for storing the Scorekeeper "point" message
    - The Scorekeeper "point" message contains properties for id, team, and timestamp
    - The DynamoDB trigger lambda function assumes that "team" is an index
7. Create an IAM role for a DynanmoDB trigger Lambda function
    - Example policies are available in lambda/polices. Look for any places that need values to be replaced such as Lambda function names and DynamoDB tables.
8. Install and configure the DynamoDB trigger Lambda function (lambda/dynamodb-trigger.js) with the following values
    - The AWS SQS queue url
    - The AWS region you are using for this challenge
    - The DynamoDB table name
9. Run the Scorekeper "device" application
```sh
$ node scorekeeper.js
```
# Challenge

Using the boilerplate code provided and setup described above, configure the AWS IoT rules engine to store the Scorekeeper's results in DynamoDB. Ensure that when the AWS IoT button is clicked, the Scorekeeper "device" application announces the point that was scored, correctly, and outputs the current results of the game (the data persisted in DynamoDB).

Now that you can see how AWS IoT interacts with devices over MQTT and how its messages are forwarded to the rest of the AWS infrastructure, its time to move forward in two distinct challenges. You can pick one, or both.

1. Collect stats such as CPU temperature, memory pressure, and other properties of the environment running the Scorekeeper "device" application and the battery voltage of the AWS IoT button, and send them as topics via MQTT. Create rules to forward messages to Cloudwatch for alerts, measurements, and graphing.

2. Upgrade the Scorekeper "device" application from a simple MQTT pubsub client, to a AWS IoT "Thing", with a device shadow. Create a second "device" application using an [AWS IoT SDK](https://aws.amazon.com/iot/sdk/) (or use the AWS IoT button), that can turn the Scorekeeper "device" on and off, by changing state via the device shadow. When the Scorekeeper "device" is turned off, it should output that it is disabled and no points should be sent to DynamoDB and no scorekeeping results should be announced.

# Additional Resources

* Setup Node Client:
    - http://docs.aws.amazon.com/iot/latest/developerguide/iot-device-sdk-node.html

* AWS IoT NodeJS SDK
    - https://github.com/aws/aws-iot-device-sdk-js
    - (Working with the Thing Shadow) https://github.com/aws/aws-iot-device-sdk-js#thing-shadow-class

* AWS IoT DynamoDB Rule:
    - http://docs.aws.amazon.com/iot/latest/developerguide/iot-ddb-rule.html

* AWS IoT Lambda Rule:
    - http://docs.aws.amazon.com/iot/latest/developerguide/iot-lambda-rule.html
