using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using refactorObjects;

namespace refactorRepositories
{
    public class ProductOptionRepository : BaseRepository<ProductOption>
    {

        #region Public Methods

        public ProductOptions GetProductOptions(Guid productId)
        {
            var parameters = new List<SqlParameter>();

            var query = "select * from productoption where productid = @ProductId";
            AddGuidParameter(parameters, "@ProductId", productId);

            var optionList = RunQuery(query.ToString(), parameters.ToArray());
            return new ProductOptions(optionList);
        }


        public ProductOption GetOneProductOption(Guid Id)
        {
            var parameters = new List<SqlParameter>();

            var query = "select * from productoption where id = @Id";
            AddGuidParameter(parameters, "@Id", Id);

            var optionList = RunQuery(query.ToString(), parameters.ToArray());
            return optionList.Count > 0 ? optionList[0] : null;
        }


        public bool CreateProductOption(Guid productId, ProductOption option)
        {
            var parameters = new List<SqlParameter>();

            var query = "insert into productoption (id, productid, name, description) values (@Id, @ProductId, @Name, @Description)";
            AddGuidParameter(parameters, "@Id", option.Id);
            AddGuidParameter(parameters, "@ProductId", productId);
            AddStringParameter(parameters, "@Name", option.Name);
            AddStringParameter(parameters, "@Description", option.Description);

            var count = RunNonQuery(query, parameters.ToArray());
            return count > 0;
        }


        public bool UpdateProductOption(Guid Id, ProductOption option)
        {
            var parameters = new List<SqlParameter>();
            var assignments = new List<string>();

            if (option.Name == null && option.Description == null)
                return false;

            // Optional parameters
            if (option.Name != null)
            {
                assignments.Add(" name = @Name");
                AddStringParameter(parameters, "@Name", option.Name);
            }
            if (option.Description != null)
            {
                assignments.Add(" description = @Description");
                AddStringParameter(parameters, "@Description", option.Description);
            }

            // query
            // "update productoption set name = @Name, description = @Description where id = @Id"
            var query = "update productoption set "
                + String.Join(", ", assignments)
                + " where id = @Id";
            AddGuidParameter(parameters, "@Id", Id);

            var count = RunNonQuery(query, parameters.ToArray());
            return count > 0;
        }


        public bool DeleteProductOption(Guid Id)
        {
            var parameters = new List<SqlParameter>();

            var query = "delete from productoption where id = @Id";
            AddGuidParameter(parameters, "@Id", Id);

            var count = RunNonQuery(query, parameters.ToArray());
            return count > 0;
        }

        #endregion


        #region Private Methods

        protected override ProductOption ReadRow(SqlDataReader reader)
        {
            var id = GetGuidValue(reader, "Id");
            var product = GetGuidValue(reader, "ProductId");
            var name = GetStringValue(reader, "Name");
            var desc = GetStringValue(reader, "Description");
            return new ProductOption(id, product, name, desc);
        }

        #endregion

    }
}
