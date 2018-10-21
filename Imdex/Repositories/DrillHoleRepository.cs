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
            _connection.Open();
            var result = _connection.QueryFirst<DrillHole>(@"Select * from DrillHole where id=@param1", new { param1 = Id });
            _connection.Close();
            return result;
        }

        public List<DrillHole> GetAll()
        {
            _connection.Open();
            var dict = new Dictionary<int, DrillHole>();
            var result = _connection.Query<DrillHole, DepthReading, DrillHole>("Select DrillHole.*, DepthReading.depthid as depthId, DepthReading.depth, DepthReading.dip, DepthReading.azimuth from DrillHole LEFT JOIN DepthReading on DepthReading.drill_id = DrillHole.Id", (drillHole, depthReading) =>
            {
                DrillHole drill;
                if (!dict.TryGetValue(drillHole.Id, out drill))
                {
                    drill = drillHole;
                    drill.Readings = new List<DepthReading>();
                    dict.Add(drill.Id, drill);
                }
                drill.Readings.Add(depthReading);
                return drill;
            },
            splitOn: "Id, depthId");
            _connection.Close();
            var groups = result.GroupBy(x => x.Id);
            return groups.Select(r => r.First()).ToList();
        }
    }
}
