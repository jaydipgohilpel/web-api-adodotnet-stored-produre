namespace web_api_adodotnet_stored_produre.Models
{
    public class StudentModel
    {
      
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string EmailAddress { get; set; }
    }

    public class StudentGetModel : StudentModel
    {
        public int? StudentId { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
}
