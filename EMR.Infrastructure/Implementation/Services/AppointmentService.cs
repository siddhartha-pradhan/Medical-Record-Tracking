using EMR.Application.Interfaces.Repositories;
using EMR.Application.Interfaces.Services;
using EMR.Core.Constants;
using EMR.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMR.Infrastructure.Implementation.Services
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

		public void AddEmergencyAppointment(Appointment appointment)
        {
			_unitOfWork.Appointment.Add(appointment);
			_unitOfWork.Save();
		}


		public void CancelAppointment(Guid Id)
        {
            var appointment = _unitOfWork.Appointment.Get(Id);
            
            _unitOfWork.Appointment.Remove(appointment);
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

        public List<Appointment> GetAllBookedAppointments()
        {
            return _unitOfWork.Appointment.GetAll().Where(x => x.AppointmentStatus == Constants.Booked).ToList();
        }

        public List<Appointment> GetAllFinalizedAppointments()
        {
            return _unitOfWork.Appointment.GetAll().Where(x => x.AppointmentStatus == Constants.Completed).ToList();
        }

        public List<Appointment> GetAllFinalizedAppointmentsList()
        {
            return _unitOfWork.Appointment.GetAll().Where(x => x.AppointmentStatus == Constants.Completed).ToList();
        }

		public List<Appointment> GetAllListAppointments()
		{
			return _unitOfWork.Appointment.GetAll();
		}

		public Appointment GetAppointment(Guid Id)
        {
            return _unitOfWork.Appointment.GetFirstOrDefault(x => x.Id == Id);  
        }
    }
}
