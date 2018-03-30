using System.Collections.Generic;
using SecretChord.Models.Requests;
using SecretChord.Models.Domain;

namespace SecretChord.Services
{
    public interface IFaqItemService
    {
        int Delete(int id);
        int Insert(FaqItemAddRequest model);
        IEnumerable<FaqItem> SelectAll();
        FaqItem SelectById(int id);
        void Update(FaqItemUpdateRequest model);
    }
}