
using Npgsql;
using NemesisEnemyList;

namespace DataBaseConnector
{
    public class SqlCommand
    {

        public static void DroppingTable(string sql, NpgsqlConnection conn)
        {
            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                cmd.ExecuteNonQuery();
                Console.WriteLine("Сброс таблицы");
            }
        }

        public static void CreateTable(string sql, NpgsqlConnection conn)
        {
            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                cmd.ExecuteNonQuery();
                Console.WriteLine("Создание таблицы");
            }
        }

        public static void InsertingTable(string sql, NpgsqlConnection conn, NemesisList nemesisEnemy)
        {
            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("id", nemesisEnemy.Id);

                cmd.Parameters.AddWithValue("name", nemesisEnemy.Name);

                cmd.Parameters.AddWithValue("damage", nemesisEnemy.Damage);

                cmd.Parameters.AddWithValue("health", nemesisEnemy.Health);

                cmd.ExecuteNonQuery();

                Console.WriteLine("Внеcение данных в таблицу");
            }
        }

        public static void InsertingTableNew(string sql, NpgsqlConnection conn)
        {
            NemesisList nemesisEnemy = new NemesisList();

            nemesisEnemy.Id = 4;

            Console.WriteLine("Введите: имя ");
            nemesisEnemy.Name = Console.ReadLine();

            Console.WriteLine("урон ");
            nemesisEnemy.Damage = int.Parse(Console.ReadLine());

            Console.WriteLine("здоровье ");
            nemesisEnemy.Health = int.Parse(Console.ReadLine());

            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("id", nemesisEnemy.Id);

                cmd.Parameters.AddWithValue("name", nemesisEnemy.Name);

                cmd.Parameters.AddWithValue("damage", nemesisEnemy.Damage);

                cmd.Parameters.AddWithValue("health", nemesisEnemy.Health);

                cmd.ExecuteNonQuery();

                Console.WriteLine("Внеcение данных в таблицу");
            }
        }

        public static void DeleteTable(string sql, NpgsqlConnection conn)
        {
            Console.WriteLine("Введите какого ксеноморфа вы хотите удалить");
            int id = int.Parse(Console.ReadLine());

            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("i", id);
                cmd.ExecuteNonQuery();
            }
        }

        public static void SelectTable(string sql, NpgsqlConnection conn)
        {
            Console.WriteLine("Введите какого ксеноморфа вы хотите вывести на консоль");
            int id = int.Parse(Console.ReadLine());

            using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("id", id);

                using (NpgsqlDataReader reader = cmd.ExecuteReader())

                    while (reader.Read())
                    {
                        NemesisEnemy Enemy = ReadNemesisEnemy(reader);

                        Console.WriteLine(Enemy.Name + ": " + Enemy.Damage + " - урон " + Enemy.Health + " - здоровье");

                        //для коллеции пример:
                        //List<NemesisEnemy> EnemyLists = new List<NemesisEnemy>();

                        //Enemy.Id = reader.GetInt32(0);
                        //Enemy.Name = reader.GetString(1);
                        //Enemy.Damage = reader.GetInt16(2);
                        //Enemy.Health = reader.GetInt16(3);
                        //EnemyLists.Add(Enemy);

                        //foreach (NemesisEnemy c in EnemyLists)
                        //{
                        //    Console.WriteLine(c.Id + "\t" + c.Name + "\t" + c.Damage + "\t" + c.Health);
                        //}
                    }
            }
        }

        private static NemesisEnemy ReadNemesisEnemy(NpgsqlDataReader reader)
        {
            int? id = reader["id"] as int?;
            string name = reader["name"] as string;
            int? damage = reader["damage"] as Int16?;
            int? health = reader["health"] as Int16?;

            NemesisEnemy Enemy = new NemesisEnemy
            {
                Id = id.Value,
                Name = name,
                Damage = damage.Value,
                Health = health.Value,
            };

            return Enemy;
        }

        public static void SelectTable1(string sql, NpgsqlConnection conn, int id)
        {
            using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("id", id);

                using (NpgsqlDataReader reader = cmd.ExecuteReader())

                    while (reader.Read())
                    {
                        NemesisEnemy Enemy = ReadNemesisEnemy(reader);

                        Console.WriteLine(Enemy.Name + ": " + Enemy.Damage + " - урон " + Enemy.Health + " - здоровье");

                    }
            }
        }
            

        public static void UpdateTable(string sql, NpgsqlConnection conn)
        {
            NemesisList nemesisEnemy = new NemesisList();

            Console.WriteLine("Введите какого ксеноморфа вы хотите изменить");
            nemesisEnemy.Id = int.Parse(Console.ReadLine());

            Console.WriteLine("имя ");
            nemesisEnemy.Name = Console.ReadLine();

            Console.WriteLine("урон ");
            nemesisEnemy.Damage = int.Parse(Console.ReadLine());

            Console.WriteLine("здоровье ");
            nemesisEnemy.Health = int.Parse(Console.ReadLine());
            

            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("id", nemesisEnemy.Id);
                cmd.Parameters.AddWithValue("name", nemesisEnemy.Name);
                cmd.Parameters.AddWithValue("damage", nemesisEnemy.Damage);
                cmd.Parameters.AddWithValue("health", nemesisEnemy.Health);
                cmd.ExecuteNonQuery();
                Console.WriteLine("Изменения данных в таблице");
                
                SelectTable1("SELECT * FROM NemesisEnemy WHERE ID = @id", conn, nemesisEnemy.Id);
            }
        }

        public static void ConvertToJson(string sql, NpgsqlConnection conn)
        {
            using (var command = new NpgsqlCommand(sql, conn))
            {
                List<NemesisEnemy> list = new List<NemesisEnemy>();

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    NemesisEnemy obj = new NemesisEnemy();
                    obj.Id = reader.GetInt32(0);
                    obj.Name = reader.GetString(1);
                    obj.Damage = reader.GetInt32(2);
                    obj.Health = reader.GetInt32(2);
                    list.Add(obj);
                }
                reader.Close();
                JsonCommand.Convert(list);
            }
        }

        public static void GetVersion(string sql, NpgsqlConnection conn)
        {
            using var cmd = new NpgsqlCommand(sql, conn);

            var versionFromQuery = cmd.ExecuteScalar().ToString();

            Console.WriteLine(versionFromQuery);
            Console.ReadKey();
        }

    }
}

