using Imdex.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace Imdex.Repositories
{
    public interface IDrillHoleRepository : IRepository<DrillHole>
    {

    }

    public class DrillHoleRepository : IDrillHoleRepository
    {
        private readonly DbConnection _connection;

        public DrillHoleRepository(DbConnection dbConnection)
        {
            _connection = dbConnection;
        }

        public DrillHole Get(int Id)
        {
            var result = _connection.QueryFirst<DrillHole>(@"Select * from DrillHole where id=@param1", new { param1 = Id });
            return result;
        }

        public List<DrillHole> GetAll()
        {
            var result = _connection.Query<DrillHole>("Select * from DrillHole");
            return result.ToList();
        }
    }
}
