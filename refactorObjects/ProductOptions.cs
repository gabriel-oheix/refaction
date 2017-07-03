using System;
using System.Collections.Generic;

namespace refactorObjects
{
    public class ProductOptions
    {

        #region Fields

        public List<ProductOption> Items { get; protected set; }

        #endregion


        #region Constructors

        public ProductOptions()
        {
            Items = new List<ProductOption>();
        }

        public ProductOptions(IEnumerable<ProductOption> productOptionList)
        {
            Items = new List<ProductOption>(productOptionList);
        }

        #endregion

    }
}
