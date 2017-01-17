using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Amazon.Lambda.Core;

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

        //--- Constructors ---
        public Function() { }

        //--- Methods ---
        public string Handler(AWSIoTButtonEvent iot, ILambdaContext context) {
            return "Click received!";
        }
    }
}
