using Relief.DTOs;
using Relief.DTOs.RequestModel;
using Relief.DTOs.ResponseModel;
using Relief.Entities;
using Relief.Interfaces.Services;
using Relief.Interfaces.Repositories;

namespace Relief.Implementations.Services
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository _reportRepository;

        public ReportService(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }

        public async Task<ReportsResponseModel> GetAll()
        {
            var reports = await _reportRepository.GetAll();
            if(reports == null)
            {
                return new ReportsResponseModel
                {
                    Message = "No report found",
                    Success = false
                };
            }
            var dto = reports.Select(x => new ReportDTO
            {
                Detail = x.Detail,
                Id = x.Id
            }).ToHashSet();
            return new ReportsResponseModel
            {
                Data = dto,
                Success = true,
                Message = "List of Reports"
            };

        }

        public async Task<ReportResponseModel> GetReport(int id)
        {
            var report = await _reportRepository.GetReport(id);
            if (report == null)
            {
                return new ReportResponseModel
                {
                    Message = "Report not found",
                    Success = false
                };
            }
            var dto = new ReportDTO
            {
                Detail = report.Detail,
            };
            return new ReportResponseModel
            {
                Data = dto,
                Success = true,
                Message = ""
            };
            
        }

        public async Task<ReportsResponseModel> GetByDonorId(int id)
        {
            var reports = await _reportRepository.GetAll();
            if (reports == null)
            {
                return new ReportsResponseModel
                {
                    Message = "No report found",
                    Success = false
                };
            }
            var donorReports = reports.Where(x => x.DonorId == id).ToList();
            if(donorReports == null)
            {
                return new ReportsResponseModel
                {
                    Message = "No report found",
                    Success = false
                };
            }
            var dto = donorReports.Select(x => new ReportDTO
            {
                Detail = x.Detail,
                CreatedOn = x.CreatedOn,
                DonorName = $"{x.Donor.LastName} {x.Donor.FirstName}",
                NgoName = x.NGO.Name,
                DonorImage = x.Donor.Image,
                NgoImage = x.NGO.Image,
                Id = x.Id
            }).ToList();
            return new ReportsResponseModel
            {
                Data = dto,
                Message = "Reports",
                Success = true
            };

        }

        public async Task<ReportsResponseModel> GetReportsByContent(string content)
        {
            if (content == null)
            {
                return new ReportsResponseModel
                {
                    Message = "Field cannot be empty",
                    Success = false
                };
            }
            var reports = await _reportRepository.GetReportsByContent(content);
            if (reports == null)
            {
                return new ReportsResponseModel
                {
                    Message = "No report found",
                    Success = false
                };
            }
            var dto = reports.Select(x => new ReportDTO
            {
                Detail = x.Detail
            }).ToHashSet();
            return new ReportsResponseModel
            {
                Message = "List of reports",
                Success = true,
                Data = dto
            };

        }

        public async Task<BaseResponse> MakeReport(CreateReportRequestModel model, int ngoId, int donorId)
        {
            if (model == null)
            {
                return new BaseResponse
                {
                    Message = "Report cannot be null",
                    Success = false
                };
            }
            var report = new Report
            {
                Detail = model.Detail,
                NGOId = ngoId,
                DonorId = donorId,
            };
            await _reportRepository.Register(report);
            return new BaseResponse
            {
                Message = "Report posted successfully",
                Success = true
            };
        }

        public async Task<BaseResponse> UpdateReport(UpdateReportRequestModel model, int id)
        {
            if (model == null)
            {
                return new BaseResponse
                {
                    Message = "Field cannot be empty",
                    Success = false
                };
            }
            var report = await _reportRepository.Get(x => x.Id == id);
            if (report == null)
            {
                return new BaseResponse
                {
                    Message = "Report not found",
                    Success = false
                };
            }
            report.Detail = model.Detail;
            await _reportRepository.Update(report);

            return new BaseResponse
            {
                Message = "Report updated successfully",
                Success = true
            };
        }
    }
}
