using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.Repository.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                if (!context.ProductBrands.Any())
                {

                    var brandsData = File.ReadAllText("../Talabat.Repository/Data/SeedData/brands.json");
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                    foreach (var brand in brands)
                        context.Set<ProductBrand>().Add(brand);

                    await context.SaveChangesAsync();
                }
                if (!context.ProductTypes.Any())
                {

                    var typesData = File.ReadAllText("../Talabat.Repository/Data/SeedData/types.json");
                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

                    foreach (var type in types)
                        context.Set<ProductType>().Add(type);

                    await context.SaveChangesAsync();
                }
                if (!context.Products.Any())
                {

                    var productsData = File.ReadAllText("../Talabat.Repository/Data/SeedData/products.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                    foreach (var product in products)
                        context.Set<Product>().Add(product);

                    await context.SaveChangesAsync();
                }

                if (!context.DeliveryMethods.Any())
                {

                    var deliveryMethodsData = File.ReadAllText("../Talabat.Repository/Data/SeedData/delivery.json");
                    var deliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryMethodsData);

                    foreach (var deliveryMethod in deliveryMethods)
                        context.Set<DeliveryMethod>().Add(deliveryMethod);

                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<StoreContextSeed>();
                logger.LogError(ex, ex.Message);
            }

        }
    }
}
