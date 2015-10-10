using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;

namespace ApplicantTestProject.Controllers
{
    [TestClass]
    public class ApplicantControllerTests
    {
        private Applicant.Controllers.ApplicantsController ApplicantsController;
  

        [TestInitialize]
        [TestMethod]
        public void InitApplicanntsController()
        {
            ApplicantsController = new Applicant.Controllers.ApplicantsController();
        }
        [TestMethod]
        public void PageViewResultNotNull()
        {
            Assert.IsNotNull(ApplicantsController.Create() as ViewResult);
        }
    }
}
