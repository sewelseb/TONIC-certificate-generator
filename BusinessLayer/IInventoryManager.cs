using Models;

namespace BusinessLayer
{
    public interface IInventoryManager
    {
        long GetLastSerialNumber();

        Contact AddSerialNumber(Contact contact);
    }
}