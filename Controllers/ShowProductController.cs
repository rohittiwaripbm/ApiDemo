using ProductsInLLBL.DatabaseSpecific;
using ProductsInLLBL.EntityClasses;
using ProductsInLLBL.FactoryClasses;
using ProductsInLLBL.HelperClasses;
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

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? CategoryId { get; set; }

        public bool IsActive { get; set; }
        public DateTime? MfgDate { get; set; }
    }

    public class ShowProductController : ApiController
    {
        public bool Delete(int id)
        {
            try
            {
                ActionProcedures.DeleteProduct(id);

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public IEnumerable<GetGetProducts_Result> Get()
        {
            List<GetGetProducts_Result> list = new List<GetGetProducts_Result>();
            using (var adapter = new DataAccessAdapter())
            {


                list = adapter.FetchProjection<GetGetProducts_Result>(
                  RetrievalProcedures.GetGetProductsCallAsQuery());
              
            }

            List<Specification> specs = new List<Specification>();
            using (var adapter = new DataAccessAdapter())
            {
                var qf = new QueryFactory();
                var q = qf.Specification;
                var result = adapter.FetchQuery<SpecificationEntity>(q);
                foreach (var item in result)
                {
                    SpecificationEntity c = item as SpecificationEntity;
                    Specification spec = new Specification();
                    spec.ProductId = c.ProductId ?? 0;
                    spec.Value = c.Value;
                    spec.Type = c.Type;
                    specs.Add(spec);
                }
            }

            foreach (GetGetProducts_Result item in list)
            {
                item.Specificationlist = specs.Where(x=>x.ProductId == item.Id).ToList();
            }

            return list;
        }

    }

    public class GetGetProducts_Result
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        
        public bool IsActive { get; set; }
        public DateTime? MfgDate { get; set; }

        public List<Specification> Specificationlist { get; set; }
    }
}
