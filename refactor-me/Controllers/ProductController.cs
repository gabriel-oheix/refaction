using System;
using System.Net;
using System.Web.Http;
using refactorObjects;
using refactorServices;
using System.Net.Http;


namespace refactor_me.Controllers
{
    [RoutePrefix("products")]
    public class ProductController : BaseController
    {

        [Route]
        [HttpGet]
        public Products GetProducts(string name = null)
        {
            ProductService ps = new ProductService();
            return ps.GetProducts(name);
        }


        [Route]
        [HttpPost]
        public HttpResponseMessage CreateProduct(Product product)
        {
            ProductService ps = new ProductService();
            if (!ps.ValidateProduct(product))
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            ps.CreateNewProduct(product);

            // 201 Created with Location header
            var response = Request.CreateResponse(HttpStatusCode.Created);
            response.Headers.Add("Location", Request.RequestUri.AbsoluteUri + "/" + product.Id);
            return response;
        }


        [Route("{id}")]
        [HttpGet]
        public Product GetOneProduct(Guid id)
        {
            ProductService ps = new ProductService();
            var product = ps.GetOneProduct(id);
            if (product == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            return product;
        }


        [Route("{id}")]
        [HttpPut]
        public void UpdateProduct(Guid id, Product product)
        {
            ProductService ps = new ProductService();
            if (!ps.ValidatePartialProduct(product))
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var fetched = ps.GetOneProduct(id);
            if (fetched == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            ps.UpdateProduct(fetched, product);
        }


        [Route("{id}")]
        [HttpDelete]
        public void DeleteProduct(Guid id)
        {
            ProductService ps = new ProductService();
            var product = ps.GetOneProduct(id);
            if (product == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            ps.DeleteProduct(product);
        }

    }
}
