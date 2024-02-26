using System;
using System.Collections.Generic;
using System.Text;

namespace Sprout.Exam.DataAccess.Constants
{
    public static class EmployeeSQL
    {
        public const string Q_SELECT_ALL = "SELECT * FROM Employee WHERE IsDeleted = 0";

        public const string Q_SELECT_BY_ID = "SELECT * FROM Employee WHERE Id = @Id AND IsDeleted = 0";

        public const string Q_UPDATE = @"UPDATE Employee SET FullName = @FullName,
                                                              Birthdate = @Birthdate, 
                                                              TIN = @TIN, 
                                                              EmployeeTypeId = @EmployeeTypeId
                                                          WHERE Id = @Id ";

        public const string Q_DELETE = @"UPDATE Employee SET IsDeleted = 1
                                                          WHERE Id = @Id ";

        public const string Q_INSERT = @"INSERT INTO Employee (FullName, Birthdate, TIN, EmployeeTypeId, IsDeleted) VALUES
                                                               (@FullName, @Birthdate, @TIN, @EmployeeTypeId, @IsDeleted)";
    }
}
