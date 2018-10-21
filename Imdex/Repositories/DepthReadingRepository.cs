using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Imdex.Models;

namespace Imdex.Repositories
{
    public interface IDepthReadingRepository : IRepository<DepthReading>
    {

    }

    public class DepthReadingRepository : IDepthReadingRepository
    {
        private readonly DbConnection _connection;

        public DepthReadingRepository(DbConnection dbConnection)
        {
            _connection = dbConnection;
        }

        public DepthReading Get(int Id)
        {
            var result = _connection.QueryFirst<DepthReading>(@"Select * from DepthReading where DepthId=@param1", new { param1 = Id });
            return result;
        }

        public List<DepthReading> GetAll()
        {
            var result = _connection.Query<DepthReading>("Select * from DepthReading");
            return result.ToList();
        }

        public void Update(DepthReading model)
        {
            _connection.Execute(@"UPDATE DepthReading SET depth = @param2, dip = @param3, azimuth = @param4) WHERE DepthId = @parameter1)", new { param1 = model.DepthId, param2 = model.Depth, param3 = model.Dip, param4 = model.Azimuth });
        }
    }
}
