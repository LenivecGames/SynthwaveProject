
namespace CoonGames.Shop
{
    public interface IWallet
    {
        float Money { get; }

        void Withdraw(float money);
    }
}
