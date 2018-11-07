using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace MessaCord.API
{
    class NetworkFrame
    {
        public NetworkFrame(int operationCode, object data = null, string type = null, int? sequence =null)
        {
            OperationCode = operationCode;
            Type = type;
            Sequence = sequence;
            Data = data;
        }

        public NetworkFrame()
        {

        }

        [JsonProperty("op")] public int OperationCode { get; set; }

        [JsonProperty("t", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }

        [JsonProperty("s", NullValueHandling = NullValueHandling.Ignore)]
        public int? Sequence { get; set; }

        [JsonProperty("d")] public object Data { get; set; }

        public override string ToString()
        {
            return
                $"{nameof(OperationCode)}: {OperationCode}, {nameof(Type)}: {Type}, {nameof(Sequence)}: {Sequence}, {nameof(Data)}: {Data}";
        }
    }
}
