using System.Collections.Generic;
using SecretChord.Models.Domain;
using SecretChord.Models.Requests;

namespace SecretChord.Services
{
    public interface IAboutPageService
    {
        int Delete(int id);
        int Insert(AboutPageAddRequest model);
        IEnumerable<AboutPage> SelectAll();
        IEnumerable<AboutPage> SelectAllByPage(int pageNumber);
        AboutPage SelectById(int id);
        void Update(AboutPageUpdateRequest model);
    }
}