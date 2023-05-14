using Aspose.Slides;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DemoProject.Models
{
    public class UserModel
    {
        [NotMapped]
        public int inCount { get; set; }
        [Key]
        [Display(Name = "Id")]
        public int inUserId { get; set; }
        [NotMapped]
        [Required(ErrorMessage = "First Name is Required")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Enter Correct First Name Please")]
        [Display(Name = "First Name")]
        public string stFirstName { get; set; }
        [Display(Name = "Full Name")]
        public string stUserName { get; set; }
        [NotMapped]
        [Required(ErrorMessage = "Last Name is Required")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Enter Correct Last Name Please")]
        [Display(Name = "Last Name")]
        public string stLastName { get; set; }
        [Required(ErrorMessage = "BirthDate is Required")]
        [Display(Name = "Birth Date")]
      
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode =true )]
        //[RegularExpression(@"(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$", ErrorMessage = "Invalid Date Format.")]
        public DateTime dtUserBirthDate { get; set; }
        [Required(ErrorMessage = "Email Id is Required")]
        [Display(Name = "Email Address")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Email Id is Invalid")]
        public string stUserEmail { get; set; }
        
        [Required(ErrorMessage = "Password is Required")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string stUserPassword { get; set; }
        [Display(Name = "Created By")]
        public string stCreatedBy { get; set; }
        [Display(Name = "Created Date")]
        public DateTime dtUserCreationDate { get; set; }
        [Display(Name = "Modified Date")]
        public string dtUserModificationDate { get; set; }
        [Display(Name = "Deleted Date")]
        public DateTime? dtUserDeletionDate { get; set; }
        [Display(Name = "Active")]
        public bool flgIsActive { get; set; }
        [NotMapped]
        public string stSearch { get; set; }
        [NotMapped]
        public string stSortColumn { get; set; }
        [NotMapped]
        public string stSortOrder { get; set; }
        [NotMapped]
        public int inPageIndex { get; set; }
        [NotMapped]
        public int inPageSize { get; set; }
        [NotMapped]
        public int? inRecordCount { get; set; }
    }
}