using ApiMongoBosch.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ApiMongoBosch.Services
{
    public class BooksService
    {
        private readonly IMongoCollection<Book> booksCollection;

        public BooksService(IOptions<BookStoreDatabaseSettings> bookStoreDatabaseSettings)
        {
            var MongoClient = new MongoClient(bookStoreDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = MongoClient.GetDatabase(bookStoreDatabaseSettings.Value.Database);

            booksCollection = mongoDatabase.GetCollection<Book>(bookStoreDatabaseSettings.Value.BookCollectionName);
        }

        public async Task<List<Book>> GetAsync() => 
            await booksCollection.Find(_ => true).ToListAsync();

        public async Task<Book> GetAsync(string id) => 
            await booksCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Book newBook) =>
            await booksCollection.InsertOneAsync(newBook);

        public async Task UpdateAsync(string id, Book updateBook) =>
            await booksCollection.ReplaceOneAsync(x => x.Id == id, updateBook);

        public async Task RemoveAsync(string id) =>
            await booksCollection.DeleteOneAsync(x => x.Id == id);
    }
}