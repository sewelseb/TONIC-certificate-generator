using System;
using DocumentFormat.OpenXml.Drawing;
using Models;

namespace BusinessLayer
{
    public interface ITemplateManager
    {

        Object GetTemplateFromContact(string replaced, Contact contact);
    }
}