using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CasusVictuzMobile.Database.InterFaces;
using QRCoder;
using SQLite;

namespace CasusVictuzMobile.MVVM.Models
{
    [Table("Registration")]
    public class Registration:TableData
    {
        public bool IsOrginizer { get; set; }
        [Ignore]
        public Event? Event { get; set; }
        [NotNull]
        public int EventId { get; set; }
        [Ignore]
        public User? User { get; set; }
        [NotNull]
        public int UserId { get; set; }
        [Ignore]
        public ImageSource? QRImage { get; set; }

        public string GetQRCodeText()
        {
            User user = Session.UserSession.Instance.LoggedInUser;
            if (user.IsGuest)
            {
                return $"Registratiebewijs voor gast van het evenement {Event.Name} op {Event.Date}";
            }
            else 
            {
                return $"Registratiebewijs voor {user.Username}van het evenement {Event.Name} op {Event.Date.ToString("dd-MM-yyyy HH:mm")}";
            }
        }

        public void GenerateQRCode()
        {
            var qrGenerator = new QRCodeGenerator();
            var qrCodeData = qrGenerator.CreateQrCode(GetQRCodeText(), QRCodeGenerator.ECCLevel.L);
            var qrCode = new PngByteQRCode(qrCodeData);
            byte[] qrCodeAsPngByteArr = qrCode.GetGraphic(20);

            QRImage = ImageSource.FromStream(() => new MemoryStream(qrCodeAsPngByteArr));
        }



       
        public static List<Registration> GetAll()
        {
            return App.RegistrationRepository.GetAllEntities();
        }
    }

}
