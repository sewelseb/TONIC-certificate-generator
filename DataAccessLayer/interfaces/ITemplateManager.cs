using Models;

namespace DataAccessLayer
{
    public interface ITemplateManager
    {
        /// <summary>
        ///     Replace the specified key inside the template with the name of the Contact
        /// </summary>
        /// <param name="contact">The Contact</param>
        /// <returns>The Path of the created file</returns>
        string GetTemplateFromContact(Contact contact);

        void SetTemplateFile(string path);
        void SetOutputDir(string path);
    }
}