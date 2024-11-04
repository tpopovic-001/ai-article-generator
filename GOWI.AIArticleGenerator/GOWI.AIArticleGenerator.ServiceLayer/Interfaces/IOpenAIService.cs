﻿namespace GOWI.AIArticleGenerator.ServiceLayer.Interfaces
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using GOWI.AIArticleGenerator.DomainLayer.DTOs;

    public interface IOpenAIService
    {
        Task<string> GenerateArticles(string prompt, List<DTOTransaction> transactions);
    }
}