using Dapper;
using DatabaseAccessSem1;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace EksamenSemEt.DatabaseAccess.Repository
{
    public class MemberTypeRepository
    {
        private readonly IDbConnectionFactory _dbFactory;
        public MemberTypeRepository(IDbConnectionFactory dbFactory) { _dbFactory = dbFactory; }
  
        public IEnumerable<MemberTypeOption> GetAll()
        {
            using var connection = _dbFactory.CreateConnection(); //med using lukkes forbindelse automatisk efter metoden er kørt

            string sql = @"SELECT * FROM MemberTypeOptions;";

            return connection.Query<MemberTypeOption>(sql);
        }

        public MemberTypeOption GetByID(int memberTypeID)
        {
            using var connection = _dbFactory.CreateConnection(); //med using lukkes forbindelse automatisk efter metoden er kørt

            string sql = @"SELECT * FROM MemberTypeOptions
                          WHERE MemberTypeID = @MemberTypeID";

            return connection.QuerySingle<MemberTypeOption>(sql, new { MemberTypeID = memberTypeID });
        }
    }
}
