namespace GOWI.AIArticleGenerator.ServiceLayer.Helper_classes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.Json;
    using System.Text.Json.Serialization;
    using System.Threading.Tasks;
    using GOWI.AIArticleGenerator.DomainLayer.DTOs;
    using GOWI.AIArticleGenerator.ServiceLayer.Interfaces;

    public class Converter : IConverter
    {
        private static Converter _converterField;

        public static Converter ConverterInstance
        {
            get
            {
                if (_converterField == null)
                {
                    _converterField = new Converter();
                }

                return _converterField;
            }
        }

        public async Task<string> SerializeToJSON(List<DTOTransaction> data)
        {
            return await Task.FromResult(JsonSerializer.Serialize(data));
        }

        public async Task<APIResponse> DeserializeJSON(string json)
        {
            return await Task.FromResult(JsonSerializer.Deserialize<APIResponse>(json));
        }
    }
}
