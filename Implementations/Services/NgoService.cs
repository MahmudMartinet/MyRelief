using Relief.DTOs;
using Relief.DTOs.RequestModel;
using Relief.DTOs.ResponseModel;
using Relief.Entities;
using Relief.Email;
using Relief.Interfaces.Services;
using Relief.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Relief.Identity;

namespace Relief.Implementations.Services
{
    public class NgoService : INgoService
    {
        private readonly INgoRepository _ngoRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IDocumentRepository _documentRepository;
        private readonly IEmailSender _email;

        public NgoService(INgoRepository ngoRepository, ICategoryRepository categoryRepository, IUserRepository userRepository, IRoleRepository roleRepository, IDocumentRepository documentRepository, IEmailSender email)
        {
            _ngoRepository = ngoRepository;
            _categoryRepository = categoryRepository;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _documentRepository = documentRepository;
            _email = email;
        }

        public async Task<BaseResponse> ApproveNgo(int id)
        {
            var ngo = await _ngoRepository.GetNgo(id);
            if(ngo == null)
            {
                return new BaseResponse
                {
                    Message = "NGO not found",
                    Success = false
                };
            }
            ngo.IsApproved = true;
            await _ngoRepository.Update(ngo);
            return new BaseResponse
            {
                Message = "NGO approved successfully",
                Success = true
            };
        }

        public async Task<BaseResponse> BanNgo(int id)
        {
            var ngo = await _ngoRepository.GetNgo(id);
            if (ngo == null)
            {
                return new BaseResponse
                {
                    Message = "NGO not found",
                    Success = false
                };
            }
            ngo.IsBan = true;
            await _ngoRepository.Update(ngo);
            return new BaseResponse
            {
                Message = "NGO banned successfully",
                Success = true
            };
        }

        public async Task<BaseResponse> UnbanNgo(int id)
        {
            var ngo = await _ngoRepository.GetNgo(id);
            if (ngo == null)
            {
                return new BaseResponse
                {
                    Message = "NGO not found",
                    Success = false
                };
            }
            ngo.IsBan = false;
            await _ngoRepository.Update(ngo);
            return new BaseResponse
            {
                Message = "NGO unbanned successfully",
                Success = true
            };
        }

        public async Task<BaseResponse> DeleteNgo(int id)
        {
            var ngo = await _ngoRepository.GetNgo(id);
            if (ngo == null)
            {
                return new BaseResponse
                {
                    Message = "NGO not found",
                    Success = false
                };
            }
            ngo.IsDeleted = true;
            await _ngoRepository.Update(ngo);
            return new BaseResponse
            {
                Message = "Account deleted successfully",
                Success = true
            };
        }

        public async Task<NGOsResponseModel> GetAll()
        {
            var ngos = await _ngoRepository.GetAll();
            if (ngos == null)
            {
                return new NGOsResponseModel
                {
                    Message = "No category found",
                    Success = false
                };
            }
            var doc = await _documentRepository.GetAllWithNgo();
            var dtos = ngos.Select(x => new NgoDTO
            {
                Name = x.Name,
                Email = x.Email,
                Password = x.Password,
                State = x.State,
                City = x.City,
                LGA = x.LGA,
                Address = x.Address,
                Id = x.Id,
                Image = x.Image,
                Description = x.Description,
                Particulars = doc.Select(a => new DocumentDTO
                {
                    Name = a.Name
                }).ToList(),
                AccountName = x.AccountName,
                AccountNumber = x.AccountNumber,
                BankName = x.BankName,
                IsApproved = x.IsApproved,
                IsBanned = x.IsBan,
                IsDeleted = x.IsDeleted,
                CategoryName = x.Category.Name,
                CategoryId = x.CategoryId,
                
            }).ToList();
            return new NGOsResponseModel
            {
                Success = true,
                Data = dtos,
                Message = "List of NGOs",
            };
        }

        public async Task<int> GetAllCount()
        {
            var ngos = await GetAll();
            var count = ngos.Data.Count;
            return count;
        }

        public async Task<NGOsResponseModel> GetAllWithCategory()
        {
            var ngos = await _ngoRepository.GetAllWithCategory();
            if(ngos == null)
            {
                return new NGOsResponseModel
                {
                    
                    Message = "NGOs not found",
                    Success = false
                };
            }
            var doc = await _documentRepository.GetAllWithNgo();
            return new NGOsResponseModel
            {
                Data = ngos.Select(x => new NgoDTO
                {
                    Name = x.Name,
                    Email = x.Email,
                    Password = x.Password,
                    State = x.State,
                    City = x.City,
                    LGA = x.LGA,
                    Address = x.Address,
                    Id = x.Id,
                    Description = x.Description,
                    Image = x.Image,
                    Particulars = doc.Select(a => new DocumentDTO
                    {
                        Name = a.Name
                    }).ToList(),
                    AccountName = x.AccountName,
                    AccountNumber = x.AccountNumber,
                    BankName = x.BankName,
                    CategoryName = x.Category.Name,
                    CategoryId = x.CategoryId,
                    IsApproved = x.IsApproved,
                    IsBanned = x.IsBan,
                    IsDeleted = x.IsDeleted,
                }).ToList(),
                Message = "List of NGOs",
                Success = true
            };
        }

        public async Task<NGOsResponseModel> GetByDescriptionContent(string content)
        {
            var ngos = await _ngoRepository.GetByDescriptionContent(content);
            if (ngos == null)
            {
                return new NGOsResponseModel
                {
                    Message = "No NGO found",
                    Success = false
                };
            }
            var doc = await _documentRepository.GetAllWithNgo();
            var dtos = ngos.Select( x => new NgoDTO
            {
                Name = x.Name,
                Email = x.Email,
                Password = x.Password,
                State = x.State,
                City = x.City,
                LGA = x.LGA,
                Address = x.Address,
                Id = x.Id,
                Particulars = doc.Select(a => new DocumentDTO
                {
                    Name = a.Name
                }).ToList(),
                Image = x.Image,
                
                AccountName = x.AccountName,
                AccountNumber = x.AccountNumber,
                BankName = x.BankName,
                CategoryName = x.Category.Name,
                CategoryId = x.CategoryId,
                Description = x.Description,
                IsApproved = x.IsApproved,
                IsBanned = x.IsBan,
                IsDeleted = x.IsDeleted,
            }).ToList();
            return new NGOsResponseModel
            {
                Success = true,
                Data = dtos,
                Message = "List of NGOs",
            };
        }

        public async Task<NGOResponseModel> GetNgo(int id)
        {
            var ngo = await _ngoRepository.GetNgo(id);
            if(ngo == null)
            {
                return new NGOResponseModel
                {
                    Message = "NGO not found",
                    Success = false
                };
            }
            var doc = await _documentRepository.GetDocumentsByNgoId(ngo.Id);
            var dto = new NgoDTO
            {
                Name = ngo.Name,
                Email = ngo.Email,
                Password = ngo.Password,
                State = ngo.State,
                City = ngo.City,
                LGA = ngo.LGA,
                Address = ngo.Address,
                Id = ngo.Id,
                Particulars = doc.Select(a => new DocumentDTO
                {
                    Name = a.Name
                }).ToList(),
                Image = ngo.Image,
                AccountName = ngo.AccountName,
                AccountNumber = ngo.AccountNumber,
                BankName = ngo.BankName,
                IsApproved = ngo.IsApproved,
                IsBanned = ngo.IsBan,
                IsDeleted = ngo.IsDeleted,
                CategoryName = ngo.Category.Name,
                CategoryId = ngo.CategoryId,
                Description = ngo.Description,
            };
            return new NGOResponseModel
            {
                Message = "",
                Success = true,
                Data = dto
            };
        }

        public async Task<NGOResponseModel> GetNgoByEmail(string email)
        {
            var ngo = await _ngoRepository.GetNgoByEmail(email);
            if(ngo == null)
            {
                return new NGOResponseModel
                {
                    Message = "NGO not found",
                    Success = false
                };
            }
            var doc = await _documentRepository.GetAllWithNgo();
            var dto = new NgoDTO
            {
                Name = ngo.Name,
                Email = ngo.Email,
                Password = ngo.Password,
                State = ngo.State,
                City = ngo.City,
                LGA = ngo.LGA,
                Address = ngo.Address,
                Id = ngo.Id,
                Image = ngo.Image,
                Particulars = doc.Select(a => new DocumentDTO
                {
                    Name = a.Name
                }).ToList(),
                AccountName = ngo.AccountName,
                AccountNumber = ngo.AccountNumber,
                BankName = ngo.BankName,
                IsApproved = ngo.IsApproved,
                IsBanned = ngo.IsBan,
                IsDeleted = ngo.IsDeleted,
                CategoryName = ngo.Category.Name,
                CategoryId = ngo.CategoryId,
                Description = ngo.Description,
            };
            return new NGOResponseModel
            {
                Success = true,
                Data = dto,
                Message = ""
            };
        }

        public async Task<NGOsResponseModel> GetNgoByName(string name)
        {
            var ngos = await _ngoRepository.GetNgoByName(name);
            if(ngos == null)
            {
                return new NGOsResponseModel
                {
                    Success = false,
                    Message = "No NGO found"
                };
            }
            var doc = await _documentRepository.GetAllWithNgo();
            var dtos = ngos.Select(  x => new NgoDTO
            {
                Name = x.Name,
                Email = x.Email,
                Password = x.Password,
                State = x.State,
                City = x.City,
                LGA = x.LGA,
                Address = x.Address,
                Id = x.Id,
                Particulars = doc.Select(a => new DocumentDTO
                {
                    Name = a.Name
                }).ToList(),
                Image = x.Image,
                AccountName = x.AccountName,
                AccountNumber = x.AccountNumber,
                BankName = x.BankName,
                IsApproved = x.IsApproved,
                IsBanned = x.IsBan,
                IsDeleted = x.IsDeleted,
                CategoryName = x.Category.Name,
                CategoryId = x.CategoryId,
                Description = x.Description,
            }).ToList();
            return  new NGOsResponseModel
            {
                Success = true,
                Data = dtos,
                Message = "List of NGOs",
            };
        }

        public async Task<NGOsResponseModel> GetUnapprovedNgos()
        {
            var ngos = await _ngoRepository.GetUnapprovedNgos();
            if (ngos == null)
            {
                return new NGOsResponseModel
                {
                    Message = "No NGO found",
                    Success = false,
                };
            }
            var doc = await _documentRepository.GetAllWithNgo();
            var dtos = ngos.Select(x => new NgoDTO
            {
                Name = x.Name,
                Email = x.Email,
                Password = x.Password,
                State = x.State,
                City = x.City,
                LGA = x.LGA,
                Address = x.Address,
                Id = x.Id,
                Particulars = doc.Select(a => new DocumentDTO
                {
                    Name = a.Name
                }).ToList(),
                Image = x.Image,
                AccountName = x.AccountName,
                AccountNumber = x.AccountNumber,
                BankName = x.BankName,
                IsApproved = x.IsApproved,
                IsBanned = x.IsBan,
                IsDeleted = x.IsDeleted,
                CategoryName = x.Category.Name,
                CategoryId = x.CategoryId,
                Description = x.Description,
            }).ToList();
            return new NGOsResponseModel
            {
                Success = true,
                Data = dtos,
                Message = "List Of Unapproved NGOs",
            };
        }

        public async Task<int> GetUnapprovedNgosCount()
        {
            var ngo = await GetUnapprovedNgos();
            var count = ngo.Data.Count;
            return count;
        }

        public async Task<BaseResponse> Register(CreateNgoRequestModel model)
        {
            if (model == null)
            {
                return new BaseResponse
                {
                    Message = "Information cannot ne empty",
                    Success = false
                };
            }
            var check = await _userRepository.Get(x => x.Email == model.Email);
            if (check != null)
            {
                return new BaseResponse
                {
                    Message = "E-mail already existed",
                    Success = false
                };
            }

            var role = await _roleRepository.Get(x => x.Name.ToLower() == "ngo");
            if (role == null)
            {
                return new BaseResponse
                {
                    Message = "Role not found",
                    Success = false
                };
            }

            var category = await _categoryRepository.Get(x => x.Name == model.CategoryName);
            if (category == null)
            {
                return new BaseResponse
                {
                    Message = "Category not available",
                    Success = false
                };
            }

            var user = new User
            {
                UserName = model.Name,
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



            var ngo = new NGO
            {
                Image = model.Image,
                Name = model.Name,
                Email = model.Email,
                Password = model.Password,
                Description = model.Description,
                IsApproved = false,
                IsDeleted = false,
                IsBan = false,
                UserId = addUser.Id,
                User = addUser,
                Category = category,
                CategoryId = category.Id,
            };
            

            var addNgo = await _ngoRepository.Register(ngo);

            var mail = new EmailRequestModel
            {
                ReceiverEmail = model.Email,
                ReceiverName = $"{model.Name}",
                Message = $"@{model.Name} management, your registration is successful.\nKindly upload other information by clicking on the UPDATE button on your dashboard fror the activation of your account.",
                Subject = "Relief-CMS NGO Registration",
            };
            await _email.SendEmail(mail);

            return new BaseResponse
            {
                Message = "NGO registration request sent successfully",
                Success = true
            };
        }
        public async Task<BaseResponse> UploadDocuments(UploadRequestModel model, int ngoId)
        {
            if (model == null)
            {
                return new BaseResponse
                {
                    Message = "File not attached",
                    Success = false
                };
            }

            foreach (var img in model.Documents)
            {
                var image = new Document
                {
                    Path = img,
                    NgoId = ngoId
                };
                await _documentRepository.Register(image);
            }
            return new BaseResponse
            {
                Message = "Successfully uploaded",
                Success = true
            };
            
        }

        public async Task<BaseResponse> UpdateAddress(AddressRequestModel model, int id)
        {
            if (model == null)
            {
                return new BaseResponse
                {
                    Message = "fields cannot be null",
                    Success = false
                };
            }
            var ngo = await _ngoRepository.GetNgo(id);
            if(ngo == null)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "Account not found"
                };
            }
            
            ngo.Address = model.Address;
            ngo.LGA = model.LGA;
            ngo.State = model.State;
            ngo.City = model.City;
            await _ngoRepository.Update(ngo);
            return new BaseResponse
            {
                Message = "Address Updated Successfully",
                Success = true
            };
        }

        public async Task<BaseResponse> UpdateBankDetails(AccountDetailsRequestModel model, int id)
        {
            if (model == null)
            {
                return new BaseResponse
                {
                    Message = "fields cannot be null",
                    Success = false
                };
            }
            var ngo = await _ngoRepository.GetNgo(id);
            if (ngo == null)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "Account not found"
                };
            }
            ngo.BankName = model.BankName;
            ngo.AccountName = model.AccountName;
            ngo.AccountNumber = model.AccountNumber;
            await _ngoRepository.Update(ngo);
            return new BaseResponse
            {
                Message = "Details Uploaded Successfully",
                Success = true
            };
        }

        public async Task<BaseResponse> Update(UpdateNgoRequestModel model, int id)
        {
            var ngo = await _ngoRepository.Get(x => x.Id == id);
            if (ngo == null)
            {
                return new BaseResponse
                {
                    Message = "NGO not found",
                    Success = false
                };
            }
            if (model == null)
            {
                return new BaseResponse
                {
                    Message = "All fields cannot be null",
                    Success = false
                };
            }

            var category = await _categoryRepository.Get(x => x.Name == model.CategoryName);

            ngo.Name = model.Name ?? ngo.Name;
            ngo.Password = model.Password ?? ngo.Password;
            ngo.State = model.State ?? ngo.State;
            ngo.City = model.City ?? ngo.City;
            ngo.LGA = model.LGA ?? ngo.LGA;
            ngo.Description = model.Description ?? ngo.Description;
            ngo.CategoryId = ngo.CategoryId;
            ngo.Particulars = (List<Document>)model.Documents ?? ngo.Particulars;

            await _ngoRepository.Update(ngo);
            return new BaseResponse
            {
                Message = "NGO information updated successfully",
                Success = true
            };
        }

        public async Task<NGOsResponseModel> GetBannedNgos()
        {
            var ngos = await _ngoRepository.GetAll();
            if(ngos == null || ngos.Count == 0)
            {
                return new NGOsResponseModel
                {
                    Message = "List is Empty",
                    Success = false,
                };
            }
            var doc = await _documentRepository.GetAllWithNgo();
            var banned = ngos.Where(x => x.IsBan == true).Select(x => new NgoDTO
            {
                Name = x.Name,
                Email = x.Email,
                Password = x.Password,
                State = x.State,
                City = x.City,
                LGA = x.LGA,
                Address = x.Address,
                Id = x.Id,
                Particulars = doc.Select(a => new DocumentDTO
                {
                    Name = a.Name
                }).ToList(),
                Image = x.Image,
                AccountName = x.AccountName,
                AccountNumber = x.AccountNumber,
                BankName = x.BankName,
                IsApproved = x.IsApproved,
                IsBanned = x.IsBan,
                IsDeleted = x.IsDeleted,
                CategoryName = x.Category.Name,
                CategoryId = x.CategoryId,
                Description = x.Description,
            }).ToList();
            if(banned == null || banned.Count == 0)
            {
                return new NGOsResponseModel
                {
                    Message = "List is Empty",
                    Success = false
                };
            }
            return new NGOsResponseModel
            {
                Data = banned,
                Success = true,
                Message = "List of Baned NGOs",
            };
        }
    }
}