using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using refactorObjects;
using refactorRepositories;

namespace refactorServices
{
    public class ProductService : BaseService
    {

        #region Fields

        protected ProductRepository Pr { get; set; }

        #endregion


        #region Constructors

        public ProductService()
        {
            Pr = new ProductRepository();
        }

        #endregion


        #region Public Methods

        /// <summary>
        /// Validate the Product is correctly built
        /// </summary>
        public bool ValidateProduct(Product product)
        {
            if (product == null)
                return false;
            if (String.IsNullOrWhiteSpace(product.Name))
                return false;
            if (product.Price <= 0)
                return false;
            if (product.DeliveryPrice <= 0)
                return false;
            return true;
        }

        /// <summary>
        /// Validate partial Product info - allow partial update
        /// </summary>
        public bool ValidatePartialProduct(Product product)
        {
            if (product == null)
                return false;
            if (product.Name != null)
            {
                if (String.IsNullOrWhiteSpace(product.Name))
                    return false;
            }
            if (product.Price != 0)
            {
                if (product.Price < 0)
                    return false;
            }
            if (product.DeliveryPrice != 0)
            {
                if (product.DeliveryPrice < 0)
                    return false;
            }
            return true;
        }


        /// <summary>
        /// Fetch all Products matching the optional criteria
        /// </summary>
        public Products GetProducts(string name = null)
        {
            return Pr.GetProducts(name);
        }


        /// <summary>
        /// Fetch the Product with this Id
        /// </summary>
        public Product GetOneProduct(Guid Id)
        {
            return Pr.GetOneProduct(Id);
        }


        /// <summary>
        /// Register/Save the new Product
        /// </summary>
        public void CreateNewProduct(Product product)
        {
            product.Id = Guid.NewGuid();
            Pr.CreateProduct(product);
        }


        /// <summary>
        /// Update the Product with new info
        /// </summary>
        public void UpdateProduct(Product oldProduct, Product newProduct)
        {
            Pr.UpdateProduct(oldProduct.Id, newProduct);
        }


        /// <summary>
        /// Delete a Product and all its options
        /// </summary>
        public void DeleteProduct(Product product)
        {
            // delete all options first
            ProductOptionService pos = new ProductOptionService();
            var options = pos.GetProductOptions(product);
            foreach(var option in options.Items)
            {
                pos.DeleteProductOption(product, option);
            }

            Pr.DeleteProduct(product.Id);
        }

        #endregion

    }
}
