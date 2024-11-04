using Microsoft.EntityFrameworkCore;

namespace ProductApi.Data
{
    public static class DataExtension
    {
        public static async Task MigrateDbAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ProductContext>();
            await dbContext.Database.MigrateAsync();
        }
    }
}
