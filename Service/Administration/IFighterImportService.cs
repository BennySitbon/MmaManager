using System.Collections.Generic;
using Domain.Models;

namespace Service.Administration
{
    public interface IFighterImportService
    {
        List<Fighter> GetFightersFromImport();
        void HydrateWithIds(HashSet<Fighter> fighters);
    }
}