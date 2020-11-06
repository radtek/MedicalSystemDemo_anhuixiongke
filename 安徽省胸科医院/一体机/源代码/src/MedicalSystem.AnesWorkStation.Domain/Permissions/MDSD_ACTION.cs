namespace MDSD.Permission.Domain
{
    using System;
    using System.Collections.Generic;   
    using Dapper.Data;
   
    using MedicalSystem.AnesWorkStation.Domain;


    /// <summary>
    /// 实体 
    /// </summary>
    [Table("MDSD_ACTION")]
    public partial class MDSD_ACTION : BaseModel
    {
        [Key]
        public string ACTION_CODE { get; set; }
        public string MODEL_CODE { get; set; }
        public string ACTION_KEY { get; set; }
        public string ACTION_LABEL { get; set; }
        public string ACTION_DESC { get; set; }
    }
}