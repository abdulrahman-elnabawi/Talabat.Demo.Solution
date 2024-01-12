using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications.Products
{
    public class ProductWithTypeAndBrandSpecifications : BaseSpecification<Product>
    {

        public ProductWithTypeAndBrandSpecifications(ProductSpecParams productParams)
            :base(P =>
                (string.IsNullOrEmpty(productParams.Search) || P.Name.ToLower().Contains(productParams.Search)) &&
                 (!productParams.BrandId.HasValue || P.ProductBrandId == productParams.BrandId.Value) &&
                 (!productParams.TypeId.HasValue  || P.ProductTypeId == productParams.TypeId.Value)
                 )
        {
            AddInclude(P => P.ProductBrand);
            AddInclude(P => P.ProductType);
            AddOrderBy(P => P.Name);

            // PageIndex = 2
            // PageSize = 5

            ApplyPagination(productParams.PageSize * (productParams.PageIndex - 1), productParams.PageSize);

            if(!string.IsNullOrEmpty(productParams.Sort))
            {
                switch (productParams.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(P => P.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDescending(P => P.Price);
                        break;
                    default:
                        AddOrderBy(P => P.Name);
                        break;
                }
            }
        }

        public ProductWithTypeAndBrandSpecifications(int id) : base(P => P.Id == id)
        {
            AddInclude(P => P.ProductBrand);
            AddInclude(P => P.ProductType);
        }
    }
}
