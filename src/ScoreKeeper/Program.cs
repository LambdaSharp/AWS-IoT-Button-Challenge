using System;
using System.Linq;
using System.Threading.Tasks;

using Amazon;
using Amazon.SQS;
using Amazon.SQS.Model;

namespace ConsoleApplication {

    public class Program {

        //--- Class Methods ---
        public static void Main(string[] args) => ReadAndPrintMessages(args).Wait();

        private static async Task ReadAndPrintMessages(string[] args) {
            var queueClient = new AmazonSQSClient(RegionEndpoint.USWest2);

            // resolve url for SQS queue
            var queueName = args.FirstOrDefault() ?? "lambdasharp-iot-challenge";
            Console.WriteLine($"Resolving: {queueName}");
            var queueUrl = (await queueClient.GetQueueUrlAsync(queueName)).QueueUrl;

            // fetch new messages until console app is aborted forcefully
            Console.WriteLine("Waiting for messages:");
            Console.WriteLine();
            while(true) {

                // fetch next batch of messages
                var response = await queueClient.ReceiveMessageAsync(new ReceiveMessageRequest {
                    QueueUrl = queueUrl,
                    MaxNumberOfMessages = 10,
                    WaitTimeSeconds = 20
                });
                if(!response.Messages.Any()) {

                    // nothing was received this time, continue waiting for messages
                    continue;
                }

                // print each received message
                foreach(var message in response.Messages) {
                    Console.WriteLine($"{DateTime.Now.ToString()}:");
                    Console.WriteLine();
                    Console.WriteLine(message.Body);
                    Console.WriteLine("---------------");
                }

                // delete all printed SQSW messages
                await queueClient.DeleteMessageBatchAsync(new DeleteMessageBatchRequest {
                    QueueUrl = queueUrl,
                    Entries = Enumerable.Range(1, response.Messages.Count)
                        .Zip(response.Messages, (id, message) => new DeleteMessageBatchRequestEntry {
                            Id = id.ToString(),
                            ReceiptHandle = message.ReceiptHandle
                        }).ToList()
                });
            }
        }
    }
}
