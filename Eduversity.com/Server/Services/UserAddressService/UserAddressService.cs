namespace Eduversity.com.Server.Services.UserAddressService
{
    public class UserAddressService : IUserAddressService
    {
        private readonly DataContext _context;
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public UserAddressService(DataContext context, IAuthService authService, IMapper mapper)
        {
            _context = context;
            _authService = authService;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<UserAddressResponse>> AddOrUpdateAddress(UserAddressResponse userAddressResponse)
        {
            long userId = userAddressResponse.UserId == 0L ? _authService.GetUserId() : userAddressResponse.UserId;
            var dbAddress = await _context.UsersAddress
                .FirstOrDefaultAsync(a => a.UserId == userId);

            if (dbAddress == null)
            {
                UserAddress address = _mapper.Map<UserAddress>(userAddressResponse);
                address.UserId = userId;
                _context.UsersAddress.Add(address);

                await _context.SaveChangesAsync();
                userId = address.UserId;
            }
            else
            {
                dbAddress.MobileNumber = userAddressResponse.MobileNumber;
                dbAddress.TelephoneNumber = userAddressResponse.TelephoneNumber;
                dbAddress.EmailAddress = userAddressResponse.EmailAddress;
                dbAddress.NextOfKin = userAddressResponse.NextOfKin;
                dbAddress.NextOfKinEmailAddress = userAddressResponse.NextOfKinEmailAddress;
                dbAddress.NextofKinMobileNumber = userAddressResponse.NextofKinMobileNumber;
                dbAddress.NextOfKinTelephoneNumber = userAddressResponse.NextOfKinTelephoneNumber;
                dbAddress.PermanentAddress = userAddressResponse.PermanentAddress;
                dbAddress.ContactAddress = userAddressResponse.ContactAddress;
                dbAddress.CountryId = userAddressResponse.CountryId;
                dbAddress.StateId = userAddressResponse.StateId;
                dbAddress.LGAId = userAddressResponse.LGAId;

                await _context.SaveChangesAsync();
                userId = dbAddress.UserId;
            }

            var result = await GetAddress(userId);
            return new ServiceResponse<UserAddressResponse>()
            {
                Data = result.Data
            };
        }

        public async Task<ServiceResponse<UserAddressResponse>> GetAddress(long Id = 0L)
        {
            long userId = Id == 0L ? _authService.GetUserId() : Id;
            var address =
                await (from a in _context.UsersAddress
                       join c in _context.Countries
                       on a.CountryId equals c.Id into aGroup
                       from c in aGroup.DefaultIfEmpty()
                       join s in _context.States
                       on a.StateId equals s.Id into cGroup
                       from s in cGroup.DefaultIfEmpty()
                       join l in _context.LGAs
                       on a.LGAId equals l.Id into sGroup
                       from l in sGroup.DefaultIfEmpty()
                       where a.UserId == userId
                       select new UserAddressResponse
                       {
                           UserId = a.UserId,
                           EmailAddress = a.EmailAddress,
                           MobileNumber = a.MobileNumber,
                           TelephoneNumber = a.TelephoneNumber,
                           NextOfKin = a.NextOfKin,
                           NextOfKinEmailAddress = a.NextOfKinEmailAddress,
                           NextofKinMobileNumber = a.NextofKinMobileNumber,
                           NextOfKinTelephoneNumber = a.NextOfKinTelephoneNumber,
                           PermanentAddress = a.PermanentAddress,
                           ContactAddress = a.ContactAddress,
                           CountryId =  a.CountryId,
                           StateId = a.StateId,
                           LGAId = a.LGAId,
                           CountryName = c != null ? c.Name : string.Empty,
                           StateName = s != null ? s.Name : string.Empty,
                           LGAName = l != null ? l.Name : string.Empty
                       }).FirstOrDefaultAsync();

            return new ServiceResponse<UserAddressResponse>() { Data = address };
        }
    }
}