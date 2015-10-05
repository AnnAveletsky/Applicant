using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
namespace ApplicantTestProject.Controllers
{
    [TestClass]
    public class AttachmentControlerTests
    {
        private Applicant.Controllers.AttachmentsController AttachmentsController;

        [TestInitialize]
        [TestMethod]
        public void InitAttachmentsController()
        {
            AttachmentsController = new Applicant.Controllers.AttachmentsController();
        }
    }
}
