[HttpPost]
public String CreateAppointment()
{
    var appointment = new HealthCareAppointment
    {
        PatientID = int.Parse(Request.Form["PatientID"]),
        DoctorID = int.Parse(Request.Form["DoctorID"]),
        Date = Request.Form["AppointmentDate"]
    };

    var vx = Request.Files["Attachment"].ContentLength;


    // Store the attachment in local storage.
    var Str1 = Request.Files[0].FileName.Split('.');
    var FileType = Str1[Str1.Length - 1];
    var FilePath =
        Server.MapPath("~/Uploads/") +
        string.Format(@"{0}", Guid.NewGuid()) +
        "." + FileType;
    Request.Files[0].SaveAs(FilePath);

    
    if (ModelState.IsValid)
    {
        // Add the appointment into the database.
        db.HealthCareAppointments.Add(appointment);
        db.SaveChanges();

        // Send confirmation email.
        var mail = new MailMessage();
        mail.To.Add(new MailAddress(Request.Form["EmailAddress"]));
        mail.From = new MailAddress("FIT5032Y2024@outlook.com");

        mail.Subject = "Appointment Confirmation";
        mail.Body =
            "You booked an appointment.\n" +
            "Patient ID： " + Request.Form["PatientID"] + "\n" +
            "Doctor ID： " + Request.Form["DoctorID"] + "\n" +
            "Date： " + Request.Form["AppointmentDate"] + "\n";

        mail.IsBodyHtml = false;

        var attachment = new System.Net.Mail.Attachment(FilePath);
        mail.Attachments.Add(attachment);

        // Add smtp server.
        var smtp = new SmtpClient();
        smtp.Host = "smtp-mail.outlook.com";
        smtp.Port = 587;
        smtp.EnableSsl = true;
        smtp.Credentials = new System.Net.NetworkCredential
            ("FIT5032Y2024@outlook.com", "MonashPassword12345#");
        smtp.Send(mail);
        return "Success";
    }
    return "Database Unavailable.";

}