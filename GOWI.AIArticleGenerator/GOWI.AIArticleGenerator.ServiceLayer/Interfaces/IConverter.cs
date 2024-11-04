namespace GOWI.AIArticleGenerator.ServiceLayer.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using GOWI.AIArticleGenerator.DomainLayer.DTOs;
    using GOWI.AIArticleGenerator.ServiceLayer.Helper_classes;

    public interface IConverter
    {
       Task<string> SerializeToJSON(List<DTOTransaction> data);

       Task<APIResponse> DeserializeJSON(string json);
    }
}
