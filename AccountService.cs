using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDBWebAPI2.Interfaces;
using MongoDBWebAPI2.Models;

namespace MongoDBWebAPI2
{
    public class AccountService : IAccountService
    {
        private readonly IMongoCollection<Account> _accounts;
        public AccountService(IOptions<MongoDBConnection> settings, IMongoClient mongoClient)
        {
            var db = mongoClient.GetDatabase(settings.Value.DataBase);
            _accounts = db.GetCollection<Account>(settings.Value.CollectionName);
        }

        public Account CreateAccount(Account account)
        {
            _accounts.InsertOne(account);
            return account;
        }

        public void DeleteAccount(string id)
        {
            _accounts.DeleteOne(x => x.Id == id);
        }

        public Account GetAccountById(string id)
        {
            return _accounts.Find(x=> x.Id == id).FirstOrDefault();
        }

        public List<Account> GetAllAccounts()
        {
            return _accounts.Find(x=>true).ToList();
        }

        public void UpdateAccount(string id, Account account)
        {
            _accounts.ReplaceOne(x => x.Id == id, account);
        }
    }
}
