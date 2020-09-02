namespace BusinessLayer
{
    public class TemplateManager : ITemplateManager
    {
        private IContactManager _contactManager;

        public TemplateManager(IContactManager contactManager)
        {
            this._contactManager = contactManager;
        }

        public bool replaceKey(string replaced, string value)
        {

            return true;
        }
    }
}