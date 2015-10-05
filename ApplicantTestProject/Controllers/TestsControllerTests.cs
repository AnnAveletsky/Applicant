using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Applicant.Controllers;

namespace ApplicantTestProject.Controllers
{
    [TestClass]
    public class TestsControllerTests
    {
        TagsController TagsController;

        [TestInitialize]
        [TestMethod]
        public void InitTagsController()
        {
            TagsController = new TagsController();
        }
    }
}
