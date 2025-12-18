using Dapper;
using DatabaseAccessSem1;
using DatabaseAccessSem1.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace EksamenSemEt.DatabaseAccess.Repository
{
    public class CertificationRepository
    {
        private readonly IDbConnectionFactory _dbFactory;
        public CertificationRepository(IDbConnectionFactory dbFactory) { _dbFactory = dbFactory; }

        public Certificate Create(Certificate certificate)
        {
            using var connection = _dbFactory.CreateConnection(); //med using lukkes forbindelse automatisk efter metoden er kørt
            string sql = @"INSERT INTO Certifications (Name) 
                        OUTPUT INSERTED.* Values (@Name);";
            return connection.QuerySingle<Certificate>(sql, certificate);
        }

        public int Delete(int certificationID, SessionRepository sessionRepo)
        {
            using var connection = _dbFactory.CreateConnection(); //med using lukkes forbindelse automatisk efter metoden er kørt

            DeleteGroupWithCert(certificationID);
            sessionRepo.RemoveAllByType(certificationID);

            string sql = @"DELETE FROM Certifications 
                        WHERE CertificationID = @CertificationID;";
            return connection.Execute(sql, new { CertificationID = certificationID });
        }

        public int DeleteGroupWithCert(int CertificationID)
        {
            using var connection = _dbFactory.CreateConnection(); //med using lukkes forbindelse automatisk efter metoden er kørt
            string sql = @"DELETE FROM CertificationGroups 
                        WHERE CertificationID = @CertificationID;";
            return connection.Execute(sql, new { CertificationID = CertificationID });
        }

        public Certificate CreateGroup(int InstructorID, int CertificationID)
        {
            using var connection = _dbFactory.CreateConnection(); //med using lukkes forbindelse automatisk efter metoden er kørt
            string sql = @"INSERT INTO CertificationGroups 
                        (InstructorID, CertificationID) 
                        OUTPUT INSERTED.* Values 
                        (@InstructorID, @CertificationID);";
            return connection.QuerySingle<Certificate>(sql, new { InstructorID = InstructorID, CertificationID = CertificationID });
        }

        public int RemoveGroup(int InstructorID, int CertificationID)
        {
            using var connection = _dbFactory.CreateConnection(); //med using lukkes forbindelse automatisk efter metoden er kørt
            string sql = @"DELETE FROM CertificationGroups 
                        WHERE InstructorID = @InstructorID AND CertificationID = @CertificationID;";
            return connection.Execute(sql, new { InstructorID = InstructorID, CertificationID = CertificationID });
        }



        public IEnumerable<Certificate> GetAll()
        {
            using var connection = _dbFactory.CreateConnection(); //med using lukkes forbindelse automatisk efter metoden er kørt
            string sql = @"SELECT * FROM Certifications;";


            return connection.Query<Certificate>(sql);
        }

        public IEnumerable<Certificate> GetByInstructorID(int instructorID)
        {
            using var connection = _dbFactory.CreateConnection(); //med using lukkes forbindelse automatisk efter metoden er kørt
            string sql = @"SELECT c.*
                        FROM Certifications c
                        JOIN CertificationGroups cg ON c.CertificationID = cg.CertificationID
                        WHERE cg.InstructorID = @InstructorID;";

            return connection.Query<Certificate>(sql, new { InstructorID = instructorID });
        }

        public IEnumerable<Certificate> BroadSearch(string searchString)
        {
            using var connection = _dbFactory.CreateConnection(); //med using lukkes forbindelse automatisk efter metoden er kørt

            if (string.IsNullOrWhiteSpace(searchString)) return GetAll();

            var searchTerms = searchString.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            var sqlBuilder = new StringBuilder("SELECT * FROM Certifications WHERE 1=1");
            var parameters = new DynamicParameters();

            for (int i = 0; i < searchTerms.Length; i++)
            {
                var paramName = $"@term{i}";
                sqlBuilder.Append(
                                    $@" AND (
                                    CAST(CertificationID AS NVARCHAR(59)) LIKE {paramName}
                                    OR Name LIKE {paramName}
                                    )");
                parameters.Add(paramName, $"%{searchTerms[i]}%");
            }

            return connection.Query<Certificate>(sqlBuilder.ToString(), parameters);
        }

        public int Update(Certificate certificate)
        {
            using var connection = _dbFactory.CreateConnection(); //med using lukkes forbindelse automatisk efter metoden er kørt

            string sql = @" UPDATE Certifications
                        SET
                            Name = @Name
                        WHERE CertificationID = @CertificationID";
            return connection.Execute(sql, certificate); //Returnere mængden af rækker opdateret (forhåbeligt 1)
        }

        public IEnumerable<Instructor> GetByFilter(int? certificationID)
        {
            using var connection = _dbFactory.CreateConnection(); //med using lukkes forbindelse automatisk efter metoden er kørt

            if (certificationID == null || certificationID < 0)
            {
                string sqlAll = @"SELECT * FROM Instructors;";
                return connection.Query<Instructor>(sqlAll);
            }

            string sql = @"SELECT i.*
                        FROM Instructors i
                        JOIN CertificationGroups cg ON i.InstructorID = cg.InstructorID
                        WHERE cg.CertificationID = @CertificationID;";

            return connection.Query<Instructor>(sql, new { CertificationID = certificationID });
        }
    }


}
