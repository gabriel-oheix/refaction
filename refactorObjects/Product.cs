using System;

namespace refactorObjects
{
    public class Product
    {

        #region Fields

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public decimal DeliveryPrice { get; set; }

        #endregion


        #region Constructors

        public Product(Guid id, string name, string description, decimal price, decimal delivery)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            DeliveryPrice = delivery;
        }

        #endregion

    }

}
