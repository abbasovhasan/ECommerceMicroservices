using AutoMapper;
using Microsoft.EntityFrameworkCore;

public class PaymentService : IPaymentService
{
    private readonly IReadRepository<Payment> _readRepository;
    private readonly IWriteRepository<Payment> _writeRepository;
    private readonly IMapper _mapper;

    public PaymentService(IReadRepository<Payment> readRepository, IWriteRepository<Payment> writeRepository, IMapper mapper)
    {
        _readRepository = readRepository;
        _writeRepository = writeRepository;
        _mapper = mapper;
    }

    public async Task<List<PaymentDto>> GetPaymentsByOrderIdAsync(int orderId)
    {
        var payments = await _readRepository.GetWhere(p => p.OrderId == orderId).ToListAsync();
        return _mapper.Map<List<PaymentDto>>(payments);
    }

    public async Task ProcessPaymentAsync(PaymentDto paymentDto)
    {
        var payment = _mapper.Map<Payment>(paymentDto);
        await _writeRepository.AddAsync(payment);
    }
}
