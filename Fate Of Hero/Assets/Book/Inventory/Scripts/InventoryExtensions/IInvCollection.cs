
namespace InventorySystem
{
    public interface IInvCollection
    {
        void Add(Item item);
        void Add(Item item, int count);
        bool CanContain(Item item);
        void Clear();
        bool IsFull();
        bool IsEmpty();
    }
}