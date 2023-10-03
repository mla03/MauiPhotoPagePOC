using POCPictureGallery.Services;
using POCPictureGallery.Services.DatabaseServices;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POCPictureGallery.Services.DatabaseService
{
    public class BaseDatabaseService<T,B> : BaseService<T> where B : new()
    {
        protected readonly SQLiteConnection _connection;
        public BaseDatabaseService()
        {
            _connection = DatabaseInstanceService.GetDatabaseInstance();
        }
        public int Create(B pModelObject)
        {
            return _connection.Insert(pModelObject);
        }

        public virtual int Update(B pModelObject)
        {
            return _connection.Update(pModelObject);
        }

        public int Delete(B pModelObject)
        {
            return _connection.Delete(pModelObject);
        }

        public virtual int DeleteAll()
        {
            return _connection.DeleteAll<B>();
        }

        public virtual int InsertOrReplace(B pModelObject)
        {
            return _connection.InsertOrReplace(pModelObject);
        }
        public virtual List<B> GetAll()
        {
            return _connection.Table<B>().ToList();
        }
        public virtual List<int> InsertOrReplaceList(List<B> pModelObjectList)
        {
            var resultList = new List<int>();
            foreach (B pModelObject in pModelObjectList)
            {
                resultList.Add(InsertOrReplace(pModelObject));
            }
            return resultList;
        }

        public virtual GuidObject GetByGuid<GuidObject>(string pGuid) where GuidObject : new()
        {
            TableMapping mapping = _connection.GetMapping(typeof(GuidObject));
            string query = $"SELECT * FROM {mapping.TableName} WHERE Guid = '{pGuid}'";
            return _connection.Query<GuidObject>(query).FirstOrDefault();
        }

        public int IosDataSyncDeleteDeletedData()
        {
            TableMapping mapping = _connection.GetMapping(typeof(B));
            string query = $"DELETE FROM {mapping.TableName} WHERE Deleted = 1";
            return _connection.Execute(query);
        }
    }
}
