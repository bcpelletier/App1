using System.Threading.Tasks;

public class ProductViewModel
{
    private readonly ProductRepository repository = new ProductRepository();

    public string SearchTerm
    {
        get;
        set;
    }
    public Product[] Products
    {
        get;
        private set;
    }
    public async Task<int> Search()
    {
        if (string.IsNullOrEmpty(SearchTerm))
        {
            Products = null;
            return 0;
        }
        else
        {
            Products = await repository.SearchProducts(SearchTerm);
            return 1;
        }
    }
}