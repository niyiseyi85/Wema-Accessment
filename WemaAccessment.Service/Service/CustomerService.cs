using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;

using WemaAccessment.Common;
using WemaAccessment.Common.Integration;
using WemaAccessment.Data.Data;
using WemaAccessment.Data.Dto;
using WemaAccessment.Data.IRepository;
using WemaAccessment.Data.Models;
using WemaAccessment.Data.ResponseModels;
using WemaAccessment.Service.IService;

namespace WemaAccessment.Service.Service
{
  public class CustomerService : ICustomerService
  {
    private readonly IOTPService _oTPService;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    public static LocationsDto locationsDto;

    public CustomerService(IOTPService oTPService, IMapper mapper, IUnitOfWork unitOfWork)
    {
      _mapper = mapper;
      _unitOfWork = unitOfWork;
      _oTPService = oTPService;
      locationsDto = new LocationsDto()
      {
        States = new List<State>()
                {
                    new State()
                    {
                        StateId = 1,
                        StateName = "Lagos",
                        LGAs = new List<LGA>()
                        {
                            new LGA()
                            {
                                LGAId = 1,
                                LGAName = "Epe"
                            },
                            new LGA()
                            {
                                LGAId = 2,
                                LGAName = "Kosofe"
                            },
                            new LGA()
                            {
                                LGAId = 2,
                                LGAName = "Alimosho"
                            }
                        }
                    },
                     new State()
                    {
                        StateId = 2,
                        StateName = "Ogun",
                        LGAs = new List<LGA>()
                        {
                            new LGA()
                            {
                                LGAId = 1,
                                LGAName = "Ifo"
                            },
                            new LGA()
                            {
                                LGAId = 2,
                                LGAName = "Obafemi-Owode"
                            },
                            new LGA()
                            {
                                LGAId = 2,
                                LGAName = "Abeokuta North"
                            }
                        }
                    },
                }
      };
    }

    public async Task<ResponseModel<List<GetCustomerDto>>> GetAllCustomer()
    {
      var response = new ResponseModel<List<GetCustomerDto>>();
      response.HasError = false;
      var allCustomers = await _unitOfWork.CustomerRepository.GetAll();
      if (allCustomers.Count() > 0)
      {
        var allCustomersDto = _mapper.Map<List<GetCustomerDto>>(allCustomers);
        response.Data = allCustomersDto;
        response.Message = $"{allCustomers.Count()} record(s) found";
      }
      else
      {
        response.HasError = true;
        response.Message = "No record found";
      }
      return response;
    }
    public async Task<ResponseModel> OnboardCustomer(CustomerDto customerDto)
    {
      var response = new ResponseModel();
      State state;
      LGA lga;
      state = locationsDto.States.Where(x => x.StateName.ToLower() == customerDto.StateOfResidence.ToLower()).FirstOrDefault();
      if (state == null)
      {
        response.HasError = true;
        response.Message = "Invalid state enterd.";
        return response;
      }
      else
      {
        lga = state.LGAs.Where(x => x.LGAName.ToLower() == customerDto.LGA.ToLower()).FirstOrDefault();
        if (lga == null)
        {
          response.HasError = true;
          response.Message = "Invalid LGA enterd for the state provided.";
          return response;
        }
      }

      var customer = new Customer()
      {
        LGA = lga.LGAName,
        PhoneNumber = customerDto.PhoneNumber,
        StateOfResidence = state.StateName,
        Email = customerDto.Email,
        Password = PasswordHash.Hash(customerDto.Password)
      };

      await _unitOfWork.CustomerRepository.Add(customer);
      int saveResult = await _unitOfWork.SaveAsync();

      if (saveResult == 0)
      {
        response.HasError = true;
        response.Message = "Unable to onboard customer. Please try again later";
      }
      else
      {
        _oTPService.SendOTP(customerDto.PhoneNumber);
        response.HasError = false;
        response.Message = "Check Phone number for OTP verification";
      }
      return response;
    }

    public async Task<ResponseModel> VerifiyCustomer(VerifyCustomerDto varifyCustomerDto)
    {
      var response = new ResponseModel();
      var existingCustomer = await _unitOfWork.CustomerRepository.FirstOrDefault(x => x.PhoneNumber == varifyCustomerDto.PhoneNumber);
      
      if (existingCustomer == null)
      {
        response.HasError = true;
        response.Message = "No customer found for verification with the Phone Number provided";
      }
      else if (existingCustomer.IsVerified)
      {
        response.HasError = true;
        response.Message = "Customer email already verified";
      }
      else
      {
        var verifyOtpResponse = _oTPService.VerifyOTP(varifyCustomerDto.PhoneNumber, varifyCustomerDto.OTP);
        if (verifyOtpResponse)
        {
          existingCustomer.IsVerified = true;
          _unitOfWork.CustomerRepository.Update(existingCustomer);
          var updateResponse = await _unitOfWork.SaveAsync();
          
          if (updateResponse  == 0)
          {
            response.HasError = true;
            response.Message = "Customer Phone Number could not be verified";
          }
          else
          {
            response.HasError = false;
            response.Message = "Customer Phone number verified successfully";
          }
        }
        else
        {
          response.HasError = true;
          response.Message = "Invalid OTP entered";
        }
      }
      return response;
    }
  }
}