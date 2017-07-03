using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using refactorObjects;
using refactorServices;

namespace refactor_me.Controllers
{
    [RoutePrefix("products/{productId}/options")]
    public class ProductOptionController : BaseController
    {
        [Route]
        [HttpGet]
        public ProductOptions GetProductOptions(Guid productId)
        {
            ProductService ps = new ProductService();
            var product = ps.GetOneProduct(productId);
            if (product == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            ProductOptionService pos = new ProductOptionService();
            return pos.GetProductOptions(product);
        }


        [Route]
        [HttpPost]
        public HttpResponseMessage CreateProductOption(Guid productId, ProductOption option)
        {
            ProductService ps = new ProductService();
            var product = ps.GetOneProduct(productId);
            if (product == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            ProductOptionService pos = new ProductOptionService();
            if (!pos.ValidateProductOption(option))
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            pos.CreateNewProductOption(product, option);

            // 201 Created with Location header
            var response = Request.CreateResponse(HttpStatusCode.Created);
            response.Headers.Add("Location", Request.RequestUri.AbsoluteUri + "/" + option.Id);
            return response;
        }


        [Route("{id}")]
        [HttpGet]
        public ProductOption GetOneProductOption(Guid productId, Guid id)
        {
            ProductService ps = new ProductService();
            var product = ps.GetOneProduct(productId);
            if (product == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            ProductOptionService pos = new ProductOptionService();
            var option = pos.GetOneProductOption(product, id);
            if (option == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            return option;
        }


        [Route("{id}")]
        [HttpPut]
        public void UpdateProductOption(Guid productId, Guid id, ProductOption option)
        {
            ProductService ps = new ProductService();
            var product = ps.GetOneProduct(productId);
            if (product == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            ProductOptionService pos = new ProductOptionService();
            if (!pos.ValidatePartialProductOption(option))
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var fetched = pos.GetOneProductOption(product, id);
            if (fetched == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            pos.UpdateProductOption(product, fetched, option);
        }


        [Route("{id}")]
        [HttpDelete]
        public void DeleteProductOption(Guid productId, Guid id)
        {
            ProductService ps = new ProductService();
            var product = ps.GetOneProduct(productId);
            if (product == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            ProductOptionService pos = new ProductOptionService();
            var option = pos.GetOneProductOption(product, id);
            if (option == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            pos.DeleteProductOption(product, option);
        }

    }
}
