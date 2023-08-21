using MongoDB.Driver;

namespace PersonalFinances.Authentication.Infrastructure.MongoDb
{
    public class MongoDbContext
    {
        public static string DataBase { get; set; } = string.Empty;
        public static string ConnectionString { get; set; } = string.Empty;
        public static bool IsSSL { get; set; }
        public IMongoDatabase _database { get; }

        public MongoDbContext()
        {
            try
            {
                MongoClientSettings setting = MongoClientSettings.
                    FromUrl(new MongoUrl(ConnectionString));

                if (IsSSL)
                {
                    setting.SslSettings = new SslSettings
                    {
                        EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12
                    };
                }

                var mongoCliente = new MongoClient(setting);
                _database = mongoCliente.GetDatabase(DataBase);

            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possivel conectar", ex);
            }
        }
    }
}
