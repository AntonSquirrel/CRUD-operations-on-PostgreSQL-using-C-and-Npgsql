
using NemesisEnemyList;
using Npgsql;
using System.Data;

namespace DataBaseConnector
{
    public class DataBaseDriver
    {
        private static string _dropTable = "DROP TABLE IF EXISTS NemesisEnemy";
        private static string _createTable = "CREATE TABLE NemesisEnemy (id serial PRIMARY KEY, Name VARCHAR(200), Damage SMALLINT, Health SMALLINT)";
        private static string _insentTable = "INSERT INTO NemesisEnemy (id, name, damage, health) VALUES (@id, @name, @damage, @health)";
        private static string _selectTable = "SELECT * FROM NemesisEnemy WHERE ID = @id";
        private static string _updateTable = "UPDATE NemesisEnemy SET name = @name, damage = @damage, health = @health WHERE id = @id";
        private static string _deleteTable = "DELETE FROM NemesisEnemy WHERE id = @i";
        private static string _getVersion = "SELECT version()";

        string CONNECTION_STRING = "Host=localhost:5432;" + "Username=postgres;" + "Password=Mandarinka175;" + "Database=postgres";

            public NpgsqlConnection connection;
            public DataBaseDriver()
            {
                connection = new NpgsqlConnection(CONNECTION_STRING);
                connection.Open();
            }
        
        static void Main(string[] args)
        { 
            DataBaseDriver dataBaseDriver = new DataBaseDriver();
            var conn = dataBaseDriver.connection;

            if (dataBaseDriver.connection.State == ConnectionState.Closed)
            {
                Console.WriteLine("No connection!");
            }
            else
            {
                Console.Out.WriteLine("Opening connection");
            }

            NemesisList.Set();

            SqlCommand.DroppingTable(_dropTable, conn);
            SqlCommand.CreateTable(_createTable, conn);
            SqlCommand.InsertingTable(_insentTable, conn, NemesisList.nemesisEnemy1);
            SqlCommand.InsertingTable(_insentTable, conn, NemesisList.nemesisEnemy2);
            SqlCommand.InsertingTable(_insentTable, conn, NemesisList.nemesisEnemy3);
            SqlCommand.InsertingTableNew(_insentTable, conn);
            SqlCommand.DeleteTable(_deleteTable, conn);
            SqlCommand.SelectTable(_selectTable, conn);
            SqlCommand.UpdateTable(_updateTable, conn);
            SqlCommand.ConvertToJson(_selectTable, conn);
            SqlCommand.GetVersion(_getVersion, conn);

            dataBaseDriver.connection.Close();
        }
    }
}