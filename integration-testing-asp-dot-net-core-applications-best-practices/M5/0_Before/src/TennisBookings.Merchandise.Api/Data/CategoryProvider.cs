using System.Collections.Generic;

namespace TennisBookings.Merchandise.Api.Data
{
    public class CategoryProvider : ICategoryProvider
    {
        public IReadOnlyCollection<string> AllowedCategories()
        {
            var allowedCategories = new string[]
            {                
                "Bags",
                "Balls",
                "Accessories",
                "Clothing",
                "Rackets"
            };

            return allowedCategories;
        }
    }
}
