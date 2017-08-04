using LiteDB;

namespace CoonGames.Shop
{
    public interface ISerializable
    {
        BsonDocument Serialize();
        void Deserialize(BsonDocument data);
    }
}
