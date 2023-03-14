
using Newtonsoft.Json;
using NemesisEnemyList;


namespace DataBaseConnector
{
    public class JsonCommand
    {
        public static void Convert(List<NemesisEnemy> list)
        {
            string json = JsonConvert.SerializeObject(list, Formatting.Indented);
            Console.WriteLine(json);
        }
    }
}
