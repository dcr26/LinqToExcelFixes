using System;
using System.Linq;
using System.IO;
using NUnit.Framework;

namespace LinqToExcel.Tests
{
    [Author("Paul Yoder", "paulyoder@gmail.com")]

    [TestFixture]
    public class CSV_IntegrationTests
    {
        string _fileName;

        [TestFixtureSetUp]
        public void fs()
        {
            var testDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var excelFilesDirectory = Path.Combine(testDirectory, "ExcelFiles");
            _fileName = Path.Combine(excelFilesDirectory, "Companies.csv");
        }

        [Test]
        public void select_all()
        {
            var companies = from c in ExcelQueryFactory.Worksheet<Company>(null, _fileName, null)
                            select c;

            Assert.AreEqual(7, companies.ToList().Count);
        }

        [Test]
        public void where_contains_string_criteria()
        {
            var companies = from c in ExcelQueryFactory.Worksheet<Company>(null, _fileName, null)
                            where c.Name == "ACME"
                            select c;

            Assert.AreEqual(1, companies.ToList().Count);
        }

        [Test]
        public void where_contains_int_criteria()
        {
            var companies = from c in ExcelQueryFactory.Worksheet<Company>(null, _fileName, null)
                            where c.EmployeeCount > 20
                            select c;

            Assert.AreEqual(5, companies.ToList().Count);
        }

        [Test]
        public void where_contains_datetime_criteria()
        {
            var companies = from c in ExcelQueryFactory.Worksheet<Company>(null, _fileName, null)
                            where c.StartDate == new DateTime(1980, 8, 23)
                            select c;

            Assert.AreEqual(1, companies.ToList().Count);
        }
    }
}
