using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Applicant.Controllers;

namespace ApplicantTestProject.Controllers
{
    [TestClass]
    public class HistoriesControllerTests
    {
        HistoriesController HistoryController;
        [TestInitialize]
        [TestMethod]
        public void InitHistoriesController()
        {
            HistoryController = new HistoriesController();
        }
    }
}
