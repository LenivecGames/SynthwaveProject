using LiteDB;

namespace CoonGames.Shop
{
    public class TestClass : ISerializable
    {
        public float Damage;
        public int Speed;

        public TestClass() { }

        public TestClass(float damage, int count)
        {
            Damage = damage;
            Speed = count;
        }

        public BsonDocument Serialize()
        {
            return new BsonDocument(new System.Collections.Generic.Dictionary<string, BsonValue>
            {
                { nameof(Damage).ToLower() , Damage},
                { nameof(Speed).ToLower() , Speed}
            });
        }

        public void Deserialize(BsonDocument data)
        {
            Damage = (float)data.Get(nameof(Damage).ToLower()).AsDouble;
            Speed = data.Get(nameof(Speed).ToLower()).AsInt32;
        }
    }
}
