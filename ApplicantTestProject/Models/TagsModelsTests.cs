using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Applicant.Models;
using System.Collections.Generic;

namespace ApplicantTestProject.Models
{
    [TestClass]
    public class TagsModelsTests
    {
        TagFields TagFields;
        Tag Tag1;
        Tag Tag2;
        List<Applicant.Models.Applicant> Applicants;
        Applicant.Models.Applicant Applicant;
        TagCreate TagCreate;

        [TestMethod]
        [TestInitialize]
        public void InitTags()
        {
            TagFields = new TagFields("Asp.net");
            Tag1 = new Tag(TagFields);

            Applicants = new List<Applicant.Models.Applicant>();
            Applicant=new Applicant.Models.Applicant();
            Applicants.Add(Applicant);
            Tag2 = new Tag(TagFields,Applicants);

            TagCreate = new TagCreate(TagFields, Applicant.ApplicantId);
        }
    }
}
