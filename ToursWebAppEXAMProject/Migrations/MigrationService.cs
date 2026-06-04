using Microsoft.EntityFrameworkCore;
using ToursWebAppEXAMProject.DBContext;

namespace ToursWebAppEXAMProject.Migrations
{
    public class MigrationService
    {
        private readonly SqlServerDBContext _sql;
        private readonly TourFirmaDBContext _pg;

        public MigrationService(
            SqlServerDBContext sql,
            TourFirmaDBContext pg)
        {
            _sql = sql;
            _pg = pg;
        }

        public async Task MigrateAsync()
        {
            await CopyCountries();
            await CopyCities();
            await CopyProducts();
            await CopyBlogs();
            await CopyNews();
            await CopyAboutPages();
            await CopyPhotos();
        }

        private async Task CopyCountries()
        {
            var countries = await _sql.Countries.AsNoTracking().ToListAsync();
            await _pg.Countries.AddRangeAsync(countries);
            await _pg.SaveChangesAsync();
        }

        private async Task CopyCities()
        {
            var cities = await _sql.Cities.AsNoTracking().ToListAsync();
            await _pg.Cities.AddRangeAsync(cities);
            await _pg.SaveChangesAsync();
        }

        private async Task CopyProducts()
        {
            var products = await _sql.Products.AsNoTracking().ToListAsync();
            await _pg.Products.AddRangeAsync(products);
            await _pg.SaveChangesAsync();
        }

        private async Task CopyBlogs()
        {
            var blogs = await _sql.Blogs.AsNoTracking().ToListAsync();
            await _pg.Blogs.AddRangeAsync(blogs);
            await _pg.SaveChangesAsync();
        }

        private async Task CopyNews()
        {
            var news = await _sql.News.AsNoTracking().ToListAsync();
            await _pg.News.AddRangeAsync(news);
            await _pg.SaveChangesAsync();
        }

        private async Task CopyAboutPages()
        {
            var pages = await _sql.AboutPageVersions.AsNoTracking().ToListAsync();
            await _pg.AboutPageVersions.AddRangeAsync(pages);
            await _pg.SaveChangesAsync();
        }

        private async Task CopyPhotos()
        {
            var photos = await _sql.PhotoGalleryImages.AsNoTracking().ToListAsync();
            await _pg.PhotoGalleryImages.AddRangeAsync(photos);
            await _pg.SaveChangesAsync();
        }
    }
}
