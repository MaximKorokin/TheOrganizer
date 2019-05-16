using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheOrganizer.Model;

namespace TheOrganizer.Services
{
    public interface INotebookService
    {
        Notebook AddNotebook(Notebook notebook);

        bool EditNotebook(Notebook notebook);

        bool RemoveNotebookr(int notebookId, int ownerId);

        Notebook GetNotebook(int notebookId, int ownerId);

        IEnumerable<Notebook> GetNotebooks(int ownerId);
    }
}
