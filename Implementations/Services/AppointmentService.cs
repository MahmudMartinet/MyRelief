using Relief.DTOs;
using Relief.DTOs.RequestModel;
using Relief.DTOs.ResponseModel;
using Relief.Entities;
using Relief.Interfaces.Services;
using Relief.Interfaces.Repositories;
using Relief.Email;

namespace Relief.Implementations.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly INgoRepository _ngoRepository;
        private readonly IDonorRepository _donorRepository;
        private readonly IRequestDonationRepository _requestDonationRepository;
        private readonly IDonationRepository _donationRepository;
        private readonly IEmailSender _email;
        public AppointmentService(IAppointmentRepository appointmentRepository, INgoRepository ngoRepository, IDonorRepository donorRepository, IRequestDonationRepository requestDonationRepository, IEmailSender email, IDonationRepository donationRepository)
        {
            _appointmentRepository = appointmentRepository;
            _ngoRepository = ngoRepository;
            _donorRepository = donorRepository;
            _requestDonationRepository = requestDonationRepository;
            _email = email;
            _donationRepository = donationRepository;
        }

        public async Task<BaseResponse> ApproveAppointment(int id)
        {
            var appoint = await _appointmentRepository.GetById(id);
            if(appoint == null)
            {
                return new BaseResponse
                {
                    Message = "Appointment not found",
                    Success = false,
                };
            }
            appoint.IsApproved = true;
            await _appointmentRepository.Update(appoint);
            return new BaseResponse
            {
                Message = "Appointment approved",
                Success = true
            };
        }

        public async Task<BaseResponse> CreateAppointment(CreateAppointmentRequestModel model, int donorId, int requestId)
        {
            if (model == null)
            { return new BaseResponse
                 {
                    Message = "Information not attached",
                    Success = false
                 };
            }
            var request = await _requestDonationRepository.GetRequest(requestId);
            var appointment = new DonationAppointment
            {
                Time = model.Time,
                Venue = model.Venue,
                NGO = request.NGO,
                Donor = await _donorRepository.GetDonor(donorId),
                RequestDonation = request,
                NgoId = request.NgoId,
                DonorId = donorId,
                RequestId = requestId,
            };
            appointment.CreatedBy = donorId;
            await _appointmentRepository.Register(appointment);
            return new BaseResponse
            {
                Message = "Appointment created successfully",
                Success = true
            };
        }

        public async Task<AppointmentsResponseModel> GetAccomplishedByDonorId(int id)
        {
            var all = await _appointmentRepository.GetByDonorId(id);
            if (all.Count == 0)
            {
                return new AppointmentsResponseModel
                {
                    Message = "No appointment record",
                    Success = false
                };
            }


            var accomplished = all.Where(a => a.IsAccomplished == true && a.IsApproved == true ).Select(x => new AppointmentDTO
            {
                Time = x.Time,
                Venue = x.Venue,
                RequestId = x.RequestId,
                DonorName = $"{x.Donor.FirstName} {x.Donor.LastName}",
                DonorId = x.DonorId,
                NgoId = x.NgoId,
                NgoName = $"{x.NGO.Name}",
                RequestDetail = x.RequestDonation.Details,
                IsAccomplished = x.IsAccomplished,
                Id = x.Id,
            }).ToHashSet();
            return new AppointmentsResponseModel
            {
                Data = accomplished,
                Message = "Your appointments",
                Success = true
            };

        }

        public async Task<AppointmentsResponseModel> GetUnapprovedByNgoId(int id)
        {
            var all = await _appointmentRepository.GetByNgoId(id);
            if (all == null)
            {
                return new AppointmentsResponseModel
                {
                    Message = "No appointment found",
                    Success = false,
                };
            }
            var unapproved = all.Where(a => a.IsApproved == false).Select(x => new AppointmentDTO
            {
                Time = x.Time,
                Venue = x.Venue,
                RequestId = x.RequestId,
                DonorName = $"{x.Donor.FirstName} {x.Donor.LastName}",
                DonorId = x.DonorId,
                NgoId = x.NgoId,
                NgoName = $"{x.NGO.Name}",
                RequestDetail = x.RequestDonation.Details,
                IsAccomplished = x.IsAccomplished,
                Id = x.Id,
            }).ToHashSet();
            if (unapproved == null)
            {
                return new AppointmentsResponseModel
                {
                    Message = "No appointment found",
                    Success = false,
                };
            }
            return new AppointmentsResponseModel
            {
                Message = "Unapproved Appointments",
                Success = true,
                Data = unapproved,
            };

        }

        public async Task<AppointmentsResponseModel> GetUnaccomplishedByDonorId(int id)
        {
            var all = await _appointmentRepository.GetByDonorId(id);
            if (all == null)
            {
                return new AppointmentsResponseModel
                {
                    Message = "No appointment record",
                    Success = false
                };
            }
            var unaccomplished = all.Where(a => a.IsAccomplished == false && a.IsApproved == true).Select(x => new AppointmentDTO
            {
                Time = x.Time,
                Venue = x.Venue,
                RequestId = x.RequestId,
                DonorName = $"{x.Donor.FirstName} {x.Donor.LastName}",
                DonorId = x.DonorId,
                NgoId = x.NgoId,
                NgoName = $"{x.NGO.Name}",
                RequestDetail = x.RequestDonation.Details,
                IsAccomplished = x.IsAccomplished,
                Id = x.Id,
            }).ToHashSet();
            return new AppointmentsResponseModel
            {
                Data = unaccomplished,
                Message = "Your appointments",
                Success = true
            };

        }

        public async Task<AppointmentsResponseModel> GetAccomplishedByNgoId(int id)
        {
            var all = await _appointmentRepository.GetByNgoId(id);
            if (all == null)
            {
                return new AppointmentsResponseModel
                {
                    Message = "No appointment record yet",
                    Success = false
                };
            }

            var accomplished = all.Where(a => a.IsAccomplished == true && a.IsApproved == true).Select(x => new AppointmentDTO
            {
                Time = x.Time,
                Venue = x.Venue,
                RequestId = x.RequestId,
                DonorName = $"{x.Donor.FirstName} {x.Donor.LastName}",
                DonorId = x.DonorId,
                NgoId = x.NgoId,
                NgoName = $"{x.NGO.Name}",
                RequestDetail = x.RequestDonation.Details,
                IsAccomplished = x.IsAccomplished,
                Id = x.Id,
            }).ToHashSet();
            return new AppointmentsResponseModel
            {
                Message = "Your appouintments",
                Success = true,
                Data = accomplished
            };
        }

        public async Task<AppointmentsResponseModel> GetUnaccomplishedByNgoId(int id)
        {
            var all = await _appointmentRepository.GetByNgoId(id);
            if (all == null)
            {
                return new AppointmentsResponseModel
                {
                    Message = "No appointment record yet",
                    Success = false
                };
            }

            var unaccomplished = all.Where(a => a.IsAccomplished == false && a.IsApproved == true).Select(x => new AppointmentDTO
            {
                Time = x.Time,
                Venue = x.Venue,
                RequestId = x.RequestId,
                DonorName = $"{x.Donor.FirstName} {x.Donor.LastName}",
                DonorId = x.DonorId,
                NgoId = x.NgoId,
                NgoName = $"{x.NGO.Name}",
                RequestDetail = x.RequestDonation.Details,
                IsAccomplished = x.IsAccomplished,
                Id = x.Id,
            }).ToHashSet();
            return new AppointmentsResponseModel
            {
                Message = "Your appointments",
                Success = true,
                Data = unaccomplished
            };
        }

        public async Task<AppointmentsResponseModel> GetAll()
        {
            var list = await _appointmentRepository.GetAll();
            if(list == null)
            {
                return new AppointmentsResponseModel
                {
                    Success = false,
                    Message = "No appointment found",
                    Data = null,
                };
            }

            return new AppointmentsResponseModel
            {
                Data = list.Select(x => new AppointmentDTO
                {
                    Time = x.Time,
                    Venue = x.Venue,
                    RequestId = x.RequestId,
                    DonorName = $"{x.Donor.FirstName} {x.Donor.LastName}",
                    DonorId = x.DonorId,
                    NgoId = x.NgoId,
                    NgoName = $"{x.NGO.Name}",
                    RequestDetail = x.RequestDonation.Details,
                    IsAccomplished = x.IsAccomplished,
                    Id = x.Id,
                }).ToHashSet(),
                Success = true,
                Message = "List of Appointment"
            };
        }

        public async Task<AppointmentsResponseModel> GetByDonorId(int id)
        {
            var appointment = await _appointmentRepository.GetByDonorId(id);
            if (appointment == null)
            {
                return new AppointmentsResponseModel
                {
                    Success = false,
                    Message = "No appointment found",
                    Data = null,
                };
            }
            return new AppointmentsResponseModel
            {
                Data = appointment.Select(x => new AppointmentDTO
                {
                    Time = x.Time,
                    Venue = x.Venue,
                    RequestId = x.RequestId,
                    DonorName = $"{x.Donor.FirstName} {x.Donor.LastName}",
                    DonorId = x.DonorId,
                    NgoId = x.NgoId,
                    NgoName = $"{x.NGO.Name}",
                    RequestDetail = x.RequestDonation.Details,
                    IsAccomplished = x.IsAccomplished,
                    Id = x.Id,
                    IsApproved = x.IsApproved,
                }).ToHashSet(),
                Success = true,
                Message = "List of Appointments"
            };

        }

        public async Task<AppointmentResponseModel> GetById(int id)
        {
            var appointment = await _appointmentRepository.GetById(id);
            if(appointment == null)
            {
                return new AppointmentResponseModel
                {
                    Message = "Appointment not found",
                    Success = false
                };
            }
            return new AppointmentResponseModel
            {
                Message = "",
                Data = new AppointmentDTO
                {
                    Time = appointment.Time,
                    Venue = appointment.Venue,
                    Id = appointment.Id,
                    RequestDetail = appointment.RequestDonation.Details,
                    RequestId = appointment.RequestId,
                    NgoName = appointment.NGO.Name,
                    NgoId = appointment.NgoId,
                    DonorName = $"{appointment.Donor.FirstName} {appointment.Donor.LastName}",
                    DonorId = appointment.DonorId,
                    IsAccomplished = appointment.IsAccomplished,
                },
                Success = true,

            };
        }

        public async Task<AppointmentsResponseModel> GetByNgoId(int id)
        {
            var appointment = await _appointmentRepository.GetByNgoId(id);
            if (appointment == null)
            {
                return new AppointmentsResponseModel
                {
                    Success = false,
                    Message = "No appointment found",
                    Data = null,
                };
            }
            return new AppointmentsResponseModel
            {
                Data = appointment.Select(x => new AppointmentDTO
                {
                    Time = x.Time,
                    Venue = x.Venue,
                    RequestId = x.RequestId,
                    DonorName = $"{x.Donor.FirstName} {x.Donor.LastName}",
                    DonorId = x.DonorId,
                    NgoId = x.NgoId,
                    NgoName = $"{x.NGO.Name}",
                    RequestDetail = x.RequestDonation.Details,
                    IsAccomplished = x.IsAccomplished,
                    Id = x.Id,
                }).ToHashSet(),
                Success = true,
                Message = "List of Appointments"
            };
        }

        public async Task<AppointmentsResponseModel> GetByRequestId(int id)
        {
            var appointment = await _appointmentRepository.GetByRequestId(id);
            if (appointment == null)
            {
                return new AppointmentsResponseModel
                {
                    Success = false,
                    Message = "No appointment found",
                    Data = null,
                };
            }
            return new AppointmentsResponseModel
            {
                Data = appointment.Select(x => new AppointmentDTO
                {
                    Time = x.Time,
                    Venue = x.Venue,
                    RequestId = x.RequestId,
                    DonorName = $"{x.Donor.FirstName} {x.Donor.LastName}",
                    DonorId = x.DonorId,
                    NgoId = x.NgoId,
                    NgoName = $"{x.NGO.Name}",
                    RequestDetail = x.RequestDonation.Details,
                    IsAccomplished = x.IsAccomplished,
                    Id = x.Id,
                }).ToHashSet(),
                Success = true,
                Message = "List of Appointments"
            };
        }

        public async Task<AppointmentsResponseModel> GetByVenue(string venue)
        {
            var appointment = await _appointmentRepository.GetByVenue(venue);
            if (appointment == null)
            {
                return new AppointmentsResponseModel
                {
                    Success = false,
                    Message = "No appointment found",
                    Data = null,
                };
            }
            return new AppointmentsResponseModel
            {
                Data = appointment.Select(x => new AppointmentDTO
                {
                    Time = x.Time,
                    Venue = x.Venue,
                    RequestId = x.RequestId,
                    DonorName = $"{x.Donor.FirstName} {x.Donor.LastName}",
                    DonorId = x.DonorId,
                    NgoId = x.NgoId,
                    NgoName = $"{x.NGO.Name}",
                    RequestDetail = x.RequestDonation.Details,
                    IsAccomplished = x.IsAccomplished,
                    Id = x.Id,
                }).ToHashSet(),
                Success = true,
                Message = "List of Appointments"
            };
        }

        public async Task<BaseResponse> CancelAppointment(int id)
        {
            var appointment = await _appointmentRepository.GetById(id);
            if (appointment == null)
            {
                return new BaseResponse
                {
                    Message = "Appointment does not exist",
                    Success = false
                };
            }

            appointment.IsDeleted = true;
            await _appointmentRepository.Update(appointment);
            return new BaseResponse
            {
                Message = "Appointment cancelled",
                Success = true
            };
        }

        public async Task<BaseResponse> MarkAccomplished(int id)
        {
            var appointment = await _appointmentRepository.GetById(id);
            if (appointment == null)
            {
                return new BaseResponse
                {
                    Message = "Appointment does not exist",
                    Success = false
                };
            }
            var request = await _requestDonationRepository.GetRequest(appointment.RequestId);

            request.QuantityGathered += appointment.Quantity;
            request.Progress += (decimal)(appointment.Quantity / request.RequestQuantity * 100);
            await _requestDonationRepository.Update(request);
            
            var donation = new Donation
            {
                IsApproved = false,
                IsFund = false,
                DonorId = appointment.DonorId,
                ReqeustDonationId = appointment.RequestId,
                Donor = appointment.Donor,
                ReqeustDonation = appointment.RequestDonation,
                DonationPaymentId = 0,
                AppointmentId = appointment.Id,
                DonationAppointment = appointment,
            };
            await _donationRepository.Register(donation);

            appointment.IsAccomplished = true;
            await _appointmentRepository.Update(appointment);

            var mail = new EmailRequestModel
            {
                ReceiverEmail = appointment.Donor.Email,
                ReceiverName = $"{appointment.Donor.FirstName} {appointment.Donor.LastName}",
                Message = $"Dear {appointment.Donor.FirstName}, your appointment with {request.NGO.Name} on {appointment.Time} at {appointment.Venue} has been marked accomplished and your kind donation has been recieved with gratitude.\nThanks",
                Subject = "Relief-CMS Appointment Notification",
            };
            await _email.SendEmail(mail);

            return new BaseResponse
            {
                Message = "Donation made successfully",
                Success = true
            };
        }

        public async Task<BaseResponse> UpdateAppointment(UpdateAppointmentRequestModel model, int id)
        {
            var appointment = await _appointmentRepository.GetById(id);
            if (appointment == null)
            {
                return new BaseResponse
                {
                    Message = "Appointment does not exist",
                    Success = false
                };
            }
            appointment.Venue = model.Venue;
            appointment.Time = model.Time;
            await _appointmentRepository.Update(appointment);
            return new BaseResponse
            {
                Message = "Appointment updated successfuly",
                Success = true
            };
        }
    }
}
