using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;
using System.Collections.Generic;

namespace ApplicantTestProject.Models
{
    [TestClass]
    public class ApplicantModelsTests
    {
        private Applicant.Models.ApplicantFields ApplicantFields1;
        private Applicant.Models.ApplicantFields ApplicantFields2;
        private Applicant.Models.ApplicantFields ApplicantFields3;

        private Applicant.Models.ApplicantEdit ApplicantEdit1;
        private Applicant.Models.ApplicantEdit ApplicantEdit2;

        private Applicant.Models.Applicant Applicant1;
        private Applicant.Models.Applicant Applicant2;
        private Applicant.Models.Applicant Applicant3;

        public List<Applicant.Models.History> Histories;
        public List<Applicant.Models.Tag> Tags;
        public List<Applicant.Models.Attachment> Attachments;

        [TestInitialize]
        [TestMethod]
        public void InitApplicants()
        {
            ApplicantFields1 = new Applicant.Models.ApplicantFields("Серба", "Анна", "Владимировна", ApplicantClassLibrary.Gender.Женский, new DateTime(1994,12,30), "", "annserba94@gmail.com", "", "", "", "+79788127402", "", 30000, "", "", "");
            ApplicantFields2 = new Applicant.Models.ApplicantFields("Малиновский", "Александр", "Александрович", ApplicantClassLibrary.Gender.Мужской, new DateTime(1994, 07, 03), "", "ixus.van.axel@gmail.com", "", "", "", "+79787881634", "", 40000, "", "", "");
            ApplicantFields3 = new Applicant.Models.ApplicantFields("Иванов", "Иван", "Иванович", ApplicantClassLibrary.Gender.Мужской, new DateTime(1993, 08, 06), "", "email@gmail.com", "", "", "", "+7978823345", "", 40000, "", "", "");

            ApplicantEdit1 = new Applicant.Models.ApplicantEdit(ApplicantFields1);
            ApplicantEdit2 = new Applicant.Models.ApplicantEdit(ApplicantEdit1);

            Applicant1 = new Applicant.Models.Applicant();
            Applicant2 = new Applicant.Models.Applicant(ApplicantFields2);

            Histories = new List<Applicant.Models.History>();
            Histories.Add(new Applicant.Models.History());
            Histories.Add(new Applicant.Models.History());
            Histories.Add(new Applicant.Models.History());
            Tags = new List<Applicant.Models.Tag>();
            Tags.Add(new Applicant.Models.Tag());
            Tags.Add(new Applicant.Models.Tag());
            Tags.Add(new Applicant.Models.Tag());
            Tags.Add(new Applicant.Models.Tag());
            Attachments = new List<Applicant.Models.Attachment>();
            Attachments.Add(new Applicant.Models.Attachment());
            Attachments.Add(new Applicant.Models.Attachment());

            Applicant3 = new Applicant.Models.Applicant(ApplicantFields3, Tags, Histories, Attachments);
        }
        [TestMethod]
        public void AreNotEqualApplicantEdit()
        {
            Assert.AreNotEqual(ApplicantEdit1, ApplicantEdit2);
        }
        [TestMethod]
        public void EditApplicant()
        {
            Applicant1.Edit(ApplicantFields1);
            Applicant2.Edit(ApplicantEdit2);
            Applicant3.Edit(ApplicantFields2);
        }
        [TestMethod]
        public void CollectionHistories()
        {
            Assert.AreEqual(Applicant3.Histories.Count, 3);
        }
        [TestMethod]
        public void CollectionTags()
        {
            Assert.AreEqual(Applicant3.Tags.Count, 4);
        }
        [TestMethod]
        public void CollectionAttachments()
        {
            Assert.AreEqual(Applicant3.Attachments.Count, 2);
        }
        
    }
}
