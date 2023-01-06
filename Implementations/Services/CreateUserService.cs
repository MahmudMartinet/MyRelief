using Relief.DTOs;
using Relief.DTOs.RequestModel;
using Relief.DTOs.ResponseModel;
using Relief.Email;
using Relief.Entities;
using Relief.Interfaces.Repositories;
using Relief.Interfaces.Services;

namespace Relief.Implementations.Services
{
    public class CreateUserService : ICreateUserService
    {
        private readonly ICreateUserRepository _createUserRepository;
        private readonly IUserRepository _userRepository;
        private readonly IEmailSender _email;

        public CreateUserService(ICreateUserRepository createUserRepository, IUserRepository userRepository, IEmailSender email)
        {
            _createUserRepository = createUserRepository;
            _userRepository = userRepository;
            _email = email;
        }
        public async Task<BaseResponse> CreateAdmin(AddAdminRequestModel model)
        {
            var user = await _createUserRepository.Get(x => x.Email == model.Email);
            if(user != null)
            {
                return new BaseResponse
                {
                    Message = "Email Already Exist",
                    Success = false
                };
            }
            var check = await _userRepository.Get(x => x.Email == model.Email);
            if(check != null)
            {
                return new BaseResponse
                {
                    Message = "Email Already Exist",
                    Success = false
                };
            }
            var guid = Guid.NewGuid().ToString()[..5];
            var createUser = new CreateUser
            {
                Email = model.Email,
                VerificationCode = guid,
            };
            if(model.Role == "SuperAdmin")
            {
                createUser.RoleName = "SuperAdmin";
            }
            else
            {
                createUser.RoleName = "Admin";
            }

            var mail = new EmailRequestModel
            {
                ReceiverEmail = model.Email,
                ReceiverName = model.Email,
                Message = $"You have been registered successfully as an Administrator on RELIEF-CMS.\nClick this link\n file:///C:/Users/user/Desktop/ReliefFrontEnd/AdminRegistration/emailVerification.html \n Verification Code : {guid}\nand enter The verification code attached to this Mail to complete your registratio\nIf you are unable to click the link, kindly copy and paste the link to your browser.",
                Subject = "Relief-CMS Administrator Email Verification",
            };

            await _email.SendEmail(mail);
            await _createUserRepository.Register(createUser);
            return new BaseResponse
            {
                Message = "Verification in progress",
                Success = true
            };
        }

        public async Task<BaseResponse> CreateDonor(CreateUserRequestModel model)
        {
            var user = await _createUserRepository.Get(x => x.Email == model.Email);
            if (user != null)
            {
                return new BaseResponse
                {
                    Message = "Email Already Exist",
                    Success = false
                };
            }
            var check = await _userRepository.Get(x => x.Email == model.Email);
            if (check != null)
            {
                return new BaseResponse
                {
                    Message = "Email Already Exist",
                    Success = false
                };
            }
            var guid = Guid.NewGuid().ToString()[..5];
            var mail = new EmailRequestModel
            {
                ReceiverEmail = model.Email,
                ReceiverName = model.Email,
                Message = $"Verification Code : {guid}\nand enter The verification code attached to this Mail to complete your registratio.",
                Subject = "Relief-CMS Email Verification",
            };
            await _email.SendEmail(mail);

            var createUser = new CreateUser
            {
                Email = model.Email,
                VerificationCode = guid,
                RoleName = "Donor"
            };
            await _createUserRepository.Register(createUser);

            return new BaseResponse
            {
                Message = "Verification in progress",
                Success = true
            };
        }

        public async Task<BaseResponse> CreateNgo(CreateUserRequestModel model)
        {
            var user = await _createUserRepository.Get(x => x.Email == model.Email);
            if (user != null)
            {
                return new BaseResponse
                {
                    Message = "Email Already Exist",
                    Success = false
                };
            }
            var check = await _userRepository.Get(x => x.Email == model.Email);
            if (check != null)
            {
                return new BaseResponse
                {
                    Message = "Email Already Exist",
                    Success = false
                };
            }
            var guid = Guid.NewGuid().ToString()[..5];
            var mail = new EmailRequestModel
            {
                ReceiverEmail = model.Email,
                ReceiverName = model.Email,
                Message = $"Verification Code : {guid}\nand enter The verification code attached to this Mail to complete your registratio.",
                Subject = "Relief-CMS Email Verification",
            };
            await _email.SendEmail(mail);

            var createUser = new CreateUser
            {
                Email = model.Email,
                VerificationCode = guid,
                RoleName = "Ngo"
            };
            await _createUserRepository.Register(createUser);
            return new BaseResponse
            {
                Message = "Verification in progress",
                Success = true
            };
        }

        public async Task<BaseResponse> VerifyUser(VerifyUserRequestModel model)
        {
            var user = await _createUserRepository.Get(x => x.Email == model.Email);
            if(user != null && model.Code == user.VerificationCode)
            {
                return new BaseResponse
                {
                    Success = true,
                    Message = "Email Verified"
                };
            }
            else if(user != null && model.Code != user.VerificationCode)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "Email not verified"
                };
            }
            return new BaseResponse
            {
                Success = false,
                Message = "Email not found"
            };
        }

        public async Task<CreateUserResponseModel> GetUser(string email)
        {
            var user = await _createUserRepository.Get(x => x.Email == email);
            if (user != null)
            {
                var dto = new CreateUserDTO
                {
                    Email = user.Email,
                    VerificationCode = user.VerificationCode,
                    Id = user.Id
                };
                return new CreateUserResponseModel
                {
                    Data = dto,
                    Message = "found",
                    Success = true
                };
            }
            return new CreateUserResponseModel
            {
                Success = false,
                Message = "Email not found"
            };
        }

    }
}
