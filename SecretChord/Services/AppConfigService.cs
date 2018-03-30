using DbConnector.Tools;
using SecretChord.Models.Domain;
using SecretChord.Models.Requests;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SecretChord.Services
{
    public class AppConfigService : BaseService, IAppConfigService
    {
        public IEnumerable<AppConfig> SelectAll()
        {
            return Adapter.LoadObject<AppConfig>("dbo.AppConfig_SelectAll");
        }

        public IEnumerable<string> SelectByKey(string configKey)
        {
            return Adapter.LoadObject<string>("dbo.AppConfig_SelectByKey", new[] { new SqlParameter("@ConfigKey", configKey) });
        }

        public AppConfig SelectById(int id)
        {
            return Adapter.LoadObject<AppConfig>("dbo.AppConfig_SelectById", new[] { new SqlParameter("@Id", id) }).FirstOrDefault();
        }

        public int Insert(AppConfigAddRequest model)
        {
            //SqlParameter idOut = new SqlParameter("@Id", 0);
            //idOut.Direction = System.Data.ParameterDirection.Output;
            int id = 0;
            Adapter.ExecuteQuery("dbo.AppConfig_Insert",
                new[]
            {
                SqlDbParameter.Instance.BuildParameter("@ConfigKey", model.ConfigKey, System.Data.SqlDbType.NVarChar, 100),
                SqlDbParameter.Instance.BuildParameter("@ConfigValue", model.ConfigValue, System.Data.SqlDbType.NVarChar, 300),
                SqlDbParameter.Instance.BuildParameter("@Id", 0, System.Data.SqlDbType.Int, paramDirection: System.Data.ParameterDirection.Output)

                //new SqlParameter("@Title", model.Title), THIS COULD BE IT BUT TOP ONE IS COOLER
                //new SqlParameter("@Message", model.Message),
                //new SqlParameter("@StackTrace", model.StackTrace),
                //new SqlParameter("@ErrorSourceTypeId", model.ErrorSourceTypeId),
            },
            (parameters =>
            {
                id = parameters.GetParamValue<int>("@Id");//an extention against the IDBparameters array
                //int.TryParse(parameters[4].Value.ToString(), out id);
            }
            ));
            return id;
        }

        public void Update(AppConfigUpdateRequest model) //change out void to int 
        {
            //int id = 0;
            Adapter.ExecuteQuery("dbo.AppConfig_Update",
                new[]
            {
                SqlDbParameter.Instance.BuildParameter("@ConfigKey", model.ConfigKey, System.Data.SqlDbType.NVarChar, 100),
                SqlDbParameter.Instance.BuildParameter("@ConfigValue", model.ConfigValue, System.Data.SqlDbType.NVarChar, 300),
                SqlDbParameter.Instance.BuildParameter("@Id", model.Id, System.Data.SqlDbType.Int)
            }); //get rid of the );
                //,
                //(parameters =>
                //{
                //    id = parameters.GetParamValue<int>("@Id");//an extention against the IDBparameters array
                //}
                //));
                //return id;
        }

        public int Delete(int id)
        {
            return Adapter.ExecuteQuery("dbo.AppConfig_Delete", new[]
            {
               SqlDbParameter.Instance.BuildParameter("@Id", id, System.Data.SqlDbType.Int)
            });
        }

    }
}