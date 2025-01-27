// RegistrationService.cs
using CasusVictuzMobile.Database.InterFaces;
using CasusVictuzMobile.MVVM.Models;
using System.Linq;
using System.Threading.Tasks;

namespace CasusVictuzMobile.Services
{
    public class RegistrationService
    {
        private readonly IBaseRepository<Registration> _registrationRepository;
        private readonly IBaseRepository<Event> _eventRepository;

        public RegistrationService()
        {
            _registrationRepository = App.RegistrationRepository;
            _eventRepository = App.EventRepository;
        }

        public List<Registration> GetAllRegistrationsByUserId(int userId)
        {
            return _registrationRepository.GetAllEntities()
                .Where(r => r.UserId == userId)
                .ToList();
        }

        public async Task<bool> RegisterAsync(int eventId, int userId)
        {
            // Check if already registered
            var existingRegistration = _registrationRepository.GetAllEntities()
                .FirstOrDefault(r => r.EventId == eventId && r.UserId == userId);
            if (existingRegistration != null)
                return false;

            // Check if event is full
            var eventObj = _eventRepository.GetEntity(eventId);
            if (eventObj == null || eventObj.IsFull())
                return false;

            // Create new registration
            var registration = new Registration
            {
                EventId = eventId,
                UserId = userId,
                IsOrginizer = false
            };
            _registrationRepository.SafeEntity(registration);
            return true;
        }

        public async Task<bool> UnregisterAsync(int eventId, int userId)
        {
            var registration = _registrationRepository.GetAllEntities()
                .FirstOrDefault(r => r.EventId == eventId && r.UserId == userId);
            if (registration == null)
                return false;

            _registrationRepository.DeleteEntity(registration);
            return true;
        }
    }
}
