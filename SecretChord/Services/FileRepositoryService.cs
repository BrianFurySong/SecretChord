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
    public class FileRepositoryService : BaseService, IFileRepositoryService
    {
        public IEnumerable<FileRepository> SelectAll()
        {
            return Adapter.LoadObject<FileRepository>("dbo.FileRepository_SelectAll");
        }

        public FileRepository SelectById(int id)
        {
            return Adapter.LoadObject<FileRepository>("dbo.FileRepository_SelectById", new[] { new SqlParameter("@Id", id) }).FirstOrDefault();
        }

        public int Insert(FileRepositoryAddRequest model)
        {
            //SqlParameter idOut = new SqlParameter("@Id", 0);
            //idOut.Direction = System.Data.ParameterDirection.Output;
            int id = 0;
            Adapter.ExecuteQuery("dbo.FileRepository_Insert",
                new[]
            {
                SqlDbParameter.Instance.BuildParameter("@FilePathName", model.FilePathName, System.Data.SqlDbType.NVarChar, 256),
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

        public void Update(FileRepositoryUpdateRequest model) //change out void to int 
        {
            //int id = 0;
            Adapter.ExecuteQuery("dbo.FileRepository_Update",
                new[]
            {
                SqlDbParameter.Instance.BuildParameter("@FilePathName", model.FilePathName, System.Data.SqlDbType.NVarChar, 256),
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
            return Adapter.ExecuteQuery("dbo.FileRepository_Delete", new[]
            {
               SqlDbParameter.Instance.BuildParameter("@Id", id, System.Data.SqlDbType.Int)
            });
        }

    }
}