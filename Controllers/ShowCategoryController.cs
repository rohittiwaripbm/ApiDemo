
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

namespace ProductsInApi.Controllers
{
    public class ShowCategoryController : ApiController
    {

        public class Category
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
        public List<Category> Get()
        {
            List<Category> list = new List<Category>();
            using (var adapter = new DataAccessAdapter())
            {
                var qf = new QueryFactory();
                var q = qf.Category;
                var result = adapter.FetchQuery<CategoryEntity>(q);
                foreach (var item in result)
                {
                    CategoryEntity c = item as CategoryEntity;
                    Category category= new Category();
                    category.Id = c.Id;
                    category.Name = c.Category;

                    list.Add(category);
                }
            }
            return list;
        }
    }
}
