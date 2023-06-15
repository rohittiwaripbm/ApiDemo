using Microsoft.Ajax.Utilities;
using ProductsInLLBL.DatabaseSpecific;
using ProductsInLLBL.EntityClasses;
using ProductsInLLBL.FactoryClasses;
using SD.LLBLGen.Pro.QuerySpec.Adapter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static ProductsInApi.Controllers.AddProductsController;
using static ProductsInApi.Controllers.ShowCategoryController;

namespace ProductsInApi.Controllers
{
    public class AddProductsController : ApiController
    {

        public class Specification
        {
            public int ProductId { get; set; }
            public string Type { get; set; }
            public string Value { get; set; }
        }

        public class Product
        {

            public List<Specification> Specificationlist { get; set; }
            public string Name { get; set; }
            public int CategoryId { get; set; }

            public bool IsActive { get; set; }
            public DateTime? MfgDate { get; set; }
            //public string Specifications { get; set; }
        }
        public string Post(Product product)
        {
           
            var p = new ProductEntity();
            p.Name = product.Name;
            p.CategoryId= product.CategoryId;
            p.IsActive= product.IsActive;
            p.MfgDate = product.MfgDate;
            var adapter = new DataAccessAdapter();
            adapter.SaveEntity(p,true);
            foreach (var item in product.Specificationlist)
            {
                var r = new SpecificationEntity();
                r.ProductId = p.Id;
                r.Type = item.Type;
                r.Value= item.Value;
                var adapter2 = new DataAccessAdapter();
                adapter2.SaveEntity(r);
            }

            return "Added Successfully";
        }

    }
}
