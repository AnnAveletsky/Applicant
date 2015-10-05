using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;

namespace ApplicantTestProject.Controllers
{
    [TestClass]
    public class ApplicantControllerTests
    {
        private Applicant.Controllers.ApplicantsController ApplicantsController;
        private ApplicantClassLibrary.SortSearch SortSearch;
        private ApplicantClassLibrary.Page Page;

        [TestInitialize]
        [TestMethod]
        public void InitApplicanntsController()
        {
            ApplicantsController = new Applicant.Controllers.ApplicantsController();
            SortSearch =new ApplicantClassLibrary.SortSearch();
            Page = new ApplicantClassLibrary.Page(SortSearch,80);
        }
        [TestMethod]
        public void IndexViewResultNotNull()
        {
            Assert.IsNotNull(ApplicantsController.Index(SortSearch) as ViewResult);
        }
        [TestMethod]
        public void PageViewResultNotNull()
        {
            Assert.IsNotNull(ApplicantsController.Create() as ViewResult);
        }
    }
}
