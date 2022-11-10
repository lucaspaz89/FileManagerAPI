namespace FileManagerAPI.Helpers
{
    public class SQLString
    {
        private readonly string cadenaSQL = string.Empty;

        public SQLString()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();

            cadenaSQL = builder.GetSection("ConnectionStrings:CadenaSQL").Value;
        }

        public string GetCadenaSQL()
        {
            return cadenaSQL;
        }
    }
}
