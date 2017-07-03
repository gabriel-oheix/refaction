using System;
using Newtonsoft.Json;

namespace refactorObjects
{
    public class ProductOption
    {

        #region Fields

        public Guid Id { get; set; }

        [JsonIgnore]
        public Guid ProductId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        #endregion


        #region Constructors

        public ProductOption(Guid id, Guid productId, string name, string description)
        {
            Id = id;
            ProductId = productId;
            Name = name;
            Description = description;
        }

        #endregion

    }
}
