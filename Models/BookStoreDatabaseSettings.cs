
namespace ApiMongoBosch.Models
{
    public class BookStoreDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string Database { get; set; } = null!;
        public string BookCollectionName { get; set; } = null!;
    }

}
