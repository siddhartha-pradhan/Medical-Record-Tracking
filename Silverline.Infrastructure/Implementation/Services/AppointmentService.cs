using Silverline.Application.Interfaces.Repositories;
using Silverline.Application.Interfaces.Services;
using Silverline.Core.Constants;
using Silverline.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silverline.Infrastructure.Implementation.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppointmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void BookAppointment(Appointment appointment)
        {
            _unitOfWork.Appointment.Add(appointment);
            _unitOfWork.Save();
        }

        public List<Appointment> GetAllAppointments(Guid Id)
        {
            return _unitOfWork.Appointment.GetAll().Where(x => x.DoctorId == Id).ToList();   
        }

        public List<Appointment> GetAllBookedAppointment(Guid Id)
        {
            return _unitOfWork.Appointment.GetAll().Where(x => x.DoctorId == Id && x.AppointmentStatus == Constants.Booked).ToList();
        }

        public Appointment GetAppointment(Guid Id)
        {
            return _unitOfWork.Appointment.GetFirstOrDefault(x => x.Id == Id);  
        }
    }
}
