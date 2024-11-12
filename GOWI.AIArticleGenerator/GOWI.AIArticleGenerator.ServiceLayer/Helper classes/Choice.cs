namespace GOWI.AIArticleGenerator.ServiceLayer.Helper_classes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.Json.Serialization;
    using System.Threading.Tasks;

    public class Choice
    {
        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonPropertyName("index")]
        public int Index { get; set; }

        [JsonPropertyName("logprobs")]
        public object Logprobs { get; set; }

        [JsonPropertyName("finish_reason")]
        public string FinishReason { get; set; }

        public int TransactionId { get; set; }
    }
}
