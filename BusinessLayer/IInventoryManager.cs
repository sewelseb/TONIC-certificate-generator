using Models;

namespace BusinessLayer
{
    public interface IInventoryManager
    {
        int GetLastSerialNumber();

        void AddSerialNumber(Contact contact);


    }
}