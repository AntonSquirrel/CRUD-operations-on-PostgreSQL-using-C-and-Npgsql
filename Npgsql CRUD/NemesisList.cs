
namespace NemesisEnemyList
{
    public class NemesisEnemy
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Damage { get; set; }
        public int Health { get; set; }
    }

    public class NemesisList : NemesisEnemy
    {
        public static NemesisList nemesisEnemy1 = new NemesisList();
        public static NemesisList nemesisEnemy2 = new NemesisList();
        public static NemesisList nemesisEnemy3 = new NemesisList();

        public static void Set()
        {
            nemesisEnemy1.Id = 1;
            nemesisEnemy1.Name = "Личинка";
            nemesisEnemy1.Damage = 1;
            nemesisEnemy1.Health = 5;

            nemesisEnemy2.Id = 2;
            nemesisEnemy2.Name = "Ползун";
            nemesisEnemy2.Damage = 3;
            nemesisEnemy2.Health = 10;

            nemesisEnemy3.Id = 3;
            nemesisEnemy3.Name = "Громила";
            nemesisEnemy3.Damage = 4;
            nemesisEnemy3.Health = 14;
        }
    }
}


