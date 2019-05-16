using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheOrganizer.Model;

namespace TheOrganizer.Services
{
    public class NotebookService : INotebookService
    {
        private readonly TheOrganizerDBContext _db;
        public NotebookService(TheOrganizerDBContext db)
        {
            _db = db;
        }
        public Notebook AddNotebook(Notebook notebook)
        {
            if (notebook != null && !string.IsNullOrWhiteSpace(notebook.Title))
            {
                _db.Notebooks.Add(notebook);
                _db.SaveChanges();
                return notebook;
            }
            return null;
        }

        public bool EditNotebook(Notebook notebook)
        {
            if (string.IsNullOrWhiteSpace(notebook.Title))
                return false;

            Notebook oldNotebook = _db.Notebooks.Find(notebook.Id);

            if (oldNotebook != null && oldNotebook.OwnerId == notebook.OwnerId)
            {
                _db.Entry(oldNotebook).CurrentValues.SetValues(notebook);
                _db.SaveChanges();
                return true;
            }
            return false;
        }

        public Notebook GetNotebook(int notebookId, int ownerId)
        {
            Notebook notebook = _db.Notebooks.Find(notebookId);
            if (notebook.OwnerId == ownerId)
                return notebook;
            return null;
        }

        public IEnumerable<Notebook> GetNotebooks(int ownerId)
        {
            return _db.Notebooks.Where(x => x.OwnerId == ownerId);
        }

        public bool RemoveNotebookr(int notebookId, int ownerId)
        {
            Notebook notebook = _db.Notebooks.Find(notebookId);

            if (notebook != null && notebook.OwnerId == ownerId)
            {
                _db.Notebooks.Remove(notebook);
                _db.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
