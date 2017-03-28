using System.Linq;
using System.Threading.Tasks;

public class ProductRepository
{
    private Product[] products = new[]
   {
        new Product { Id = 1, Name = "Shoes", Price = 19.99f },
        new Product { Id = 2, Name = "Shirt", Price = 15.99f },
        new Product { Id = 3, Name = "Hat", Price = 9.99f },
    };

    public async Task<Product[]> SearchProducts(string searchTerm)
    {
        // Wait 2 seconds to simulate web request
        await Task.Delay(3000);
        // Use Linq-to-objects to search, ignoring case
        searchTerm = searchTerm.ToLower();
        return products.Where(p =>
        p.Name.ToLower().Contains(searchTerm))
        .ToArray();
    }
}