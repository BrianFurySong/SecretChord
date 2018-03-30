using System.Collections.Generic;
using SecretChord.Models.Domain;
using SecretChord.Models.Requests;

namespace SecretChord.Services
{
    public interface IAppConfigService
    {
        int Delete(int id);
        int Insert(AppConfigAddRequest model);
        IEnumerable<AppConfig> SelectAll();
        AppConfig SelectById(int id);
        IEnumerable<string> SelectByKey(string configKey);
        void Update(AppConfigUpdateRequest model);
    }
}