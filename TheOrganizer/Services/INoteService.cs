using System.Collections.Generic;
using TheOrganizer.Model;

namespace TheOrganizer.Services
{
    public interface INoteService
    {
        bool AddNote(Note note, int ownerId);

        bool EditNote(Note note, int ownerId);

        bool RemoveNote(int noteId, int ownerId);

        IEnumerable<Note> GetNotes(int notebookId, int ownerId);

        Note GetNote(int notesId, int ownerId);
    }
}
