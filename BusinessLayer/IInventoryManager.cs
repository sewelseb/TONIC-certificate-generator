using Models;

namespace BusinessLayer
{
    public interface IInventoryManager
    {
        long GetLastSerialNumber();

        void AddSerialNumber(Contact contact);
    }
}