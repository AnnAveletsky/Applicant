﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ApplicantClassLibrary;

namespace Applicant.Models
{
    public class HistoryFields
    {
        [
            DataType(DataType.Date),
            Display(Name = "Дата прохождения")
        ]
        public DateTime CommunicationDate { get; set; }

        [
            Required(ErrorMessage = "Поле должно быть установлено"),
            Display(Name = "Тип встречи")
        ]
        public TypeHistory TypeCommunication { get; set; }

        [
            DataType(DataType.MultilineText),
            Display(Name = "Коментарии"),
            MaxLength(500)
        ]
        public string HistoryComments { get; set; }
        public HistoryFields() { }
        public HistoryFields(DateTime communicationDate, TypeHistory typeCommunication, string historyComments)
        {
            CommunicationDate = communicationDate;
            TypeCommunication = typeCommunication;
            HistoryComments = historyComments;
        }
    }
    public class HistoryEdit : HistoryFields
    {
        [Key]
        public int HistoryId { get; set; }
        public HistoryEdit() { }
        public HistoryEdit(HistoryFields historyFields)
            : base(historyFields.CommunicationDate, historyFields.TypeCommunication, historyFields.HistoryComments){ }
        public void Edit(HistoryFields historyFields)
        {
            CommunicationDate = historyFields.CommunicationDate;
            TypeCommunication = historyFields.TypeCommunication;
            HistoryComments = historyFields.HistoryComments;
        }
    }
    public class HistoryCreate : HistoryEdit
    {
        [
            ScaffoldColumn(false),
            ForeignKey("Applicant")
        ]
        public int? ApplicantId { get; set; }

        [Display(Name = "Соискатель")]
        public Applicant Applicant { get; set; }
        public HistoryCreate() :base(){ }
        public HistoryCreate(HistoryFields historyFields, int? applicantId, Applicant applicant)
            : base(historyFields)
        {
            ApplicantId = applicantId;
            Applicant = applicant;
        }
    }
    public class History : HistoryCreate
    {
        public History() { }
        public History(HistoryCreate historyCreate)
            : base(historyCreate,historyCreate.ApplicantId,historyCreate.Applicant) { }
    }
}