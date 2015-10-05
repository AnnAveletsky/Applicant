using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Applicant.Models;

namespace ApplicantTestProject.Models
{
    [TestClass]
    public class AttachmentModelsTests
    {
        Attachment Attachment1;
        Attachment Attachment2;
        Attachment Attachment3;

        AttachmentFields AttachmentFields;

        Applicant.Models.Applicant Applicant;

        History History;

        [TestInitialize]
        [TestMethod]
        public void InitAttachments()
        {
            Attachment1 = new Attachment();

            AttachmentFields = new AttachmentFields("Файл",".txt",null);
            Applicant=new Applicant.Models.Applicant();
            Attachment2 = new Attachment(AttachmentFields,Applicant.ApplicantId,Applicant);

            History = new History();
            Attachment3 = new Attachment(AttachmentFields,Applicant.ApplicantId, Applicant, History.HistoryId, History);
        }
    }
}
