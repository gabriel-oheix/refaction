using System;
using System.Collections.Generic;

using refactorObjects;
using refactorRepositories;

namespace refactorServices
{
    public class ProductOptionService : BaseService
    {

        #region Fields

        protected ProductOptionRepository Por { get; set; }

        #endregion


        #region Constructors

        public ProductOptionService()
        {
            Por = new ProductOptionRepository();
        }

        #endregion


        #region Public Methods

        /// <summary>
        /// Validate the Product Option is correctly built
        /// </summary>
        public bool ValidateProductOption(ProductOption option)
        {
            if (option == null)
                return false;
            if (String.IsNullOrWhiteSpace(option.Name))
                return false;
            return true;
        }


        /// <summary>
        /// Validate partial Product Option info
        /// </summary>
        public bool ValidatePartialProductOption(ProductOption option)
        {
            if (option == null)
                return false;
            if (option.Name != null)
            {
                if (String.IsNullOrWhiteSpace(option.Name))
                    return false;
            }
            return true;
        }


        /// <summary>
        /// Fetch all Options for this Product 
        /// </summary>
        public ProductOptions GetProductOptions(Product product)
        {
            return Por.GetProductOptions(product.Id);
        }



        /// <summary>
        /// Fetch the Product Option with this Id
        /// </summary>
        public ProductOption GetOneProductOption(Product product, Guid Id)
        {
            return Por.GetOneProductOption(Id);
        }


        /// <summary>
        /// Register/Save the new Product Option
        /// </summary>
        public void CreateNewProductOption(Product product, ProductOption option)
        {
            option.Id = Guid.NewGuid();
            option.ProductId = product.Id;
            Por.CreateProductOption(product.Id, option);
        }


        /// <summary>
        /// Update the Product Option with new info
        /// </summary>
        public void UpdateProductOption(Product product, ProductOption oldOption, ProductOption newOption)
        {
            Por.UpdateProductOption(oldOption.Id, newOption);
        }


        /// <summary>
        /// Delete a Product Option
        /// </summary>
        public void DeleteProductOption(Product product, ProductOption option)
        {
            Por.DeleteProductOption(option.Id);
        }

        #endregion

    }
}
