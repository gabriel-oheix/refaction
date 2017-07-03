using System.Collections.Generic;

namespace refactorObjects
{
    public class Products
    {

        #region Fields

        public List<Product> Items { get; protected set; }

        #endregion


        #region Constructors

        public Products()
        {
            Items = new List<Product>();
        }

        public Products(IEnumerable<Product> productList)
        {
            Items = new List<Product>(productList);
        }

        #endregion

    }
}
