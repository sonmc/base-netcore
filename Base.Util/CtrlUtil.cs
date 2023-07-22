
using Microsoft.EntityFrameworkCore;

namespace Base.Utils
{
    public class CtrlUtil
    {
        public static async Task<object> ApplySortAndPaging<T, TKey>(int cursor, int pageSize, List<T> items, string sortName, string ascending = "asc")
    where T : class
        {
            try
            {
                cursor = cursor * pageSize;
                //var query = schema.AsQueryable();
                //var items = await query.ToListAsync();  

                // Apply cursor-based filtering
                var filteredItems = items.Where(item => GetItemId(item) > cursor);

                // Apply sorting
                var sortedItems = ascending == "asc"
                    ? filteredItems.OrderBy(item => GetSortValue(item, sortName))
                    : filteredItems.OrderByDescending(item => GetSortValue(item, sortName));

                // Perform paging
                var pageItems = sortedItems.Take(pageSize).ToList();
                var hasNextPage = sortedItems.Skip(pageSize).Any();

                return new
                {
                    Items = pageItems,
                    HasNextPage = hasNextPage
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new();
            }
        }

        public static int GetItemId<T>(T item)
        {
            var itemIdProperty = typeof(T).GetProperty("Id");
            return (int)itemIdProperty.GetValue(item);
        }

        public static object GetSortValue<T>(T item, string sortName)
        {
            var property = typeof(T).GetProperty(sortName);
            return property.GetValue(item);
        }
    }
}

