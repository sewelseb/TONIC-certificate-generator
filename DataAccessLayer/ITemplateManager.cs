using DocumentFormat.OpenXml.Packaging;
using Models;

namespace DataAccessLayer
{
    public interface ITemplateManager
    {
        /// <summary>
        ///  Replace the specified key inside the template with the name of the Contact 
        /// </summary>
        /// <param name="replaced">The key to be replaced</param>
        /// <param name="contact">The Contact</param>
        /// <returns>The Path of the created file</returns>
        string GetTemplateFromContact(string replaced, Contact contact);
        bool SetTemplateFile(string path);
        bool SetOutputDir(string path);
    }
}