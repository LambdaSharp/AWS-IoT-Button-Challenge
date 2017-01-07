using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Amazon.Lambda.Core;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.SQS;

using Newtonsoft.Json;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializerAttribute(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
namespace AWSIoTButton {
    public class AWSIoTButtonEvent {

        //--- Fields ---
        [JsonProperty("serialNumber")]
        public string SerialNumber;

        [JsonProperty("clickType")]
        public string ClickType;

        [JsonProperty("batteryVoltage")]
        public string BatteryVoltage;
    }

    public class Function {

        //--- Fields ---
        private string _tableName;
        private AmazonDynamoDBClient _table;
        private AmazonSQSClient _queue;
        private string _queueUrl;

        //--- Constructors ---
        public Function() {
            _tableName = System.Environment.GetEnvironmentVariable("dynamodb");
            _table = new AmazonDynamoDBClient();
            _queueUrl = System.Environment.GetEnvironmentVariable("queueurl");
            _queue = new AmazonSQSClient();
        }

        //--- Methods ---
        public async Task<string> Handler(AWSIoTButtonEvent iot, ILambdaContext context) {
            var message = "hello!";
            var response = await _table.QueryAsync(new QueryRequest {
                TableName = _tableName,
                KeyConditionExpression = "id = :v_id",
                ExpressionAttributeValues = new Dictionary<string, AttributeValue> {
                    { ":v_id", new AttributeValue { S =  iot.SerialNumber }}
                }
            });
            if(response.HttpStatusCode == System.Net.HttpStatusCode.OK) {
                Console.WriteLine($"found {response.Items.Count} records");
                var clickTypes = response.Items
                    .Select(item => item["clickType"])
                    .ToLookup(value => value.S);
                var singleClicks = clickTypes["SINGLE"].Count();
                var doubleClicks = clickTypes["DOUBLE"].Count();

                message = $"single={singleClicks} double={doubleClicks}";
                await _queue.SendMessageAsync(_queueUrl, message);
            } else {
                message = $"error: {response.HttpStatusCode}";
            }
            Console.WriteLine(message);
            return message;
        }
    }
}
