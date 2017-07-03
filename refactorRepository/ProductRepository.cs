using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using refactorObjects;

namespace refactorRepositories
{
    public class ProductRepository : BaseRepository<Product>
    {

        #region Public Methods

        public Products GetProducts(string name = null)
        {
            var parameters = new List<SqlParameter>();

            var query = new StringBuilder("select * from product where 1 = 1");
            if(name != null)
            {
                query.Append(" and lower(name) like @Name");
                AddStringParameter(parameters, "@Name", "%" + name + "%");
            }

            var productList = RunQuery(query.ToString(), parameters.ToArray());
            return new Products(productList);
        }


        public Product GetOneProduct(Guid Id)
        {
            var parameters = new List<SqlParameter>();

            var query = "select * from product where id = @Id";
            AddGuidParameter(parameters, "@Id", Id);

            var productList = RunQuery(query.ToString(), parameters.ToArray());
            return productList.Count > 0 ? productList[0] : null;
        }


        public bool CreateProduct(Product product)
        {
            var parameters = new List<SqlParameter>();

            var query = "insert into product (id, name, description, price, deliveryprice) values (@Id, @Name, @Description, @Price, @DeliveryPrice)";
            AddGuidParameter(parameters, "@Id", product.Id);
            AddStringParameter(parameters, "@Name", product.Name);
            AddStringParameter(parameters, "@Description", product.Description);
            AddDecimalParameter(parameters, "@Price", product.Price);
            AddDecimalParameter(parameters, "@DeliveryPrice", product.DeliveryPrice);

            var count = RunNonQuery(query, parameters.ToArray());
            return count > 0;
        }


        public bool UpdateProduct(Guid Id, Product product)
        {
            var parameters = new List<SqlParameter>();
            var assignments = new List<string>(); 

            if (product.Name == null && product.Description == null && product.Price == 0 && product.DeliveryPrice == 0)
                return false;

            // Optional parameters
            if (product.Name != null)
            {
                assignments.Add(" name = @Name");
                AddStringParameter(parameters, "@Name", product.Name);
            }
            if (product.Description != null)
            {
                assignments.Add(" description = @Description");
                AddStringParameter(parameters, "@Description", product.Description);
            }
            if (product.Price != 0)
            {
                assignments.Add(" price = @Price");
                AddDecimalParameter(parameters, "@Price", product.Price);
            }
            if (product.DeliveryPrice != 0)
            {
                assignments.Add(" deliveryprice = @DeliveryPrice");
                AddDecimalParameter(parameters, "@DeliveryPrice", product.DeliveryPrice);
            }

            // query
            // "update product set name = @Name, description = @Description, price = @Price, deliveryprice = @DeliveryPrice where id = @Id"
            var query = "update product set "
                + String.Join(", ", assignments)
                + " where id = @Id";
            AddGuidParameter(parameters, "@Id", Id);

            var count = RunNonQuery(query, parameters.ToArray());
            return count > 0;
        }


        public bool DeleteProduct(Guid Id)
        {
            var parameters = new List<SqlParameter>();

            var query = "delete from product where id = @Id";
            AddGuidParameter(parameters, "@Id", Id);

            var count = RunNonQuery(query, parameters.ToArray());
            return count > 0;
        }

        #endregion


        #region Private Methods

        protected override Product ReadRow(SqlDataReader reader)
        {
            /*return new
            {
                Id = reader["Id"],
                Name = reader["Name"],
                Description = reader["Description"],
                Price = reader["Price"],
                DeliveryPrice = reader["DeliveryPrice"]
            };*/

            /*var product = new Product();
            product.Id = Guid.Parse(reader["Id"].ToString());
            product.Name = reader["Name"].ToString();
            product.Description = (DBNull.Value == reader["Description"]) ? null : reader["Description"].ToString();
            product.Price = decimal.Parse(reader["Price"].ToString());
            product.DeliveryPrice = decimal.Parse(reader["DeliveryPrice"].ToString());
            return product;*/

            var id = GetGuidValue(reader, "Id");
            var name = GetStringValue(reader, "Name");
            var desc = GetStringValue(reader, "Description");
            var price = GetDecimalValue(reader, "Price");
            var delivery = GetDecimalValue(reader, "DeliveryPrice");
            return new Product(id, name, desc, price, delivery);
        }

        #endregion

    }
}
