using Relief.DTOs;
using Relief.DTOs.RequestModel;
using Relief.DTOs.ResponseModel;
using Relief.Entities;
using Relief.Identity;
using Relief.Email;
using Relief.Interfaces.Services;
using Relief.Interfaces.Repositories;

namespace Relief.Implementations.Services
{
    public class DonorService : IDonorService
    {
        private readonly IDonorRepository _donorRepository;
        private readonly IAdminRepository _adminRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IEmailSender _email;
        public DonorService(IDonorRepository donorRepository, IUserRepository userRepository, IRoleRepository roleRepository, IAdminRepository adminRepository, IEmailSender email)
        {
            _donorRepository = donorRepository;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _adminRepository = adminRepository;
            _email = email;
        }

        public async Task<BaseResponse> UnbanDonor(int donorId, int adminId)
        {
            var donor = await _donorRepository.GetDonor(donorId);
            if (donor == null)
            {
                return new BaseResponse
                {
                    Message = "Donor not found",
                    Success = false
                };
            }

            var admin = await _adminRepository.GetAdminInfo(adminId);
            if (admin == null)
            {
                return new BaseResponse
                {
                    Message = "Admin not found",
                    Success = false
                };
            }

            donor.IsBan = false;
            donor.BannedOn = DateTime.Now;
            donor.BannedBy = adminId;
            donor.LastModifiedOn = DateTime.Now;
            await _donorRepository.Update(donor);

            return new BaseResponse
            {
                Message = "Donor bannned successfully",
                Success = true
            };
        }

        public async Task<BaseResponse> BanDonor(int donorId, int adminId)
        {
            var donor = await _donorRepository.GetDonor(donorId);
            if(donor == null)
            {
                return new BaseResponse
                {
                    Message = "Donor not found",
                    Success = false
                };
            }

            var admin = await _adminRepository.GetAdminInfo(adminId);
            if(admin == null)
            {
                return new BaseResponse
                {
                    Message = "Admin not found",
                    Success = false
                };
            }

            donor.IsBan = true;
            donor.BannedOn = DateTime.Now;
            donor.BannedBy = adminId;
            donor.LastModifiedOn = DateTime.Now;
            await _donorRepository.Update(donor);

            return new BaseResponse
            {
                Message = "Donor bannned successfully",
                Success = true
            };
        }

        public async Task<BaseResponse> DeleteDonor(int donorId)
        {
            var donor = await _donorRepository.GetDonor(donorId);
            if (donor == null)
            {
                return new BaseResponse
                {
                    Message = "Donor not found",
                    Success = false
                };
            }

            

            donor.IsDeleted = true;
            donor.DeletedOn = DateTime.Now;
            donor.DeletedBy = donorId;
            await _donorRepository.Update(donor);

            return new BaseResponse
            {
                Message = "Donor bannned successfully",
                Success = true
            };
        }

        public async Task<DonorsResponseModel> GetActiveDonors()
        {
            var actives = await _donorRepository.GetActiveDonors();
            if(actives == null)
            {
                return new DonorsResponseModel
                {
                    Message = "No active donor found",
                    Success = false,
                };
            }

            var dto = actives.Select(x => new DonorDTO
            {
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                Password = x.Password,
                PhoneNumber = x.PhoneNumber,
                Id = x.Id,
                IsBanned = x.IsBan,
                IsDeleted = x.IsDeleted,
                Image = x.Image
            }).ToHashSet();

            return new DonorsResponseModel
            {
                Data = dto,
                Message = "Active Donors",
                Success = true
            };
        }

        public async Task<int> GetActiveDonorsCount()
        {
            var donors = await GetActiveDonors();
            var count = donors.Data.Count;
            return count;
        }


        public async Task<DonorsResponseModel> GetAll()
        {
            var donors = await _donorRepository.GetAll();
            if (donors == null)
            {
                return new DonorsResponseModel
                {
                    Message = "No available donor",
                    Success = false
                };
            }

            var dto = donors.Select(x => new DonorDTO
            {
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                Password = x.Password,
                PhoneNumber = x.PhoneNumber,
                Id = x.Id,
                IsDeleted = x.IsDeleted,
                IsBanned = x.IsBan,
                Image = x.Image
            }).ToHashSet();
            if (dto == null)
            {
                return new DonorsResponseModel
                {
                    Message = "No available donor",
                    Success = false
                };
            }
            return new DonorsResponseModel
            { 
                Success = true,
                Data = dto,
                Message = "List of donors"
            };
        }

        public async Task<int> GetAllCount()
        {
            var donors = await GetAll();
            var count = donors.Data.Count;
            return count;
        }

        public async Task<DonorsResponseModel> GetBannedDonors()
        {
            var banned = await _donorRepository.GetAllBannedDonors();
            if(banned == null)
            {
                return new DonorsResponseModel
                {
                    Message = "No banned donor found",
                    Success = false,
                };
            }

            var dto = banned.Select(x => new DonorDTO
            {
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                Password = x.Password,
                PhoneNumber = x.PhoneNumber,
                Id = x.Id,
                IsBanned = x.IsBan,
                IsDeleted = x.IsDeleted,
                Image = x.Image
            }).ToList();
            return new DonorsResponseModel
            {
                Message = "Banned Donors",
                Success = true,
                Data = dto,
            };
        }

        public async Task<int> GetBannedDonorsCount()
        {
            var donors = await GetBannedDonors();
            var count = donors.Data.Count;
            return count;
        }

        public async Task<DonorsResponseModel> GetByName(string name)
        {
            var donors = await _donorRepository.GetByName(name);
            if (donors == null)
            {
                return new DonorsResponseModel
                {
                    Message = "No donor found",
                    Success = false
                };
            }
            var dto = donors.Select(x => new DonorDTO
            {
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                Password = x.Password,
                PhoneNumber = x.PhoneNumber,
                Id = x.Id,
                IsDeleted = x.IsDeleted,
                IsBanned = x.IsBan,
                Image = x.Image
            }).ToHashSet();

            if (dto == null)
            {
                return new DonorsResponseModel
                {
                    Message = "No donor found",
                    Success = false
                };
            }
            return new DonorsResponseModel
            {
                Data = donors.Select(x => new DonorDTO
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Email = x.Email,
                    Password = x.Password,
                    PhoneNumber = x.PhoneNumber,
                    Id = x.Id,
                    IsBanned = x.IsBan,
                    IsDeleted = x.IsDeleted,
                    Image = x.Image
                }).ToHashSet(),
                Message = "List of Donors",
                Success = true
            };
        }

        public async Task<DonorsResponseModel> GetDeletedDonors()
        {
            var donors = await _donorRepository.GetAllDeletedDonors();
            if (donors == null)
            {
                return new DonorsResponseModel
                {
                    Success = false,
                    Message = "No deleted Donors",
                };
            }

            var dto = donors.Select(x => new DonorDTO
            {
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                Password = x.Password,
                PhoneNumber = x.PhoneNumber,
                Id = x.Id,
                IsDeleted = x.IsDeleted,
                IsBanned = x.IsBan,
                Image = x.Image
            }).ToList();
            return new DonorsResponseModel
            {
                Success = true,
                Message = "Deleted Donors",
                Data = dto,
            };
        }

        public async Task<DonorResponseModel> GetDonor(int id)
        {
            var donor = await _donorRepository.GetDonor(id);
            if (donor == null)
            {
                return new DonorResponseModel
                {
                    Message = "Donot not found",
                    Success = false
                };
            }
            var dto = new DonorDTO
            {
                FirstName = donor.FirstName,
                LastName = donor.LastName,
                Email = donor.Email,
                PhoneNumber = donor.PhoneNumber,
                Password = donor.Password,
                Id = donor.Id,
                IsDeleted = donor.IsDeleted,
                IsBanned = donor.IsBan,
                Image = donor.Image,
            };
            return new DonorResponseModel
            {
                Data = dto,
                Message = "",
                Success = true
            };
        }

        public async Task<BaseResponse> Register(CreateDonorRequestModel model)
        {
            var role = await _roleRepository.Get(x => x.Name == "Donor");
            if (role == null)
            {
                return new BaseResponse
                {
                    Message = "Role not found",
                    Success = false
                };
            }
            var check = await _userRepository.Get(x => x.Email == model.Email);
            if (check != null)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "Email already existed!"
                };
            }
            var user = new User
            {
                UserName = $"{model.LastName} {model.FirstName}",
                Email = model.Email,
                Password = model.Password,
            };

            var addUser = await _userRepository.Register(user);

            
            var userRole = new UserRole
            {
                UserId = addUser.Id,
                RoleId = role.Id
            };
            user.UserRoles.Add(userRole);
            var updateUser = await _userRepository.Update(user);

            var donor = new Donor
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Password = model.Password,
                PhoneNumber = model.PhoneNumber,
                UserID = addUser.Id,
                Image = model.Image,
            };
            donor.LastModifiedBy = donor.Id;
            donor.CreatedBy = donor.Id;
            donor.IsDeleted = false;
            donor.IsBan = false;

            

            await _donorRepository.Register(donor);

            var mail = new EmailRequestModel
            {
                ReceiverEmail = model.Email,
                ReceiverName = $"{model.FirstName} {model.LastName}",
                Message = $"Dear {model.FirstName}, your registration is successful.",
                Subject = "Relief-CMS Donor Registration",
            };
            await _email.SendEmail(mail);


            return new BaseResponse
            {
                Success = true,
                Message = "Account registered successfully!"
            };
        }

        public async Task<BaseResponse> Update(UpdateDonorRequestModel model, int id)
        {
            if (model == null)
            {
                return new BaseResponse
                {
                    Message = "Information cannot be empty",
                    Success = false
                };
            }
            var donor = await _donorRepository.Get(x => x.Id == id);
            if (donor == null)
            {
                return new BaseResponse
                {
                    Message = "Donor not found",
                    Success = false
                };
            }

            //var folderPath = Path.Combine(Directory.GetCurrentDirectory() + "\\Images\\");
            //if (!Directory.Exists(folderPath))
            //{
            //    Directory.CreateDirectory(folderPath);
            //}
            //var fileName = Path.GetFileNameWithoutExtension(model.Image.FileName);
            //var filePath = Path.Combine(folderPath, model.Image.FileName);
            //var extension = Path.GetExtension(model.Image.FileName);
            //if (!Directory.Exists(filePath))
            //{
            //    using (var stream = new FileStream(filePath, FileMode.Create))
            //    {
            //        await model.Image.CopyToAsync(stream);
            //    }
            //    donor.Image = fileName;
            //}
            

            donor.FirstName = model.FirstName ?? donor.FirstName;
            donor.LastName = model.LastName ?? donor.LastName;
            donor.PhoneNumber = model.PhoneNumber ?? donor.PhoneNumber;
            donor.Email = model.Email ?? donor.Email;
            donor.Password = model.Password ?? donor.Password;
            donor.LastModifiedBy = donor.Id;
            await _donorRepository.Update(donor);
            return new BaseResponse
            {
                Message = "Donor's information updated successfully!",
                Success = true
            };
        }
    }
}
