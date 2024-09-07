namespace BlazorApp.Helpers
{
    public static class DataLoadingHelper
    {
        public static async Task<IQueryable<T>> LoadDataAsync<T>(Func<Task<IEnumerable<T>>> apiMethod)
        {
            try
			{
				var result = await apiMethod();
				return result.AsQueryable();
			}	
			catch (Exception ex)
			{

				Console.WriteLine($"Error loading data: {ex.Message}");
				return Enumerable.Empty<T>().AsQueryable();
			}
        }
    }
}
