using Event.Data;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;

namespace Event.Utility
{
    public class QueryHandler
    {
   
        private static QueryHandler _instance;
        private readonly IConfiguration _configuration;

        private static object lockObject = new object();
        private QueryHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public static QueryHandler Instance
        {

            get
            {
                lock (lockObject)
                {

                    if (_instance == null)
                    {
                        IConfiguration configuration = new ConfigurationBuilder()
                           .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                           .Build();

                        _instance = new QueryHandler(configuration);
                    }
                    return _instance;

                }
            }
            
        }
       

        public int Register(string name, string password, string email,
            string businessName, string phoneNumber, string modeOfBusiness, string category)
        {
            string query = @"INSERT INTO Registration (Name, Password, Email, BusinessName, PhoneNumber, ModeOfBusiness, Category)
                             VALUES (@Name, @Password, @Email, @BusinessName, @PhoneNumber, @ModeOfBusiness, @Category)";

            MySqlCommand command = new MySqlCommand(query);
            command.Parameters.AddWithValue("@Name", name);
            command.Parameters.AddWithValue("@Password", password);
            command.Parameters.AddWithValue("@Email", email);
            command.Parameters.AddWithValue("@BusinessName", businessName);
            command.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
            command.Parameters.AddWithValue("@ModeOfBusiness", modeOfBusiness);
            command.Parameters.AddWithValue("@Category", category);

            int rowsAffected = 0;

            //System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStrings"].ConnectionString

            string connectionString = _configuration.GetConnectionString("Default");
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                command.Connection = connection;
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();

            }
           
                return rowsAffected;    
        }
        public bool Login(string email, string password)
        {

            string query = @"SELECT COUNT(*) FROM UserRegistration WHERE email= @Email AND password = @Password";
            
            MySqlCommand command = new MySqlCommand(query);
            command.Parameters.AddWithValue("@Password", password);
            command.Parameters.AddWithValue("@Email", email);

            int count = 0;

            string connectionString = _configuration.GetConnectionString("Default");
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                command.Connection = connection;
                connection.Open();

                // ExecuteScalar returns the first column of the first row in the result set
                object result = command.ExecuteScalar();

                // Convert the result to an integer
                if (result != null)
                    count = Convert.ToInt32(result);

            }
            return count > 0;

        }
        public bool Reset(string email,string password) {

            string query = @"UPDATE UserRegistration SET Password = @Password WHERE Email = @Email";
            MySqlCommand command = new MySqlCommand(query);
            command.Parameters.AddWithValue("@Password", password);
            command.Parameters.AddWithValue("@Email", email);
            int rowsAffected = 0;

            string connectionString = _configuration.GetConnectionString("Default");
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                command.Connection = connection;
                connection.Open();

                rowsAffected = command.ExecuteNonQuery();

            }
            return rowsAffected>0;

        }
        public bool Forgot(string email)
        {

            string query = @"SELECT COUNT(*) FROM UserRegistration WHERE email = @Email";

            MySqlCommand command = new MySqlCommand(query);
           
            command.Parameters.AddWithValue("@Email", email);

            int count = 0;

            string connectionString = _configuration.GetConnectionString("Default");
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                command.Connection = connection;
                connection.Open();

                // ExecuteScalar returns the first column of the first row in the result set
                object result = command.ExecuteScalar();

                // Convert the result to an integer
                if (result != null)
                    count = Convert.ToInt32(result);

            }
            return count > 0;

        }
    }
    }

