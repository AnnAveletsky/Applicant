using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Applicant.Models;

namespace ApplicantTestProject.Models
{
    [TestClass]
    public class HistoriesModelsTests
    {

        HistoryFields HistoryFields;
        HistoryCreate HistoryCreate;
        History History;
        HistoryEdit HistoryEdit;

        Applicant.Models.Applicant Applicant;

        [TestInitialize]
        [TestMethod]
        public void InitHistories()
        {
            Applicant = new Applicant.Models.Applicant();
            HistoryFields = new HistoryFields(new DateTime(2015, 09, 15), ApplicantClassLibrary.TypeHistory.Interview, "Здорово!!!");
            HistoryCreate = new HistoryCreate(HistoryFields, Applicant.ApplicantId, Applicant);
            History = new History(HistoryCreate);
            HistoryEdit = new HistoryEdit(HistoryFields);
        }
    }
}
